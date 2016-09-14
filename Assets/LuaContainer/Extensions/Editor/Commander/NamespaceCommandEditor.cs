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

using UnityEditor;
using LuaContainer.Container;
using System;
using System.Collections.Generic;

namespace LuaContainer.Editors
{
    public abstract class NamespaceCommandEditor<T> : Editor where T : NamespaceCommandBehaviour
    {
        /// <summary>
        /// 需要编辑的组件
        /// </summary>
        protected T component;

        /// <summary>
        /// 可用 command（类型）名
        /// </summary>
        protected Dictionary<string, IList<string>> types;

        /// <summary>
        /// 可用 commands 命名空间
        /// </summary>
        protected string[] namespaceNames;

        protected void OnEnable()
        {
            component = (T)this.target;

            var availableCommands = CommanderUtils.GetAvailableCommands();
            types = CommanderUtils.GetTypesAsString(availableCommands);
            namespaceNames = new List<string>(types.Keys).ToArray();
        }

        public override void OnInspectorGUI()
        {
            // 命名空间
            var namespaceIndex = Array.IndexOf(this.namespaceNames, this.component.commandNamespace);
            if (namespaceIndex == -1) namespaceIndex = 0;
            namespaceIndex = EditorGUILayout.Popup("Namespace", namespaceIndex, this.namespaceNames);
            this.component.commandNamespace = this.namespaceNames[namespaceIndex];

            // Command 
            var commands = this.types[this.component.commandNamespace];
            var commandIndex = commands.IndexOf(this.component.commandName);
            if (commandIndex < 0) commandIndex = 0;
            commandIndex = EditorGUILayout.Popup("Command", commandIndex, new List<string>(commands).ToArray());
            component.commandName = commands[commandIndex];

            // 更新编辑器
            EditorUtility.SetDirty(this.target);
        }
    }
}