using UnityEngine;
using System.Collections;
using SightpLua;

public class BehaviourBase : MonoBehaviour {
    private AppFacade m_Facade;
    private LuaScriptMgr m_LuaMgr;
    private ResManager resMgr;
    private ObjManager objMgr;
    protected AppFacade facade
    {
        get
        {
            if (m_Facade == null)
            {
                m_Facade = AppFacade.Instance;
            }
            return m_Facade;
        }
    }

    protected LuaScriptMgr LuaManager
    {
        get
        {
            if (m_LuaMgr == null)
            {
                m_LuaMgr = facade.GetManager<LuaScriptMgr>(ManagerName.Lua);
            }
            return m_LuaMgr;
        }
        set { m_LuaMgr = value; }
    }

    protected ResManager ResMgr
    {
        get
        {
            if (resMgr == null)
            {
                resMgr = facade.GetManager<ResManager>(ManagerName.Resource);
            }
            return resMgr;
        }
        set
        {
            resMgr = value;
        }
    }


    protected ObjManager ObjMgr
    {
        get
        {
            if (objMgr == null)
            {
                objMgr = facade.GetManager<ObjManager>(ManagerName.ObjMgr);
            }
            return objMgr;
        }
        set
        {
            objMgr = value;
        }
    }

}
