using UnityEngine;

namespace ToluaContainer.Container
{
    /// <summary>
    /// Ϊ MonoBehaviour �ṩע�뷽����Ĭ�� ContextRoot Extension ����ʹ��
    /// </summary>
    public static class MonoInjection
    {
        /// <summary>
        /// Ϊ MonoBehaviour ִ��ע��,���� script Ϊע��Ŀ��
        /// </summary>
        public static void Inject(this MonoBehaviour script)
        {
            InjectionUtil.Inject(script);
        }

    }
}