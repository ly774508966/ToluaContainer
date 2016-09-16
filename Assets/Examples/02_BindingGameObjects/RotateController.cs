/*
 * Copyright (c) 2016 Joey1258
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in all
 *  copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  SOFTWARE.
 */

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
            this.Inject();

            lua = new LuaState();
            lua.Start();
            //如果移动了ToLua目录，自己手动修复吧，只是例子就不做配置了
            string fullPath = Application.dataPath + "\\Examples/02_BindingGameObjects";
            lua.AddSearchPath(fullPath);
            lua.Require("LuaRotator");
            func = lua.GetFunction("test.Func");
            print(func == null);
        }

        void Update()
        {
            //objectToRotate.Rotate(1.0f, 1.0f, 1.0f);
            CallFunc();
        }


        void CallFunc()
        {
            func.BeginPCall();
            func.Push(objectToRotate);
            func.PCall();
            func.EndPCall();
        }
    }
}
