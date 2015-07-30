using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SightpLua;
public class BootstrapCommands : SimpleCommand{

    public override void Execute(INotification notification)
    {
        //-----------------关联命令-----------------------
      //  Facade.RegisterCommand(NotiConst.DISPATCH_MESSAGE, typeof(SocketCommand));

        //-----------------初始化管理器-----------------------
     /*   Facade.AddManager(ManagerName.Lua, new LuaScriptMgr());

        Facade.AddManager<PanelManager>(ManagerName.Panel);
        Facade.AddManager<MusicManager>(ManagerName.Music);
        Facade.AddManager<TimerManager>(ManagerName.Timer);
        Facade.AddManager<NetworkManager>(ManagerName.Network);
        Facade.AddManager<ResourceManager>(ManagerName.Resource);
     */
        //-----------------初始化管理器-----------------------
        Facade.AddManager(ManagerName.Lua, new LuaScriptMgr());


        //添加资源管理器
        Facade.AddManager<ResManager>(ManagerName.Resource);

        //游戏对象管理器
       Facade.AddManager<ObjManager>(ManagerName.ObjMgr);

        //添加音效管理器
       Facade.AddManager<AudioManager>(ManagerName.Music);

        //添加游戏管理器......!!资源管理器对其他管理器对象有依赖，，，必须放所有管理器对象后面添加
        Facade.AddManager<GameManager>(ManagerName.Game);


        

        Debug.Log("SimpleFramework StartUp-------->>>>>");
    }
}
