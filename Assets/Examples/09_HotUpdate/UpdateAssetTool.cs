using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UpdateAssetTool : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Request());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Request()
    {
        // 拼接请求字符串
        string url = AppDefine.WebUrl;
        string random = DateTime.Now.ToString("yyyymmddhhmmss");
        string listUrl = url + "files.txt?v=" + random;
        Debug.Log("LoadUpdate---->>>" + listUrl);

        // 发送请求
        WWWForm form = new WWWForm();
        form.AddField("str", listUrl);
        WWW www = new WWW(AppDefine.WebUrl, form);

        yield return www;
        // 如果失败就打印消息并退出
        if (www.error != null)
        {
            print("更新失败1!");
            yield break;
        }
        print(www.text);

        string dataPath = Application.dataPath + "/StreamingAssets/";//Utils.PathUtils.DataPath;
        // 在 dataPath 创建 files.txt 并覆盖写入返回结果
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        File.WriteAllBytes(dataPath + "files.txt", www.bytes);

        // 拆分返回消息判断是否更新
        string filesText = www.text;
        string[] files = filesText.Split('\n');
        for (int i = 0; i < files.Length; i++)
        {
            // 拼接本地路径字符串(c:/toluacontainer/StreamingAssets)
            if (string.IsNullOrEmpty(files[i])) continue;
            string[] keyValue = files[i].Split('|');
            string currentFileName = keyValue[0];
            string localfile = (dataPath + currentFileName).Trim();

            // 获取 localfile 所在的目录，如果不存在就创建
            // 如果服务器返回的是路径就创建目录，是文件就创建文件
            string path = Path.GetDirectoryName(localfile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // 拼写当前文件的请求以便之后需要时发送
            string fileUrl = url + keyValue[0] + "?v=" + random;
            // 如果不存在则请求下载，如果存在但 MD5 不同删除原文件后重新下载
            bool canUpdate = !File.Exists(localfile);
            if (!canUpdate)
            {
                string remoteMd5 = keyValue[1].Trim();
                string localMd5 = Utils.Md5Utils.md5file(localfile);
                canUpdate = !remoteMd5.Equals(localMd5);
                if (canUpdate) File.Delete(localfile);
            }

            // 下载
            if (canUpdate)
            {
                Debug.Log("downloading>>" + fileUrl);
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    //绑定下载完成事件，进行完成提示
                    client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(OnDownloadFileCompleted);
                    // WebClient.DownloadFileAsync 方法 将具有指定 URI 的资源下载到本地文件。此方法不会阻止调用线程（异步）。
                    client.DownloadFileAsync(new System.Uri(fileUrl), localfile);
                }
            }
        }
        yield return new WaitForEndOfFrame();
        print("更新完成!");
        // 刷新
        AssetDatabase.Refresh();
    }

    void OnDownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e.Cancelled) print("下载取消");
        else print("下载完毕");
    }
}
