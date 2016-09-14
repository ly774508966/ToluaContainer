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

namespace LuaContainer
{
    public interface ICommand
    {
        /// <summary>
        /// commandDispatcher
        /// </summary>
        ICommandDispatcher dispatcher { get; set; }

        /// <summary>
        /// command 是否在运行中
        /// </summary>
        bool running { get; set; }

        /// <summary>
        /// Indicates whether the command must be kept alive even after its execution.
        /// </summary>
        bool keepAlive { get; set; }

        /// <summary>
        /// 是否是单例 单例能提高性能，避免重复注入
        /// </summary>
        bool singleton { get; }

        /// <summary>
        /// command 对象池预加载数量，默认为1
        /// </summary>
        int preloadPoolSize { get; }

        /// <summary>
        /// command 对象池大小，默认为10
        /// </summary>
        int maxPoolSize { get; }

        /// <summary>
        /// command 的执行方法
        /// </summary>
        void Execute(params object[] parameters);

        /// <summary>
        /// 保留 command，执行后不释放,如要释放可在执行后调用 Release() 方法
        /// </summary>
        void Retain();

        /// <summary>
        /// 释放 command.
        /// </summary>
        void Release();
    }
}
