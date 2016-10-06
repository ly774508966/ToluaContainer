/*
 * Copyright (c) 2016 Joey1258
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Utils
{
    public class TypeUtils
    {
        /// <summary>
        /// 返回类型是否相同或继承于同样的父类
        /// </summary>
        public static bool IsAssignable(Type potentialBase, Type potentialDescendant)
        {
            return potentialBase.IsAssignableFrom(potentialDescendant);
        }

        /// <summary>
        /// 获取指定基类在指定命名空间中的所有同类或子类（但不包括 unity 和 mono 相关的部分)
        /// </summary>
        public static Type[] GetAssignableTypes(Type baseType)
        {
            return GetAssignableTypes(baseType, string.Empty, false);
        }

        /// <summary>
        /// 获取指定基类在指定命名空间中的所有同类或子类（但不包括 unity 和 mono 相关的部分)
        /// </summary>
        public static Type[] GetAssignableTypes(Type baseType, string namespaceName)
        {
            return GetAssignableTypes(baseType, namespaceName, false);
        }

        /// <summary>
        /// 获取指定基类在指定命名空间中的所有同类或子类（但不包括 unity 和 mono 相关的部分)
        /// </summary>
        public static Type[] GetAssignableTypes(
            Type baseType,
            string namespaceName,
            bool includeChildren)
        {
            var assignableTypes = new List<Type>();

            // 过滤所有无效程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int assemblyIndex = 0; assemblyIndex < assemblies.Length; assemblyIndex++)
            {
                var assembly = assemblies[assemblyIndex];

                if (assembly.FullName.StartsWith("Unity") ||
                    assembly.FullName.StartsWith("Boo") ||
                    assembly.FullName.StartsWith("Mono") ||
                    assembly.FullName.StartsWith("System") ||
                    assembly.FullName.StartsWith("mscorlib"))
                {
                    continue;
                }

                try
                {
                    // 尝试获取程序集中的类型
                    var assemblyTypes = assemblies[assemblyIndex].GetTypes();
                    for (int i = 0; i < assemblyTypes.Length; i++)
                    {
                        var type = assemblyTypes[i];

                        // 如果参数 namespaceName 为空
                        // 或参数 includeChildren 为真而当前 type 元素的 Namespace 属性不为空
                        // 或参数 includeChildren 为假而当前 type 元素的 Namespace 属性和参数 namespaceName相等时 isTypeInNamespace 为真
                        var isTypeInNamespace =
                            (string.IsNullOrEmpty(namespaceName)) ||
                            (includeChildren && !string.IsNullOrEmpty(type.Namespace) && type.Namespace.StartsWith(namespaceName)) ||
                            (!includeChildren && type.Namespace == namespaceName);

                        // 如果 isTypeInNamespace 为真，且当前 type 元素是类类型
                        // 且参数 baseType 与当前 type 是相同或继承于同样的父类，将它添加到 list
                        if (isTypeInNamespace &&
                            type.IsClass &&
                            TypeUtils.IsAssignable(baseType, type))
                        {
                            assignableTypes.Add(type);
                        }
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    // 如果程序集无法读取，continue.
                    continue;
                }
            }
            // 以数组形式返回
            return assignableTypes.ToArray();
        }

        /// <summary>
        /// 根据类型名称字符串获取 Type（但不包括 unity 和 mono 相关的部分)
        /// </summary>
		public static Type GetType(string typeName)
        {
            return GetType(string.Empty, typeName);
        }

        /// <summary>
        /// 根据命名空间和类型名称字符串获取 Type（但不包括 unity 和 mono 相关的部分)
        /// </summary>
		public static Type GetType(string namespaceName, string typeName)
        {
            // 组合命名空间和类名
            string fullName = null;
            if (!string.IsNullOrEmpty(typeName))
            {
                if (string.IsNullOrEmpty(namespaceName) || namespaceName == "-")
                {
                    fullName = typeName;
                }
                else {
                    fullName = string.Format("{0}.{1}", namespaceName, typeName);
                }
            }

            // 如果组合后的字符串为空，返回空
            if (string.IsNullOrEmpty(fullName)) return null;

            // 过滤所有无效程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int assemblyIndex = 0; assemblyIndex < assemblies.Length; assemblyIndex++)
            {
                var assembly = assemblies[assemblyIndex];

                if (assembly.FullName.StartsWith("Unity") ||
                    assembly.FullName.StartsWith("Boo") ||
                    assembly.FullName.StartsWith("Mono") ||
                    assembly.FullName.StartsWith("System") ||
                    assembly.FullName.StartsWith("mscorlib"))
                {
                    continue;
                }

                try
                {
                    // 变量程序集中的类型，如果全名相等，返回该元素
                    var allTypes = assemblies[assemblyIndex].GetTypes();
                    for (int i = 0; i < allTypes.Length; i++)
                    {
                        if (allTypes[i].FullName == fullName) { return allTypes[i]; }
                    }
                }
                catch (ReflectionTypeLoadException)
                {
                    // 如果程序集无法读取，continue.
                    continue;
                }
            }
            // 返回空
            return null;
        }
    }
}