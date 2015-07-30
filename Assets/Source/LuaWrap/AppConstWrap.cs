using System;
using UnityEngine;
using LuaInterface;

public class AppConstWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("New", _CreateAppConst),
			new LuaMethod("GetClassType", GetClassType),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("UsePbc", get_UsePbc, set_UsePbc),
			new LuaField("UseLpeg", get_UseLpeg, set_UseLpeg),
			new LuaField("UsePbLua", get_UsePbLua, set_UsePbLua),
			new LuaField("UseCJson", get_UseCJson, set_UseCJson),
			new LuaField("UseSproto", get_UseSproto, set_UseSproto),
			new LuaField("LuaEncode", get_LuaEncode, set_LuaEncode),
			new LuaField("DebugMode", get_DebugMode, set_DebugMode),
			new LuaField("UpdateMode", get_UpdateMode, set_UpdateMode),
			new LuaField("TimerInterval", get_TimerInterval, set_TimerInterval),
			new LuaField("GameFrameRate", get_GameFrameRate, set_GameFrameRate),
			new LuaField("luaScripts", get_luaScripts, set_luaScripts),
			new LuaField("UserId", get_UserId, set_UserId),
			new LuaField("AppName", get_AppName, set_AppName),
			new LuaField("AppPrefix", get_AppPrefix, set_AppPrefix),
			new LuaField("WebUrl", get_WebUrl, set_WebUrl),
			new LuaField("SocketPort", get_SocketPort, set_SocketPort),
			new LuaField("SocketAddress", get_SocketAddress, set_SocketAddress),
		};

		LuaScriptMgr.RegisterLib(L, "AppConst", typeof(AppConst), regs, fields, typeof(object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAppConst(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			AppConst obj = new AppConst();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: AppConst.New");
		}

		return 0;
	}

	static Type classType = typeof(AppConst);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UsePbc(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UsePbc);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseLpeg(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UseLpeg);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UsePbLua(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UsePbLua);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseCJson(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UseCJson);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UseSproto(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UseSproto);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_LuaEncode(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.LuaEncode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DebugMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.DebugMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UpdateMode(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UpdateMode);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TimerInterval(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.TimerInterval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_GameFrameRate(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.GameFrameRate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_luaScripts(IntPtr L)
	{
		LuaScriptMgr.PushArray(L, AppConst.luaScripts);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_UserId(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.UserId);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppName(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.AppName);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_AppPrefix(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.AppPrefix);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_WebUrl(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.WebUrl);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketPort(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.SocketPort);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_SocketAddress(IntPtr L)
	{
		LuaScriptMgr.Push(L, AppConst.SocketAddress);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UsePbc(IntPtr L)
	{
		AppConst.UsePbc = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UseLpeg(IntPtr L)
	{
		AppConst.UseLpeg = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UsePbLua(IntPtr L)
	{
		AppConst.UsePbLua = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UseCJson(IntPtr L)
	{
		AppConst.UseCJson = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UseSproto(IntPtr L)
	{
		AppConst.UseSproto = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_LuaEncode(IntPtr L)
	{
		AppConst.LuaEncode = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DebugMode(IntPtr L)
	{
		AppConst.DebugMode = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UpdateMode(IntPtr L)
	{
		AppConst.UpdateMode = LuaScriptMgr.GetBoolean(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_TimerInterval(IntPtr L)
	{
		AppConst.TimerInterval = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_GameFrameRate(IntPtr L)
	{
		AppConst.GameFrameRate = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_luaScripts(IntPtr L)
	{
		AppConst.luaScripts = LuaScriptMgr.GetArrayObject<TextAsset>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_UserId(IntPtr L)
	{
		AppConst.UserId = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AppName(IntPtr L)
	{
		AppConst.AppName = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_AppPrefix(IntPtr L)
	{
		AppConst.AppPrefix = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_WebUrl(IntPtr L)
	{
		AppConst.WebUrl = LuaScriptMgr.GetString(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketPort(IntPtr L)
	{
		AppConst.SocketPort = (int)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_SocketAddress(IntPtr L)
	{
		AppConst.SocketAddress = LuaScriptMgr.GetString(L, 3);
		return 0;
	}
}

