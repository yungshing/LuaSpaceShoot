using UnityEngine;
using System.Collections;
using SightpLua;

public class ObjManager : MonoBehaviour {


    //初始化稳固的GameObject
    public void InitStableObj()
    {

    }

    //根据assetName 加载相应的资源，，并创建对象
    public GameObject CreateObjByBundle(string bundleName,  string objName=null)
    {
        ResManager resMgr = AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource);

        AssetBundle bundle = resMgr.LoadBundle(bundleName);
        GameObject prefab = null;
        if (objName != null)
        {
            prefab = bundle.Load(objName, typeof(GameObject)) as GameObject;
        }
        else
        {
            prefab = bundle.mainAsset as GameObject;
        }
        GameObject go = Instantiate(prefab) as GameObject;
        return go;
    }




    //动态地添加lua脚本组件
    //public void AddLuaComponent(GameObject go, string luaFilename)
    //{
    //    Types.GetType("")
    //     go.AddComponent<ParticleSystem>();
    //     go.AddComponent<BaseLua>();
    //    LuaCompoenet Lc = go.AddComponent<LuaCompoenet>() as LuaCompoenet;
    //    Lc.luaFilename = luaFilename;
    //     Lc.init(luaFilename);
    //}

}
