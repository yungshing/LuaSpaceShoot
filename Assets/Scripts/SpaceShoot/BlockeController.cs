using UnityEngine;
using System.Collections;

public class BlockeController : LuaBehaviour {

    //public float rotate_speed;
	// Use this for initialization


    void Awake()
    {
        base.Init("spaceshoot/BlockeController");
        base.Awake();
    }
	void Start () {
        base.Start();
        CallMethod("Start");
	}
	
}
