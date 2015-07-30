using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


//当消息处理完成后，回调方法，，flag：true-->资源更新成功   false--->资源更新失败
public delegate void OnResHandle(bool flag);
public class ResManager : MonoBehaviour {
    private IDictionary<string, AssetBundle> bundlesMap;

    void Awake()
    {
        Init();
    }
    /// <summary>
    /// 初始化资源管理器
    /// </summary>
    private void Init()
    {
        bundlesMap = new Dictionary<string, AssetBundle>();
    }

    //清理掉所有加载到内存中的AssetBundle对象
    void Destroy()
    {
        foreach (KeyValuePair<string, AssetBundle> kvp in bundlesMap)
        {
            if (kvp.Value != null)
            {
                kvp.Value.Unload(true);
            }
        }
        bundlesMap = null;
    }

    /// <summary>
    /// 载入素材
    /// </summary>
    public AssetBundle LoadBundle(string name)
    {
        if (bundlesMap.ContainsKey(name))
        {
            return bundlesMap[name];
        }
        else
        {
            byte[] stream = null;
            AssetBundle bundle = null;
            string uri = Util.DataPath + name.ToLower() + ".assetbundle";
            stream = File.ReadAllBytes(uri);
            bundle = AssetBundle.CreateFromMemoryImmediate(stream); //关联数据的素材绑定
            bundlesMap.Add(name, bundle);
            return bundle;
        }
    }


    //====================================   资源处理、更新   =================================================

    /// <summary>
    /// 释放资源
    /// </summary>
    public void CheckExtractResource(OnResHandle resHandle)
    {
        bool isExists = Directory.Exists(Util.DataPath) &&
          Directory.Exists(Util.DataPath + "lua/") && File.Exists(Util.DataPath + "files.txt");
        if (isExists || AppConst.DebugMode)
        {
            StartCoroutine(OnUpdateResource(resHandle));
            return;   //文件已经解压过了，自己可添加检查文件列表逻辑
        }
        StartCoroutine(OnExtractResource(resHandle));    //启动释放协成 
    }

    IEnumerator OnExtractResource(OnResHandle resHandle)
    {
        string dataPath = Util.DataPath;  //数据目录
        string resPath = Util.AppContentPath(); //游戏包资源目录

        if (Directory.Exists(dataPath)) Directory.Delete(dataPath);
        Directory.CreateDirectory(dataPath);

        string infile = resPath + "files.txt";
        string outfile = dataPath + "files.txt";
        if (File.Exists(outfile)) File.Delete(outfile);

        Debug.Log("正在解包文件:>files.txt");
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(infile);
            yield return www;

            if (www.isDone)
            {
                File.WriteAllBytes(outfile, www.bytes);
            }
            yield return 0;
        }
        else File.Copy(infile, outfile, true);
        yield return new WaitForEndOfFrame();

        //释放所有文件到数据目录
        string[] files = File.ReadAllLines(outfile);
        foreach (var file in files)
        {
            string[] fs = file.Split('|');
            infile = resPath + fs[0];  //
            outfile = dataPath + fs[0];
            //message = "正在解包文件:>" + fs[0];
            Debug.Log("正在解包文件:>" + infile);

            string dir = Path.GetDirectoryName(outfile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outfile, www.bytes);
                }
                yield return 0;
            }
            else File.Copy(infile, outfile, true);
            yield return new WaitForEndOfFrame();
        }
       // message = "解包完成!!!";
        yield return new WaitForSeconds(0.1f);
      //  message = string.Empty;

        //释放完成，开始启动更新资源
        StartCoroutine(OnUpdateResource(resHandle));
    }


    /// <summary>
    /// TODO:由于Web环境没有搭建，，，所以此处没有做测试
    /// </summary>

    IEnumerator OnUpdateResource(OnResHandle resHandle)
    {
        if (!AppConst.UpdateMode)
        {
            OnResourceInited(resHandle);
            yield break;
        }
        WWW www = null;
        string dataPath = Util.DataPath;  //数据目录
        string url = string.Empty;
#if UNITY_5 
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            url = AppConst.WebUrl + "/ios/";
        } else {
            url = AppConst.WebUrl + "android/5x/";
        }
#else
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            url = AppConst.WebUrl + "/iphone/";
        }
        else
        {
            url = AppConst.WebUrl + "android/4x/";
        }
#endif
        string random = DateTime.Now.ToString("yyyymmddhhmmss");
        string listUrl = url + "files.txt?v=" + random;
        if (Debug.isDebugBuild) Debug.LogWarning("LoadUpdate---->>>" + listUrl);

        www = new WWW(listUrl); yield return www;
        if (www.error != null)
        {
            OnUpdateFailed(resHandle, string.Empty);
            yield break;
        }
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        File.WriteAllBytes(dataPath + "files.txt", www.bytes);
        string filesText = www.text;
        string[] files = filesText.Split('\n');

        for (int i = 0; i < files.Length; i++)
        {
            if (string.IsNullOrEmpty(files[i])) continue;
            string[] keyValue = files[i].Split('|');
            string f = keyValue[0].Remove(0, 1);
            string localfile = (dataPath + f).Trim();
            string path = Path.GetDirectoryName(localfile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileUrl = url + f + "?v=" + random;
            bool canUpdate = !File.Exists(localfile);
            if (!canUpdate)
            {
                string remoteMd5 = keyValue[1].Trim();
                string localMd5 = Util.md5file(localfile);
                canUpdate = !remoteMd5.Equals(localMd5);
                if (canUpdate) File.Delete(localfile);
            }
            if (canUpdate)
            {   //本地缺少文件
                Debug.Log(fileUrl);
                //message = "downloading>>" + fileUrl;
                www = new WWW(fileUrl); yield return www;
                if (www.error != null)
                {
                    OnUpdateFailed(resHandle, path);   //
                    yield break;
                }
                File.WriteAllBytes(localfile, www.bytes);
            }
        }
        yield return new WaitForEndOfFrame();
        //message = "更新完成!!";

        OnResourceInited(resHandle);
    }

    private void OnUpdateFailed(OnResHandle resHandle , string p)
    {
        Util.Log("OnUpdateFailed : "+ p);
        resHandle(false);
    }

    private void OnResourceInited(OnResHandle reshandle)
    {
        Util.Log("OnResourceInited()");
        reshandle(true);
    }
}
