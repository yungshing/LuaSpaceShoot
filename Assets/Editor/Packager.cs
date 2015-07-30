using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Packager {
    public static string platform = string.Empty;
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();

    ///-----------------------------------------------------------
    static string[] exts = { ".txt", ".xml", ".lua", ".assetbundle", ".json" };
    static bool CanCopy(string ext) {   //根据文件的扩展名来判断文件是不是应该被拷贝
        foreach (string e in exts) {
            if (ext.Equals(e)) return true;
        }
        return false;
    }

    /// <summary>
    /// 载入素材
    /// </summary>
    static UnityEngine.Object LoadAsset(string file) {
        if (file.EndsWith(".lua")) file += ".txt";
        return AssetDatabase.LoadMainAssetAtPath("Assets/Builds/" + file);
    }

  /*  [MenuItem("Game/Build iPhone Resource", false, 11)]
    public static void BuildiPhoneResource() { 
        BuildTarget target;
#if UNITY_5
        target = BuildTarget.iOS;
#else
        target = BuildTarget.iPhone;
#endif
        BuildAssetResource(target, false);
    }

    [MenuItem("Game/Build Android Resource", false, 12)]
    public static void BuildAndroidResource() {
        BuildAssetResource(BuildTarget.Android, true);
    }*/

    [MenuItem("Game/Generate AssetsBundles", false, 13)]
    public static void BuildWindowsResource() {
        BuildAssetResource(BuildTarget.StandaloneWindows, true);
    }

    /// <summary>
    /// 生成绑定素材          TODO : 这里需要更改！！！！！！！！！！！！！！！！！！！
    /// </summary>
    public static void BuildAssetResource(BuildTarget target, bool isWin) {
      /*  Object mainAsset = null;        //主素材名，单个
        Object[] addis = null;     //附加素材名，多个
        string assetfile = string.Empty;  //素材文件名

        BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle | 
                                          BuildAssetBundleOptions.CollectDependencies | 
                                          BuildAssetBundleOptions.DeterministicAssetBundle;
        string dataPath = Util.DataPath;
        if (Directory.Exists(dataPath)) {
            Directory.Delete(dataPath, true);
        }
        string assetPath = AppDataPath + "/StreamingAssets/";
        if (Directory.Exists(dataPath)) {
            Directory.Delete(assetPath, true);
        }
        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        ///-----------------------------生成共享的关联性素材绑定-------------------------------------
        BuildPipeline.PushAssetDependencies();

        assetfile = assetPath + "shared.assetbundle";
        mainAsset = LoadAsset("Shared/Atlas/Dialog.prefab");
        BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, target);

        ///------------------------------生成PromptPanel素材绑定-----------------------------------
        BuildPipeline.PushAssetDependencies();
        mainAsset = LoadAsset("Prompt/Prefabs/PromptPanel.prefab");
        addis = new Object[1];
        addis[0] = LoadAsset("Prompt/Prefabs/PromptItem.prefab");
        assetfile = assetPath + "prompt.assetbundle";
        BuildPipeline.BuildAssetBundle(mainAsset, addis, assetfile, options, target);
        BuildPipeline.PopAssetDependencies();

        ///------------------------------生成MessagePanel素材绑定-----------------------------------
        BuildPipeline.PushAssetDependencies();
        mainAsset = LoadAsset("Message/Prefabs/MessagePanel.prefab");
        assetfile = assetPath + "message.assetbundle";
        BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, target);
        BuildPipeline.PopAssetDependencies();

        ///-------------------------------刷新---------------------------------------
        BuildPipeline.PopAssetDependencies();*/

        //HandleLuaFile(isWin);

        /**
         * 此处打包的逻辑做了一定的修改，可用法：
         * 1.在Assets中选择需要打包的资源，可以选择duoge
         * 2.点击菜单栏中的Game/Generate AssetsBundles就可以将.assetBundle生成到StreamingAssets目录下了 
         * 
         * 
         * !!!!!!!!!此处忽略了对共享资源的考虑，，，，所以整体打包出的资源会比较大
         */
        Object[] os = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        bool isExist = Directory.Exists(Application.dataPath + "/StreamingAssets");
        if (!isExist)
        {
            Directory.CreateDirectory(Application.dataPath + "/StreamingAssets");
        }
        foreach (Object o in os)
        {
            string sourcePath = AssetDatabase.GetAssetPath(o);

            string targetPath = Application.dataPath + "/StreamingAssets/" + o.name.ToLower() + ".assetbundle";
            if (BuildPipeline.BuildAssetBundle(o, null, targetPath, BuildAssetBundleOptions.CollectDependencies))
            {
                UnityEngine.Debug.Log("create bundle cuccess!");
            }
            else
            {
                UnityEngine.Debug.Log("failure happen");
            }
            AssetDatabase.Refresh();
        }









        AssetDatabase.Refresh();
    }


    [MenuItem("Game/Copy Lua Files", false, 13)]
    static void copyLuaFiles()
    {
        HandleLuaFile(true);
        AssetDatabase.Refresh();
    }



    /// <summary>
    /// 处理Lua文件
    /// </summary>
    static void HandleLuaFile(bool isWin) {
        string resPath = AppDataPath + "/StreamingAssets/";
        string luaPath = resPath + "/lua/";

        //----------复制Lua文件----------------
        if (!Directory.Exists(luaPath)) {
            Directory.CreateDirectory(luaPath); 
        }
        paths.Clear(); files.Clear();
        string luaDataPath = AppDataPath + "/lua/".ToLower();
        Recursive(luaDataPath);     //递归读取luaDataPath中所有的文件
        int n = 0;
        foreach (string f in files) {
            if (f.EndsWith(".meta")) continue;
            string newfile = f.Replace(luaDataPath, "");
            string newpath = luaPath + newfile;
            string path = Path.GetDirectoryName(newpath);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (File.Exists(newpath)) {
                File.Delete(newpath);
            }
            if (AppConst.LuaEncode) {
                UpdateProgress(n++, files.Count, newpath);
                EncodeLuaFile(f, newpath, isWin);
            } else {
                File.Copy(f, newpath, true);
            }
        }
        EditorUtility.ClearProgressBar();

        ///----------------------创建文件列表-----------------------
        string newFilePath = resPath + "/files.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++) {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta")) continue;

            string md5 = Util.md5file(file);
            string value = file.Replace(resPath, string.Empty); 
            sw.WriteLine(value + "|" + md5);
        }
        sw.Close(); fs.Close();

        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 数据目录
    /// </summary>
    static string AppDataPath {
        get { return Application.dataPath.ToLower(); }
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path) {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names) {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs) {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    static void UpdateProgress(int progress, int progressMax, string desc) {
        string title = "Processing...[" + progress + " - " + progressMax + "]";
        float value = (float)progress / (float)progressMax;
        EditorUtility.DisplayProgressBar(title, desc, value);
    }

    static void EncodeLuaFile(string srcFile, string outFile, bool isWin) {
        if (!srcFile.ToLower().EndsWith(".lua")) {
            File.Copy(srcFile, outFile, true);
            return;
        }
        string luaexe = string.Empty;
        string args = string.Empty;
        string exedir = string.Empty;
        string currDir = Directory.GetCurrentDirectory();
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            luaexe = "luajit.exe";
            args = "-b " + srcFile + " " + outFile;
            exedir = AppDataPath + "/Encoder/luajit/";
        } else if (Application.platform == RuntimePlatform.OSXEditor) {
            luaexe = "./luac";
            args = "-o " + outFile + " " + srcFile;
            exedir = AppDataPath + "/Encoder/luavm/";
        }
        Directory.SetCurrentDirectory(exedir);
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = luaexe;
        info.Arguments = args;
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.UseShellExecute = isWin;
        info.ErrorDialog = true;
        Util.Log(info.FileName + " " + info.Arguments);

        Process pro = Process.Start(info);
        pro.WaitForExit();
        Directory.SetCurrentDirectory(currDir);
    }

   /* [MenuItem("Game/Build Protobuf-lua-gen File")]
    public static void BuildProtobufFile() {
        string dir = AppDataPath + "/Lua/bin";
        paths.Clear(); files.Clear(); Recursive(dir);

        string protoc = "d:/protobuf-2.4.1/src/protoc.exe";
        string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

        foreach (string f in files) {
            string name = Path.GetFileName(f);
            string ext = Path.GetExtension(f);
            if (!ext.Equals(".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = protoc;
            info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            Util.Log(info.FileName + " " + info.Arguments);

            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
        AssetDatabase.Refresh();
    }
    * */
}