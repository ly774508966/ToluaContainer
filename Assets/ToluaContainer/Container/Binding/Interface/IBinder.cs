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

namespace ToluaContainer.Container
{
    public interface IBinder
    {
        #region AOT event

        event BindingAddedHandler beforeAddBinding;
        event BindingAddedHandler afterAddBinding;
        event BindingRemovedHandler beforeRemoveBinding;
        event BindingRemovedHandler afterRemoveBinding;

        #endregion

        #region Bind

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
        /// 返回一个指定 type 属性的新 Binding 实例，BindingType 属性为 FACTORY，值约束为 SINGLE
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

        #region GetBinding

        /// <summary>
        /// 根据类型获取储存容器中的所有同类型 Binding
        /// </summary>
        IList<IBinding> GetTypes<T>();

        /// <summary>
        /// 根据类型获取储存容器中的所有同类型 Binding
        /// </summary>
        IList<IBinding> GetTypes(Type type);

        /// <summary>
        /// 获取储存容器中所有指定 id 的 binding
        /// </summary>
        IList<IBinding> GetIds(object id);

        /// <summary>
        /// 获取 binder 中所有的 Binding
        /// </summary>
		IList<IBinding> GetAll();

        /// <summary>
        /// 返回储存容器中除自身以外所有 type 和值都相同的 binding
        /// </summary>
        IList<IBinding> GetSameNullId(IBinding binding);

        /// <summary>
        /// 根据类型和id获取储存容器中的 Binding
        /// </summary>
        IBinding GetBinding<T>(object id);

        /// <summary>
        /// 根据类型和id获取储存容器中的 Binding
        /// </summary>
        IBinding GetBinding(Type type, object id);

        #endregion

        #region Unbind

        /// <summary>
        /// 根据类型从所有容器中删除所有同类型 Binding
        /// </summary>
        void UnbindType<T>();

        /// <summary>
        /// 根据类型从所有容器中删除所有同类型 Binding
        /// </summary>
        void UnbindType(Type type);

        /// <summary>
        /// 根据 id 从所有容器中删除所有同类型 Binding
        /// </summary>
        void UnbindId(object id);

        /// <summary>
        /// 根据类型从所有容器中删除所有同类型 Binding
        /// </summary>
        void UnbindNullIdType<T>();

        /// <summary>
        /// 根据类型从所有容器中删除所有同类型 Binding
        /// </summary>
        void UnbindNullIdType(Type type);

        /// <summary>
        /// 根据类型和 id 从所有容器中删除 Binding
        /// </summary>
		void Unbind<T>(object id);

        /// <summary>
        /// 根据类型和 id 从所有容器中删除 Binding
        /// </summary>
		void Unbind(Type type, object id);

        /// <summary>
        /// 根据 binding 从所有容器中删除 Binding
        /// </summary>
        void Unbind(IBinding binding);

        #endregion

        /// <summary>
        /// 储存 binding
        /// </summary>
        void Storing(IBinding binding);
    }
}