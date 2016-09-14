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

namespace LuaContainer.Container
{
    public interface IContextRoot
    {
        /// <summary>
        /// 将 container添加到 containers List，并默认 destroyOnLoad 为真
        /// </summary>
        IInjectionContainer AddContainer<T>() where T : IInjectionContainer, new();

        /// <summary>
        /// 将 container添加到 containers List，并默认 destroyOnLoad 为真
        /// </summary>
        IInjectionContainer AddContainer(IInjectionContainer container);

        /// <summary>
        /// 将 container添加到 containers List，并设置其 destroyOnLoad 属性
        /// </summary>
        IInjectionContainer AddContainer(IInjectionContainer container, bool destroyOnLoad);

        /// <summary>
        /// Dispose 指定 id 的容器
        /// </summary>
        void Dispose(object id);

        /// <summary>
        /// 设置容器
        /// </summary>
        void SetupContainers();

        /// <summary>
        /// 初始化
        /// </summary>
        void Init();
    }
}