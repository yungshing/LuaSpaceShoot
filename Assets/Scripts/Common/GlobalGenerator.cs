using UnityEngine;
using System.Collections;

public class GlobalGenerator : MonoBehaviour {
   void Awake() {
            InitGameMangager();
        }

        /// <summary>
        /// 实例化游戏管理器
        /// </summary>
        public void InitGameMangager() {



            string name = "GameManager";
            GameObject manager = GameObject.Find(name);
            if (manager == null) {
                manager = new GameObject(name);
                manager.name = name;
            }
            AppFacade.Instance.StartUp();   //启动游戏
        }

        void OnGUI() {
           
        }
}
