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
    public class ReflectionInfo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type type { get; set; }

        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public Constructor constructor { get; set; }

        /// <summary>
        /// 有参数构造函数
        /// </summary>
        public ParamsConstructor paramsConstructor { get; set; }

        /// <summary>
        /// 构造函数的参数
        /// </summary>
        public ParameterInfo[] constructorParameters { get; set; }

        /// <summary>
        /// 接受注入的方法
        /// </summary>
        public MethodInfo[] methods { get; set; }

        /// <summary>
        /// 接受注入的公共属性
        /// </summary>
        public SetterInfo[] properties { get; set; }

        /// <summary>
        /// 接受注入的公共字段
        /// </summary>
        public SetterInfo[] fields { get; set; }
    }
}