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

namespace ToluaContainer.Examples.UsingConditions
{
    public class UsingConditionsRoot : ContextRoot
    {
        public override void SetupContainers()
        {
            //Create the container.
            AddContainer<InjectionContainer>()
                //Register any extensions the container may use.
                .RegisterAOT<UnityContainerAOT>()
                //Bind a Transform component to the two cubes on the scene, using a "As"
                //condition to define their identifiers.
                .Bind<Transform>().ToGameObject("LeftCube").As("LeftCube")
                .Bind<Transform>().ToGameObject("RightCube").As("RightCube")
                //Bind the "GameObjectRotator" component to a new game object of the same name.
                //This component will then receive the reference to the "LeftCube", making only
                //this cube rotate.
                .Bind<RotateController>().ToGameObject();
        }

        public override void Init()
        {
            //Init the game.
        }
    }
}
