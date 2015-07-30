require "Common/functions"
require "Common/define"

ObjManager = {};
local this = ObjManager;

function this.InitObj()
	print("========ObjManger initObj=========");
	this.CreatePlane();
	this.CreateBoundary();
	this.CreateGenerateCube()

end

--调用C#层ObjMnager去实例化需要加载的bundle对象

--游戏中的飞机
function this.CreatePlane()
	print("========ObjManger createPlane=========");
	objMgr:CreateObjByBundle("planepreb");
	--local go = objMgr:AddLuaComponent(go , "");
end


--游戏中的边界检测对象
function this.CreateBoundary()
	print("========ObjManger CreateBoundary=========");
	objMgr:CreateObjByBundle("BoundaryPreb");
end

--游戏中不断生成Cube的bundle
function this.CreateGenerateCube()
	print("========ObjManger CreateGenerateCube()=========");
	objMgr:CreateObjByBundle("gamecontroller");
end
