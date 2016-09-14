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
using LuaContainer.Container;

namespace LuaContainer
{
    public static class CommanderContainerExtension
    {
        /// <summary>
        /// 在容器中实例化 ICommandDispatcher 对象
        /// </summary>
        public static ICommandDispatcher GetCommandDispatcher(this IInjectionContainer container)
        {
            return container.Resolve<ICommandDispatcher>();
        }

        /// <summary>
        /// 在容器中绑定一个以指定类型为值，ICommand 为 type 的 binding
        /// </summary>
        public static IInjectionContainer RegisterCommand<T>(this IInjectionContainer container) where T : ICommand, new()
        {
            container.RegisterCommand(typeof(T));

            return container;
        }

        /// <summary>
        /// 在容器中绑定一个以指定类型为值，ICommand 为 type 的 binding.
        /// 执行后,可调用 PoolCommands 方法来储存所有 command 到对象池.
        /// RegisterCommands 方法用于使 command 准备进入对象池.
        /// </summary>
        public static IInjectionContainer RegisterCommand(this IInjectionContainer container, Type type)
        {
            if (!type.IsClass && type.IsAssignableFrom(typeof(ICommand)))
            {
                throw new CommandException(CommandException.TYPE_NOT_A_COMMAND);
            }

            container.Bind<ICommand>().To(type);

            return container;
        }

        /// <summary>
        /// 在容器中绑定指定命名空间中的所有类型为值，ICommand 为 type 的多个 binding，再将其存入对象池
        /// </summary>
        public static IInjectionContainer RegisterCommands(this IInjectionContainer container, string namespaceName)
        {
            container.RegisterCommands(namespaceName, true);

            return container;
        }

        /// <summary>
        /// 在容器中绑定指定命名空间中的所有类型为值，ICommand 为 type 的多个 binding，再将其存入对象池
        /// </summary>
        public static IInjectionContainer RegisterCommands(this IInjectionContainer container,
            string namespaceName,
            bool includeChildren)
        {
            // 获取指定命名空间下实现了 ICommand 的类型
            var commands = TypeUtils.GetAssignableTypes(typeof(ICommand), namespaceName, includeChildren);

            // 如果不为空，就讲其类型作为值逐一绑定一条 ICommand 类型的 TEMP binding
            if (commands.Length > 0)
            {
                for (var i = 0; i < commands.Length; i++)
                {
                    var commandType = commands[i];
                    if (!commandType.IsAbstract)
                    {
                        container.Bind<ICommand>().To(commandType);
                    }
                }
                // 为容器实例化一个 ICommandPool（CommandDispatcher）实例，并将容器内的所有 commands
                // 实例化、注入并存入对象池（储存为 List<ICommand> 并根据类型添加到 CommandDispatcher 
                // 的字典中）
                PoolCommands(container);
            }

            return container;
        }

        /// <summary>
        /// 为容器实例化一个 ICommandPool（CommandDispatcher）实例，并将容器内的所有 commands 实例化、
        /// 注入并存入对象池（储存为 List<ICommand> 并根据类型添加到 CommandDispatcher 的字典中）
        /// </summary>
        public static IInjectionContainer PoolCommands(this IInjectionContainer container)
        {
            var commandPool = container.Resolve<ICommandPool>();
            commandPool.Pool();

            return container;
        }
    }
}