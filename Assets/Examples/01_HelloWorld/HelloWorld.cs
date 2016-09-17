using LuaInterface;

namespace ToluaContainer.Examples.HelloWorld
{
    public class HelloWorld
    {
        LuaState lua = new LuaState();
        string hello = @" print('Tolua#: Hello, world!') ";


        /// <summary>
        /// 输出 "Hello, world!"
        /// </summary>
        public void DisplayHelloWorld()
        {
            lua.Start();
            // 在 c# 端输出
            Debugger.Log("c#: Hello, world!");
            // 在 lua 端输出
            lua.DoString(hello, "HelloWorld.cs");
            lua.CheckTop();
            lua.Dispose();
            lua = null;
        }
    }
}