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
    public class InjectionSystemException : Exception
    {
        public const string NO_CONSTRUCTORS = "There are no constructors on the type {0}, Is it an interface?";
        public const string PARAMETER_TYPE_ERROR = "Array or IList type parameters, like 'typeof(object[])' or 'typeof(IList<object>)' should be obtains the actual type on the outside of the method {0}";
        public const string SAME_OBJECT = "The object with the same key and id already exists.";
        public const string CANNOT_RESOLVE_MONOBEHAVIOUR = "A MonoBehaviour cannot be resolved directly.";

        public InjectionSystemException(string message) : base(message) { }
    }
}