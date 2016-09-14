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
    /// <summary>
    /// 条件判断委托
    /// </summary>
    public delegate bool Condition(InjectionInfo context);

    /// <summary>
    /// 添加 Binding 的 aots 委托
    /// </summary>
    public delegate void BindingAddedHandler(IBinder source, ref IBinding binding);

    /// <summary>
    /// 移除 Binding 的 aots 委托
    /// </summary>
    public delegate void BindingRemovedHandler(IBinder source, IList<IBinding> bindings);

    /// <summary>
    /// 约束类型
    /// </summary>
	public enum ConstraintType
    {
        /// <summary>
        /// 约束Segment只能携带一个值
        /// </summary>
        SINGLE,
        /// <summary>
        /// 约束Segment可以携带多个值
        /// </summary>
        MULTIPLE,
    }

    /// <summary>
    /// 注入类型
    /// </summary>
    public enum BindingType
    {
        /// <summary> 
        /// binging 的 value 属性为 Type，或 prefab 路径信息类等，用于储存将要多次使用或创建的类型或路径
        /// 实例化的结果不会储存并覆盖到 value
        /// </summary>
        ADDRESS,
        /// <summary> 
        /// bingding 的 value 为类型或实例，如果是类型，Inject 系统会自动为其创建实例并为实例执行注入
        /// 实例将会保存并覆盖到 bingding 的 value，以保证每次获取的都是同一个实例。
        /// </summary>
        SINGLETON,
        /// <summary> 
        /// bingding 的 value 为类型或实例，如果是类型，Inject 系统会自动为其创建实例并为实例执行注入
        /// bingding 的 value 将储存复数个值，注入的实例将会保存并覆盖到指定元素
        /// </summary>
        MULTITON,
        /// <summary> 
        /// bingding 的 value 为工厂类型或者实例，如果是类型，Inject 系统会自动创建实例并注入
        /// 实例将会保存并覆盖到 bingding 的 value，以保证每次获取的都是同一个实例
        /// 而一旦工厂类被实例化，之后就都是通过工厂类的 Create 方法的具体实现来创建实例 
        /// </summary>
        FACTORY
    }
}
