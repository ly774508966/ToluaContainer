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
using System;

namespace LuaContainer.Container
{
    [RequireComponent(typeof(ContextRoot))]
    public class SceneInjector : MonoBehaviour
    {
        private void Awake()
        {
            var contextRoot = GetComponent<ContextRoot>();
            var baseType = (contextRoot.baseBehaviourTypeName == "UnityEngine.MonoBehaviour" ?
                typeof(MonoBehaviour) : TypeUtils.GetType(contextRoot.baseBehaviourTypeName));

            switch (contextRoot.injectionType)
            {
                case ContextRoot.MonoBehaviourInjectionType.Children:
                    {
                        InjectOnChildren(baseType);
                    }
                    break;

                case ContextRoot.MonoBehaviourInjectionType.BaseType:
                    {
                        InjectFromBaseType(baseType);
                    }
                    break;
            }
        }

        /// <summary>
        /// 对当前物体的所有子物体注入
        /// </summary>
        public void InjectOnChildren(Type baseType)
        {
            var sceneInjectorType = GetType();
            var components = GetComponent<Transform>().GetComponentsInChildren(baseType, true);
            foreach (var component in components)
            {
                // 如果组件是 ContextRoot 或者是自身则忽略
                var componentType = component.GetType();
                if (componentType == sceneInjectorType ||
                    TypeUtils.IsAssignable(typeof(ContextRoot), componentType)) continue;

                ((MonoBehaviour)component).Inject();
            }
        }

        /// <summary>
        /// 为所有 MonoBehaviour 注入指定类型
        /// </summary>
        public void InjectFromBaseType(Type baseType)
        {
            var components = (MonoBehaviour[])Resources.FindObjectsOfTypeAll(baseType);

            for (var index = 0; index < components.Length; index++)
            {
                components[index].Inject();
            }
        }
    }
}