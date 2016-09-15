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

namespace ToluaContainer.Examples.BindingGameObjects
{
    public class BindingGameObjectsRoot : ContextRoot
    {
        public override void SetupContainers()
        {
            // 创建容器
            AddContainer<InjectionContainer>()
                // 注册一个 UnityContainerAOT 到当前容器以供操作
                .RegisterAOT<UnityContainerAOT>()
                // 绑定 Transform 组建到 gameObject "Cube"
                .Bind<Transform>().ToGameObject("Cube")
                //Bind the "GameObjectRotator" component to a new ame object of the same name.
                //This component will then receive the reference above so it can rotate the cube.
                .Bind<RotateController>().ToGameObject()/**/;
        }

        public override void Init()
        {
            //Init the game.
        }
    }
}
