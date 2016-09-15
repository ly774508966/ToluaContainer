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
using ToluaContainer.Container;

namespace ToluaContainer.Examples.BindingsSetup
{
	/// <summary>
	/// Cube rotator.
	/// </summary>
	public class CubeRotator : MonoBehaviour {
		/// <summary>Speed data for the cube.</summary>
		[Inject]
		public CubeRotationSpeed speedData;

		/// <summary>The cached transform object.</summary>
		protected Transform cachedTransform;

		protected void Start() {
			cachedTransform = GetComponent<Transform>();

			//Call "Inject" to inject any dependencies in the component.
			//In a production game, it's useful to place this in a base component.
			this.Inject();
        }

		protected void Update() {
			cachedTransform.Rotate(speedData.speed, speedData.speed, speedData.speed);
		}
	}
}