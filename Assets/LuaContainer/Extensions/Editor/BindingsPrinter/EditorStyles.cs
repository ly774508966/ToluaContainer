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

using UnityEngine;

namespace LuaContainer.Editors
{
    public static class EditorStyles
    {
        /// <summary>
        /// Message style
        /// </summary>
        public static GUIStyle message
        {
            get
            {
                var style = new GUIStyle();
                style.fontSize = 20;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleCenter;
                return style;
            }
        }

        /// <summary>
        /// Styles for titles
        /// </summary>
        public static GUIStyle title
        {
            get
            {
                var style = new GUIStyle();
                style.fontSize = 18;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.MiddleLeft;
                return style;
            }
        }

        /// <summary>
        /// Styles for container's info
        /// </summary>
        public static GUIStyle containerInfo
        {
            get
            {
                var style = new GUIStyle();
                style.fontSize = 12;
                style.alignment = TextAnchor.MiddleLeft;
                return style;
            }
        }

        /// <summary>
        /// Styles for container's names
        /// </summary>
        public static GUIStyle containerName
        {
            get
            {
                var style = new GUIStyle();
                style.fontSize = 16;
                style.fontStyle = FontStyle.Bold;
                style.alignment = TextAnchor.UpperLeft;
                return style;
            }
        }

        /// <summary>
        /// Styles for binding's data
        /// </summary>
        public static GUIStyle bindings
        {
            get
            {
                var style = new GUIStyle();
                style.fontSize = 13;
                style.alignment = TextAnchor.UpperLeft;
                return style;
            }
        }
    }
}