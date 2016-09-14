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

namespace ToluaContainer.Container
{
    public interface IPool<T> : IPool
    {
        new T GetInstance();
        new T GetInstance(bool doublue);
        new T GetInstance(bool doublue, bool throwException);
    }

    public interface IPool
    {
        /// <summary>
        /// 属性，储存对象池的类型（由第一个放入对象池的对象类型决定）
        /// </summary>
        Type type { get; set; }

        /// <summary>
        /// 如果对象池中有可用的实例就按顺序返回
        /// </summary>
        object GetInstance();
        object GetInstance(bool doublue);
        object GetInstance(bool doublue, bool throwException);

        /// <summary>
        /// 将一个实例返回到对象池 (如果被释放的实例实现了IPoolable接口，那么应该调用Release()方法)
        /// </summary>
        void ReturnInstance(object value);

        /// <summary>
        /// 清空对象池
        /// </summary>
        void Clean();

        /// <summary>
        /// 返回non-committed实例的数量
        /// </summary>
        int availableCount { get; }

        /// <summary>
        /// 属性，获取或设置对象池可以储存多少个对象 (设置为0表示无限大小，可以无限制的扩展)
        /// </summary>
        int size { get; set; }

        /// <summary>
        /// 属性，返回当前由对象池管理的实例总数
        /// </summary>
        int instanceCount { get; }

        /// <summary>
        /// 重置
        /// </summary>
        void Restore();
    }
}