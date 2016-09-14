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

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using LuaContainer.Container;

namespace LuaContainer.Editors
{
    [CustomPropertyDrawer(typeof(CommandReference))]
    public class CommandReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// 默认行高
        /// </summary>
        private const int LINE_HEIGHT = 18;

        /// <summary>
        /// 可用 commands(类型)名字典，以命名空间为 key
        /// a</summary>
        private Dictionary<string, IList<string>> types;

        /// <summary>
        /// 可用 commands 命名空间
        /// </summary>
        private string[] namespaceNames;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (types == null || types.Count == 0)
            {
                var availableCommands = CommanderUtils.GetAvailableCommands();
                types = CommanderUtils.GetTypesAsString(availableCommands);
                namespaceNames = new List<string>(types.Keys).ToArray();
            }

            EditorGUI.BeginProperty(position, label, property);

            // Label.
            EditorGUI.LabelField(position, label);

            // 子段落不缩进
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // 命名空间
            var namespaceRect = new Rect(position.x, position.y + LINE_HEIGHT, position.width, LINE_HEIGHT);
            var propertyNamespace = property.FindPropertyRelative("commandNamespace");
            var i = Array.IndexOf(namespaceNames, propertyNamespace.stringValue);
            if (i < 0) { i = 0; }

            EditorGUI.BeginChangeCheck();
            i = EditorGUI.Popup(namespaceRect, "Namespace", i, namespaceNames);
            if (EditorGUI.EndChangeCheck() || string.IsNullOrEmpty(propertyNamespace.stringValue))
            {
                propertyNamespace.stringValue = namespaceNames[i];
            }

            // Command.
            var commandRect = new Rect(position.x, position.y + LINE_HEIGHT * 2, position.width, LINE_HEIGHT);
            var propertyCommand = property.FindPropertyRelative("commandName");
            var commands = types[propertyNamespace.stringValue];
            var commandIndex = commands.IndexOf(propertyCommand.stringValue);
            if (commandIndex < 0) commandIndex = 0;

            EditorGUI.BeginChangeCheck();
            commandIndex = EditorGUI.Popup(commandRect, "Command", commandIndex, new List<string>(commands).ToArray());
            if (EditorGUI.EndChangeCheck() || string.IsNullOrEmpty(propertyCommand.stringValue))
            {
                propertyCommand.stringValue = commands[commandIndex];
            }

            // 设置缩进
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return LINE_HEIGHT * 3;
        }
    }
}