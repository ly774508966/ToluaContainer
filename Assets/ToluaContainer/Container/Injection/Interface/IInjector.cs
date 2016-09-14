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

namespace ToluaContainer.Container
{
    public interface IInjector
    {
        /// <summary>
        /// binding 实例化模式
        /// </summary>
        ResolutionMode resolutionMode { get; set; }

        #region Injector AOT Event

        event TypeResolutionHandler beforeResolve;
        event TypeResolutionHandler afterResolve;
        event BindingEvaluationHandler beforeDefaultInstantiate;
        event BindingResolutionHandler afterInstantiate;
        event InstanceInjectionHandler beforeInject;
        event InstanceInjectionHandler afterInject;

        #endregion

        #region Resolve

        /// <summary>
        /// 为指定类型的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例
        /// 返回结果可能是 object 或者数组，使用时应加以判断
        /// </summary>
        T Resolve<T>();

        /// <summary>
        /// 为指定类型、id 的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例
        /// </summary>
        T Resolve<T>(object identifier);

        /// <summary>
        /// 为指定类型的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例
        /// 返回结果可能是 object 或者数组，使用时应加以判断
        /// </summary>
        object Resolve(Type type);

        /// <summary>
        /// 为指定的多个类型的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例
        /// 返回结果可能是 object 或者数组，使用时应加以判断
        /// </summary>
        T[] ResolveAll<T>();

        /// <summary>
        /// 为指定的多个类型的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例
        /// 返回结果可能是 object 或者数组，使用时应加以判断
        /// </summary>
        object[] ResolveAll(Type type);

        /// <summary>
        /// 为指定类型和 id 的所有 binding 执行相应的实例化和注入操作，并返回所有新生成的实例数组
        /// </summary>
        T[] ResolveSpecified<T>(object identifier);

        #endregion

        #region Inject

        /// <summary>
        /// 为实例注入依赖
        /// </summary>
        T Inject<T>(T instance) where T : class;

        /// <summary>
        /// 为实例注入依赖
        /// </summary>
        object Inject(object instance);

        #endregion
    }
}