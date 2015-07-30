using UnityEngine;
using System.Collections;
using SightpLua;

public class GameManager : LuaBehaviour
{
    void Awake()
    {
        Init();
    }


    private void Init()
    {
        DontDestroyOnLoad(gameObject);

        //更新资源
        updateRes();
        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = AppConst.GameFrameRate;

    }

    private void updateRes()
    {
        ResManager resMgr = facade.GetManager<ResManager>(ManagerName.Resource);
        resMgr.CheckExtractResource(OnResHandle);

    }
    //当资源管理器更新完资源后回调改方法，，继续初始化剩下的游戏逻辑
    private void OnResHandle(bool rst)
    {
        LuaManager.Start();
        Util.Log("OnResHandle : " + rst);
        if (rst)        //更新资源成功
        {
            InitObj();
        }
        else
        {
            //TODO : 资源更新失败的处理方案


        }
    }


    /// <summary>
    /// 完成游戏对象的初始化工作
    /// </summary>
    private void InitObj()
    {
        base.Init("logic/GameManager");
        //初始化稳固的游戏对象
        ObjMgr.InitStableObj();

        //初始化固定脚本的游戏对象
        object[] os = CallMethod("CreateObj");
        foreach (object o in os)
        {
            ObjMgr.CreateObjByBundle((string)o);
        }

        //在lua中初始化动态地游戏对象
        CallMethod("InitObj");
        
    }


    void Update()
    {
        if (LuaManager != null && initialize)
        {
            LuaManager.Update();
        }

    }

    void LateUpdate()
    {
        if (LuaManager != null && initialize)
        {
            LuaManager.LateUpate();
        }
        CallMethod("Update");
    }

    void FixedUpdate()
    {
        if (LuaManager != null && initialize)
        {
            LuaManager.FixedUpdate();
        }
    }


    protected void OnDestroy()
    {
        //if (bundle) {
        //    bundle.Unload(true);
        //    bundle = null;  //销毁素材
        //}
        //ClearClick();

        CallMethod("Destroy");      //调用绑定的lua脚本中的Destroy（）方法
        Util.ClearMemory();
        Debug.Log("~" + name + " was destroy!");
    }
}
