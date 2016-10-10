﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToluaContainer_Container_PrefabInfoWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ToluaContainer.Container.PrefabInfo), typeof(System.Object));
		L.RegFunction("GetCoroutineObject", GetCoroutineObject);
		L.RegFunction("GetAsyncObject", GetAsyncObject);
		L.RegFunction("UnloadAsset", UnloadAsset);
		L.RegFunction("New", _CreateToluaContainer_Container_PrefabInfo);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("prefab", get_prefab, null);
		L.RegVar("type", get_type, set_type);
		L.RegVar("path", get_path, set_path);
		L.RegVar("useCount", get_useCount, set_useCount);
		L.RegVar("isLoaded", get_isLoaded, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateToluaContainer_Container_PrefabInfo(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				string arg0 = ToLua.CheckString(L, 1);
				System.Type arg1 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
				ToluaContainer.Container.PrefabInfo obj = new ToluaContainer.Container.PrefabInfo(arg0, arg1);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(UnityEngine.Object), typeof(string), typeof(System.Type)))
			{
				UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.CheckUnityObject(L, 1, typeof(UnityEngine.Object));
				string arg1 = ToLua.CheckString(L, 2);
				System.Type arg2 = (System.Type)ToLua.CheckObject(L, 3, typeof(System.Type));
				ToluaContainer.Container.PrefabInfo obj = new ToluaContainer.Container.PrefabInfo(arg0, arg1, arg2);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: ToluaContainer.Container.PrefabInfo.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCoroutineObject(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.PrefabInfo));
			System.Action<UnityEngine.Object> arg0 = null;
			LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

			if (funcType2 != LuaTypes.LUA_TFUNCTION)
			{
				 arg0 = (System.Action<UnityEngine.Object>)ToLua.CheckObject(L, 2, typeof(System.Action<UnityEngine.Object>));
			}
			else
			{
				LuaFunction func = ToLua.ToLuaFunction(L, 2);
				arg0 = DelegateFactory.CreateDelegate(typeof(System.Action<UnityEngine.Object>), func) as System.Action<UnityEngine.Object>;
			}

			System.Collections.IEnumerator o = obj.GetCoroutineObject(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAsyncObject(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.PrefabInfo), typeof(System.Action<UnityEngine.Object>)))
			{
				ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)ToLua.ToObject(L, 1);
				System.Action<UnityEngine.Object> arg0 = null;
				LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

				if (funcType2 != LuaTypes.LUA_TFUNCTION)
				{
					 arg0 = (System.Action<UnityEngine.Object>)ToLua.ToObject(L, 2);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 2);
					arg0 = DelegateFactory.CreateDelegate(typeof(System.Action<UnityEngine.Object>), func) as System.Action<UnityEngine.Object>;
				}

				System.Collections.IEnumerator o = obj.GetAsyncObject(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.PrefabInfo), typeof(System.Action<UnityEngine.Object>), typeof(System.Action<float>)))
			{
				ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)ToLua.ToObject(L, 1);
				System.Action<UnityEngine.Object> arg0 = null;
				LuaTypes funcType2 = LuaDLL.lua_type(L, 2);

				if (funcType2 != LuaTypes.LUA_TFUNCTION)
				{
					 arg0 = (System.Action<UnityEngine.Object>)ToLua.ToObject(L, 2);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 2);
					arg0 = DelegateFactory.CreateDelegate(typeof(System.Action<UnityEngine.Object>), func) as System.Action<UnityEngine.Object>;
				}

				System.Action<float> arg1 = null;
				LuaTypes funcType3 = LuaDLL.lua_type(L, 3);

				if (funcType3 != LuaTypes.LUA_TFUNCTION)
				{
					 arg1 = (System.Action<float>)ToLua.ToObject(L, 3);
				}
				else
				{
					LuaFunction func = ToLua.ToLuaFunction(L, 3);
					arg1 = DelegateFactory.CreateDelegate(typeof(System.Action<float>), func) as System.Action<float>;
				}

				System.Collections.IEnumerator o = obj.GetAsyncObject(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ToluaContainer.Container.PrefabInfo.GetAsyncObject");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadAsset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.PrefabInfo));
			obj.UnloadAsset();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_prefab(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			UnityEngine.Object ret = obj.prefab;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index prefab on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_type(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			System.Type ret = obj.type;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index type on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_path(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			string ret = obj.path;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index path on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_useCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			int ret = obj.useCount;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useCount on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isLoaded(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			bool ret = obj.isLoaded;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index isLoaded on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_type(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			obj.type = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index type on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_path(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.path = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index path on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_useCount(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.PrefabInfo obj = (ToluaContainer.Container.PrefabInfo)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.useCount = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index useCount on a nil value" : e.Message);
		}
	}
}

