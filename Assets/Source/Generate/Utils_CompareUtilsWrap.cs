﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class Utils_CompareUtilsWrap
{
	public static void Register(LuaState L)
	{
		L.BeginStaticLibs("CompareUtils");
		L.RegFunction("isSameValueIList", isSameValueIList);
		L.RegFunction("isSameValueArray", isSameValueArray);
		L.RegFunction("isSameObject", isSameObject);
		L.EndStaticLibs();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isSameValueIList(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			System.Collections.Generic.IList<object> arg0 = (System.Collections.Generic.IList<object>)ToLua.CheckObject(L, 1, typeof(System.Collections.Generic.IList<object>));
			System.Collections.Generic.IList<object> arg1 = (System.Collections.Generic.IList<object>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.IList<object>));
			bool o = Utils.CompareUtils.isSameValueIList(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isSameValueArray(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			object[] arg0 = ToLua.CheckObjectArray(L, 1);
			object[] arg1 = ToLua.CheckObjectArray(L, 2);
			bool o = Utils.CompareUtils.isSameValueArray(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int isSameObject(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			object arg0 = ToLua.ToVarObject(L, 1);
			object arg1 = ToLua.ToVarObject(L, 2);
			bool o = Utils.CompareUtils.isSameObject(arg0, arg1);
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
