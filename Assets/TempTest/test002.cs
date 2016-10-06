using UnityEngine;
using ToluaContainer.Container;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class test002 : MonoBehaviour
{
    Text t;
    int num = 0;
    string url;
    public AssetBundleInfo abi;
    public AssetBundle abiab ;
    public enum LoadType
    {
        LoadFormFile,
        GetAsyncFromFile,
        GetCoroutineFromFile,
        LoadFromMemory_NW,
        GetAsyncFromMemory_NW,
        GetCoroutineFromMemory_NW,
        LoadFromMemory_LFCOD,
        GetAsyncFromMemory_LFCOD,
        GetCoroutineFromMemory_LFCOD,
    }
    public LoadType testType = new LoadType();
    // Use this for initialization
    void Start ()
    {
        t = GameObject.Find("Text1").GetComponent<Text>();
        abi = new AssetBundleInfo(
            Application.dataPath + "/StreamingAssets/05_assetbundle.unity3d");

    }
    void OnGUI()
    {
        LoadType[] lt = new LoadType[]
        {
            LoadType.LoadFormFile,
            LoadType.GetAsyncFromFile,
            LoadType.GetCoroutineFromFile,
            LoadType.LoadFromMemory_NW,
            //LoadType.GetAsyncFromMemory_NW,
            //LoadType.GetCoroutineFromMemory_NW,
            LoadType.LoadFromMemory_LFCOD,
            //LoadType.GetAsyncFromMemory_LFCOD,
            LoadType.GetCoroutineFromMemory_LFCOD,
        };

        if (GUI.Button(new Rect(10, 70, 50, 30), "Test"))
        {
            abl(lt[0]);
            num++;
            if (num >= lt.Length) num = 0;
        }

        if (GUI.Button(new Rect(70, 70, 50, 30), "Test2"))
        {
            pf();
        }
        /*
        if (GUI.Button(new Rect(140, 70, 50, 30), "Test3"))
        {
            pf2();
        }*/
    }

    void abl (LoadType type)
    {
        List<object> objs = new List<object>();
        switch (type)
        {
            case LoadType.LoadFormFile:
                if(abi.isLoaded) abi.Dispose(true);
                print(abi.asetBundle.mainAsset);
                objs = new List<object>(abi.asetBundle.LoadAllAssets());
                print("LoadFormFile:  " + objs[0].ToString());
                break;

            case LoadType.GetAsyncFromFile:
                if (abi.isLoaded) abi.Dispose(true);
                StartCoroutine(abi.GetAsyncFromFile(o => print("GetAsyncFromFile:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString()), actionFloat));
                break;

            case LoadType.GetCoroutineFromFile:
                if (abi.isLoaded) abi.Dispose(true);
                StartCoroutine(abi.GetCoroutineFromFile(o => print("GetCoroutineFromFile:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString())));
                break;

            case LoadType.LoadFromMemory_NW:
                if (abi.isLoaded) abi.Dispose(true);
                abi.LoadFromMemory_NW();
                objs = new List<object>(abi.asetBundle.LoadAllAssets());
                print("LoadFromMemory_NW:  " + objs[0].ToString());
                break;

            /*case LoadType.GetAsyncFromMemory_NW:
                print(111);
                if (abi.isLoaded) abi.Dispose(true);
                StartCoroutine(abi.GetAsyncFromMemory_NW(o => print("GetAsyncFromMemory_NW:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString()), actionFloat));
                break;

            case LoadType.GetCoroutineFromMemory_NW:
                if (abi.isLoaded) abi.Dispose(true);
                StartCoroutine(abi.GetCoroutineFromMemory_NW(o => print("GetCoroutineFromMemory_NW:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString())));
                break;*/
            case LoadType.LoadFromMemory_LFCOD:
                if (abi.isLoaded) abi.Dispose(true);
                abi.url = "file://" + abi.url;
                abi.LoadFromCacheOrDownload();
                objs = new List<object>(abi.asetBundle.LoadAllAssets());
                print("LoadFromMemory_LFCOD:  " + objs[0].ToString());
                break;
            /*case LoadType.GetAsyncFromMemory_LFCOD:
                if (abi.isLoaded) abi.Dispose(true);
                abi.url = "file://" + abi.url;
                StartCoroutine(abi.GetAsyncFromMemory_LFCOD(o => print("GetAsyncFromMemory_LFCOD:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString()), actionFloat));
                break;*/
            case LoadType.GetCoroutineFromMemory_LFCOD:
                if (abi.isLoaded) abi.Dispose(true);
                abi.url = "file://" + abi.url;
                StartCoroutine(abi.LoadCoroutineFromCacheOrDownload(o => print("GetCoroutineFromMemory_LFCOD:  " + ((AssetBundle)o).LoadAllAssets()[0].ToString())));
                break;
        }
    }

    void pf()
    {
        PrefabInfo pfi = new PrefabInfo("04_Prefabs/Cube",null);
        StartCoroutine(pfi.GetAsyncObject(actionObj, actionFloatTot));
    }

    void pf2()
    {
        PrefabInfo pfi = new PrefabInfo("04_Prefabs/Cube", null);
        object o = pfi.prefab;
        Debug.Log(pfi.isLoaded);
    }

    void actionObj(object o)
    {
        Debug.Log(o);
    }

    void actionFloat(float f)
    {
        Debug.Log(f);
    }
    void actionFloatTot(float f)
    {
        Debug.Log(f);
        t.text = f.ToString();
    }
}
