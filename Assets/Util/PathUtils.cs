﻿using UnityEngine;

namespace Utils
{
    public class PathUtils
    {
        /// <summary>
        /// 应用程序名称
        /// </summary>
        public const string AppName = "ToluaContainer";

        /// <summary>
        /// 临时目录
        /// </summary>
        public const string LuaTempDir = "/TempLuaFiles/";

        /// <summary>
        /// 素材目录 
        /// </summary>
        public const string AssetDir = "StreamingAssets";

        /// <summary>
        /// 应用程序前缀
        /// </summary>
        public const string AppPrefix = AppName + "_";

        /// <summary>
        /// 测试更新地址（请根据自己的测试服务器地址进行更改）
        /// </summary>
        public const string WebUrl = "http://192.168.232.130:2345/";

        /// <summary>
        /// Socket服务器端口
        /// </summary>
        public static int SocketPort = 0;

        /// <summary>
        /// Socket服务器地址
        /// </summary>
        public static string SocketAddress = string.Empty;

        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                string game = AppName.ToLower();
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (AppDefine.DebugMode)
                {
                    return Application.dataPath + "/" + AssetDir + "/";
                }
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    int i = Application.dataPath.LastIndexOf('/');
                    return Application.dataPath.Substring(0, i + 1) + game + "/";
                }
                return "c:/" + game + "/";
            }
        }

        /// <summary>
        /// 框架根目录
        /// </summary>
        public static string FrameworkRoot
        {
            get
            {
                return Application.dataPath + "/" + AppName;
            }
        }

        /// <summary>
        /// 本地目录(更多对应平台请自行添加)
        /// </summary>
        public static string LocalFilePath
        {
            get
            {
#if UNITY_ANDROID
		return "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
		return Application.dataPath + "/Raw/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
                return "file://" + Application.dataPath + "/StreamingAssets/";
#else
        return string.Empty;
#endif
            }
        }
    }
}

