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
using System.Collections;
using System.Collections.Generic;

namespace LuaInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class LuaTable : LuaBaseRef
    {
        #region constructor

        public LuaTable(int reference, LuaState state)
        {
            this.reference = reference;
            this.luaState = state;
        }

        #endregion

        public object this[string key]
        {
            get
            {
                int oldTop = luaState.LuaGetTop();

                try
                {
                    luaState.Push(this);
                    luaState.Push(key);
                    luaState.LuaGetTable(-2);
                    object ret = luaState.ToVariant(-1);
                    luaState.LuaSetTop(oldTop);
                    return ret;
                }
                catch (Exception e)
                {
                    luaState.LuaSetTop(oldTop);
                    throw e;                    
                }                
            }

            set
            {
                int oldTop = luaState.LuaGetTop();

                try
                {
                    luaState.Push(this);
                    luaState.Push(key);
                    luaState.Push(value);
                    luaState.LuaSetTable(-3);
                    luaState.LuaSetTop(oldTop);
                }
                catch (Exception e)
                {
                    luaState.LuaSetTop(oldTop);
                    throw e;
                }
            }
        }

        public object this[int key]
        {
            get
            {
                int oldTop = luaState.LuaGetTop();

                try
                {
                    luaState.Push(this);
                    luaState.LuaRawGetI(-1, key);
                    object obj = luaState.ToVariant(-1);
                    luaState.LuaSetTop(oldTop);
                    return obj;
                }
                catch (Exception e)
                {
                    luaState.LuaSetTop(oldTop);
                    throw e;
                }
            }

            set
            {
                int oldTop = luaState.LuaGetTop();

                try
                {
                    luaState.Push(this);
                    luaState.Push(value);
                    luaState.LuaRawSetI(-2, key);
                    luaState.LuaSetTop(oldTop);
                }
                catch (Exception e)
                {
                    luaState.LuaSetTop(oldTop);
                    throw e;
                }
            }
        }

        /// <summary>
        /// 返回 Length
        /// </summary>
        public int Length
        {
            get
            {
                luaState.Push(this);
                int n = luaState.LuaObjLen(-1);
                luaState.LuaPop(1);
                return n;
            }
        }

        /// <summary>
        /// 获取 LuaFunction、设置其 name 属性并返回
        /// </summary>
        public LuaFunction RawGetLuaFunction(string key)
        {            
            int top = luaState.LuaGetTop();

            try
            {
                luaState.Push(this);
                luaState.Push(key);
                luaState.LuaRawGet(-2);
                LuaFunction func = luaState.CheckLuaFunction(-1);
                luaState.LuaSetTop(top);

                if (func != null)
                {
                    func.name = name + "." + key;
                }

                return func;
            }
            catch(Exception e)            
            {
                luaState.LuaSetTop(top);
                throw e;
            }
        }

        /// <summary>
        /// 获取 LuaFunction、设置其 name 属性并返回
        /// </summary>
        public LuaFunction GetLuaFunction(string key)
        {
            int top = luaState.LuaGetTop();

            try
            {
                luaState.Push(this);
                luaState.Push(key);
                luaState.LuaGetTable(-2);
                LuaFunction func = luaState.CheckLuaFunction(-1);
                luaState.LuaSetTop(top);

                if (func != null)
                {
                    func.name = name + "." + key;
                }

                return func;
            }
            catch(Exception e)
            {
                luaState.LuaSetTop(top);
                throw e;
            }
        }

        /// <summary>
        /// 获取 String 字段
        /// </summary>
        public string GetStringField(string name)
        {
            int oldTop = luaState.LuaGetTop();
     
            try
            {
                luaState.Push(this);
                luaState.LuaGetField(-1, name);
                string str = luaState.CheckString(-1);
                luaState.LuaSetTop(oldTop);
                return str;
            }
            catch(LuaException e)
            {
                luaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddTable(string name)
        {
            int oldTop = luaState.LuaGetTop();

            try
            {
                luaState.Push(this);
                luaState.Push(name);
                luaState.LuaCreateTable();                
                luaState.LuaRawSet(-3);
                luaState.LuaSetTop(oldTop);
            }
            catch (Exception e)
            {
                luaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object[] ToArray()
        {
            int oldTop = luaState.LuaGetTop();

            try
            {
                luaState.Push(this);
                int len = luaState.LuaObjLen(-1);
                List<object> list = new List<object>(len + 1);
                int index = 1;
                object obj = null;

                while(index <= len)
                {
                    luaState.LuaRawGetI(-1, index++);
                    obj = luaState.ToVariant(-1);
                    luaState.LuaPop(1);
                    list.Add(obj);
                }                

                luaState.LuaSetTop(oldTop);
                return list.ToArray();
            }
            catch (Exception e)
            {
                luaState.LuaSetTop(oldTop);
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            luaState.Push(this);
            IntPtr p = luaState.LuaToPointer(-1);
            luaState.LuaPop(1);
            return string.Format("table:0x{0}", p.ToString("X"));            
        }

        /// <summary>
        /// 
        /// </summary>
        public LuaArrayTable ToArrayTable()
        {            
            return new LuaArrayTable(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public LuaDictTable ToDictTable()
        {
            return new LuaDictTable(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public LuaTable GetMetaTable()
        {            
            int oldTop = luaState.LuaGetTop();

            try
            {
                LuaTable t = null;
                luaState.Push(this);

                if (luaState.LuaGetMetaTable(-1) != 0)
                {
                    t = luaState.CheckLuaTable(-1);
                }

                luaState.LuaSetTop(oldTop);
                return t;
            }
            catch (Exception e)
            {
                luaState.LuaSetTop(oldTop);
                throw e;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LuaArrayTable : IDisposable, IEnumerable<object>
    {
        /// <summary>
        /// 
        /// </summary> 
        private LuaTable table = null;

        /// <summary>
        /// 
        /// </summary>
        private LuaState state = null;

        /// <summary>
        /// 
        /// </summary>
        public LuaArrayTable(LuaTable table)           
        {
            table.AddRef();
            this.table = table;            
            this.state = table.GetLuaState();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (table != null)
            {
                table.Dispose();
                table = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length
        {
            get
            {
                return table.Length;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object this[int key]
        {
            get
            {
                return table[key];
            }
            set 
            {
                table[key] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerator<object> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        class Enumerator : IEnumerator<object>
        {
            /// <summary>
            /// 
            /// </summary>    
            LuaState state;

            /// <summary>
            /// 
            /// </summary>
            int index = 1;

            /// <summary>
            /// 
            /// </summary>
            object current = null;

            /// <summary>
            /// 
            /// </summary>
            public Enumerator(LuaArrayTable list)
            {                
                state = list.state;
                state.Push(list.table);                
            }

            /// <summary>
            /// 
            /// </summary>
            public object Current
            {
                get
                {
                    return current;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool MoveNext()
            {
                state.LuaRawGetI(-1, index);
                current = state.ToVariant(-1);
                state.LuaPop(1);
                ++index;
                return current == null ? false : true;
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                index = 1;
                current = null;
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                state.LuaPop(1);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LuaDictTable : IDisposable, IEnumerable<DictionaryEntry>
    {
        /// <summary>
        /// 
        /// </summary>
        LuaTable table;

        /// <summary>
        /// 
        /// </summary>
        LuaState state;

        /// <summary>
        /// 
        /// </summary>
        public LuaDictTable(LuaTable table)            
        {
            table.AddRef();
            this.table = table;
            this.state = table.GetLuaState() ;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (table != null)
            {
                table.Dispose();
                table = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public object this[string key]
        {
            get
            {
                return table[key];
            }

            set
            {
                table[key] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Hashtable ToHashtable()
        {
            Hashtable hash = new Hashtable();
            var iter = GetEnumerator();

            while (iter.MoveNext())
            {
                hash.Add(iter.Current.Key, iter.Current.Value);                
            }

            iter.Dispose();
            return hash;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerator<DictionaryEntry> GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        class Enumerator : IEnumerator<DictionaryEntry>
        {
            /// <summary>
            /// 
            /// </summary>  
            LuaState state;

            /// <summary>
            /// 
            /// </summary>                      
            DictionaryEntry current = new DictionaryEntry();

            /// <summary>
            /// 
            /// </summary>
            public Enumerator(LuaDictTable list)
            {                
                state = list.state;
                state.Push(list.table);
                state.LuaPushNil();                
            }

            /// <summary>
            /// 
            /// </summary>
            public DictionaryEntry Current
            {
                get 
                {
                    return current;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public bool MoveNext()
            {
                if (state.LuaNext(-2))
                {
                    current = new DictionaryEntry();
                    current.Key = state.ToVariant(-2);
                    current.Value = state.ToVariant(-1);
                    state.LuaPop(1);
                    return true;
                }
                else
                {
                    current = new DictionaryEntry();
                    return false;
                }                
            }

            /// <summary>
            /// 
            /// </summary>
            public void Reset()
            {
                current = new DictionaryEntry();
            }

            /// <summary>
            /// 
            /// </summary>
            public void Dispose()
            {
                state.LuaPop(1);
            }
        }
    }
}
