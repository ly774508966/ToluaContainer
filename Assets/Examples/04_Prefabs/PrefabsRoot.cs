using UnityEngine;
using ToluaContainer.Container;

namespace ToluaContainer.Examples.Prefabs
{
	public class PrefabsRoot : ContextRoot
    {
		public override void SetupContainers()
        {
			// �������
			AddContainer<InjectionContainer>()
				.RegisterAOT<UnityContainerAOT>()
				// ʵ����ָ�� prefab ��ָ�����ơ�ִ��ע��
				.Bind<Transform>().ToPrefab("04_Prefabs/Cube").As("cube")
				// ʵ��������
				.BindSingleton<GameObject>().ToPrefab("04_Prefabs/Plane");
        }
		
		public override void Init() { }
	}
}