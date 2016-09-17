using UnityEngine;
using LuaInterface;

namespace ToluaContainer.Examples.UsingConditions
{
    public class RotateController : MonoBehaviour
    {
        [Inject("LeftCube")]
        public Transform LeftCube;
        [Inject("RightCube")]
        public Transform RightCube;

        public LuaState lua { get; private set; }
        LuaFunction func = null;

        void Start()
        {
            lua = new LuaState();
            lua.Start();
            LuaBinder.Bind(lua);
            // 获取 RotateController 的 lua 属性，并将其设置给 LuaLooper 的 luaState
            SetUpLuaLooper();

            // 如果移动了目录，请自行调整为相应路径
            string fullPath = Application.dataPath + "\\Examples/03_UsingConditions";
            lua.AddSearchPath(fullPath);
            lua.Require("LuaCoroutineRotator");
            LuaFunction setTrs = lua.GetFunction("SetTransform");

            // 将 RightCube 传入 lua
            setTrs.BeginPCall();
            setTrs.Push(RightCube);
            setTrs.PCall();
            setTrs.EndPCall();

            func = lua.GetFunction("StartDelay");

            // 调用 lua 方法，由 LuaLooper 来协调 lua 脚本进行 Coroutine 使 RightCube 旋转
            CallFunc();
        }

        void Update()
        {
            // 在 c# 中使 LeftCube 旋转
            LeftCube.Rotate(1.0f, 1.0f, 1.0f);
        }

        /// <summary>
        /// 设置 LuaLooper 的 luaState
        /// </summary>
        void SetUpLuaLooper()
        {
            LuaLooper looper = UsingConditionsRoot.containers[0].GetTypes<LuaLooper>()[0].value as LuaLooper;
            looper.luaState = lua;
        }

        /// <summary>
        /// 调用 lua 函数而不产生 GC 的方式
        /// </summary>
        void CallFunc()
        {
            func.BeginPCall();
            func.Push();
            func.PCall();
            func.EndPCall();
        }
    }
}

