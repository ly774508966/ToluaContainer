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

namespace ToluaContainer
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// 发送一个指定类型的 command
        /// </summary>
        void Dispatch<T>(params object[] parameters) where T : ICommand;

        /// <summary>
        /// 发送一个指定类型的 command
        /// </summary>
        void Dispatch(Type type, params object[] parameters);

        /// <summary>
        /// 通过 EventContainerAOT.eventBehaviour 在等待指定秒后发送一个 command 
        /// </summary>
        void InvokeDispatch<T>(float time, params object[] parameters) where T : ICommand;

        /// <summary>
        /// 通过 EventContainerAOT.eventBehaviour 在等待指定秒后发送一个 command 
        /// </summary>
        void InvokeDispatch(Type type, float time, params object[] parameters);

        /// <summary>
        /// 释放 command
        /// </summary>
        void Release(ICommand command);

        /// <summary>
        /// 释放所有 command
        /// </summary>
        void ReleaseAll();

        /// <summary>
        /// 释放指定类型的所有 command
        /// </summary>
        void ReleaseAll<T>() where T : ICommand;

        /// <summary>
        /// 释放指定类型的所有 command
        /// </summary>
        void ReleaseAll(Type type);

        /// <summary>
        /// 返回 commands 字典中是否含有指定类型的 command
        /// </summary>
        bool ContainsCommands<T>() where T : ICommand;

        /// <summary>
        /// 返回 commands 字典中是否含有指定类型的 command
        /// </summary>
        bool ContainsCommands(Type type);

        /// <summary>
        /// 返回字典中的所有 key（类型）
        /// </summary>
        Type[] GetAllRegistrations();

        /// <summary>
        /// 将容器中所有 ICommand 对象实例化并缓存到字典
        /// </summary>
        void Pool();
    }
}

