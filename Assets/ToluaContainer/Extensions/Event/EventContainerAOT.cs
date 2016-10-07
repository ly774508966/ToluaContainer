﻿using UnityEngine;
using System;
using System.Collections.Generic;
using ToluaContainer.Container;

namespace ToluaContainer
{
    public class EventContainerAOT : IContainerAOT
    {
        /// <summary>
        /// 可释放对象 list
        /// </summary>
        public static List<IDisposable> disposable = new List<IDisposable>();

        /// <summary>
        /// 每帧更新对象 list
        /// </summary>
        public static List<IUpdatable> updateable = new List<IUpdatable>();

        /// <summary>
        /// event
        /// </summary>
        public static EventBehaviour eventBehaviour;

        #region constructor

        public EventContainerAOT()
        {
            //在游戏中创建一个游戏物体来挂载 EventBehaviour 组件
            var gameObject = new GameObject("EventBehaviour");
            eventBehaviour = gameObject.AddComponent<EventBehaviour>();
        }

        #endregion

        #region functions

        /// <summary>
        /// 注册容器
        /// </summary>
        public void OnRegister(IInjectionContainer container)
        {
            // 将容器添加到 IDisposable list.
            disposable.Add(container);

            // 如果容器中含有 ICommandDispatcher 类型的 binding，且它实现了 IDisposable 接口
            // 就实例化 ICommandDispatcher 类型并将其也添加到 IDisposable list
            var commandDispatches = container.GetTypes<ICommandDispatcher>();
            if (commandDispatches != null && commandDispatches.Count != 0)
            {
                var dispatcher = container.Resolve<ICommandDispatcher>();
                if (dispatcher is IDisposable)
                {
                    disposable.Add((IDisposable)dispatcher);
                }
            }

            // 添加 AOT 委托
            container.afterAddBinding += this.OnAfterAddBinding;
            container.afterInstantiate += this.OnBindingResolution;
        }

        /// <summary>
        /// 注销容器
        /// </summary>
        public void OnUnregister(IInjectionContainer container)
        {
            // 取消 AOT 委托
            container.afterAddBinding -= this.OnAfterAddBinding;
            container.afterInstantiate -= this.OnBindingResolution;

            // 释放 list 并销毁组件
            disposable.Clear();
            updateable.Clear();
            MonoBehaviour.Destroy(eventBehaviour);
        }

        /// <summary>
        /// 处理 binding 添加之后的工作，用于对 SINGLETON 和 MULTITON 类型的 binding 的值
        /// 分别根据其自身类型添加到对应的 list 中去(IDisposable list 或 IUpdatable list)
        /// </summary>
        protected void OnAfterAddBinding(IBinder source, ref IBinding binding)
        {
            if (binding.bindingType == BindingType.SINGLETON ||
                binding.bindingType == BindingType.MULTITON)
            {
                int length = binding.valueList.Count;
                for (int i = 0; i < length; i++)
                {
                    // 如果是 ICommand 对象就直接退出
                    if (binding.valueList[i] is ICommand) { return; }

                    // 如果是 IDisposable 对象且 disposable list 中没有该对象，就进行添加
                    if (binding.valueList[i] is IDisposable && 
                        !disposable.Contains((IDisposable)binding.valueList[i]))
                    {
                        disposable.Add((IDisposable)binding.valueList[i]);
                    }

                    // 如果是 IUpdatable 对象且 updateable list 中没有该对象，就进行添加
                    if (binding.valueList[i] is IUpdatable && 
                        !updateable.Contains((IUpdatable)binding.valueList[i]))
                    {
                        updateable.Add((IUpdatable)binding.valueList[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 在 ResolveBinding 方法的最后，获取到实例之后、返回实例之前根据 BindingType 以及实例的类型
        /// 对实例进行分类，分别添加到相应的 list 中(IDisposable list 或 IUpdatable list)
        /// </summary>
        protected void OnBindingResolution(IInjector source, ref IBinding binding, ref object instance)
        {
            // 如果是 SINGLETON 或 MULTITON 类型 binding，或是 ICommand 对象就直接退出
            if (binding.bindingType == BindingType.SINGLETON ||
                binding.bindingType == BindingType.MULTITON ||
                instance is ICommand)
            { return; }

            // 根据各自的类型添加到相应的 list
            if (instance is IDisposable && !disposable.Contains((IDisposable)instance))
            {
                disposable.Add((IDisposable)instance);
            }
            if (instance is IUpdatable && !updateable.Contains((IUpdatable)instance))
            {
                updateable.Add((IUpdatable)instance);
            }
        }

        #endregion
    }
}
