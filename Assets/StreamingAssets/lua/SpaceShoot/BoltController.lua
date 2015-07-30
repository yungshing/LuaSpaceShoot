require "common/define"
BoltController = {};
local this = BoltController;

--初始化时给子弹一个向上的出速度
function BoltController.Start()
	this.curInstance.rigidbody.velocity = Vector3.forward * 30;
end

--子弹的碰撞检测逻辑
function BoltController.OnTriggerEnter(this , other)
	print("function BoltController.OnTriggerEnter(this , other)" .. other.tag);
	if(other.tag=="Blocke")then
		audioMgr:playAudio("Explode");
		obj = objMgr:CreateObjByBundle("ArtilleryStrike");
		obj.transform.position = other.gameObject.transform.position;
		obj.transform.localScale = Vector3.New(0.1,0.1,0.1);
		this.gameObject.Destroy(obj , 1);
		this.gameObject.Destroy(other.gameObject)
		this.gameObject.Destroy(this.gameObject);
	end
end
