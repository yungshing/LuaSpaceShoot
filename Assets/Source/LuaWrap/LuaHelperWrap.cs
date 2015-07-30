using System;
using LuaInterface;

public class LuaHelperWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("GetResManager", GetResManager),
			new LuaMethod("GetObjManager", GetObjManager),
			new LuaMethod("GetAudioManager", GetAudioManager),
			new LuaMethod("New", _CreateLuaHelper),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
		};

		LuaScriptMgr.RegisterLib(L, "LuaHelper", typeof(LuaHelper), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateLuaHelper(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			LuaHelper obj = new LuaHelper();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: LuaHelper.New");
		}

		return 0;
	}

	static Type classType = typeof(LuaHelper);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetResManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ResManager o = LuaHelper.GetResManager();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObjManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		ObjManager o = LuaHelper.GetObjManager();
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAudioManager(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		AudioManager o = LuaHelper.GetAudioManager();
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

