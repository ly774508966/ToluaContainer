using UnityEngine;
using ToluaContainer.Container;
using LuaInterface;

namespace ToluaContainer.Examples.BindingGameObjects
{
    public class RotateController : MonoBehaviour
    {
        [Inject]
        public Transform objectToRotate;
        LuaState lua = null;
        LuaFunction func = null;

        void Start()
        {
            // 为自身带有 [Inject] 特性的成员进行注入
            this.Inject();
            lua = new LuaState();
            lua.Start();
            LuaBinder.Bind(lua);

            // 如果移动了目录，请自行调整为相应路径
            string fullPath = Application.dataPath + "\\Files/lua/Examples/02_BindingGameObjects";
            lua.AddSearchPath(fullPath);
            lua.Require("LuaRotator");
            func = lua.GetFunction("test.Func");
        }

        void Update()
        {
            CallFunc();
        }

        /// <summary>
        /// 调用 lua 函数而不产生 GC 的方式
        /// </summary>
        void CallFunc()
        {
            func.BeginPCall();
            func.Push(objectToRotate);
            func.PCall();
            func.EndPCall();
        }
    }
}
