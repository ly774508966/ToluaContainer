﻿/*
 * Copyright 2016 Sun Ning（Joey1258）
 *
 *	Licensed under the Apache License, Version 2.0 (the "License");
 *	you may not use this file except in compliance with the License.
 *	You may obtain a copy of the License at
 *
 *		http://www.apache.org/licenses/LICENSE-2.0
 *
 *		Unless required by applicable law or agreed to in writing, software
 *		distributed under the License is distributed on an "AS IS" BASIS,
 *		WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *		See the License for the specific language governing permissions and
 *		limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace uMVVMCS.DIContainer
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