BoundaryCube = {}

local this = BoundaryCube;
--游戏中的边界碰撞检测逻辑（子弹或者Cube离开游戏界面是销毁功能
function this.OnTriggerExit(other)
	print("OnTriggerExit(other");
	this.gameObject.Destroy(other.gameObject);
end
