/*
Copyright (c) 2015-2016 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
//打开开关没有写入导出列表的纯虚类自动跳过
//#define JUMP_NODEFINED_ABSTRACT         

using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using System.Diagnostics;
using LuaInterface;

using Object = UnityEngine.Object;
using Debug = UnityEngine.Debug;
using Debugger = LuaInterface.Debugger;
using System.Threading;

/// <summary>
/// 实现 Unity 菜单栏中的 lua 菜单下的功能
/// </summary>
[InitializeOnLoad]
public static class ToLuaMenu
{
    /// <summary>
    /// 不需要导出或者无法导出的类型
    /// </summary>
    public static List<Type> dropType = new List<Type>
    {
        typeof(ValueType),                                  //不需要导出值类型
#if !UNITY_5
        typeof(Motion),                                     //很多平台只是空类
#endif
        typeof(UnityEngine.YieldInstruction),               //无需导出的类      
        typeof(UnityEngine.WaitForEndOfFrame),              //内部支持
        typeof(UnityEngine.WaitForFixedUpdate),
        typeof(UnityEngine.WaitForSeconds),        
        typeof(UnityEngine.Mathf),                          //lua层支持                
        typeof(Plane),                                      
        typeof(LayerMask),                                  
        typeof(Vector3),
        typeof(Vector4),
        typeof(Vector2),
        typeof(Quaternion),
        typeof(Ray),
        typeof(Bounds),
        typeof(Color),                                    
        typeof(Touch),
        typeof(RaycastHit),                                 
        typeof(TouchPhase),     
        //typeof(LuaInterface.LuaOutMetatable),               //手写支持
        typeof(LuaInterface.NullObject),             
        typeof(System.Array),                        
        typeof(System.Reflection.MemberInfo),    
        typeof(System.Reflection.BindingFlags),
        typeof(LuaClient),
        typeof(LuaInterface.LuaFunction),
        typeof(LuaInterface.LuaTable),
        typeof(LuaInterface.LuaThread),
        typeof(LuaInterface.LuaByteBuffer),                 //只是类型标识符

        // 无需导出，导出类支持lua函数转换为委托。如UIEventListener.OnClick(luafunc)
        typeof(DelegateFactory),                            
    };
    
    /// <summary>
    /// 可以导出的内部支持类型
    /// </summary>
    public static List<Type> baseType = new List<Type>
    {
        typeof(System.Object),
        typeof(System.Delegate),
        typeof(System.String),
        typeof(System.Enum),
        typeof(System.Type),
        typeof(System.Collections.IEnumerator),
        typeof(UnityEngine.Object),
        typeof(LuaInterface.EventObject),
        typeof(LuaInterface.LuaMethod),
        typeof(LuaInterface.LuaProperty),
        typeof(LuaInterface.LuaField),
        typeof(LuaInterface.LuaConstructor),        
    };

    /// <summary>
    /// 是否正在自动生成
    /// </summary>
    private static bool beAutoGen = false;

    /// <summary>
    /// 是否还未完成第一次生成
    /// </summary>
    private static bool beCheck = true;

    /// <summary>
    /// （已经注册的？）（或是需要导出的？）所有类型的 BindType 信息类 list
    /// </summary>  
    static List<BindType> allTypes = new List<BindType>();

    #region constructor

    static ToLuaMenu()
    {
        // 获取路径
        string dir = CustomSettings.saveDir;
        // 获取路径下的 c# 文件（Directory.GetFiles：返回指定目录中与指定的搜索模式匹配的文件的名称，
        // 包含其路径，只搜索当前目录及其子目录
        string[] files = Directory.GetFiles(dir, "*.cs", SearchOption.TopDirectoryOnly);

        // 如果路径下获取到的 c# 文件小于 3 个且 beCheck 为真（说明尚未生成 wrap 文件）
        if (files.Length < 3 && beCheck)
        {
            // 弹出确认生成菜单（框架刚刚导入时的确认弹窗）进行生成
            if (EditorUtility.DisplayDialog("自动生成", "点击确定自动生成常用类型注册文件， 也可通过菜单逐步完成此功能", "确定", "取消"))
            {
                beAutoGen = true;
                // 根据 CustomSettings.customDelegateList 生成 "DelegateFactory.cs" 脚本
                GenLuaDelegates();
                // 刷新 AssetDatabase
                AssetDatabase.Refresh();
                // 生成 c# Wrap 文件到指定(CustomSettings.saveDir)目录
                GenerateClassWraps();
                // 生成 LuaBinder.cs 文件
                //（LuaBinder 用于与各种 UnityEngine 的 Wrap 文件协调，处理各种事件、委托、回调等）
                //（由 ToLuaMenu.GenLuaBinder 方法自动生成）
                GenLuaBinder();
                beAutoGen = false;
            }

            beCheck = false;
        }
    }

    #endregion

    /// <summary>
    /// 移除命名空间（根据参数 space 的字符位数从参数 name 中移除）
    /// </summary>
    static string RemoveNameSpace(string name, string space)
    {
        if (space != null)
        {
            name = name.Remove(0, space.Length + 1);
        }

        return name;
    }

    /// <summary>
    /// 储存类型的具体信息（如类型名称、命名空间、是否静态等）
    /// </summary>
    public class BindType
    {
        /// <summary>
        /// 类名称
        /// </summary>
        public string name;

        /// <summary>
        /// 类类型
        /// </summary>
        public Type type;

        /// <summary>
        /// 是否是静态类型或者虚类、密封类
        /// </summary>
        public bool IsStatic;

        /// <summary>
        /// 产生的 wrap 文件名字
        /// </summary>
        public string wrapName = "";

        /// <summary>
        /// 注册到 lua 的名字
        /// </summary>
        public string libName = "";

        /// <summary>
        /// 父类类型
        /// </summary>
        public Type baseType = null;

        /// <summary>
        /// 命名空间（注册到 lua 的 table 层级）
        /// </summary>
        public string nameSpace = null;

        /// <summary>
        /// 已经存在的类型？？
        /// </summary>
        public List<Type> extendList = new List<Type>();

        #region constructor

        public BindType(Type t)
        {
            // 如果是委托及其衍生类，抛出异常
            if (typeof(System.MulticastDelegate).IsAssignableFrom(t))
            {
                throw new NotSupportedException(string.Format("\nDon't export Delegate {0} as a class, register it in customDelegateList", LuaMisc.GetTypeName(t)));
            }

            //if (IsObsolete(t))
            //{
            //    throw new Exception(string.Format("\n{0} is obsolete, don't export it!", LuaMisc.GetTypeName(t)));
            //}

            type = t;
            // 获取 type 的命名空间，并将 libName 更新为从完全限定名到命名空间后一位（也就是带'.'字符）的字符
            nameSpace = ToLuaExport.GetNameSpace(t, out libName);
            // 用"."字符拼接参数 space 与 name 为一个字符串并返回
            name = ToLuaExport.CombineTypeStr(nameSpace, libName);
            // 过滤、替换字符串中指定的字符并返回
            libName = ToLuaExport.ConvertToLibSign(libName);

            // 按规律设置 name 和 wrapName 的值
            if (name == "object")
            {
                wrapName = "System_Object";
                name = "System.Object";
            }
            else if (name == "string")
            {
                wrapName = "System_String";
                name = "System.String";
            }
            else
            {
                wrapName = name.Replace('.', '_');
                wrapName = ToLuaExport.ConvertToLibSign(wrapName);
            }

            // 获取 CustomSettings.staticClassTypes（静态类型）中 type 的 index
            int index = CustomSettings.staticClassTypes.IndexOf(type);

            // 如果大等于 0（代表没有这个类型）或 type 是虚类、密封类，设置 IsStatic 为真
            if (index >= 0 || (type.IsAbstract && type.IsSealed))
            {
                IsStatic = true;
            }

            // 获取导出文件的基类
            baseType = LuaMisc.GetExportBaseType(type);
        }

        #endregion

        /// <summary>
        /// 设置父类类型 baseType 变量并返回 BindType 自身
        /// </summary>
        public BindType SetBaseType(Type t)
        {
            baseType = t;
            return this;
        }

        /// <summary>
        /// 添加参数 t 到 list extendList 并返回 BindType 自身
        /// </summary>
        public BindType AddExtendType(Type t)
        {
            if (!extendList.Contains(t))
            {
                extendList.Add(t);
            }

            return this;
        }

        /// <summary>
        /// 设置变量 wrapName 并返回 BindType 自身
        /// </summary>
        public BindType SetWrapName(string str)
        {
            wrapName = str;
            return this;
        }

        /// <summary>
        /// 设置变量 libName 并返回 BindType 自身
        /// </summary>
        public BindType SetLibName(string str)
        {
            libName = str;
            return this;
        }

        /// <summary>
        /// 设置变量 nameSpace 并返回 BindType 自身
        /// </summary>
        public BindType SetNameSpace(string space)
        {
            nameSpace = space;            
            return this;
        }

        /// <summary>
        /// 返回指定类型是否已经被废弃
        /// </summary>
        public static bool IsObsolete(Type type)
        {
            object[] attrs = type.GetCustomAttributes(true);

            for (int j = 0; j < attrs.Length; j++)
            {
                Type t = attrs[j].GetType();

                if (t == typeof(System.ObsoleteAttribute) || t == typeof(NoToLuaAttribute) || t.Name == "MonoNotSupportedAttribute" || t.Name == "MonoTODOAttribute")
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// （自动）添加参数 BindType bt 的基类类型到 List<BindType> allTypes
    /// </summary>
    static void AutoAddBaseType(BindType bt, bool beDropBaseType)
    {
        // 获取参数 bt 中 baseType 的值
        Type t = bt.baseType;

        // 如果基类为空直接返回
        if (t == null)
        {
            return;
        }

        // 如果是接口类型则打印警告
        if (t.IsInterface)
        {
            Debugger.LogWarning("{0} has a base type {1} is Interface, use SetBaseType to jump it", bt.name, t.FullName);
            // 将参数 bt 的基类设为类型 t 的基类（object？）
            bt.baseType = t.BaseType;
        }
        // 如果存在于不导出 list 中，同样打印警告
        else if (dropType.IndexOf(t) >= 0)
        {
            Debugger.LogWarning("{0} has a base type {1} is a drop type", bt.name, t.FullName);
            // 将参数 bt 的基类设为类型 t 的基类（object？）
            bt.baseType = t.BaseType;
        }
        // 如果参数 beDropBaseType 为假，或可导出类型中没有类型 t
        else if (!beDropBaseType || baseType.IndexOf(t) < 0)
        {
            // 找到 t 位于 List<BindType> allTypes 中类型相同的 BindType 元素位置
            int index = allTypes.FindIndex((iter) => { return iter.type == t; });

            // 如果没有找到就打印警告并新建 BindType 加入到 allTypes 中去，否则返回空（已经被添加过了）
            if (index < 0)
            {
#if JUMP_NODEFINED_ABSTRACT
                if (t.IsAbstract && !t.IsSealed)
                {
                    Debugger.LogWarning("not defined bindtype for {0}, it is abstract class, jump it, child class is {1}", t.FullName, bt.name);
                    bt.baseType = t.BaseType;
                }
                else
                {
                    Debugger.LogWarning("not defined bindtype for {0}, autogen it, child class is {1}", t.FullName, bt.name);
                    bt = new BindType(t);
                    allTypes.Add(bt);
                }
#else
                Debugger.LogWarning("not defined bindtype for {0}, autogen it, child class is {1}", t.FullName, bt.name);                        
                bt = new BindType(t);
                allTypes.Add(bt);
#endif
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }

        // 调用自身以验证添加结果（如果有进行添加的话则进行相应处理，无添加则返回）
        AutoAddBaseType(bt, beDropBaseType);
    }

    /// <summary>
    /// 根据参数数组 list 生成基类的 BindType 信息类以及更新 List<BindType> allTypes
    /// （根据参数 beDropBaseType 判断是否移除 list 中的基类），最后将 allTypes 以数组形式返回
    /// </summary>
    static BindType[] GenBindTypes(BindType[] list, bool beDropBaseType = true)
    {
        // 将参数 list 数组赋值给 allTypes，使其成为一个全新的 list
        allTypes = new List<BindType>(list);

        for (int i = 0; i < list.Length; i++)
        {
            // 如果之后的元素类型与当前元素相等，抛出异常
            for (int j = i + 1; j < list.Length; j++)
            {
                if (list[i].type == list[j].type)
                    throw new NotSupportedException("Repeat BindType:" + list[i].type);
            }

            // 如果存在于不需要导出的 list 中，打印警告并从 allTypes 中移除
            if (dropType.IndexOf(list[i].type) >= 0)
            {
                Debug.LogWarning(list[i].type.FullName + " in dropType table, not need to export");
                allTypes.Remove(list[i]);
                continue;
            }
            // 如果 beDropBaseType 为真且存在于 baseType 中，打印警告并从 allTypes 中移除、跳过本次循环
            else if (beDropBaseType && baseType.IndexOf(list[i].type) >= 0)
            {
                Debug.LogWarning(list[i].type.FullName + " is Base Type, not need to export");
                allTypes.Remove(list[i]);
                continue;
            }
            // 如果当前元素类型是枚举则跳过
            else if (list[i].type.IsEnum)
            {
                continue;
            }

            // 添加父类类型到 List<BindType> allTypes
            AutoAddBaseType(list[i], beDropBaseType);
        }
        // 将 allTypes 以数组形式返回
        return allTypes.ToArray();
    }

    /// <summary>
    /// Lua 菜单 Gen Lua Wrap Files 功能
    /// 生成 c# Wrap 文件到指定(CustomSettings.saveDir)目录
    /// 生成的内容（按顺序）包括：
    /// 1.当前类中的 List<Type> baseType；
    /// 2.根据 CustomSettings.customTypeList 调用 GenBindTypes 方法更新后的 List<BindType> allTypes；
    /// </summary>
    [MenuItem("Lua/Gen Lua Wrap Files", false, 1)]
    public static void GenerateClassWraps()
    {
        // 如果 beAutoGen 为假且脚本正在编译中就弹窗警告
        if (!beAutoGen && EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        // 如果指定目录不存在就进行创建
        if (!File.Exists(CustomSettings.saveDir))
        {
            Directory.CreateDirectory(CustomSettings.saveDir);
        }

        // 清空 List<BindType> allTypes
        allTypes.Clear();
        // 以数组形式获取 CustomSettings.customTypeList
        BindType[] typeList = CustomSettings.customTypeList;

        // 获取根据 CustomSettings.customTypeList 更新后的 List<BindType> allTypes
        BindType[] list = GenBindTypes(typeList);
        // 将 List<Type> baseType 添加到 ToLuaExport.allTypes 中(不是自身的 allTypes)
        ToLuaExport.allTypes.AddRange(baseType);

        // 将更新后的 List<BindType> allTypes 添加到 ToLuaExport.allTypes 中(不是自身的 allTypes)
        for (int i = 0; i < list.Length; i++)
        {            
            ToLuaExport.allTypes.Add(list[i].type);
        }

        // 逐一生成 c# Wrap 文件到指定目录
        for (int i = 0; i < list.Length; i++)
        {
            ToLuaExport.Clear();
            ToLuaExport.className = list[i].name;
            ToLuaExport.type = list[i].type;
            ToLuaExport.isStaticClass = list[i].IsStatic;            
            ToLuaExport.baseType = list[i].baseType;
            ToLuaExport.wrapClassName = list[i].wrapName;
            ToLuaExport.libClassName = list[i].libName;
            ToLuaExport.extendList = list[i].extendList;
            ToLuaExport.Generate(CustomSettings.saveDir);
        }

        // 打印结果并释放、刷新资源
        Debug.Log("Generate lua binding files over");
        ToLuaExport.allTypes.Clear();
        allTypes.Clear();        
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 以 HashSet<Type> 形式获取 CustomSettings.customTypeList 中所有委托类型元素(字段、属性、方法参数）的类型
    /// </summary>
    static HashSet<Type> GetCustomTypeDelegates()
    {
        // 以数组形式获取 CustomSettings.customTypeList 的所有元素
        BindType[] list = CustomSettings.customTypeList;
        HashSet<Type> set = new HashSet<Type>();
        // 指定 BindingFlags 为公共、静态、忽略大小写、包括实例
        BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.Instance;

        for (int i = 0; i < list.Length; i++)
        {
            Type type = list[i].type;
            // 根据 BindingFlags 指定规则获取字段
            FieldInfo[] fields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | binding);
            // 根据 BindingFlags 指定规则获取属性信息
            PropertyInfo[] props = type.GetProperties(BindingFlags.GetProperty | BindingFlags.SetProperty | binding);
            MethodInfo[] methods = null;

            // 为 MethodInfo[] methods 获取 MethodInfo
            if (type.IsInterface)
            {
                methods = type.GetMethods();
            }
            else
            {
                methods = type.GetMethods(BindingFlags.Instance | binding);
            }

            // 为 HashSet<Type> set 添加委托类型字段的类型
            for (int j = 0; j < fields.Length; j++)
            {
                Type t = fields[j].FieldType;

                if (ToLuaExport.IsDelegateType(t))
                {
                    set.Add(t);
                }
            }

            // 为 HashSet<Type> set 添加委托类型属性的类型
            for (int j = 0; j < props.Length; j++)
            {
                Type t = props[j].PropertyType;

                if (ToLuaExport.IsDelegateType(t))
                {
                    set.Add(t);
                }
            }

            for (int j = 0; j < methods.Length; j++)
            {
                MethodInfo m = methods[j];

                // 如果是泛型方法直接跳过
                if (m.IsGenericMethod)
                {
                    continue;
                }

                // 获取当前元素的参数信息
                ParameterInfo[] pifs = m.GetParameters();

                // 为 HashSet<Type> set 添加委托类型参数的类型
                for (int k = 0; k < pifs.Length; k++)
                {
                    Type t = pifs[k].ParameterType;
                    if (t.IsByRef) t = t.GetElementType();

                    if (ToLuaExport.IsDelegateType(t))
                    {
                        set.Add(t);
                    }
                }
            }

        }

        return set;
    }

    /// <summary>
    /// Lua 菜单 Gen Lua Delegates 功能
    /// 根据 CustomSettings.customDelegateList 生成 "DelegateFactory.cs" 脚本
    /// </summary>
    [MenuItem("Lua/Gen Lua Delegates", false, 2)]
    static void GenLuaDelegates()
    {
        // 如果 beAutoGen 为假且脚本正在编译中就弹窗警告
        if (!beAutoGen && EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        // 重置 ToLuaExport
        ToLuaExport.Clear();
        // 新建一个委托类型信息类 list，并将 CustomSettings.customDelegateList 中所有元素添加进来
        List<DelegateType> list = new List<DelegateType>();
        list.AddRange(CustomSettings.customDelegateList);
        // 以 HashSet<Type> 形式获取 CustomSettings.customTypeList 中所有委托类型元素(字段、属性、方法参数）的类型
        HashSet<Type> set = GetCustomTypeDelegates();

        // 如果委托 list 中找不到当前类型的委托类型信息类，就新建一个并添加到 HashSet 中
        foreach (Type t in set)
        {
            if (null == list.Find((p) => { return p.type == t; }))
            {
                list.Add(new DelegateType(t));
            }
        }

        // 生成 "DelegateFactory.cs" 脚本
        ToLuaExport.GenDelegates(list.ToArray());
        // 释放资源
        set.Clear();
        ToLuaExport.Clear();
        // 刷新 AssetDatabase、打印消息
        AssetDatabase.Refresh();
        Debug.Log("Create lua delegate over");
    }

    /// <summary>
    /// 
    /// </summary>
    static ToLuaTree<string> InitTree()
    {                        
        ToLuaTree<string> tree = new ToLuaTree<string>();
        ToLuaNode<string> root = tree.GetRoot();        
        BindType[] list = GenBindTypes(CustomSettings.customTypeList);

        for (int i = 0; i < list.Length; i++)
        {
            string space = list[i].nameSpace;
            AddSpaceNameToTree(tree, root, space);
        }

        DelegateType[] dts = CustomSettings.customDelegateList;
        string str = null;      

        for (int i = 0; i < dts.Length; i++)
        {            
            string space = ToLuaExport.GetNameSpace(dts[i].type, out str);
            AddSpaceNameToTree(tree, root, space);            
        }

        return tree;
    }

    /// <summary>
    /// 
    /// </summary>
    static void AddSpaceNameToTree(ToLuaTree<string> tree, ToLuaNode<string> parent, string space)
    {
        if (space == null || space == string.Empty)
        {
            return;
        }

        string[] ns = space.Split(new char[] { '.' });

        for (int j = 0; j < ns.Length; j++)
        {
            List<ToLuaNode<string>> nodes = tree.Find((_t) => { return _t == ns[j]; }, j);

            if (nodes.Count == 0)
            {
                ToLuaNode<string> node = new ToLuaNode<string>();
                node.value = ns[j];
                parent.childs.Add(node);
                node.parent = parent;
                node.layer = j;
                parent = node;
            }
            else
            {
                bool flag = false;
                int index = 0;

                for (int i = 0; i < nodes.Count; i++)
                {
                    int count = j;
                    int size = j;
                    ToLuaNode<string> nodecopy = nodes[i];

                    while (nodecopy.parent != null)
                    {
                        nodecopy = nodecopy.parent;
                        if (nodecopy.value != null && nodecopy.value == ns[--count])
                        {
                            size--;
                        }
                    }

                    if (size == 0)
                    {
                        index = i;
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    ToLuaNode<string> nnode = new ToLuaNode<string>();
                    nnode.value = ns[j];
                    nnode.layer = j;
                    nnode.parent = parent;
                    parent.childs.Add(nnode);
                    parent = nnode;
                }
                else
                {
                    parent = nodes[index];
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static string GetSpaceNameFromTree(ToLuaNode<string> node)
    {
        string name = node.value;

        while (node.parent != null && node.parent.value != null)
        {
            node = node.parent;
            name = node.value + "." + name;
        }

        return name;
    }

    /// <summary>
    /// 
    /// </summary>
    static string RemoveTemplateSign(string str)
    {
        str = str.Replace('<', '_');

        int index = str.IndexOf('>');

        while (index > 0)
        {
            str = str.Remove(index, 1);
            index = str.IndexOf('>');
        }

        return str;
    }

    /// <summary>
    /// Lua 菜单 Gen LuaBinder File 功能
    /// 生成 LuaBinder.cs 文件。LuaBinder 用于与各种 UnityEngine 的 Wrap 文件协调（互相注册、载入与绑定），处理各种事件、委托、回调等
    /// 涉及到的 list：
    /// 1.CustomSettings.customDelegateList；
    /// 2.自身 allTypes；
    /// 3.CustomSettings.dynamicList；
    /// </summary>
    [MenuItem("Lua/Gen LuaBinder File", false, 4)]
    static void GenLuaBinder()
    {
        if (!beAutoGen && EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        allTypes.Clear();
        ToLuaTree<string> tree = InitTree();        
        StringBuilder sb = new StringBuilder();
        List<DelegateType> dtList = new List<DelegateType>();

        List<DelegateType> list = new List<DelegateType>();
        list.AddRange(CustomSettings.customDelegateList);
        // 以 HashSet<Type> 形式获取 CustomSettings.customTypeList 中所有委托类型元素(字段、属性、方法参数）的类型
        HashSet<Type> set = GetCustomTypeDelegates();

        // 以 List<BindType> 形式获取自身 allTypes 所有元素
        List<BindType> backupList = new List<BindType>();
        backupList.AddRange(allTypes);
        ToLuaNode<string> root = tree.GetRoot();
        string libname = null;

        // 补充 List<DelegateType> list 中没有的 DelegateType
        foreach (Type t in set)
        {
            if (null == list.Find((p) => { return p.type == t; }))
            {
                DelegateType dt = new DelegateType(t);                                
                AddSpaceNameToTree(tree, root, ToLuaExport.GetNameSpace(t, out libname));
                list.Add(dt);
            }
        }

        // 拼接文本
        sb.AppendLineEx("//this source code was auto-generated by tolua#, do not modify it");
        sb.AppendLineEx("using System;");
        sb.AppendLineEx("using UnityEngine;");
        sb.AppendLineEx("using LuaInterface;");
        sb.AppendLineEx();
        sb.AppendLineEx("public static class LuaBinder");
        sb.AppendLineEx("{");
        sb.AppendLineEx("\tpublic static void Bind(LuaState L)");
        sb.AppendLineEx("\t{");
        sb.AppendLineEx("\t\tfloat t = Time.realtimeSinceStartup;");
        sb.AppendLineEx("\t\tL.BeginModule(null);");

        // 生成注册信息字符串并对 list、dtList 两个 list 做相应更新
        GenRegisterInfo(null, sb, list, dtList);

        // 为 DepthFirstTraversal 方法新建一个将 value 不为空的 ToLuaNode<string> 的 value 组织字符串，并添加到参数 sb 中（于开头处添加字符）；再获取参数 node 的完全限定名、根据 t 的 Namespace 更新参数 tree 和 root 的父或子节点信息
        Action<ToLuaNode<string>> begin = (node) =>
        {
            if (node.value == null)
            {
                return;
            }

            sb.AppendFormat("\t\tL.BeginModule(\"{0}\");\r\n", node.value);
            string space = GetSpaceNameFromTree(node);

            GenRegisterInfo(space, sb, list, dtList);
        };
        // 为 DepthFirstTraversal 方法新建一个将 value 不为空的 ToLuaNode<string> 的 value 组织字符串并添加到参数 sb 中（于结尾处添加字符）
        Action <ToLuaNode<string>> end = (node) =>
        {
            if (node.value != null)
            {
                sb.AppendLineEx("\t\tL.EndModule();");
            }
        };

        // 从最底层的子节点开始对参数 node 及其子节点进行遍历，同时使用 begin 和 end 委托对字符串进行修改
        tree.DepthFirstTraversal(begin, end, tree.GetRoot());
        // 在结尾处添加字符串      
        sb.AppendLineEx("\t\tL.EndModule();");

        // 处理动态类型 list 字符串
        if (CustomSettings.dynamicList.Count > 0)
        {
            sb.AppendLineEx("\t\tL.BeginPreLoad();");            

            for (int i = 0; i < CustomSettings.dynamicList.Count; i++)
            {
                Type t1 = CustomSettings.dynamicList[i];
                BindType bt = backupList.Find((p) => { return p.type == t1; });
                sb.AppendFormat("\t\tL.AddPreLoad(\"{0}\", LuaOpen_{1}, typeof({0}));\r\n", bt.name, bt.wrapName);
            }

            sb.AppendLineEx("\t\tL.EndPreLoad();");
        }

        sb.AppendLineEx("\t\tDebugger.Log(\"Register lua type cost time: {0}\", Time.realtimeSinceStartup - t);");
        sb.AppendLineEx("\t}");

        // 遍历 dtList，为每一个元素生成指定类型的 EventFunction 字符串并添加到参数 StringBuilder sb 中
        for (int i = 0; i < dtList.Count; i++)
        {
            ToLuaExport.GenEventFunction(dtList[i].type, sb);
        }

        // 遍历动态类型 list 并为每一个 backupList 中类型与当前元素相等的 BindType 生成 PreLoadFunction 字符串并添加到参数 StringBuilder sb
        if (CustomSettings.dynamicList.Count > 0)
        {
            
            for (int i = 0; i < CustomSettings.dynamicList.Count; i++)
            {
                Type t = CustomSettings.dynamicList[i];
                BindType bt = backupList.Find((p) => { return p.type == t; });
                GenPreLoadFunction(bt, sb);
            }            
        }

        // 添加结尾字符串，并清空 allTypes，合成文件储存路径
        sb.AppendLineEx("}\r\n");
        allTypes.Clear();
        string file = CustomSettings.saveDir + "LuaBinder.cs";

        // 生成文件、刷新 AssetDatabase 并打印提示
        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }

        AssetDatabase.Refresh();
        Debugger.Log("Generate LuaBinder over !");
    }

    /// <summary>
    /// 
    /// </summary>
    static void GenRegisterInfo(string nameSpace, StringBuilder sb, List<DelegateType> delegateList, List<DelegateType> wrappedDelegatesCache)
    {
        for (int i = 0; i < allTypes.Count; i++)
        {
            Type dt = CustomSettings.dynamicList.Find((p) => { return allTypes[i].type == p; });

            if (dt == null && allTypes[i].nameSpace == nameSpace)
            {
                string str = "\t\t" + allTypes[i].wrapName + "Wrap.Register(L);\r\n";
                sb.Append(str);
                allTypes.RemoveAt(i--);
            }
        }

        string funcName = null;

        for (int i = 0; i < delegateList.Count; i++)
        {
            DelegateType dt = delegateList[i];
            Type type = dt.type;
            string typeSpace = ToLuaExport.GetNameSpace(type, out funcName);

            if (typeSpace == nameSpace)
            {
                funcName = ToLuaExport.ConvertToLibSign(funcName);
                string abr = dt.abr;
                abr = abr == null ? funcName : abr;
                sb.AppendFormat("\t\tL.RegFunction(\"{0}\", {1});\r\n", abr, dt.name);
                wrappedDelegatesCache.Add(dt);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static void GenPreLoadFunction(BindType bt, StringBuilder sb)
    {
        string funcName = "LuaOpen_" + bt.wrapName;

        sb.AppendLineEx("\r\n\t[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]");
        sb.AppendFormat("\tstatic int {0}(IntPtr L)\r\n", funcName);
        sb.AppendLineEx("\t{");
        sb.AppendLineEx("\t\ttry");
        sb.AppendLineEx("\t\t{");        
        sb.AppendLineEx("\t\t\tLuaState state = LuaState.Get(L);");
        sb.AppendFormat("\t\t\tstate.BeginPreModule(\"{0}\");\r\n", bt.nameSpace);
        sb.AppendFormat("\t\t\t{0}Wrap.Register(state);\r\n", bt.wrapName);
        sb.AppendFormat("\t\t\tint reference = state.GetMetaReference(typeof({0}));\r\n", bt.name);
        sb.AppendLineEx("\t\t\tstate.EndPreModule(L, reference);");                
        sb.AppendLineEx("\t\t\treturn 1;");
        sb.AppendLineEx("\t\t}");
        sb.AppendLineEx("\t\tcatch(Exception e)");
        sb.AppendLineEx("\t\t{");
        sb.AppendLineEx("\t\t\treturn LuaDLL.toluaL_exception(L, e);");
        sb.AppendLineEx("\t\t}");
        sb.AppendLineEx("\t}");
    }

    /// <summary>
    /// 
    /// </summary>
    static string GetOS()
    {
        return ToLuaConst.osDir;
    }

    /// <summary>
    /// 
    /// </summary>
    static string CreateStreamDir(string dir)
    {
        dir = Application.streamingAssetsPath + "/" + dir;

        if (!File.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        return dir;
    }

    /// <summary>
    /// 
    /// </summary>
    static void BuildLuaBundle(string subDir, string sourceDir)
    {
        string[] files = Directory.GetFiles(sourceDir + subDir, "*.bytes");
        string bundleName = subDir == null ? "lua.unity3d" : "lua" + subDir.Replace('/', '_') + ".unity3d";
        bundleName = bundleName.ToLower();

#if UNITY_5        
        for (int i = 0; i < files.Length; i++)
        {
            AssetImporter importer = AssetImporter.GetAtPath(files[i]);            

            if (importer)
            {
                importer.assetBundleName = bundleName;
                importer.assetBundleVariant = null;                
            }
        }
#else        
        List<Object> list = new List<Object>();

        for (int i = 0; i < files.Length; i++)
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(files[i]);
            list.Add(obj);
        }

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;

        if (files.Length > 0)
        {
            string output = string.Format("{0}/{1}/" + bundleName, Application.streamingAssetsPath, GetOS());
            File.Delete(output);
            BuildPipeline.BuildAssetBundle(null, list.ToArray(), output, options, EditorUserBuildSettings.activeBuildTarget);            
        }
#endif        
    }

    /// <summary>
    /// 
    /// </summary>
    static void ClearAllLuaFiles()
    {
        string osPath = Application.streamingAssetsPath + "/" + GetOS();

        if (Directory.Exists(osPath))
        {
            string[] files = Directory.GetFiles(osPath, "Lua*.unity3d");

            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }

        string path = osPath + "/Lua";

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        path = Application.streamingAssetsPath + "/Lua";

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        path = Application.dataPath + "/temp";

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        path = Application.dataPath + "/Resources/Lua";

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        path = Application.persistentDataPath + "/" + GetOS() + "/Lua";

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Gen LuaWrap + Binder", false, 4)]
    static void GenLuaWrapBinder()
    {
        if (EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        beAutoGen = true;        
        AssetDatabase.Refresh();
        GenerateClassWraps();
        GenLuaBinder();
        beAutoGen = false;   
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Generate All", false, 5)]
    static void GenLuaAll()
    {
        if (EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        beAutoGen = true;
        GenLuaDelegates();
        AssetDatabase.Refresh();
        GenerateClassWraps();
        GenLuaBinder();
        beAutoGen = false;
    }

    /// <summary>
    /// Lua 菜单 Clear wrap files 功能
    /// 清除所有 wrap 文件、释放资源、重新生成空的 LuaBinder.cs
    /// </summary>
    [MenuItem("Lua/Clear wrap files", false, 6)]
    static void ClearLuaWraps()
    {
        string[] files = Directory.GetFiles(CustomSettings.saveDir, "*.cs", SearchOption.TopDirectoryOnly);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        ToLuaExport.Clear();
        List<DelegateType> list = new List<DelegateType>();
        ToLuaExport.GenDelegates(list.ToArray());
        ToLuaExport.Clear();

        StringBuilder sb = new StringBuilder();
        sb.AppendLineEx("using System;");
        sb.AppendLineEx("using LuaInterface;");
        sb.AppendLineEx();
        sb.AppendLineEx("public static class LuaBinder");
        sb.AppendLineEx("{");
        sb.AppendLineEx("\tpublic static void Bind(LuaState L)");
        sb.AppendLineEx("\t{");
        sb.AppendLineEx("\t\tthrow new LuaException(\"Please generate LuaBinder files first!\");");
        sb.AppendLineEx("\t}");
        sb.AppendLineEx("}");

        string file = CustomSettings.saveDir + "LuaBinder.cs";

        using (StreamWriter textWriter = new StreamWriter(file, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    static void CopyLuaBytesFiles(string sourceDir, string destDir, bool appendext = true, string searchPattern = "*.lua", SearchOption option = SearchOption.AllDirectories)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

        string[] files = Directory.GetFiles(sourceDir, searchPattern, option);
        int len = sourceDir.Length;

        if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
        {
            --len;
        }         

        for (int i = 0; i < files.Length; i++)
        {
            string str = files[i].Remove(0, len);
            string dest = destDir + "/" + str;
            if (appendext) dest += ".bytes";
            string dir = Path.GetDirectoryName(dest);
            Directory.CreateDirectory(dir);
            File.Copy(files[i], dest, true);
        }
    }

    /// <summary>
    /// Lua 菜单 Copy Lua  files to Resources 功能
    /// </summary>
    [MenuItem("Lua/Copy Lua  files to Resources", false, 51)]
    public static void CopyLuaFilesToRes()
    {
        ClearAllLuaFiles();
        string destDir = Application.dataPath + "/Resources" + "/Lua";
        CopyLuaBytesFiles(ToLuaConst.luaDir, destDir);
        CopyLuaBytesFiles(ToLuaConst.toluaDir, destDir);
        AssetDatabase.Refresh();
        Debug.Log("Copy lua files over");
    }

    /// <summary>
    /// Lua 菜单 Copy Lua  files to Persistent 功能
    /// </summary>
    [MenuItem("Lua/Copy Lua  files to Persistent", false, 52)]
    public static void CopyLuaFilesToPersistent()
    {
        ClearAllLuaFiles();
        string destDir = Application.persistentDataPath + "/" + GetOS() + "/Lua";
        CopyLuaBytesFiles(ToLuaConst.luaDir, destDir, false);
        CopyLuaBytesFiles(ToLuaConst.toluaDir, destDir, false);
        AssetDatabase.Refresh();
        Debug.Log("Copy lua files over");
    }

    /// <summary>
    /// 
    /// </summary>
    static void GetAllDirs(string dir, List<string> list)
    {
        string[] dirs = Directory.GetDirectories(dir);
        list.AddRange(dirs);

        for (int i = 0; i < dirs.Length; i++)
        {
            GetAllDirs(dirs[i], list);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    static void CopyDirectory(string source, string dest, string searchPattern = "*.lua", SearchOption option = SearchOption.AllDirectories)
    {                
        string[] files = Directory.GetFiles(source, searchPattern, option);

        for (int i = 0; i < files.Length; i++)
        {
            string str = files[i].Remove(0, source.Length);
            string path = dest + "/" + str;
            string dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);
            File.Copy(files[i], path, true);
        }        
    }

    /// <summary>
    /// Lua 菜单 Build Lua files to Resources (PC) 功能
    /// 拷贝文件到对应目录
    /// </summary>
    [MenuItem("Lua/Build Lua files to Resources (PC)", false, 53)]
    public static void BuildLuaToResources()
    {
        ClearAllLuaFiles();
        string tempDir = CreateStreamDir("Lua");
        string destDir = Application.dataPath + "/Resources" + "/Lua";        

        string path = Application.dataPath.Replace('\\', '/');
        path = path.Substring(0, path.LastIndexOf('/'));
        File.Copy(path + "/Luajit/Build.bat", tempDir +  "/Build.bat", true);
        CopyLuaBytesFiles(ToLuaConst.luaDir, tempDir, false);
        Process proc = Process.Start(tempDir + "/Build.bat");
        proc.WaitForExit();
        CopyLuaBytesFiles(tempDir + "/Out/", destDir, false, "*.lua.bytes");
        CopyLuaBytesFiles(ToLuaConst.toluaDir, destDir);
        
        Directory.Delete(tempDir, true);        
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Build Lua files to Persistent (PC)", false, 54)]
    public static void BuildLuaToPersistent()
    {
        ClearAllLuaFiles();
        string tempDir = CreateStreamDir("Lua");        
        string destDir = Application.persistentDataPath + "/" + GetOS() + "/Lua/";

        string path = Application.dataPath.Replace('\\', '/');
        path = path.Substring(0, path.LastIndexOf('/'));
        File.Copy(path + "/Luajit/Build.bat", tempDir + "/Build.bat", true);
        CopyLuaBytesFiles(ToLuaConst.luaDir, tempDir, false);
        Process proc = Process.Start(tempDir + "/Build.bat");
        proc.WaitForExit();        
        CopyLuaBytesFiles(ToLuaConst.toluaDir, destDir, false);

        path = tempDir + "/Out/";
        string[] files = Directory.GetFiles(path, "*.lua.bytes");
        int len = path.Length;

        for (int i = 0; i < files.Length; i++)
        {
            path = files[i].Remove(0, len);
            path = path.Substring(0, path.Length - 6);
            path = destDir + path;

            File.Copy(files[i], path, true);
        }

        Directory.Delete(tempDir, true);
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Build bundle files not jit", false, 55)]
    public static void BuildNotJitBundles()
    {
        ClearAllLuaFiles();
        CreateStreamDir(GetOS());

#if !UNITY_5
        string tempDir = CreateStreamDir("Lua");
#else
        string tempDir = Application.dataPath + "/temp/Lua";

        if (!File.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }        
#endif
        CopyLuaBytesFiles(ToLuaConst.luaDir, tempDir);
        CopyLuaBytesFiles(ToLuaConst.toluaDir, tempDir);

        AssetDatabase.Refresh();
        List<string> dirs = new List<string>();
        GetAllDirs(tempDir, dirs);

#if UNITY_5        
        for (int i = 0; i < dirs.Count; i++)
        {
            string str = dirs[i].Remove(0, tempDir.Length);
            BuildLuaBundle(str.Replace('\\', '/'), "Assets/temp/Lua");
        }

        BuildLuaBundle(null, "Assets/temp/Lua");

        AssetDatabase.SaveAssets();        
        string output = string.Format("{0}/{1}", Application.streamingAssetsPath, GetOS());        
        BuildPipeline.BuildAssetBundles(output, BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);

        //Directory.Delete(Application.dataPath + "/temp/", true);
#else
        for (int i = 0; i < dirs.Count; i++)
        {
            string str = dirs[i].Remove(0, tempDir.Length);
            BuildLuaBundle(str.Replace('\\', '/'), "Assets/StreamingAssets/Lua");
        }

        BuildLuaBundle(null, "Assets/StreamingAssets/Lua");
        Directory.Delete(Application.streamingAssetsPath + "/Lua/", true);
#endif            
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Build Luajit bundle files   (PC)", false, 56)]
    public static void BuildLuaBundles()
    {
        ClearAllLuaFiles();                
        CreateStreamDir(GetOS());

#if !UNITY_5
        string tempDir = CreateStreamDir("Lua");
#else
        string tempDir = Application.dataPath + "/temp/Lua";

        if (!File.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }
#endif

        string path = Application.dataPath.Replace('\\', '/');
        path = path.Substring(0, path.LastIndexOf('/'));
        File.Copy(path + "/Luajit/Build.bat", tempDir + "/Build.bat", true);
        CopyLuaBytesFiles(ToLuaConst.luaDir, tempDir, false);
        Process proc = Process.Start(tempDir + "/Build.bat");
        proc.WaitForExit();
        CopyLuaBytesFiles(ToLuaConst.toluaDir, tempDir + "/Out");

        AssetDatabase.Refresh();

        string sourceDir = tempDir + "/Out";
        List<string> dirs = new List<string>();        
        GetAllDirs(sourceDir, dirs);

#if UNITY_5
        for (int i = 0; i < dirs.Count; i++)
        {
            string str = dirs[i].Remove(0, sourceDir.Length);
            BuildLuaBundle(str.Replace('\\', '/'), "Assets/temp/Lua/Out");
        }

        BuildLuaBundle(null, "Assets/temp/Lua/Out");

        AssetDatabase.Refresh();
        string output = string.Format("{0}/{1}", Application.streamingAssetsPath, GetOS());
        BuildPipeline.BuildAssetBundles(output, BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget);
        Directory.Delete(Application.dataPath + "/temp/", true);
#else
        for (int i = 0; i < dirs.Count; i++)
        {
            string str = dirs[i].Remove(0, sourceDir.Length);
            BuildLuaBundle(str.Replace('\\', '/'), "Assets/StreamingAssets/Lua/Out");
        }

        BuildLuaBundle(null, "Assets/StreamingAssets/Lua/Out/");
        Directory.Delete(tempDir, true);
#endif
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Clear all Lua files", false, 57)]
    public static void ClearLuaFiles()
    {
        ClearAllLuaFiles();
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Gen BaseType Wrap", false, 101)]
    static void GenBaseTypeLuaWrap()
    {
        if (!beAutoGen && EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译再执行此功能", "确定");
            return;
        }

        string dir = CustomSettings.toluaBaseType;

        if (!File.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        allTypes.Clear();
        ToLuaExport.allTypes.AddRange(baseType);
        List<BindType> btList = new List<BindType>();
        
        for (int i = 0; i < baseType.Count; i++)
        {
            btList.Add(new BindType(baseType[i]));
        }

        GenBindTypes(btList.ToArray(), false);
        BindType[] list = allTypes.ToArray();

        for (int i = 0; i < list.Length; i++)
        {
            ToLuaExport.Clear();
            ToLuaExport.className = list[i].name;
            ToLuaExport.type = list[i].type;
            ToLuaExport.isStaticClass = list[i].IsStatic;
            ToLuaExport.baseType = list[i].baseType;
            ToLuaExport.wrapClassName = list[i].wrapName;
            ToLuaExport.libClassName = list[i].libName;
            ToLuaExport.Generate(dir);
        }
        
        Debug.Log("Generate base type files over");
        allTypes.Clear();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 
    /// </summary>
    static void CreateDefaultWrapFile(string path, string name)
    {
        StringBuilder sb = new StringBuilder();
        path = path + name + ".cs";
        sb.AppendLineEx("using System;");
        sb.AppendLineEx("using LuaInterface;");
        sb.AppendLineEx();
        sb.AppendLineEx("public static class " + name);
        sb.AppendLineEx("{");
        sb.AppendLineEx("\tpublic static void Register(LuaState L)");
        sb.AppendLineEx("\t{");        
        sb.AppendLineEx("\t\tthrow new LuaException(\"Please click menu Lua/Gen BaseType Wrap first!\");");
        sb.AppendLineEx("\t}");
        sb.AppendLineEx("}");

        using (StreamWriter textWriter = new StreamWriter(path, false, Encoding.UTF8))
        {
            textWriter.Write(sb.ToString());
            textWriter.Flush();
            textWriter.Close();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Lua/Clear BaseType Wrap", false, 102)]
    static void ClearBaseTypeLuaWrap()
    {
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_ObjectWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_DelegateWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_StringWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_EnumWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_TypeWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "System_Collections_IEnumeratorWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "UnityEngine_ObjectWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "LuaInterface_EventObjectWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "LuaInterface_LuaMethodWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "LuaInterface_LuaPropertyWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "LuaInterface_LuaFieldWrap");
        CreateDefaultWrapFile(CustomSettings.toluaBaseType, "LuaInterface_LuaConstructorWrap");        

        Debug.Log("Clear base type wrap files over");
        AssetDatabase.Refresh();
    }
            }
