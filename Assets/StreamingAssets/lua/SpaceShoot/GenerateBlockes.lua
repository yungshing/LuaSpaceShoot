require "system/Math"
require "common/define"
--不断生成障碍物实现

GenerateBlockes = {};
local this = GenerateBlockes;


local Time = UnityEngine.Time;
local Object = UnityEngine.Object;

local interval = 2;
local _interval = 0;

function this.Start()
	_interval = interval;
end

function this.FixedUpdate()

	_interval = _interval - Time.deltaTime;
	if(_interval <= 0 )then
		vect = Vector3.New(math.Random(-6,6),1,16);
		go = objMgr:CreateObjByBundle("blockecubepreb");
		go.transform.position = vect;
		_interval = interval;
	end
end
