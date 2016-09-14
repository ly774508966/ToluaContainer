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
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LuaInterface
{
    public abstract class LuaBaseRef : IDisposable
    {
        /// <summary>
        /// 名称 (string)
        /// </summary>
        public string name = null;

        /// <summary>
        /// 用于比较
        /// </summary>
        protected int reference = -1;

        /// <summary>
        /// LuaState
        /// </summary>
        protected LuaState luaState;

        /// <summary>
        /// ?
        /// </summary>
        protected ObjectTranslator translator = null;

        /// <summary>
        /// 是否已被释放
        /// </summary>
        protected bool beDisposed;

        /// <summary>
        /// 引用计数
        /// </summary>
        protected int count = 0;

        #region constructor

        public LuaBaseRef()
        {
            count = 1;
        }

        #endregion

        #region destructor

        /// <summary>
        /// 析构函数 当对象脱离其作用域时（例如对象所在的函数已调用完毕），系统自动执行析构函数。
        /// 析构函数往往用来做“清理善后” 的工作（例如在建立对象时用new开辟了一片内存空间，应在
        /// 退出前在析构函数中用delete释放）
        /// </summary>
        ~LuaBaseRef()
        {
            Dispose(false);
        }

        #endregion

        /// <summary>
        /// 释放 （count - 1，如果 - 1 后 count 仍然大于 0 就直接返回，否则才进行释放）
        /// </summary>
        public virtual void Dispose()
        {
            --count;

            if (count > 0)
            {
                return;
            }            

            Dispose(true);            
        }

        /// <summary>
        /// 增加引用计数
        /// </summary>
        public void AddRef()
        {
            ++count;            
        }

        /// <summary>
        /// 释放资源 （如果 reference 大于 0 且 luaState 不为空则释放资源）
        /// </summary>
        public virtual void Dispose(bool disposeManagedResources)
        {
            if (!beDisposed)
            {
                beDisposed = true;   

                if (reference > 0 && luaState != null)
                {
                    luaState.CollectRef(reference, name, !disposeManagedResources);
                }
                
                reference = -1;
                luaState = null;
                count = 0;
            }            
        }

        /// <summary>
        /// 当 count 大于参数 generation 时进行释放 （！！慎用！！）
        /// </summary>
        public void Dispose(int generation)
        {                         
            if (count > generation)
            {
                return;
            }

            Dispose(true);
        }

        /// <summary>
        /// 返回 LuaState
        /// </summary>
        public LuaState GetLuaState()
        {
            return luaState;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Push()
        {
            luaState.Push(this);
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        public override int GetHashCode()
        {
            return RuntimeHelpers.GetHashCode(this);            
        }

        /// <summary>
        /// 返回 reference 的值
        /// </summary>
        public virtual int GetReference()
        {
            return reference;
        }

        /// <summary>
        /// 比较是否相等 （如果参数 o 不为空，比较参数的 reference 是否相等且大于 0）
        /// </summary>
        public override bool Equals(object o)
        {
            if (o == null) return reference <= 0;
            LuaBaseRef lr = o as LuaBaseRef;      
            
            if (lr == null || lr.reference != reference)
            {
                return false;
            }

            return reference > 0;
        }

        /// <summary>
        /// 比较两个 LuaBaseRef 是否相同 （如果引用的对象相同返回真，否则比较 reference 的值）
        /// </summary>
        static bool CompareRef(LuaBaseRef a, LuaBaseRef b)
        {
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            object l = a;
            object r = b;

            if (l == null && r != null)
            {
                return r == null || b.reference <= 0;
            }

            if (l != null && r == null)
            {
                return a.reference <= 0;
            }

            if (a.reference != b.reference)
            {
                return false;
            }

            return a.reference > 0;
        }

        /// <summary>
        /// 实现比较操作符 ==
        /// </summary>
        public static bool operator == (LuaBaseRef a, LuaBaseRef b)
        {
            return CompareRef(a, b);
        }

        /// <summary>
        /// 实现比较操作符 !=
        /// </summary>
        public static bool operator != (LuaBaseRef a, LuaBaseRef b)
        {
            return !CompareRef(a, b);
        }

        /// <summary>
        /// 返回是否还未被释放的 bool 值
        /// </summary>
        public bool IsAlive()
        {
            return !beDisposed;
        }
    }
}