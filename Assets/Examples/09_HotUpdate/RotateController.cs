using UnityEngine;
using ToluaContainer.Container;
using LuaInterface;

namespace ToluaContainer.Examples.HotUpdate
{
    public class RotateController : MonoBehaviour
    {
        [Inject]
        public Transform objectToRotate;
        [Inject]
        public AssetBundleInfo asi;

        public LuaState lua { get; private set; }
        LuaFunction func = null;
        string luaText;

        void Start()
        {
            // 为自身带有 [Inject] 特性的成员进行注入
            this.Inject();
            lua = new LuaState();
            lua.Start();
            LuaBinder.Bind(lua);

            // 如果移动了目录，请自行调整为相应路径
            luaText = asi.asetBundle.LoadAllAssets()[0].ToString();
            lua.DoString(luaText);
            func = lua.GetFunction("Rotator");
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

        private void OnApplicationQuit()
        {
            lua.Dispose();
            lua = null;
        }
    }
}
