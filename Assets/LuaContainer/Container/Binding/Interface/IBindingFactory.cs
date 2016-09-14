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

namespace LuaContainer.Container
{
    public interface IBindingFactory
    {
        /// <summary>
        /// binding 数组
        /// </summary>
        IBinding[] bindings { get; }

        #region Create default (MANY)

        /// <summary>
        /// 创建并返回指定类型的Binding实例，ConstraintType 为 MULTIPLE
        /// </summary>
        IBinding Create<T>(BindingType bindingType);

        /// <summary>
        /// 创建并返回指定类型的Binding实例，ConstraintType 为 MULTIPLE
        /// </summary>
        IBinding Create(Type type, BindingType bindingType);

        #endregion

        #region Create SINGLE

        /// <summary>
        /// 创建并返回指定类型的Binding实例，ConstraintType 为 SINGLE
        /// </summary>
        IBinding CreateSingle<T>(BindingType bindingType);

        /// <summary>
        /// 创建并返回指定类型的Binding实例，ConstraintType 为 SINGLE
        /// </summary>
        IBinding CreateSingle(Type type, BindingType bindingType);

        #endregion

        /// <summary>
        /// 创建指定类型的多个 Binding 实例，ConstraintType 为 MULTIPLE，并返回 IBindingFactory
        /// </summary>
        IBindingFactory MultipleCreate(Type[] types, BindingType[] bindingType);

        #region Binding System Function

        /// <summary>
        /// 为多个 binding 添加值,如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        IBindingFactory To(object[] os);

        /// <summary>
        /// 设置多个 binding 的 id 属性
        /// </summary>
        IBindingFactory As(object[] os);

        /// <summary>
        /// 设置多个 binding 的 condition 属性
        /// 如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        IBindingFactory When(Condition[] cs);

        /// <summary>
        /// 设置多个 binding 的 condition 属性为 context.parentType 与指定类型相等
        /// 如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        IBindingFactory Into(Type[] ts);

        /// <summary>
        /// 返回一个新的Binding实例，并设置指定类型给 type, BindingType 为 ADDRESS，值约束为 MULTIPLE
        /// </summary>
        IBinding Bind<T>();

        /// <summary>
        /// 返回一个新的Binding实例，并设置指定类型给 type, BindingType 为 SINGLETON，值约束为 SINGLE
        /// </summary>
        IBinding BindSingleton<T>();

        /// <summary>
        ///  返回一个新的Binding实例，并设置指定类型给 type 属性和 BindingType 属性为 FACTORY，值约束为 SINGLE
        /// </summary>
        IBinding BindFactory<T>();

        /// <summary>
        /// 创建多个指定类型的 binding，并返回 IBindingFactory
        /// </summary>
        IBindingFactory MultipleBind(Type[] types, BindingType[] bindingTypes);

        #endregion
    }
}