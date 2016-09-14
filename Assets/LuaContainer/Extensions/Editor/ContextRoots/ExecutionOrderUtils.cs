﻿/*
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

using UnityEditor;
using System;

namespace LuaContainer.Editors
{
    public static class ExecutionOrderUtils
    {
        /// <summary>
        /// 设置执行顺序
        /// </summary>
        public static int SetScriptExecutionOrder(Type type, int order)
        {
            return SetScriptExecutionOrder(type, order, true);
        }

        /// <summary>
        /// 设置执行顺序
        /// </summary>
        public static int SetScriptExecutionOrder(Type type, int order, bool unique)
        {
            var executionOrder = order;
            MonoScript selectedScript = null;

            // 按顺序执行
            var available = false;
            while (!available)
            {
                available = true;

                var scripts = MonoImporter.GetAllRuntimeMonoScripts();
                int length = scripts.Length;
                for (int i = 0; i < length; i++)
                {
                    if (selectedScript == null && scripts[i].GetClass() == type)
                    {
                        selectedScript = scripts[i];
                        if (!unique) { break; }
                    }

                    if (scripts[i].GetClass() != type && MonoImporter.GetExecutionOrder(scripts[i]) == executionOrder)
                    {
                        executionOrder += order;
                        available = false;
                        continue;
                    }
                }
            }

            MonoImporter.SetExecutionOrder(selectedScript, executionOrder);

            return executionOrder;
        }
    }
}