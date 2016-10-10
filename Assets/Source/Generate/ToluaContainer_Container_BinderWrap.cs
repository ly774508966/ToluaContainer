﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToluaContainer_Container_BinderWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ToluaContainer.Container.Binder), typeof(System.Object));
		L.RegFunction("Bind", Bind);
		L.RegFunction("BindSingleton", BindSingleton);
		L.RegFunction("BindFactory", BindFactory);
		L.RegFunction("BindMultiton", BindMultiton);
		L.RegFunction("MultipleBind", MultipleBind);
		L.RegFunction("GetTypes", GetTypes);
		L.RegFunction("GetIds", GetIds);
		L.RegFunction("GetAll", GetAll);
		L.RegFunction("GetSameNullId", GetSameNullId);
		L.RegFunction("GetBinding", GetBinding);
		L.RegFunction("UnbindType", UnbindType);
		L.RegFunction("UnbindId", UnbindId);
		L.RegFunction("UnbindNullIdType", UnbindNullIdType);
		L.RegFunction("Unbind", Unbind);
		L.RegFunction("RemoveBinding", RemoveBinding);
		L.RegFunction("Storing", Storing);
		L.RegFunction("New", _CreateToluaContainer_Container_Binder);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("beforeAddBinding", get_beforeAddBinding, set_beforeAddBinding);
		L.RegVar("afterAddBinding", get_afterAddBinding, set_afterAddBinding);
		L.RegVar("beforeRemoveBinding", get_beforeRemoveBinding, set_beforeRemoveBinding);
		L.RegVar("afterRemoveBinding", get_afterRemoveBinding, set_afterRemoveBinding);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateToluaContainer_Container_Binder(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 0)
			{
				ToluaContainer.Container.Binder obj = new ToluaContainer.Container.Binder();
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: ToluaContainer.Container.Binder.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Bind(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

            if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Binder), typeof(System.Type)))
			{
				ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				ToluaContainer.Container.IBinding o = obj.Bind(arg0);
				ToLua.PushObject(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Binder), typeof(System.Type), typeof(ToluaContainer.Container.BindingType)))
			{
				ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				ToluaContainer.Container.BindingType arg1 = (ToluaContainer.Container.BindingType)ToLua.ToObject(L, 3);
				ToluaContainer.Container.IBinding o = obj.Bind(arg0, arg1);
				ToLua.PushObject(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ToluaContainer.Container.Binder.Bind");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BindSingleton(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			ToluaContainer.Container.IBinding o = obj.BindSingleton(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BindFactory(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			ToluaContainer.Container.IBinding o = obj.BindFactory(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BindMultiton(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			ToluaContainer.Container.IBinding o = obj.BindMultiton(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int MultipleBind(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type[] arg0 = ToLua.CheckObjectArray<System.Type>(L, 2);
			ToluaContainer.Container.BindingType[] arg1 = ToLua.CheckObjectArray<ToluaContainer.Container.BindingType>(L, 3);
			ToluaContainer.Container.IBindingFactory o = obj.MultipleBind(arg0, arg1);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetTypes(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			System.Collections.Generic.IList<ToluaContainer.Container.IBinding> o = obj.GetTypes(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetIds(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			object arg0 = ToLua.ToVarObject(L, 2);
			System.Collections.Generic.IList<ToluaContainer.Container.IBinding> o = obj.GetIds(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Collections.Generic.IList<ToluaContainer.Container.IBinding> o = obj.GetAll();
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSameNullId(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			ToluaContainer.Container.IBinding arg0 = (ToluaContainer.Container.IBinding)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.IBinding));
			System.Collections.Generic.IList<ToluaContainer.Container.IBinding> o = obj.GetSameNullId(arg0);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBinding(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			object arg1 = ToLua.ToVarObject(L, 3);
			ToluaContainer.Container.IBinding o = obj.GetBinding(arg0, arg1);
			ToLua.PushObject(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnbindType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			obj.UnbindType(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnbindId(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			object arg0 = ToLua.ToVarObject(L, 2);
			obj.UnbindId(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnbindNullIdType(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			obj.UnbindNullIdType(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Unbind(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Binder), typeof(ToluaContainer.Container.IBinding)))
			{
				ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.ToObject(L, 1);
				ToluaContainer.Container.IBinding arg0 = (ToluaContainer.Container.IBinding)ToLua.ToObject(L, 2);
				obj.Unbind(arg0);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Binder), typeof(System.Type), typeof(object)))
			{
				ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				obj.Unbind(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ToluaContainer.Container.Binder.Unbind");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveBinding(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			ToluaContainer.Container.IBinding arg0 = (ToluaContainer.Container.IBinding)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.IBinding));
			obj.RemoveBinding(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Storing(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			ToluaContainer.Container.IBinding arg0 = (ToluaContainer.Container.IBinding)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.IBinding));
			obj.Storing(arg0);
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_beforeAddBinding(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Binder.beforeAddBinding"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_afterAddBinding(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Binder.afterAddBinding"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_beforeRemoveBinding(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Binder.beforeRemoveBinding"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_afterRemoveBinding(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Binder.afterRemoveBinding"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_beforeAddBinding(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Binder.beforeAddBinding' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Binder'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingAddedHandler ev = (ToluaContainer.Container.BindingAddedHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingAddedHandler), arg0.func);
				obj.beforeAddBinding += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingAddedHandler ev = (ToluaContainer.Container.BindingAddedHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Binder), "beforeAddBinding");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingAddedHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.beforeAddBinding -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_afterAddBinding(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Binder.afterAddBinding' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Binder'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingAddedHandler ev = (ToluaContainer.Container.BindingAddedHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingAddedHandler), arg0.func);
				obj.afterAddBinding += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingAddedHandler ev = (ToluaContainer.Container.BindingAddedHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Binder), "afterAddBinding");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingAddedHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.afterAddBinding -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_beforeRemoveBinding(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Binder.beforeRemoveBinding' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Binder'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingRemovedHandler ev = (ToluaContainer.Container.BindingRemovedHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingRemovedHandler), arg0.func);
				obj.beforeRemoveBinding += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingRemovedHandler ev = (ToluaContainer.Container.BindingRemovedHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Binder), "beforeRemoveBinding");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingRemovedHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.beforeRemoveBinding -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_afterRemoveBinding(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Binder obj = (ToluaContainer.Container.Binder)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Binder));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Binder.afterRemoveBinding' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Binder'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingRemovedHandler ev = (ToluaContainer.Container.BindingRemovedHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingRemovedHandler), arg0.func);
				obj.afterRemoveBinding += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingRemovedHandler ev = (ToluaContainer.Container.BindingRemovedHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Binder), "afterRemoveBinding");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingRemovedHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.afterRemoveBinding -= ev;
						state.DelayDispose(ld.func);
						break;
					}
				}

				arg0.func.Dispose();
			}

			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}
