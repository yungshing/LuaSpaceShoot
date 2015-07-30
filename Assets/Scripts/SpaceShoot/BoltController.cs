using UnityEngine;
using System.Collections;

public class BoltController : LuaBehaviour
{
    void Awake()
    {
        Init("SpaceShoot/BoltController");
    }


     void Start()
    {
        base.Start();
        CallMethod("Start");
    }

    void OnTriggerEnter(Collider other)
    {
        CallMethod("OnTriggerEnter", this, other);
       /* if (other.gameObject.tag == "Blocke")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            
        }
        print("Collision Cube");*/
    }
}
