using UnityEngine;
using System.Collections;

public class GenerateBlockes : LuaBehaviour
{

   // public float interval;
   // public GameObject go;
  //  private float _interval;

    void Awake()
    {
        base.Init("SpaceShoot/GenerateBlockes");
        base.Awake();
        
    }
    void FixedUpdate()
    {
        CallMethod("FixedUpdate");
       /* _interval -= Time.deltaTime;
        if (_interval <= 0)
        {
            Vector3 vector = new Vector3
            (
                Random.Range(-6,6),
                1F,
                16f
            );

            Instantiate(go ,vector ,go.transform.rotation);
            _interval = interval;
        }*/
    }
}
