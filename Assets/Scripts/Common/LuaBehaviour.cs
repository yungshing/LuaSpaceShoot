using UnityEngine;
using System.Collections;
using LuaInterface;
using System.Collections.Generic;
using SightpLua;
public class LuaBehaviour : BehaviourBase
{

        protected static bool initialize = false;
        [HideInInspector]
        public string moduleName = string.Empty;

       /*private string data = null;
         private AssetBundle bundle = null;
         List<LuaFunction> buttons = new List<LuaFunction>();
        */



        public void Init(string filename)
        {
            if (filename != null)
            {
                base.LuaManager.DoFile(filename);
                string name = filename;
                int index = filename.LastIndexOf('/');
                if (index != -1)
                {
                    name = filename.Substring(index + 1);
                }
                initialize = true;
                moduleName = name;
            }
        }
        

        protected void Awake() {
            CallMethod("Awake");
        }

        protected void Start() {
            if (LuaManager != null && initialize)
            {
                LuaState l = LuaManager.lua;
                l[moduleName + ".transform"] = transform;
                l[moduleName + ".gameObject"] = gameObject;
                l[moduleName + ".curInstance"] = this;
            }
        
        }



      
        /*  
          /// <summary>
          /// 初始化面板
          /// </summary>
          public void OnInit(AssetBundle bundle, string text = null) {
              this.data = text;   //初始化附加参数
              this.bundle = bundle; //初始化
              Debug.LogWarning("OnInit---->>>" + name + " text:>" + text);
          }

          /// <summary>
          /// 获取一个GameObject资源
          /// </summary>
          /// <param name="name"></param>
          public GameObject GetGameObject(string name) {
              if (bundle == null) return null;
              return Util.LoadAsset(bundle, name);
          }
   
         /// <summary>
          /// 添加单击事件
          /// </summary>
          public void AddClick(GameObject go, LuaFunction luafunc) {
              if (go == null) return;
              UIEventListener.Get(go).onClick = delegate(GameObject o) {
                  luafunc.Call(go);
                  buttons.Add(luafunc);
              };
          }

          /// <summary>
          /// 清除单击事件
          /// </summary>
          public void ClearClick() {
              for (int i = 0; i < buttons.Count; i++ ) {
                  if (buttons[i] != null) {
                      buttons[i].Dispose();
                      buttons[i] = null;
                  }
              }
          }
                protected void OnClick() {
            CallMethod("OnClick");
        }

        protected void OnClickEvent(GameObject go) {
            CallMethod("OnClick", go);
        }
*/
        /// <summary>
        /// 执行Lua方法
        /// </summary>
        protected object[] CallMethod(string func, params object[] args) {
            if (!initialize) return null;
            return Util.CallMethod(moduleName, func, args);
        }

        protected void OnDestroy()
        {
            //TODO : 处理释放的逻辑
            CallMethod("OnDestroy");
        }
      
}
