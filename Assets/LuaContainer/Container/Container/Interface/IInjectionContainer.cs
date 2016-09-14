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
    public interface IInjectionContainer : IBinder, IInjector, IDisposable
    {
        /// <summary>
        /// load 时是否摧毁容器
        /// </summary>
        bool destroyOnLoad { get; set; }

        /// <summary>
        /// 容器 id
        /// </summary>
		object id { get; }

        /// <summary>
        /// 反射信息缓存
        /// </summary>
        IReflectionCache cache { get; }

        /// <summary>
        /// 注册容器到 aots list
        /// </summary>
        IInjectionContainer RegisterAOT<T>() where T : IContainerAOT;

        /// <summary>
        /// 注册容器到 aots list 
        /// </summary>
        IInjectionContainer RegisterAOT(IContainerAOT extension);

        /// <summary>
        /// 将一个容器从 aots list 中移除 
        /// </summary>
        IInjectionContainer UnregisterAOT<T>() where T : IContainerAOT;

        /// <summary>
        /// 将一个容器从 aots list 中移除
        /// </summary>
        IInjectionContainer UnregisterAOT(IContainerAOT extension);
    }
}