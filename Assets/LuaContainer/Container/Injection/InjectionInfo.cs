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
    public class InjectionInfo
    {
        /// <summary>
        /// 需要注入的成员枚举 （None | Constructor | Field | Property）
        /// </summary>
        public InjectionInto member;

        /// <summary>
        /// 注入成员的类型
        /// </summary>
        public Type memberType;

        /// <summary>
        /// 对应 binding 的 id 属性，储存于Inject等特性中，用于注入时通过比较是否相同来确认身份
        /// </summary>
        public object id;

        /// <summary>
        /// 需要注入的对象的类型
        /// </summary>
        public Type parentType;

        /// <summary>
        /// 需要注入的对象的实例
        /// </summary>
        public object parentInstance;

        /// <summary>
        /// 被注入对象的类型
        /// </summary>
        public Type injectType;
    }
}