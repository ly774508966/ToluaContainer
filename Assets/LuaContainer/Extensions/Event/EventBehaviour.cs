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

namespace LuaContainer
{
    public class EventBehaviour : MonoBehaviour
    {
        protected void Update()
        {
            // 如果游戏暂停则不执行（Mathf.Approximately 方法用于比较浮点值，由于浮点值不精确所以运算时可能产生误差，所以必须使用特殊的方法来比较）
            if (Mathf.Approximately(Time.deltaTime, 0)) { return; }

            for (var objIndex = 0; objIndex < EventContainerAOT.updateable.Count; objIndex++)
            {
                EventContainerAOT.updateable[objIndex].Update();
            }
        }

        /// <summary>
        /// 当组件被销毁时调用，销毁 disposable list 中的所有元素
        /// </summary>
        protected void OnDestroy()
        {
            int length = EventContainerAOT.disposable.Count;
            for (int i = 0; i < length; i++)
            {
                EventContainerAOT.disposable[i].Dispose();
            }

            EventContainerAOT.disposable.Clear();
            EventContainerAOT.updateable.Clear();
        }
    }
}
