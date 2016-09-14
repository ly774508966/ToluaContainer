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
    /// <summary>
    /// 无参数构造函数委托
    /// </summary>
    public delegate object Constructor();

    /// <summary>
    /// 有参数构造函数委托
    /// </summary>
    public delegate object ParamsConstructor(object[] parameters);

    /// <summary>
    /// 回调一个无参数方法
    /// </summary>
    public delegate void ParameterlessMethod(object instance);

    /// <summary>
    /// 回调一个有参数方法
    /// </summary>
    public delegate void ParamsMethod(object instance, object[] parameters);

    /// <summary>
    /// 为一个属性或字段回调一个设值注入方法
    /// </summary>
    public delegate void Setter(object instance, object value);

    /// <summary>
    /// Setter 信息类
    /// </summary>
    public class SetterInfo : ParameterInfo
    {
        /// <summary>
        /// Setter 方法
        /// </summary>
        public Setter setter;

        #region constructor

        public SetterInfo(Type type, object id, Setter setter) : base(type, id)
        {
            this.setter = setter;
        }

        #endregion
    }

    /// <summary>
    /// 参数信息类
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Setter 类型
        /// </summary>
        public Type type;

        /// <summary>
        /// id
        /// </summary>
        public object id;

        #region constructor

        public ParameterInfo(Type type, object id)
        {
            this.type = type;
            this.id = id;
        }

        #endregion
    }

    /// <summary>
    /// Method 信息类
    /// </summary>
    public class MethodInfo
    {
        /// <summary>
        /// 无参数方法
        /// </summary>
        public ParameterlessMethod method;

        /// <summary>
        /// 有参数方法
        /// </summary>
        public ParamsMethod paramsMethod;

        /// <summary>
        /// 参数信息类
        /// </summary>
        public ParameterInfo[] parameters;

        #region constructor

        public MethodInfo(ParameterInfo[] parameters)
        {
            this.parameters = parameters;
        }

        #endregion
    }
}