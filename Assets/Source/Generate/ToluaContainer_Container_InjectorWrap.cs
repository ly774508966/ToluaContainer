﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ToluaContainer_Container_InjectorWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ToluaContainer.Container.Injector), typeof(System.Object));
		L.RegFunction("Resolve", Resolve);
		L.RegFunction("ResolveAll", ResolveAll);
		L.RegFunction("Inject", Inject);
		L.RegFunction("New", _CreateToluaContainer_Container_Injector);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("cache", get_cache, null);
		L.RegVar("binder", get_binder, null);
		L.RegVar("resolutionMode", get_resolutionMode, set_resolutionMode);
		L.RegVar("beforeResolve", get_beforeResolve, set_beforeResolve);
		L.RegVar("afterResolve", get_afterResolve, set_afterResolve);
		L.RegVar("beforeDefaultInstantiate", get_beforeDefaultInstantiate, set_beforeDefaultInstantiate);
		L.RegVar("afterInstantiate", get_afterInstantiate, set_afterInstantiate);
		L.RegVar("beforeInject", get_beforeInject, set_beforeInject);
		L.RegVar("afterInject", get_afterInject, set_afterInject);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateToluaContainer_Container_Injector(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3)
			{
				ToluaContainer.Container.IReflectionCache arg0 = (ToluaContainer.Container.IReflectionCache)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.IReflectionCache));
				ToluaContainer.Container.IBinder arg1 = (ToluaContainer.Container.IBinder)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.IBinder));
				ToluaContainer.Container.ResolutionMode arg2 = (ToluaContainer.Container.ResolutionMode)ToLua.CheckObject(L, 3, typeof(ToluaContainer.Container.ResolutionMode));
				ToluaContainer.Container.Injector obj = new ToluaContainer.Container.Injector(arg0, arg1, arg2);
				ToLua.PushObject(L, obj);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to ctor method: ToluaContainer.Container.Injector.New");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Resolve(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Injector), typeof(System.Type)))
			{
				ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				object o = obj.Resolve(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 2 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Injector), typeof(object)))
			{
				ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.ToObject(L, 1);
				object arg0 = ToLua.ToVarObject(L, 2);
				object o = obj.Resolve(arg0);
				ToLua.Push(L, o);
				return 1;
			}
			else if (count == 3 && TypeChecker.CheckTypes(L, 1, typeof(ToluaContainer.Container.Injector), typeof(System.Type), typeof(object)))
			{
				ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.ToObject(L, 1);
				System.Type arg0 = (System.Type)ToLua.ToObject(L, 2);
				object arg1 = ToLua.ToVarObject(L, 3);
				object o = obj.Resolve(arg0, arg1);
				ToLua.Push(L, o);
				return 1;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: ToluaContainer.Container.Injector.Resolve");
			}
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResolveAll(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			System.Type arg0 = (System.Type)ToLua.CheckObject(L, 2, typeof(System.Type));
			object[] o = obj.ResolveAll(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Inject(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			object arg0 = ToLua.ToVarObject(L, 2);
			object o = obj.Inject(arg0);
			ToLua.Push(L, o);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cache(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)o;
			ToluaContainer.Container.IReflectionCache ret = obj.cache;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index cache on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_binder(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)o;
			ToluaContainer.Container.IBinder ret = obj.binder;
			ToLua.PushObject(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index binder on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_resolutionMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)o;
			ToluaContainer.Container.ResolutionMode ret = obj.resolutionMode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index resolutionMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_beforeResolve(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.beforeResolve"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_afterResolve(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.afterResolve"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_beforeDefaultInstantiate(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.beforeDefaultInstantiate"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_afterInstantiate(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.afterInstantiate"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_beforeInject(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.beforeInject"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_afterInject(IntPtr L)
	{
		ToLua.Push(L, new EventObject("ToluaContainer.Container.Injector.afterInject"));
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_resolutionMode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)o;
			ToluaContainer.Container.ResolutionMode arg0 = (ToluaContainer.Container.ResolutionMode)ToLua.CheckObject(L, 2, typeof(ToluaContainer.Container.ResolutionMode));
			obj.resolutionMode = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o == null ? "attempt to index resolutionMode on a nil value" : e.Message);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_beforeResolve(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.beforeResolve' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.TypeResolutionHandler ev = (ToluaContainer.Container.TypeResolutionHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.TypeResolutionHandler), arg0.func);
				obj.beforeResolve += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.TypeResolutionHandler ev = (ToluaContainer.Container.TypeResolutionHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "beforeResolve");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.TypeResolutionHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.beforeResolve -= ev;
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
	static int set_afterResolve(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.afterResolve' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.TypeResolutionHandler ev = (ToluaContainer.Container.TypeResolutionHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.TypeResolutionHandler), arg0.func);
				obj.afterResolve += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.TypeResolutionHandler ev = (ToluaContainer.Container.TypeResolutionHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "afterResolve");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.TypeResolutionHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.afterResolve -= ev;
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
	static int set_beforeDefaultInstantiate(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.beforeDefaultInstantiate' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingEvaluationHandler ev = (ToluaContainer.Container.BindingEvaluationHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingEvaluationHandler), arg0.func);
				obj.beforeDefaultInstantiate += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingEvaluationHandler ev = (ToluaContainer.Container.BindingEvaluationHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "beforeDefaultInstantiate");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingEvaluationHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.beforeDefaultInstantiate -= ev;
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
	static int set_afterInstantiate(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.afterInstantiate' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.BindingResolutionHandler ev = (ToluaContainer.Container.BindingResolutionHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.BindingResolutionHandler), arg0.func);
				obj.afterInstantiate += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.BindingResolutionHandler ev = (ToluaContainer.Container.BindingResolutionHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "afterInstantiate");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.BindingResolutionHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.afterInstantiate -= ev;
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
	static int set_beforeInject(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.beforeInject' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.InstanceInjectionHandler ev = (ToluaContainer.Container.InstanceInjectionHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.InstanceInjectionHandler), arg0.func);
				obj.beforeInject += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.InstanceInjectionHandler ev = (ToluaContainer.Container.InstanceInjectionHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "beforeInject");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.InstanceInjectionHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.beforeInject -= ev;
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
	static int set_afterInject(IntPtr L)
	{
		try
		{
			ToluaContainer.Container.Injector obj = (ToluaContainer.Container.Injector)ToLua.CheckObject(L, 1, typeof(ToluaContainer.Container.Injector));
			EventObject arg0 = null;

			if (LuaDLL.lua_isuserdata(L, 2) != 0)
			{
				arg0 = (EventObject)ToLua.ToObject(L, 2);
			}
			else
			{
				return LuaDLL.luaL_throw(L, "The event 'ToluaContainer.Container.Injector.afterInject' can only appear on the left hand side of += or -= when used outside of the type 'ToluaContainer.Container.Injector'");
			}

			if (arg0.op == EventOp.Add)
			{
				ToluaContainer.Container.InstanceInjectionHandler ev = (ToluaContainer.Container.InstanceInjectionHandler)DelegateFactory.CreateDelegate(typeof(ToluaContainer.Container.InstanceInjectionHandler), arg0.func);
				obj.afterInject += ev;
			}
			else if (arg0.op == EventOp.Sub)
			{
				ToluaContainer.Container.InstanceInjectionHandler ev = (ToluaContainer.Container.InstanceInjectionHandler)LuaMisc.GetEventHandler(obj, typeof(ToluaContainer.Container.Injector), "afterInject");
				Delegate[] ds = ev.GetInvocationList();
				LuaState state = LuaState.Get(L);

				for (int i = 0; i < ds.Length; i++)
				{
					ev = (ToluaContainer.Container.InstanceInjectionHandler)ds[i];
					LuaDelegate ld = ev.Target as LuaDelegate;

					if (ld != null && ld.func == arg0.func)
					{
						obj.afterInject -= ev;
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

