using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using SightpLua;
using System.Security.Cryptography;

public class Util {
    private static string _dataPath = string.Empty;
    /// <summary>
    /// 取得数据存放目录
    /// </summary>
    public static string DataPath
    {
        get
        {
            if (_dataPath == string.Empty)
            {
                string game = AppConst.AppName.ToLower();
                _dataPath = "c:/" + game + "/";
                if (Application.isMobilePlatform)
                {
                    _dataPath = Application.persistentDataPath + "/" + game + "/";
                }
                if (Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    _dataPath = Application.streamingAssetsPath + "/";
                }
                if (AppConst.DebugMode)
                {
                    if (Application.isEditor)
                    {
                        _dataPath = Application.dataPath + "/StreamingAssets/";
                    }
                }
            }
            return _dataPath;
          
            
        }
    }
    public static string LuaPath()
    {
        if (AppConst.DebugMode)
        {
            return Application.dataPath + "/lua/";
        }
        return DataPath + "lua/";
    }

    /// <summary>
    /// 取得Lua路径
    /// </summary>
    public static string LuaPath(string name) {
        //string path = Application.dataPath ;
        string path = AppConst.DebugMode ? Application.dataPath + "/" : DataPath;
        //去除lua文件名的后缀
        string lowerName = name.ToLower();
        if (lowerName.EndsWith(".lua")) {
            int index = name.LastIndexOf('.');
            name = name.Substring(0, index);
        }
        name = name.Replace('.', '/');
        return path + "/lua/" + name + ".lua";
    }

    /// <summary>
    /// 应用程序内容路径
    /// </summary>
    public static string AppContentPath()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/";
                break;
            default:
                path = Application.dataPath + "/StreamingAssets/";
                break;
        }
        return path;
    }




    public static void Log(string str) {
        Debug.Log(str);
    }

    public static void LogWarning(string str) {
        Debug.LogWarning(str);
    }

    public static void LogError(string str) {
        Debug.LogError(str); 
    }


    /// <summary>
    /// 执行Lua方法
    /// </summary>
    public static object[] CallMethod(string module, string func, params object[] args)
    {
        LuaScriptMgr luaMgr = AppFacade.Instance.GetManager<LuaScriptMgr>(ManagerName.Lua);
        if (luaMgr == null) return null;
        string funcName = module + "." + func;
        funcName = funcName.Replace("(Clone)", "");
        return luaMgr.CallLuaFunction(funcName, args);
    }


    public static GameObject LoadPrefab(string name)
    {
        return Resources.Load("Prefabs/" + name, typeof(GameObject)) as GameObject;
    }


    public static GameObject LoadAsset(AssetBundle bundle, string name)
    {
#if UNITY_5
        return bundle.LoadAsset(name, typeof(GameObject)) as GameObject;
#else
        return bundle.Load(name, typeof(GameObject)) as GameObject;
#endif
    }


    /// <summary>
    /// 计算字符串的MD5值
    /// </summary>
    public static string md5(string source)
    {
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
        byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
        md5.Clear();

        string destString = "";
        for (int i = 0; i < md5Data.Length; i++)
        {
            destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
        }
        destString = destString.PadLeft(32, '0');
        return destString;
    }

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    public static string md5file(string file)
    {
        try
        {
            FileStream fs = new FileStream(file, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("md5file() fail, error:" + ex.Message);
        }
    }

    /// <summary>
    /// 清理内存
    /// </summary>
    public static void ClearMemory()
    {
        GC.Collect(); 
        Resources.UnloadUnusedAssets();
        LuaScriptMgr mgr = AppFacade.Instance.GetManager<LuaScriptMgr>(ManagerName.Lua);
        if (mgr == null) mgr.LuaGC();
    }

}