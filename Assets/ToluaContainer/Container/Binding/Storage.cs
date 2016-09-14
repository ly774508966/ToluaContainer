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

using System;
using System.Collections.Generic;

namespace ToluaContainer
{
    /// <summary>
    /// 双key储存结构，为避免误覆盖屏蔽了Add方法；
    /// 根据 type 取值时自动创建空值，无需 Contains 判断；
    /// 根据 type 和 id 设置自动创建空值，时无需 Contains 判断，但取值时仍需与字典一样先 Contains 判断以免错误。
    /// </summary>
    public class Storage<T>
    {
        public Items<T> this[Type type] { get { return Get(type); } }

        public int Count { get { return storage.Count; } }

        public Dictionary<Type, Items<T>>.KeyCollection Keys { get { return storage.Keys; } }

        public Dictionary<Type, Items<T>>.ValueCollection Values { get { return storage.Values; } }

        protected Dictionary<Type, Items<T>> storage;

        #region constructor

        public Storage() { storage = new Dictionary<Type, Items<T>>(); }

        #endregion

        /// <summary>
        /// 添加指定的类型
        /// </summary>
        protected void Add(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (!Contains(type))
            {
                storage.Add(type, new Items<T>());
            }
        }

        /// <summary>
        /// 移除指定类型的 BindingStorage
        /// </summary>
        public void Remove(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (Contains(type))
            {
                storage.Remove(type);
            }
        }

        /// <summary>
        /// 获取当前是否储存有指定类型的 BindingStorage，传入参数为空时也返回 false
        /// </summary>
        public bool Contains(Type type)
        {
            if (type == null) { return false; }

            return storage.ContainsKey(type);
        }

        /// <summary>
        /// 获取指定类型的 BindingStorage
        /// </summary>
        public Items<T> Get(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            if (!Contains(type)) { Add(type); }

            return storage[type];
        }

    }

    public class Items<T>
    {
        public T this[object id]
        {
            get { return Get(id); }
            set { Set(id, value); }
        }

        public int Count { get { return items.Count; } }

        public Dictionary<object, T>.KeyCollection Keys { get { return items.Keys; } }

        public Dictionary<object, T>.ValueCollection Values { get { return items.Values; } }

        protected Dictionary<object, T> items;

        #region constructor

        public Items() { items = new Dictionary<object, T>(); }

        #endregion

        /// <summary>
        /// 添加指定 ID 
        /// </summary>
        protected void Add(object id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }

            if (!Contains(id)) { items.Add(id, default(T)); }
        }

        /// <summary>
        /// 移除指定 ID 的 IBinding
        /// </summary>
        public void Remove(object id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }

            if (Contains(id)) { items.Remove(id); }
        }

        /// <summary>
        /// 获取当前是否储存有指定 ID 的 IBinding，传入参数为空时也返回 false
        /// </summary>
        public bool Contains(object id)
        {
            if (id == null) { return false; }

            return items.ContainsKey(id);
        }

        /// <summary>
        /// 根据 ID 获取 IBinding
        /// </summary>
        public T Get(object id)
        {
            if (id == null) { throw new ArgumentNullException("id"); }

            if (!Contains(id)) { return default(T); }

            return items[id];
        }

        /// <summary>
        /// 将指定 ID 的 IBinding 存入 items
        /// </summary>
        public void Set(object id, T value)
        {
            if(id == null || value == null) { throw new ArgumentNullException("id"); }

            if (!Contains(id)) { Add(id); }

            items[id] = value;
        }
    }
}