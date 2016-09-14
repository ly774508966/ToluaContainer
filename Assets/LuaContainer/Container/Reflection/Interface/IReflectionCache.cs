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

namespace LuaContainer.Container
{
    public interface IReflectionCache
    {
        /// <summary>
        /// 为当前缓存的 ReflectInfo 实现类型索引
        /// </summary>
        ReflectionInfo this[Type type] { get; }

        /// <summary>
        /// 添加一个类型的缓存
        /// </summary>
        void Add(Type type);

        /// <summary>
        /// 移除一个类型的缓存
        /// </summary>
        void Remove(Type type);

        /// <summary>
        /// 获取指定类型的 ReflectInfo 类型缓存
        /// </summary>
        ReflectionInfo GetInfo(Type type);

        /// <summary>
        /// 查询指定类型的缓存是否存在
        /// </summary>
        bool Contains(Type type);

        /// <summary>
        /// 为 binder 中的所有 binding 生成缓存
        /// </summary>
        void CacheFromBinder(IBinder binder);
    }
}