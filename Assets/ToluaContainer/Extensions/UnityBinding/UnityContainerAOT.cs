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
using UnityEngine;
using Utils;

namespace ToluaContainer.Container
{
    public class UnityContainerAOT : IContainerAOT
    {
        #region IContainerExtension implementation 

        public void OnRegister(IInjectionContainer container)
        {
            container.beforeAddBinding += OnBeforeAddBinding;
            container.beforeDefaultInstantiate += this.OnBindingEvaluation;
        }

        public void OnUnregister(IInjectionContainer container)
        {
            container.beforeAddBinding -= this.OnBeforeAddBinding;
            container.beforeDefaultInstantiate -= this.OnBindingEvaluation;
        }

        #endregion

        /// <summary>
        /// 如果当前 binding 的值是类型且是 MonoBehaviour 派生类就抛出异常
        /// </summary>
        protected void OnBeforeAddBinding(IBinder source, ref IBinding binding)
        {
            if (binding.value is Type &&
                TypeUtils.IsAssignable(typeof(MonoBehaviour), binding.value as Type))
            {
                throw new InjectionSystemException(InjectionSystemException.CANNOT_RESOLVE_MONOBEHAVIOUR);
            }
        }

        /// <summary>
        /// 为 ADDRESS 类型 binding 返回实例化并加载好组件的 gameObject(在 Injector 类的 
        /// ResolveBinding 方法中触发)
        /// </summary>
        protected object OnBindingEvaluation(
            IInjector source, 
            ref IBinding binding)
        {
            if (binding.value is PrefabInfo &&
                binding.bindingType == BindingType.ADDRESS)
            {
                var prefabInfo = (PrefabInfo)binding.value;
                var gameObject = (GameObject)MonoBehaviour.Instantiate(prefabInfo.prefab);

                if (prefabInfo.type.Equals(typeof(GameObject)))
                {
                    return gameObject;
                }
                else
                {
                    var component = gameObject.GetComponent(prefabInfo.type);

                    if (component == null)
                    {
                        component = gameObject.AddComponent(prefabInfo.type);
                    }

                    return component;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
