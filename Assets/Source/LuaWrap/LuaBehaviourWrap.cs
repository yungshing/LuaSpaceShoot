using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class LuaBehaviourWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("Init", Init),
			new LuaMethod("New", _CreateLuaBehaviour),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("moduleName", get_moduleName, set_moduleName),
		};

		LuaScriptMgr.RegisterLib(L, "LuaBehaviour", typeof(LuaBehaviour), regs, fields, typeof(BehaviourBase));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaBehaviour(IntPtr L)
	{
		LuaDLL.luaL_error(L, "LuaBehaviour class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(LuaBehaviour);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_moduleName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaBehaviour obj = (LuaBehaviour)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moduleName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moduleName on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.moduleName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_moduleName(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		LuaBehaviour obj = (LuaBehaviour)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name moduleName");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index moduleName on a nil value");
			}
		}

		obj.moduleName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		LuaBehaviour obj = (LuaBehaviour)LuaScriptMgr.GetUnityObjectSelf(L, 1, "LuaBehaviour");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.Init(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetLuaObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetLuaObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

