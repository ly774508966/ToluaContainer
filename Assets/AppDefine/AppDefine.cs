using UnityEngine;
using System.Collections;

public static class AppDefine
{
    /// <summary>
    /// 场景根物体名称（切换场景不保留）
    /// </summary>
    public static string RootObjectName = "SceneRoot";

    /// <summary>
    /// 切换场景时不进行销毁的根物体名称
    /// </summary>
    public static string DDOLRootObjectName = "DDOLRoot";

    /// <summary>
    /// Debug 模式（开启后 DataPath 根据 Application.dataPath 返回路径）
    /// </summary>
    public const bool DebugMode = false;

    /// <summary>
    /// 应用程序名称
    /// </summary>
    public const string AppName = "ToluaContainer";

    /// <summary>
    /// 临时目录
    /// </summary>
    public const string LuaTempDir = "/TempLuaFiles/";

    /// <summary>
    /// 应用程序前缀
    /// </summary>
    public const string AppPrefix = AppName + "_";

    /// <summary>
    /// 素材扩展名
    /// </summary>
    public const string ExtName = ".unity3d";

    /// <summary>
    /// 素材目录 
    /// </summary>
    public const string AssetDir = "StreamingAssets";

    /// <summary>
    /// 测试更新地址（请根据自己的测试服务器地址进行更改）
    /// </summary>
    public const string WebUrl = "http://192.168.232.129:2345/";

    /// <summary>
    /// 用户ID
    /// </summary>
    public static string UserId = string.Empty;

    /// <summary>
    /// Socket服务器端口
    /// </summary>
    public static int SocketPort = 0;

    /// <summary>
    /// Socket服务器地址
    /// </summary>
    public static string SocketAddress = string.Empty;
}
