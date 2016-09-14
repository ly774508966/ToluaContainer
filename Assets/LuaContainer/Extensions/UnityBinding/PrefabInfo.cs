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
using System.Collections;

namespace LuaContainer.Container
{
    public class PrefabInfo
    {
        #region constructor

        public PrefabInfo(UnityEngine.Object prefab, string path, Type type)
        {
            _prefab = prefab;
            this.path = path;
            this.type = type;
        }

        public PrefabInfo(string path, Type type)
        {
            this.path = path;
            this.type = type;
        }

        #endregion

        #region property

        /// <summary>
        /// 资源对象
        /// </summary>
        public UnityEngine.Object prefab
        {
            get
            {
                if (_prefab == null) { ResourcesLoad(); }
                return _prefab;
            }
        }
        private UnityEngine.Object _prefab;

        /// <summary>
        /// prefab 上的组件
        /// </summary>
        public Type type { get; set; }

        /// <summary>
        /// 资源路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 资源生成次数计数
        /// </summary>
        public int useCount { get; set; }

        /// <summary>
        /// 资源对象是否已经加载
        /// </summary>
        public bool isLoaded
        {
            get { return prefab != null; }
        }

        #endregion

        #region functions

        /// <summary>
        /// 加载资源
        /// </summary>
        private void ResourcesLoad()
        {
            _prefab = UnityEngine.Resources.Load(path);
            if (_prefab == null)
            {
                throw new BindingSystemException(
                    string.Format(BindingSystemException.RESOURCES_LOAD_FAILURE, path));
            }
        }

        /// <summary>
        /// 协程加载资源
        /// </summary>
        public IEnumerator GetCoroutineObject(Action<UnityEngine.Object> handle)
        {
            while (true)
            {
                yield return null;
                if (_prefab == null) { ResourcesLoad(); yield return null; }
                if (handle != null) { handle(_prefab); }
                yield break;
            }
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> handle)
        {
            return GetAsyncObject(handle, null);
        }

        /// <summary>
        /// 异步加载资源(带进度条功能)
        /// </summary>
        public IEnumerator GetAsyncObject(Action<UnityEngine.Object> handle, Action<float> progress)
        {
            // 如果 _prefab 不为空说明已经读取完成，执行 yield break 之后不再执行下面语句  
            if (_prefab != null) { handle(_prefab); yield break; }
            
            UnityEngine.ResourceRequest resRequest = UnityEngine.Resources.LoadAsync(path);

            // 进度判断值不能为1，否则会卡住
            while (resRequest.progress < 0.9)
            {
                if (progress != null) { progress(resRequest.progress); }
                yield return null;
            }

            // 在0.9~1之间如果未完成继续读取
            while (!resRequest.isDone)
            {
                if (progress != null) { progress(resRequest.progress); }
                yield return null;
            }

            _prefab = resRequest.asset;

            if (handle != null) { handle(_prefab); }

            yield return resRequest;
        }

        #endregion
    }
}