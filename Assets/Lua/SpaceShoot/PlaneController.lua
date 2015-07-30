require "common/functions"
require "system/Math"
require "system/Quaternion"
require "common/define"

PlaneController = {position};


local this =  PlaneController;


local fireRate = 0.25;
local nextFire = 0;
local speed = 10;
local tilt = 45;
local boundary={xMin = -6.4 , xMax =  6.6 , zMin = -2.5 , zMax = 15.5};

local Input = UnityEngine.Input;
local Time = UnityEngine.Time;
local Object = UnityEngine.Object;

function this.Awake()
	print("Awake");
end


function this.Start()
	print("PlaneController.Start()");
end

--发射子弹
function PlaneController.Update()
	  nextFire = Time.deltaTime + nextFire;
	if (Input.GetButton("Fire1") and nextFire > fireRate)then
			audioMgr:playAudio("Shoot");
			Object.Instantiate(this.bulletPre, this.bulletPos.position, this.transform.rotation);
			nextFire = 0;

	end
end
--飞机移动
function this.FixedUpdate()
	--print("====FixedUpdate======");
	local horizontal = Input.GetAxis("Horizontal");
	local verticla = Input.GetAxis("Vertical");
	this.curInstance.rigidbody.velocity = Vector3.New(horizontal*speed , 0 , verticla*speed);

	this.curInstance.rigidbody.position = Vector3.New(math.clamp(this.curInstance.rigidbody.position.x ,  boundary.xMin , boundary.xMax), 0.6 , math.clamp(this.curInstance.rigidbody.position.z , boundary.zMin , boundary.zMax));

	this.transform.rotation = Quaternion.Euler(0, 180, horizontal * tilt);
end
--飞机碰撞到Cube
function this.OnTriggerEnter(other)
	print("unction this.OnTriggerEnter(other)");
	if (other.gameObject.tag == "Blocke")then
		this.curInstance.lifeValue = this.curInstance.lifeValue-1;
		if (this.curInstance.lifeValue <= 0)then
				--Time.timeScale = 0;
                this.gameObject.Destroy(this.gameObject);
				obj = objMgr:CreateObjByBundle("ArtilleryStrike");
				obj.transform.position = this.transform.position;
				this.gameObject.Destroy(obj , 1);
		end
		this.gameObject.Destroy(other.gameObject);
	end
end



function this.OnDestroy()
	print("PlaneController.Destroy()");


end

