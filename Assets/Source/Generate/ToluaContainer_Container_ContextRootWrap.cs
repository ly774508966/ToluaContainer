﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToluaContainer_Container_ContextRootWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ToluaContainer.Container.ContextRoot), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("AddContainer", AddContainer);
		L.RegFunction("Dispose", Dispose);
		L.RegFunction("SetupContainers", SetupContainers);
		L.RegFunction("Init", Init);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("injectionType", get_injectionType, set_injectionType);
		L.RegVar("baseBehaviourTypeName", get_baseBehaviourTypeName, set_baseBehaviourTypeName);
		L.RegVar("containers", get_containers, set_containers);
		L.RegVar("containersDic", get_containersDic, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddContainer(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.ContextRoot), typeof(ToluaContainer.Container.IInjectionContainer)))
			{
				ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)ToLua.ToObject(L, 1);
				ToluaContainer.Container.IInjectionContainer arg0 = (ToluaContainer.Container.IInjectionContainer)ToLua.ToObject(L, 2);
				ToluaContainer.Container.IInjectionContainer o = obj.AddContainer(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.ContextRoot), typeof(ToluaContainer.Container.IInjectionContainer), typeof(bool)))
			{
				ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)ToLua.ToObject(L, 1);
				ToluaContainer.Container.IInjectionContainer arg0 = (ToluaContainer.Container.IInjectionContainer)ToLua.ToObject(L, 2);
				bool arg1 = LuaDLL.lua_toboolean(L, 3);
				ToluaContainer.Container.IInjectionContainer o = obj.AddContainer(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ToluaContainer.Container.ContextRoot.AddContainer");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Dispose(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.ContextRoot));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.Dispose(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetupContainers(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.ContextRoot));
			obj.SetupContainers();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.ContextRoot));
			obj.Init();
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_injectionType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)o;
			ToluaContainer.Container.ContextRoot.MonoBehaviourInjectionType ret = obj.injectionType;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index injectionType on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_baseBehaviourTypeName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)o;
			string ret = obj.baseBehaviourTypeName;
			LuaDLL.lua_pushstring(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index baseBehaviourTypeName on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_containers(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, ToluaContainer.Container.ContextRoot.containers);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_containersDic(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, ToluaContainer.Container.ContextRoot.containersDic);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_injectionType(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)o;
			ToluaContainer.Container.ContextRoot.MonoBehaviourInjectionType arg0 = (ToluaContainer.Container.ContextRoot.MonoBehaviourInjectionType)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.ContextRoot.MonoBehaviourInjectionType));
			obj.injectionType = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index injectionType on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_baseBehaviourTypeName(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.ContextRoot obj = (ToluaContainer.Container.ContextRoot)o;
			string arg0 = ToLua.CheckString(L, 2);
			obj.baseBehaviourTypeName = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index baseBehaviourTypeName on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_containers(IntPtr L)
	{
		try
		{
			System.Collections.Generic.List<ToluaContainer.Container.IInjectionContainer> arg0 = (System.Collections.Generic.List<ToluaContainer.Container.IInjectionContainer>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<ToluaContainer.Container.IInjectionContainer>));
			ToluaContainer.Container.ContextRoot.containers = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

