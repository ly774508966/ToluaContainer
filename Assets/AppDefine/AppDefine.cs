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
    /// AssetBundle 文件扩展名
    /// </summary>
    public const string ExtName = ".unity3d";

    /// <summary>
    /// 用户ID
    /// </summary>
    public static string UserId = string.Empty;
}
