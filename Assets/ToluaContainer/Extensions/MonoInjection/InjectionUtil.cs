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