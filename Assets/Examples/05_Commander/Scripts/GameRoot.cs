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

using UnityEngine;
using ToluaContainer.Container;

namespace ToluaContainer.Examples.Commander
{
	public class GameRoot : ContextRoot
    {
		protected ICommandDispatcher dispatcher;

		public override void SetupContainers()
        {
			// 添加容器
			var container = AddContainer<InjectionContainer>();

			container
				// 注册所需的容器扩展
				.RegisterAOT<CommanderContainerAOT>()
				.RegisterAOT<EventContainerAOT>()
				.RegisterAOT<UnityContainerAOT>()
                // 注册 "uMVVMCS.Examples.Commander" 命名空间下的所有 Command
                .RegisterCommands("uMVVMCS.Examples.Commander")
				// 绑定 prefab
				.Bind<Transform>().ToPrefab("05_Commander/Prism");
		
			// 获取 commandDispatcher 以便在 Init() 方法中调用
			dispatcher = container.GetCommandDispatcher();
		}
		
		public override void Init()
        {
			dispatcher.Dispatch<SpawnGameObjectCommand>();
		}
	}
}