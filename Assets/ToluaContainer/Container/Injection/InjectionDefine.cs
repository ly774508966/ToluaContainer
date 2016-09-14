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
    /// <summary>
    /// Resolve 方法的 AOT 委托，在 Resolve 方法实际操作开始之前和完成之后根据类型参数进行前置/后置操作
    /// （修改传入的委托参数 resolutionInstance ）（Resolution : 正式决定，决议）
    /// </summary>
    public delegate bool TypeResolutionHandler(IInjector source, Type type, InjectionInto member, object parentInstance, object id, ref object resolutionInstance);

    /// <summary>
    /// ResolveBinding 方法 AOT 委托，在方法内部对 id 进行完过滤之后，根据 binding 的 bindingType
    /// 对其进行相应的实例化与注入操作的之前进行的前置操作（修改传入的委托参数 binding）
    /// </summary>
    public delegate object BindingEvaluationHandler(IInjector source, ref IBinding binding);

    /// <summary>
    /// ResolveBinding 方法相关委托，在方法内部根据 binding 的 bindingType
    /// 对其进行相应的实例化与注入操作之后进行后置操作（修改传入的委托参数 instance）
    /// </summary>
    public delegate void BindingResolutionHandler(IInjector source, ref IBinding binding, ref object instance);

    /// <summary>
    /// Inject 方法相关委托，在 Inject 方法实际操作开始之前和完成之后根据类型参数进行前置/后置操作
    /// （修改传入的委托参数 instance）
    /// </summary>
    public delegate void InstanceInjectionHandler(IInjector source, ref object instance, ReflectionInfo reflectInfo);

    /// <summary>
    /// 注入方式
    /// </summary>
	public enum InjectionInto
    {
        None,
        Constructor,
        Field,
        Property
    }

    /// <summary>
    /// binding 实例化模式
    /// </summary>
    public enum ResolutionMode
    {
        /// <summary>
        /// 不论有没有绑定到容器，尝试对所有的类型执行 resolve（默认模式）
        /// </summary>
        ALWAYS_RESOLVE,
        /// <summary>
        /// 只对绑定到容器的类型进行 resolves，尝试对没有绑定的类型执行 resolves 将返回空
        /// </summary>
        BOUND_ONLY
    }
}