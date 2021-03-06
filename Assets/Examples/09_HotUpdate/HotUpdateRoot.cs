﻿using UnityEngine;
using ToluaContainer.Container;

namespace ToluaContainer.Examples.HotUpdate
{
    public class HotUpdateRoot : ContextRoot
    {
        public override void SetupContainers()
        {
            // 添加容器
            AddContainer<InjectionContainer>()
                // 注册 AOT 容器扩展
                .RegisterAOT<UnityContainerAOT>()
                // 绑定 UpdateAssetTool 到场景中
                .Bind<UpdateAssetTool>().ToGameObject("Main Camera")
                // 绑定 AssetBundleInfo 
                .BindSingleton<AssetBundleInfo>().ToAssetBundleFromCacheOrDownload("file://" + Application.dataPath + "/StreamingAssets/lua/lua_02_bindinggameobjects.unity3d.unity3d")
                // 绑定 Transform 组件到 gameObject "Cube"
                .Bind<Transform>().ToGameObject("Cube")
                // 绑定 GameObjectRotator 脚本到一个与脚本同名的新的空物体来控制 Cube 转动
                .Bind<RotateController>().ToGameObject("Main Camera");
        }

        public override void Init() { }
    }
}
