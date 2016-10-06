﻿using UnityEngine;
using ToluaContainer.Container;
using ToluaContainer.Examples.Factory.Behaviours;

namespace ToluaContainer.Examples.Factory.Commands
{
	public class SpawnObjectsCommand : Command
    {
		[Inject]
		public IInjectionContainer container;

		public override void Execute(params object[] parameters)
        {
			for (var cubeIndex = 0; cubeIndex < 36; cubeIndex++)
            {
				var cube = container.Resolve<GameObject>();
				cube.name = string.Format("Cube {0:00}", cubeIndex);
			}
		}
	}
}