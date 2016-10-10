﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Utils_TypeUtilsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(Utils.TypeUtils), typeof(System.Object));
		L.RegFunction("IsAssignable", IsAssignable);
		L.RegFunction("GetAssignableTypes", GetAssignableTypes);
		L.RegFunction("GetType", GetType);
		L.RegFunction("New", _CreateUtils_TypeUtils);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUtils_TypeUtils(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				Utils.TypeUtils obj = new Utils.TypeUtils();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: Utils.TypeUtils.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IsAssignable(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 1, typeof(System.Type));
			System.Type arg1 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			bool o = Utils.TypeUtils.IsAssignable(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAssignableTypes(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(System.Type)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				System.Type[] o = Utils.TypeUtils.GetAssignableTypes(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(System.Type), typeof(string)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				System.Type[] o = Utils.TypeUtils.GetAssignableTypes(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(System.Type), typeof(string), typeof(bool)))
			{
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				bool arg2 = LuaDLL.lua_toboolean(L, 3);
				System.Type[] o = Utils.TypeUtils.GetAssignableTypes(arg0, arg1, arg2);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Utils.TypeUtils.GetAssignableTypes");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetType(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(Utils.TypeUtils)))
			{
				Utils.TypeUtils obj = (Utils.TypeUtils)ToLua.ToObject(L, 1);
				System.Type o = obj.GetType();
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 1 && TypeChecker.CheckTypes(L, 1, typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				System.Type o = Utils.TypeUtils.GetType(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(string), typeof(string)))
			{
				string arg0 = ToLua.ToString(L, 1);
				string arg1 = ToLua.ToString(L, 2);
				System.Type o = Utils.TypeUtils.GetType(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: Utils.TypeUtils.GetType");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

