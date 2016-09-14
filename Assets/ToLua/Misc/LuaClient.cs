/*
Copyright (c) 2015-2016 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using UnityEngine;
using System.Collections.Generic;
using LuaInterface;
using System.Collections;
using System.IO;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// 集成了 luaState、LuaLooper 等功能于一身的 MonoBehaviour 类
/// </summary>
public class LuaClient : MonoBehaviour
{
    /// <summary>
    /// LuaClient 实例
    /// </summary>
    public static LuaClient Instance
    {
        get;
        protected set;
    }

    /// <summary>
    /// LuaState
    /// </summary>
    protected LuaState luaState = null;

    /// <summary>
    /// LuaLooper
    /// </summary>
    protected LuaLooper loop = null;

    /// <summary>
    /// 读取关卡方法信息类
    /// </summary>
    protected LuaFunction levelLoaded = null;

    /// <summary>
    /// 
    /// </summary>
    protected bool openLuaSocket = false;

    /// <summary>
    /// 
    /// </summary>
    protected bool beZbStart = false;

    /// <summary>
    /// 返回一个 LuaFileUtils （可重写成自定义 Loader）
    /// </summary>
    protected virtual LuaFileUtils InitLoader()
    {
        if (LuaFileUtils.Instance != null)
        {
            return LuaFileUtils.Instance;
        }

        return new LuaFileUtils();
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void LoadLuaFiles()
    {
        OnLoadFinished();
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void OpenLibs()
    {
        luaState.OpenLibs(LuaDLL.luaopen_pb);
        luaState.OpenLibs(LuaDLL.luaopen_struct);
        luaState.OpenLibs(LuaDLL.luaopen_lpeg);
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
        luaState.OpenLibs(LuaDLL.luaopen_bit);
#endif

        if (AppConst.openLuaSocket)
        {
            OpenLuaSocket();            
        }        

        if (AppConst.openZbsDebugger)
        {
            OpenZbsDebugger();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OpenZbsDebugger(string ip = "localhost")
    {
        if (!Directory.Exists(AppConst.zbsDir))
        {
            Debugger.LogWarning("ZeroBraneStudio not install or AppConst.zbsDir not right");
            return;
        }

        if (!AppConst.openLuaSocket)
        {                            
            OpenLuaSocket();
        }

        if (!string.IsNullOrEmpty(AppConst.zbsDir))
        {
            luaState.AddSearchPath(AppConst.zbsDir);
        }

        luaState.LuaDoString(string.Format("DebugServerIp = '{0}'", ip));
    }

    /// <summary>
    /// 
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Socket_Core(IntPtr L)
    {        
        return LuaDLL.luaopen_socket_core(L);
    }

    /// <summary>
    /// 
    /// </summary>
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int LuaOpen_Mime_Core(IntPtr L)
    {
        return LuaDLL.luaopen_mime_core(L);
    }

    /// <summary>
    /// 
    /// </summary>
    protected void OpenLuaSocket()
    {
        AppConst.openLuaSocket = true;

        luaState.BeginPreLoad();
        luaState.RegFunction("socket.core", LuaOpen_Socket_Core);
        luaState.RegFunction("mime.core", LuaOpen_Mime_Core);                
        luaState.EndPreLoad();                     
    }

    /// <summary>
    /// 开启 cjson 类库并获取相关信息 （cjson 比较特殊，只new了一个table，没有注册库，这里注册一下）
    /// </summary>
    protected void OpenCJson()
    {
        luaState.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
        luaState.OpenLibs(LuaDLL.luaopen_cjson);
        luaState.LuaSetField(-2, "cjson");

        luaState.OpenLibs(LuaDLL.luaopen_cjson_safe);
        luaState.LuaSetField(-2, "cjson.safe");                               
    }

    /// <summary>
    /// 调用 Main 方法 (获取 main 方法再调用，这里采用的是有 GC 的方法)
    /// </summary>
    protected virtual void CallMain()
    {
        LuaFunction main = luaState.GetFunction("Main");
        main.Call();
        main.Dispose();
        main = null;                
    }

    /// <summary>
    /// 执行 Main.lua 文件，从 lua 中获取 OnLevelWasLoaded 方法，再调用 CallMain 方法
    /// </summary>
    protected virtual void StartMain()
    {
        luaState.DoFile("Main.lua");
        levelLoaded = luaState.GetFunction("OnLevelWasLoaded");
        CallMain();
    }

    /// <summary>
    /// 添加 LuaLooper 组件并设置其 luaState
    /// </summary>
    protected void StartLooper()
    {
        loop = gameObject.AddComponent<LuaLooper>();
        loop.luaState = luaState;
    }

    /// <summary>
    /// 完成 wrap 文件与 lua 文件之间的相互加载、注册
    /// </summary>
    protected virtual void Bind()
    {        
        LuaBinder.Bind(luaState);
        LuaCoroutine.Register(luaState, this);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    protected void Init()
    {        
        InitLoader();
        luaState = new LuaState();
        OpenLibs();
        luaState.LuaSetTop(0);
        Bind();
        LoadLuaFiles();    
    }

    protected void Awake()
    {
        Instance = this;
        Init();

#if UNITY_5_4
        SceneManager.sceneLoaded += OnSceneLoaded;
#endif        
    }

    /// <summary>
    /// 调用 luaState、luaLooper、Main 相关的 Start 方法
    /// </summary>
    protected virtual void OnLoadFinished()
    {
        luaState.Start();
        StartLooper();
        StartMain();
    }

    /// <summary>
    /// 读取关卡 （如果读取关卡方法信息类不为空则用无GC的方式调用该方法）
    /// </summary>
    void OnLevelLoaded(int level)
    {
        if (levelLoaded != null)
        {
            levelLoaded.BeginPCall();
            levelLoaded.Push(level);
            levelLoaded.PCall();
            levelLoaded.EndPCall();
        }
    }

#if UNITY_5_4
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelLoaded(scene.buildIndex);
    }
#else
    /// <summary>
    /// ？？？ （为何只是在内部调用读取关卡的方法需要再包装成一个新的方法？）
    /// </summary>
    protected void OnLevelWasLoaded(int level)
    {
        OnLevelLoaded(level);
    }
#endif

    /// <summary>
    /// 释放当前所有资源
    /// </summary>
    public virtual void Destroy()
    {
        if (luaState != null)
        {
#if UNITY_5_4
        SceneManager.sceneLoaded -= OnSceneLoaded;
#endif    
            LuaState state = luaState;
            luaState = null;

            if (levelLoaded != null)
            {
                levelLoaded.Dispose();
                levelLoaded = null;
            }

            if (loop != null)
            {
                loop.Destroy();
                loop = null;
            }

            state.Dispose();            
            Instance = null;
        }
    }

    /// <summary>
    /// 调用 Destroy() 方法
    /// </summary>
    protected void OnDestroy()
    {
        Destroy();
    }

    /// <summary>
    /// 调用 Destroy() 方法
    /// </summary>
    protected void OnApplicationQuit()
    {
        Destroy();
    }

    /// <summary>
    /// 获取 luaState
    /// </summary>
    public static LuaState GetMainState()
    {
        return Instance.luaState;
    }

    /// <summary>
    /// 获取 LuaLooper
    /// </summary>
    public LuaLooper GetLooper()
    {
        return loop;
    }
}
