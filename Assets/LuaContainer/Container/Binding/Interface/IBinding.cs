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

namespace LuaContainer.Container
{
    public interface IBinding
    {
        #region property

        /// <summary>
        /// binder 属性
        /// </summary>
        IBinder binder { get; }

        /// <summary>
        /// type 属性
        /// </summary>
        Type type { get; }

        /// <summary>
        ///  value 属性 返回 valueList 的第一个元素
        ///  valueList 返回整个 valueList
        /// </summary>
        object value { get; }
        IList<object> valueList { get; }

        /// <summary>
        /// id 属性
        /// </summary>
        object id { get; }

        /// <summary>
        /// constraint 属性，(ONE \ MULTIPLE \ POOL)
        /// </summary>
        ConstraintType constraint { get; }

        /// <summary>
        /// bindingType 属性
        /// </summary>
        BindingType bindingType { get; }

        /// <summary>
        /// condition 属性
        /// </summary>
        Condition condition { get; set; }

        #endregion

        #region To

        /// <summary>
        /// 将 value 属性设为其自身的 type
        IBinding ToSelf();

        /// <summary>
        /// 向 value 属性中添加一个类型
        /// </summary>
        IBinding To<T>() where T : class;

        /// <summary>
        /// 向 value 属性中添加一个 object 类型的实例
        /// </summary>
        IBinding To(object instance);

        /// <summary>
        /// 将多个 object 添加到 value 属性中
        /// </summary>
        IBinding To(object[] value);

        #endregion

        #region As

        /// <summary>
        /// 设置 binding 的 name 属性
        /// </summary>
        IBinding As<T>() where T : class;

        /// <summary>
        /// 设置 binding 的 name 属性
        /// </summary>
        IBinding As(object name);

        #endregion

        #region When

        /// <summary>
        /// 设置 binding 的 condition 属性
        /// </summary>
        IBinding When(Condition condition);

        #endregion

        #region Into

        /// <summary>
        /// 设置 binding 的 condition 属性为 context.parentType 与参数 T 相等
        /// </summary>
        IBinding Into<T>() where T : class;

        /// <summary>
        /// 设置 binding 的 condition 属性为 context.parentType 与指定类型相等
        /// </summary>
        IBinding Into(Type type);

        #endregion

        #region ReBind

        /// <summary>
        /// 返回一个指定 type 属性的新 Binding 实例，BindingType 为 ADDRESS，值约束为 MULTIPLE
        /// </summary>
        IBinding Bind<T>();
        IBinding Bind(Type type);

        /// <summary>
        /// 返回一个指定 type 属性的新 Binding 实例，BindingType 为 SINGLETON，值约束为 SINGLE
        /// </summary>
        IBinding BindSingleton<T>();
        IBinding BindSingleton(Type type);

        /// <summary>
        ///  返回一个指定 type 属性的新 Binding 实例，BindingType 属性为 FACTORY，值约束为 SINGLE
        /// </summary>
        IBinding BindFactory<T>();
        IBinding BindFactory(Type type);

        /// <summary>
        /// 返回一个指定 type 属性的新 Binding 实例，BindingType 为 MULTITON，值约束为 MULTIPLE
        /// </summary>
        IBinding BindMultiton<T>();
        IBinding BindMultiton(Type type);

        /// <summary>
        /// 创建多个指定类型的 binding，并返回 IBindingFactory
        /// </summary>
        IBindingFactory MultipleBind(Type[] types, BindingType[] bindingTypes);

        #endregion

        #region RemoveValue

        /// <summary>
        /// 从 binding 的 value 属性中移除指定的值，如果删除后值为空，则移除 binding
        /// </summary>
        IBinding RemoveValue(object value);

        /// <summary>
        /// 从 binding 的 value 属性中移除指定的值，如果删除后值为空，则移除 binding
        /// </summary>
        IBinding RemoveValues(object[] values);

        #endregion

        /// <summary>
        /// 设置 binding 的 condition 属性为返回 context.parentInstance 与参数 instance 相等
        /// </summary>
        IBinding ParentInstanceCondition(object instance);

        #region SetProperty

        /// <summary>
        /// 设置 binding 的值
        /// </summary>
        IBinding SetValue(object obj);

        /// <summary>
        /// 设置 binding 的 ConstraintType
        /// </summary>
        IBinding SetConstraint(ConstraintType ct);

        /// <summary>
        /// 设置 binding 的 BindingType
        /// </summary>
        IBinding SetBindingType(BindingType bt);

        #endregion
    }
}