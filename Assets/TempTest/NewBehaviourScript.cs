using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class NewBehaviourScript
{/*
    [MenuItem("Test/Build Asset Bundles")]
    static void BuildABs()
    {
        // Put the bundles in a folder called "ABs" within the
        // Assets folder.
        AssetBundleBuild[] abb = new AssetBundleBuild[2];
        abb[0].assetBundleName = "abc";
        abb[0].assetBundleVariant = "1280x720";
        abb[0].assetNames = new string[] { "Assets/aTestPic/Shared/ButtonClick.png" };
        abb[0].assetBundleName = "abd";
        abb[0].assetBundleVariant = "1280x720";
        abb[0].assetNames = new string[] { "Assets/aTestPic/Shared/ButtonDisable.png" };
        BuildPipeline.BuildAssetBundles("Assets/aTestPic/Shared", abb);
    }

    [MenuItem("Test/Copy Lua Test")]
    static void testLua()
    {
        // 拼接路径字符串并创建
        //string streamDir = Application.dataPath + "/" + AppDefine.LuaTempDir;
        //string srcDir = Application.dataPath + "/" + AppDefine.AppName + "/Lua/";
        CopyLuaTest(Application.dataPath + "/aTestPic/", Application.dataPath + "/aTestPic/lua/");
    }

    [MenuItem("Test/load Test")]
    static void testLoad()
    {
        object[] asset = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/shared.unity3d.aaa").LoadAllAssets();
        int length = asset.Length;
        Debug.Log(length);
        for(int i = 0; i < length; i++) { Debug.Log(asset[i].ToString()); }
    }

    public static void CopyLuaTest(string sourceDir, string destDir, bool appendext = true)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

        // 返回目录及其子目录下所有lua文件名
        string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);

        int len = sourceDir.Length;
        if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
        {
            --len;
        }

        for (int i = 0; i < files.Length; i++)
        {
            string str = files[i].Remove(0, len);
            string dest = destDir + str;
            if (appendext) dest += ".bytes";
            string dir = Path.GetDirectoryName(dest);
            Directory.CreateDirectory(dir);
            File.Copy(files[i], dest, true);
        }
    }*/
}
