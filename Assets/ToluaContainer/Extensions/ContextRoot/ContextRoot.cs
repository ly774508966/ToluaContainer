﻿/*
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
using System.Collections.Generic;

namespace ToluaContainer.Container
{
    public abstract class ContextRoot : MonoBehaviour, IContextRoot
    {
        #region Inner Class

        /// <summary>
        /// MonoBehaviour 注入类型
        /// </summary>
        [Serializable]
        public enum MonoBehaviourInjectionType
        {
            // 手动
            Manual,
            // 子类
            Children,
            // 基类
            BaseType
        }

        /// <summary>
        /// Container 空 ID
        /// </summary>
        public enum ContainerNullId { Null }

        #endregion

        #region property

        /// <summary>
        /// Tooltip 特性将会使鼠标悬停时浮现提示文字
        /// MonoBehaviour 注入类型
        /// </summary>
        [Tooltip("Type of injection on MonoBehaviours."), HideInInspector]
        public MonoBehaviourInjectionType injectionType;

        /// <summary>
        /// 在场景中进入注入的 behaviour 基类名称
        /// </summary>
        [Tooltip("Name of the base behaviour type to perform scene injection."), HideInInspector]
        public string baseBehaviourTypeName;

        /// <summary>
        /// 内部容器数据类 list
        /// </summary>
        public static List<IInjectionContainer> containers { get; set; }

        /// <summary>
        /// 容器仓库 (储存 containers List 中 id 不为空的binding，并用 type 和 id 索引)
        /// </summary>
        public static Dictionary<object, List<IInjectionContainer>> containersDic { get; protected set; }

        #endregion

        #region functions

        #region Mono functions

        virtual protected void Awake()
        {
            containersDic = new Dictionary<object, List<IInjectionContainer>>();
            // 如果容器数据list为空，设置它的长度为1
            if (containers == null)
            {
                containers = new List<IInjectionContainer>(1);
            }

            SetupContainers();

            // 缓存所有容器中所有 binding 的 value 属性所储存的类型信息
            CacheBindings();
        }

        virtual protected void Start()
        {
            // SceneInjector 应该比其它任何 Start 方法都早执行
            gameObject.AddComponent<SceneInjector>();

            Init();
        }

        virtual protected void OnDestroy()
        {
            // 释放 containers List 中所有 destroyOnLoad 属性为真的容器中的 binder 和缓存
            // 并将其从 containers List 中移除
            for (var i = 0; i < containers.Count; i++)
            {
                if (!containers[i].destroyOnLoad) continue;

                containers[i].Dispose();
                containers.Remove(containers[i]);
                i--;
            }
        }

        #endregion

        #region AddContainer

        /// <summary>
        /// 将 container添加到 containers List，并默认 destroyOnLoad 为真
        /// </summary>
        virtual public IInjectionContainer AddContainer<T>() where T : IInjectionContainer, new()
        {
            var container = Activator.CreateInstance<T>();
            return AddContainer(container, true);
        }

        /// <summary>
        /// 将 container添加到 containers List，并默认 destroyOnLoad 为真
        /// </summary>
        virtual public IInjectionContainer AddContainer(IInjectionContainer container)
        {
            return AddContainer(container, true);
        }

        /// <summary>
        /// 将 container添加到 containers List，并设置其 destroyOnLoad 属性
        /// </summary>
        virtual public IInjectionContainer AddContainer(IInjectionContainer container, bool destroyOnLoad)
        {
            container.destroyOnLoad = destroyOnLoad;
            containers.Add(container);
            ContainersStoring(container);

            return container;
        }

        #endregion

        #region DisposeContainer

        /// <summary>
        /// Dispose 指定 id 的容器
        /// </summary>
        virtual public void Dispose(object id)
        {
            containers.Remove(containersDic[id][0]);
            containersDic[id][0].Dispose();
            containersDic[id][0] = null;
            containersDic.Remove(containersDic[id]);
        }

        #endregion

        #region Setup & Init

        /// <summary>
        /// 设置容器
        /// </summary>
        public abstract void SetupContainers();

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        #endregion

        #region private functions

        /// <summary>
        /// 缓存所有容器中所有 binding 的 value 属性所储存的类型信息
        /// </summary>
        private void CacheBindings()
        {
            for (var i = 0; i < containers.Count; i++)
            {
                var container = containers[i];
                container.cache.CacheFromBinder(container);
            }
        }

        /// <summary>
        /// 整理储存所有容器
        /// </summary>
        virtual protected void ContainersStoring(IInjectionContainer container)
        {
            if (container.id != null)
            {
                print(container == null);
                if (!containersDic.ContainsKey(container.id))
                {
                    containersDic[container.id] = new List<IInjectionContainer>(1);
                    containersDic[container.id].Add(container);
                }
                else
                {
                    throw new InjectionSystemException(InjectionSystemException.SAME_OBJECT);
                }
            }
            else
            {
                if(!containersDic.ContainsKey(ContainerNullId.Null))
                {
                    containersDic[ContainerNullId.Null] = new List<IInjectionContainer>();
                }
                containersDic[ContainerNullId.Null].Add(container);
            }
        }

        #endregion

        #endregion
    }
}