require "common/define"
require "common/define"



BlockeController = {}

local this = BlockeController;



local rotate_speed = 45;
local move_speed = 5;

--当C障碍物ube生成时，给它初始线速度和角速度赋值
function this.Start()
	this.curInstance.rigidbody.angularVelocity = Vector3.New(math.Random(0,1),math.Random(0,1),math.Random(0,1))*rotate_speed;

	this.curInstance.rigidbody.velocity = Vector3.back*move_speed;
end


