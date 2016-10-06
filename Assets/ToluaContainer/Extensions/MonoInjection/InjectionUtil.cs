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

using ToluaContainer.Container;

namespace ToluaContainer
{
	public static class InjectionUtil {
        /// <summary>
        /// Injects into a specified object using container details.
        /// ��ȡ���� obj �ϸ��ŵ� InjectFromContainer�������Ե� id ִ��ע��
        /// </summary>
        public static void Inject(object obj)
        {
            // ��ȡ���˲���������
			var attributes = obj.GetType().GetCustomAttributes(true);

            // ���û�л�ȡ�������ÿ� id ����ע��
            if (attributes.Length == 0) { Inject(obj, null); }
            else
            {
				var containInjectFromContainer = false;
				
				for (var i = 0; i < attributes.Length; i++)
                {
                    var attribute = attributes[i];
                    // ������������� id ����ƥ�䣬�� InjectFromContainer �� id ��ΪҪ���յ� id ����
                    if (attribute is InjectFromContainer)
                    {
                        Inject(obj, (attribute as InjectFromContainer).id);
						containInjectFromContainer = true;
					}
				}
                
                //��������û�л�ȡ�� InjectFromContainer ���ԣ��ÿ� id ����ע��
                if (!containInjectFromContainer) { Inject(obj, null); }
			}
		}

        /// <summary>
        /// ������� obj ���ǵ��� binding ��ֵ��Ϊ ContextRoot �� containers List ��ÿ��
        /// Ԫ�ص� container id ����� id ��ȵ�����ע�� obj��
        /// ����� id Ϊ�գ�ֻҪ obj ���ǵ����Ͷ�ÿһ����������ע�룬����ֻ������ id ��ȵ�����ע��
        /// </summary>
        public static void Inject(object obj, object id)
        {
            // ��ȡ ContextRoot �е� containers List
            var containers = ContextRoot.containers;

            for (int i = 0; i < containers.Count; i++)
            {
                // ���� list��������� id ��Ϊ���ҺͲ��� id ��ȣ�injectOnContainer Ϊ��
                var injectOnContainer = (containers[i].id != null && containers[i].id.Equals(id));

                // ����� id Ϊ�ջ� id ������ id ��ȣ��Ҳ��� obj �����������κ� binding ��ֵ�ظ���
                // ��Ϊ��ǰ����ע�� obj(�����ظ�ע��)
                if ((id == null || injectOnContainer) && 
                    !IsExistOnContainer(obj, containers[i]))
                {
                    containers[i].Inject(obj);
				}
			}
		}
		
		/// <summary>
		/// ����ָ�������е� object �Ƿ��Ѿ�����
		/// </summary>
		public static bool IsExistOnContainer(object obj, IInjectionContainer container)
        {
			var isExist = false;
			var bindings = container.GetTypes(obj.GetType());

            if (bindings == null) { return false; }
			
			for (var i = 0; i < bindings.Count; i++)
            {
                int length = bindings[i].valueList.Count;
                for (int n = 0; n < length; n++)
                {
                    if (bindings[i].valueList[n] == obj)
                    {
                        isExist = true;
                        return isExist;
                    }
                }
			}
			
			return isExist;
        }

        /// <summary>
        /// ����ָ�� binder �е� object �Ƿ��Ѿ�����
        /// </summary>
        public static bool IsExistOnBinder(object obj, IBinder binder)
        {
            var isExist = false;
            var bindings = binder.GetTypes(obj.GetType());

            if (bindings == null) { return false; }

            for (var i = 0; i < bindings.Count; i++)
            {
                int length = bindings[i].valueList.Count;
                for (int n = 0; n < length; n++)
                {
                    if (bindings[i].valueList[n] == obj)
                    {
                        isExist = true;
                        return isExist;
                    }
                }
            }

            return isExist;
        }
    }
}