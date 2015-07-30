using System;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class AudioManagerWrap
{
	public static void Register(IntPtr L)
	{
		LuaMethod[] regs = new LuaMethod[]
		{
			new LuaMethod("playAudio", playAudio),
			new LuaMethod("New", _CreateAudioManager),
			new LuaMethod("GetClassType", GetClassType),
			new LuaMethod("__eq", Lua_Eq),
		};

		LuaField[] fields = new LuaField[]
		{
			new LuaField("audios", get_audios, set_audios),
			new LuaField("instance", get_instance, set_instance),
		};

		LuaScriptMgr.RegisterLib(L, "AudioManager", typeof(AudioManager), regs, fields, typeof(MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateAudioManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "AudioManager class does not have a constructor function");
		return 0;
	}

	static Type classType = typeof(AudioManager);

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, classType);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_audios(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name audios");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index audios on a nil value");
			}
		}

		LuaScriptMgr.PushObject(L, obj.audios);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_instance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instance on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.instance);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_audios(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name audios");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index audios on a nil value");
			}
		}

		obj.audios = (Dictionary<string,AudioClip>)LuaScriptMgr.GetNetObject(L, 3, typeof(Dictionary<string,AudioClip>));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_instance(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		AudioManager obj = (AudioManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name instance");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index instance on a nil value");
			}
		}

		obj.instance = (AudioManager)LuaScriptMgr.GetUnityObject(L, 3, typeof(AudioManager));
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int playAudio(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		AudioManager obj = (AudioManager)LuaScriptMgr.GetUnityObjectSelf(L, 1, "AudioManager");
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.playAudio(arg0);
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

