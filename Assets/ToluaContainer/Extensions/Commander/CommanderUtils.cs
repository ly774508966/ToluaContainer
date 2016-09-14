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
    public static class CommanderUtils
    {
        /// <summary>
        /// 获取运行状态的 command 类型数组
        /// </summary>
        public static Type[] GetAvailableCommands()
        {
            var types = new List<Type>();

            // 获取当前线程所在域中活动的程序集
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                var assemly = assemblies[i];

                // 如果是 u3d 或系统类型就直接进入下次循环
                if (assemly.FullName.StartsWith("Unity") ||
                    assemly.FullName.StartsWith("Boo") ||
                    assemly.FullName.StartsWith("Mono") ||
                    assemly.FullName.StartsWith("System") ||
                    assemly.FullName.StartsWith("mscorlib"))
                {
                    continue;
                }

                var allTypes = assemblies[i].GetTypes();
                for (int n = 0; n < allTypes.Length; n++)
                {
                    var type = allTypes[n];

                    // 如果命名空间不是 ToluaContainer
                    if (type.Namespace != "ToluaContainer" &&
                        type.IsClass &&
                        TypeUtils.IsAssignable(typeof(ICommand), type))
                    {
                        types.Add(type);
                    }
                }
            }

            return types.ToArray();
        }

        /// <summary>
        /// 发送 command.
        /// </summary>
        public static void DispatchCommand(Type type, params object[] parameters)
        {
            var found = false;

            // 遍历 ContextRoot 下所有的容器
            var containers = ContextRoot.containers;
            for (int i = 0; i < containers.Count; i++)
            {
                // 如果容器中含有 ICommandDispatcher binding，且含有指定类型的 binding，就发送 command 并返回真
                var commandDispatches = containers[i].GetTypes<ICommandDispatcher>();
                if (commandDispatches != null && commandDispatches.Count != 0)
                {
                    var dispatcher = containers[i].GetCommandDispatcher();

                    var bindings = containers[i].GetTypes(type);
                    if (bindings != null && bindings.Count != 0)
                    {
                        found = true;
                        dispatcher.Dispatch(type, parameters);
                        break;
                    }
                }
            }

            if (!found)
            {
                throw new CommandException(string.Format(CommandException.NO_COMMAND_FOR_TYPE, type));
            }
        }

        /// <summary>
        /// 返回命名空间对应的类型
        /// </summary>
        public static Dictionary<string, IList<string>> GetTypesAsString(Type[] types)
        {
            var typesList = new Dictionary<string, IList<string>>();
            IList<string> typeNames;

            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                var key = "-";
                
                // 设置命名空间名称为 key
                if (!string.IsNullOrEmpty(type.Namespace))
                {
                    key = type.Namespace;
                }

                // 如果命名空间已经存在，就将命名空间对应的值赋予 typeNames
                if (typesList.ContainsKey(key))
                {
                    typeNames = typesList[key];
                }
                // 否则先添加再赋予
                else
                {
                    typeNames = new List<string>();
                    typesList.Add(key, typeNames);
                }

                typeNames.Add(type.Name);
            }

            return typesList;
        }
    }
}