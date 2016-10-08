using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ToluaContainer;
using ToluaContainer.Container;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        /*Debug.Log(Application.dataPath + "/StreamingAssets/05_assetbundle.unity3d");
        //Arrange 
        IBinder binder = new Binder();
        //Act
        IBinding binding = binder.Bind<AssetBundleInfo>().ToAssetBundleFromFile(Application.dataPath + "/Editor/Tests/Prefab_AssetBundleTest/cube.prefab.unity3d");*/

        //Arrange 
        IBinder binder = new Binder();
        //Act
        IBinding binding = binder.Bind<AssetBundleInfo>().ToAssetBundleAsyncFromFile(Application.dataPath + "/Editor/Tests/Prefab_AssetBundleTest/cube.prefab.unity3d");


        Debug.Log(((AssetBundleInfo)binding.value).asetBundle != null);
    }
}
public class someClass : IInjectionFactory
{
    public int id;
    virtual public object Create(InjectionInfo context) { return this; }
}

public class someClass_b : someClass
{
    override public object Create(InjectionInfo context) { return 1; }
}

public class someClass_c : someClass
{
    [Inject]
    public someClass_b b;

    override public object Create(InjectionInfo context) { return 2; }
}

public class TestCommand1 : Command
{
    public int num = 0;

    public override void Execute(params object[] parameters)
    {
        num++;
        ((someClass)parameters[0]).id = num;
    }
}
