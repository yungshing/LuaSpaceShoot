using UnityEngine;
using System.Collections;
using LuaInterface;


[System.Serializable]
public class Boundary
{
    public float xMax, xMin, zMax, zMin;
}
public class PalyerController : LuaBehaviour
{
    //private Rigidbody rigidbody;
   // public Boundary boundary;
   // public float speed;
 //   public float tilt;


    public GameObject bulletPre;
    public Transform bulletPos;

  //  public float fireRate = 0.5F;
  //  private float nextFire = 0.0F;

    public float lifeValue;
	// Use this for initialization

    void Awake()
    {
        Init("SpaceShoot/PlaneController");
        base.Awake();
        lifeValue = 5;
    }


	void Start () {
        base.Start();
        LuaState l = LuaManager.lua;
        Transform t =  GameObject.Find("BulletPos").transform;
        l[moduleName + ".bulletPos"] = t;
        l[moduleName + ".bulletPre"] = bulletPre;

        l[moduleName + ".position"] = transform.position;
        CallMethod("Start");

	}


    void Update()
    {
        CallMethod("Update");
        /*if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Debug.Log(bulletPos.position.z.ToString());
            Instantiate(bulletPre, bulletPos.position, transform.rotation);
        }
        */
    }

    void FixedUpdate()
    {
        CallMethod("FixedUpdate");
       /* float horizontal = Input.GetAxis("Horizontal");
        float verticla = Input.GetAxis("Vertical");a
        rigidbody.velocity =  new Vector3(horizontal*speed, 0f, verticla*speed);
        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x ,  boundary.xMin , boundary.xMax),
            0.6f,
            Mathf.Clamp(rigidbody.position.z , boundary.zMin , boundary.zMax)
        );
        transform.rotation = Quaternion.Euler(0, 180, horizontal * tilt);
        */
    }



    //public void fire()
    //{
    //    GameObject go = Instantiate(bulletPre, bulletPos.position, transform.rotation) as GameObject;
        
    //}

    void OnTriggerEnter(Collider other)
    {

        CallMethod("OnTriggerEnter", other);
      /*  if (other.gameObject.tag == "Blocke")
        {
            this.lifeValue -= 1;
            if (this.lifeValue <= 0)
            {
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);

        }
        print("Collision Tag : " + other.gameObject.tag);*/
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 0, 500, 50), "当前生命值 ： " + this.lifeValue);
    }
}
