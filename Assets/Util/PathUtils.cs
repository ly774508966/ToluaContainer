using UnityEngine;

namespace Utils
{
    public class PathUtils
    {
        /// <summary>
        /// 取得数据存放目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                string game = AppDefine.AppName.ToLower();
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (AppDefine.DebugMode)
                {
                    return Application.dataPath + "/" + AppDefine.AssetDir + "/";
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
                return Application.dataPath + "/" + AppDefine.AppName;
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

