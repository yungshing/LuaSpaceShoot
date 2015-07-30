using UnityEngine;
using System.Collections;
using SightpLua;

public class LuaHelper {


    /// <summary>
    /// 资源管理器
    /// </summary>
    public static ResManager GetResManager()
    {
        return AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource);
    }

    public static ObjManager GetObjManager()
    {
        ObjManager objMgr =  AppFacade.Instance.GetManager<ObjManager>(ManagerName.ObjMgr);
        return objMgr;
    }

    public static AudioManager GetAudioManager()
    {
        AudioManager am =  AppFacade.Instance.GetManager<AudioManager>(ManagerName.Music);
        return am;

    }
}
