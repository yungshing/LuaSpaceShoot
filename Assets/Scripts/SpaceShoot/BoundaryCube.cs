using UnityEngine;
using System.Collections;

public class BoundaryCube : LuaBehaviour {

    void Awake()
    {
        base.Init("SpaceShoot/BoundaryCube");
        base.Awake();
    }

    void OnTriggerExit(Collider other)
    {
        CallMethod("OnTriggerExit", other);
        //Destroy(other.gameObject);
    }

     
}


