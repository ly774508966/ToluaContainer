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
using UnityEditor.SceneManagement;
using System;
using System.Collections.Generic;
using ToluaContainer.Container;

namespace ToluaContainer.Editors
{
    [CustomEditor(typeof(ContextRoot), true)]
    public class ContextRootEditor : Editor
    {
        /// <summary>
        /// 默认 ContextRoot 执行顺序
        /// </summary>
        protected const int DEFAULT_EXECUTION_ORDER = -100;

        /// <summary>
        /// MonoBehaviour 类型完全限定名
        /// </summary>
        protected const string MONO_BEHAVIOUR_TYPE = "UnityEngine.MonoBehaviour";

        /// <summary>
        /// Object to be edited
        /// </summary>
        protected ContextRoot editorItem;

        /// <summary>
        /// Custom 脚本类型
        /// </summary>
        protected string[] customScripts;

        protected void OnEnable()
        {
            editorItem = (ContextRoot)target;

            var customScriptsNames = new List<string>();

            // 第一个类型必须保持为 UnityEngine.MonoBehaviour 
            customScriptsNames.Add(MONO_BEHAVIOUR_TYPE);
            // 获取继承了 MonoBehaviour 的类型并遍历
            var customTypes = TypeUtils.GetAssignableTypes(typeof(MonoBehaviour));
            int length = customTypes.Length;
            for (int i = 0; i < length; i++)
            {
                // 如果不是 ToluaContainer 命名空间下的类型才添加到 customScriptsNames
                if (!customTypes[i].FullName.StartsWith("ToluaContainer"))
                {
                    customScriptsNames.Add(customTypes[i].FullName);
                }
            }
            // 将 customScriptsNames 的结果转为数组保存到 customScripts 数组
            customScripts = customScriptsNames.ToArray();

            // 如果 editorItem.baseBehaviourTypeName 为空将 MONO_BEHAVIOUR_TYPE 作为基类名
            if (string.IsNullOrEmpty(editorItem.baseBehaviourTypeName))
            {
                editorItem.baseBehaviourTypeName = MONO_BEHAVIOUR_TYPE;
            }
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            DrawDefaultInspector();

            // 注入行为类型
            editorItem.injectionType = (ContextRoot.MonoBehaviourInjectionType)
                EditorGUILayout.EnumPopup(
                    new GUIContent("Injection type", "Type of injection on MonoBehaviours."),
                    editorItem.injectionType);

            // 注入基类名
            if (editorItem.injectionType == ContextRoot.MonoBehaviourInjectionType.BaseType ||
                editorItem.injectionType == ContextRoot.MonoBehaviourInjectionType.Children)
            {
                var index = Array.IndexOf<string>(
                    customScripts, editorItem.baseBehaviourTypeName);
                index = EditorGUILayout.Popup(
                    "Base behaviour type", 
                    index, 
                    customScripts);
                if (index >= 0) editorItem.baseBehaviourTypeName = customScripts[index];
            }
            else
            {
                editorItem.baseBehaviourTypeName = MONO_BEHAVIOUR_TYPE;
            }

            if (!Application.isPlaying && EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }

            // 设置执行顺序
            EditorGUILayout.HelpBox("Use the button below to ensure the context root " +
                "will be executed before any other injectable MonoBehaviour.", MessageType.Info);
            if (GUILayout.Button("Set execution order"))
            {
                var contextRootType = this.editorItem.GetType();
                var contextRootOrder = ExecutionOrderUtils.SetScriptExecutionOrder(contextRootType, DEFAULT_EXECUTION_ORDER);
                var message = string.Format("{0} execution order set to {1}.", contextRootType.Name, contextRootOrder);

                EditorUtility.DisplayDialog("Script execution order", message, "Ok");
            }
        }
    }
}