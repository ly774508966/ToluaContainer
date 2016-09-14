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

using LuaContainer.Container;

namespace LuaContainer
{
    public class CommanderContainerAOT : IContainerAOT
    {
        public void OnRegister(IInjectionContainer container)
        {
            // 绑定一个单例的 ICommandDispatcher binding
            container.BindSingleton<ICommandDispatcher>().To<CommandDispatcher>();
            // 实例化 binding value，所有命令都通过该实例化后的 value 传播
            var dispatcher = (CommandDispatcher)container.Resolve<ICommandDispatcher>();
            // 将实例化后的 binding value 作为值绑定一条 ICommandPool 类型的 binding
            // 此时 container 中将有两条 binding，都为单例类型，且值都为 dispatcher，只有类型不同
            container.Bind<ICommandPool>().To(dispatcher);
        }

        public void OnUnregister(IInjectionContainer container)
        {
            // Unbind ICommandDispatcher 类型和 ICommandPool 类型 binding（清除 dispatcher)
            container.UnbindType<ICommandDispatcher>();
            container.UnbindType<ICommandPool>();

            // 清除所有 commands
            container.UnbindType<ICommand>();
        }
    }
}