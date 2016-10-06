﻿using UnityEngine;

/// <summary>
/// 常量类
/// </summary>
public static class ToLuaConst
{
    /// <summary>
    /// lua逻辑代码目录
    /// </summary>
    public static string luaDir = Application.dataPath + "/Lua";

    /// <summary>
    /// tolua lua文件目录
    /// </summary>
    public static string toluaDir = Application.dataPath + "/ToLua/Lua";

#if UNITY_STANDALONE
    /// <summary>
    /// 当前系统（字符串）
    /// </summary>
    public static string osDir = "Win";
#elif UNITY_ANDROID
    public static string osDir = "Android";            
#elif UNITY_IPHONE
    public static string osDir = "iOS";        
#else
    public static string osDir = "";
#endif

    /// <summary>
    /// 手机运行时lua文件下载目录
    /// </summary>
    public static string luaResDir = string.Format("{0}/{1}/Lua", Application.persistentDataPath, osDir);

#if UNITY_EDITOR_WIN || NITY_STANDALONE_WIN    
    /// <summary>
    /// ZeroBraneStudio目录
    /// </summary>
    public static string zbsDir = "D:/ZeroBraneStudio/lualibs/mobdebug";
#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
	public static string zbsDir = "/Applications/ZeroBraneStudio.app/Contents/ZeroBraneStudio/lualibs/mobdebug";
#else
    public static string zbsDir = luaResDir + "/mobdebug/";
#endif

    /// <summary>
    /// 是否打开Lua Socket库
    /// </summary>
    public static bool openLuaSocket = true;

    /// <summary>
    /// 是否连接ZeroBraneStudio调试
    /// </summary>
    public static bool openZbsDebugger = false;
}