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
    public class BindingSystemException : Exception
    {
        public const string SAME_BINDING = "The binding with the same key and id already exists.";
        public const string PARAMETERS_LENGTH_ERROR = "Parameter length is not correct";
        public const string TYPE_NOT_ASSIGNABLE = "The type or instance is not assignable to binding";
        public const string BINDINGTYPE_NOT_ASSIGNABLE = "ParameterlessMethod {0} does not allow for {1} type of binding.";
        public const string TYPE_NOT_FACTORY = "The type doesn't implement IFactory.";
        public const string RESOURCES_LOAD_FAILURE = "Resources Load Failure! path: {0}";

        public BindingSystemException() : base() { }
        public BindingSystemException(string message) : base(message) { }
    }
}