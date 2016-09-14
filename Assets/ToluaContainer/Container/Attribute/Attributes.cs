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

namespace ToluaContainer
{
    /// <summary>
    /// [Inject]标记需要注入的对象，可以在构造函数中传入一个 object 类型的值作为 id
    /// 例如：“[Inject(SomeEnum.VALUE)]”
    /// </summary>
    [AttributeUsage
        (AttributeTargets.Constructor | 
        AttributeTargets.Field | 
        AttributeTargets.Property |
        AttributeTargets.Method | 
        AttributeTargets.Parameter, 
        AllowMultiple = false, 
        Inherited = true)]
    public class Inject : Attribute
    {
        public object id;

        #region constructor

        public Inject() { id = null; }

        public Inject(object o) { id = o; }

        #endregion
    }

    /// <summary>
    /// [Priority]标记一个方法的优先级，以便决定执行顺序。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Priority : Attribute
    {
        public int priority;

        #region constructor

        public Priority() { }

        public Priority(int p) { priority = p; }

        #endregion
    }

    /// <summary>
    /// 标记 MonoBehaviour 只能从指定 id 的容器中获得注入
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class InjectFromContainer : Attribute
    {
        public object id;

        #region constructor

        public InjectFromContainer(object id) { this.id = id; }

        #endregion
    }
}