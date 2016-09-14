/*
Copyright (c) 2015-2016 topameng(topameng@qq.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaInterface
{
    /// <summary>
    /// LuaFunction 信息类 （记录栈相关的信息、实现调用方法以及栈操作方法等）
    /// </summary>
    public class LuaFunction : LuaBaseRef
    {
        /// <summary>
        /// 方法信息 (记录栈顶位置和在栈中的位置) 结构体
        /// </summary>
        protected struct FuncData
        {
            /// <summary>
            /// 栈顶位置
            /// </summary>
            public int oldTop;

            /// <summary>
            /// 在栈中的位置
            /// </summary>
            public int stackPos;

            #region constructor

            public FuncData(int top, int stack)
            {
                oldTop = top;
                stackPos = stack;
            }

            #endregion
        }

        /// <summary>
        /// 栈顶的位置
        /// </summary>
        protected int oldTop = -1;

        /// <summary>
        /// 栈的长度
        /// </summary>
        private int argCount = 0;

        /// <summary>
        /// 在栈中的位置
        /// </summary>
        private int stackPos = -1;

        /// <summary>
        /// FuncData 栈
        /// </summary>
        private Stack<FuncData> stack = new Stack<FuncData>();

        #region constructor

        public LuaFunction(int reference, LuaState state)
        {
            this.reference = reference;
            this.luaState = state;
        }

        #endregion

        /// <summary>
        /// 释放资源 （count 大于 0 才进行释放）
        /// </summary>
        public override void Dispose()
        {
#if UNITY_EDITOR
            if (oldTop != -1 && count <= 1)
            {
                Debugger.LogError("You must call EndPCall before calling Dispose");
            }
#endif
            base.Dispose();
        }

        /// <summary>
        /// 用当前的 oldTop 和 stackPos 构造一个新的是 FuncData 压入栈中，并更改 oldTop、stackPos、argCount的值，返回修改后的 oldTop 的值
        /// </summary>
        public virtual int BeginPCall()
        {
            if (luaState == null)
            {
                throw new LuaException("LuaFunction has been disposed");
            }

            stack.Push(new FuncData(oldTop, stackPos));
            oldTop = luaState.BeginPCall(reference);
            stackPos = -1;
            argCount = 0;
            return oldTop;
        }

        /// <summary>
        /// 更新在栈中的位置 stackPos，再尝试调用 PCall，调用后再 EndPCall
        /// </summary>
        public void PCall()
        {
#if UNITY_EDITOR
            if (oldTop == -1)
            {
                Debugger.LogError("You must call BeginPCall before calling PCall");
            }
#endif

            stackPos = oldTop + 1;

            try
            {
                luaState.PCall(argCount, oldTop);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 结束调用 （如果 oldTop 不等于 -1 就结束栈顶的调用，设 argCount 为 0，将 FuncData 从栈顶推出，更新 oldTop 和 stackPos）
        /// </summary>
        public void EndPCall()
        {
            if (oldTop != -1)
            {
                luaState.EndPCall(oldTop);
                argCount = 0;
                FuncData data = stack.Pop();
                oldTop = data.oldTop;
                stackPos = data.stackPos;
            }
        }

        /// <summary>
        /// 调用 （集成了 BeginPCall、PCall、EndPCall 三个方法按顺序执行）
        /// </summary>
        public void Call()
        {
            BeginPCall();
            PCall();
            EndPCall();
        }

        /// <summary>
        /// 将参数压入再返回？？慎用！！
        /// </summary>
        public object[] Call(params object[] args)
        {
            BeginPCall();
            int count = args == null ? 0 : args.Length;

            if (!luaState.LuaCheckStack(count + 6))
            {
                EndPCall();
                throw new LuaException("stack overflow");
            }

            PushArgs(args);
            PCall();
            object[] objs = luaState.CheckObjects(oldTop);
            EndPCall();
            return objs;
        }

        /// <summary>
        /// 是否开始调用 （通过栈顶是否为 -1 来判断）
        /// </summary>
        public bool IsBegin()
        {
            return oldTop != -1;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(double num)
        {
            luaState.Push(num);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(int n)
        {
            luaState.Push(n);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(uint un)
        {
            luaState.Push(un);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(long num)
        {
            luaState.Push(num);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(ulong un)
        {
            luaState.Push(un);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(bool b)
        {
            luaState.Push(b);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(string str)
        {
            luaState.Push(str);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(IntPtr ptr)
        {
            luaState.Push(ptr);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(LuaBaseRef lbr)
        {
            luaState.Push(lbr);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(object o)
        {
            luaState.Push(o);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(UnityEngine.Object o)
        {
            luaState.Push(o);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Type t)
        {
            luaState.Push(t);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Enum e)
        {
            luaState.Push(e);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Array array)
        {
            luaState.Push(array);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Vector3 v3)
        {
            luaState.Push(v3);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Vector2 v2)
        {
            luaState.Push(v2);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Vector4 v4)
        {
            luaState.Push(v4);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Quaternion quat)
        {
            luaState.Push(quat);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Color clr)
        {
            luaState.Push(clr);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void PushLayerMask(LayerMask mask)
        {
            luaState.PushLayerMask(mask);
            ++argCount;
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Ray ray)
        {
            try
            {
                luaState.Push(ray);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Bounds bounds)
        {
            try
            {
                luaState.Push(bounds);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(RaycastHit hit)
        {
            try
            {
                luaState.Push(hit);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(Touch t)
        {
            try
            {
                luaState.Push(t);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        ///  压入函数调用需要的参数，通过众多的重载函数来解决参数转换gc问题
        /// </summary>
        public void Push(LuaByteBuffer buffer)
        {
            try
            {
                luaState.Push(buffer);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 压入 ValueType Value
        /// </summary>
        public void PushValue(ValueType value)
        {
            luaState.PushValue(value);
            ++argCount;
        }

        /// <summary>
        /// 压入 Object
        /// </summary>
        public void PushObject(object o)
        {
            luaState.PushObject(o);
            ++argCount;
        }

        /// <summary>
        /// 压入 Object[] 将参数传入 luaState.PushArgs 方法调用并更新 argCount
        /// </summary>
        public void PushArgs(object[] args)
        {
            if (args == null)
            {
                return;
            }

            argCount += args.Length;
            luaState.PushArgs(args);
        }

        /// <summary>
        /// 压入 Buffer
        /// </summary>
        public void PushByteBuffer(byte[] buffer)
        {
            try
            {
                luaState.PushByteBuffer(buffer);
                ++argCount;
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值 （lua number类型） （将 stackPos 作为 luaState.LuaCheckNumber 的参数进行调用，并在调用之后使 stackPos 递增，最后返回 luaState.LuaCheckNumber 的结果）
        /// </summary>
        public double CheckNumber()
        {
            try
            {
                return luaState.LuaCheckNumber(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public bool CheckBoolean()
        {
            try
            {
                return luaState.LuaCheckBoolean(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public string CheckString()
        {
            try
            {
                return luaState.CheckString(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Vector3 CheckVector3()
        {
            try
            {
                return luaState.CheckVector3(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Quaternion CheckQuaternion()
        {
            try
            {
                return luaState.CheckQuaternion(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Vector2 CheckVector2()
        {
            try
            {
                return luaState.CheckVector2(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Vector4 CheckVector4()
        {
            try
            {
                return luaState.CheckVector4(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Color CheckColor()
        {
            try
            {
                return luaState.CheckColor(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Ray CheckRay()
        {
            try
            {
                return luaState.CheckRay(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Bounds CheckBounds()
        {
            try
            {
                return luaState.CheckBounds(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public LayerMask CheckLayerMask()
        {
            try
            {
                return luaState.CheckLayerMask(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public long CheckLong()
        {
            try
            {
                return luaState.CheckLong(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public ulong CheckULong()
        {
            try
            {
                return luaState.CheckULong(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public Delegate CheckDelegate()
        {
            try
            {
                return luaState.CheckDelegate(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public object CheckVariant()
        {
            return luaState.ToVariant(stackPos++);
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public char[] CheckCharBuffer()
        {
            try
            {
                return luaState.CheckCharBuffer(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public byte[] CheckByteBuffer()
        {
            try
            {
                return luaState.CheckByteBuffer(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public object CheckObject(Type t)
        {
            try
            {
                return luaState.CheckObject(stackPos++, t);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public LuaFunction CheckLuaFunction()
        {
            try
            {
                return luaState.CheckLuaFunction(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public LuaTable CheckLuaTable()
        {
            try
            {
                return luaState.CheckLuaTable(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }

        /// <summary>
        /// 提取返回值, 并检查返回值
        /// </summary>
        public LuaThread CheckLuaThread()
        {
            try
            {
                return luaState.CheckLuaThread(stackPos++);
            }
            catch (Exception e)
            {
                EndPCall();
                throw e;
            }
        }
    }
}
