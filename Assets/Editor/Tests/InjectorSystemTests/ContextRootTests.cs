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
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using ToluaContainer;
using ToluaContainer.Container;

namespace uMVVMCS_NUitTests
{
    [TestFixture]
    public class ContextRootTests : MonoBehaviour
    {
        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 type 属性是否正确创建
        /// </summary>
        [Test]
        public void SetupContainers_setInt_Correct()
        {
            //Arrange 
            //IContextRoot root = GameObject.Find("Main Camera").GetComponent<TestsRoot_a>() as IContextRoot;
            //Act
            //int num = root.container.Resolve<TestsFunctions>()
            //Assert
            //Assert.AreEqual(typeof(someClass_d), reflectionInfo.type);
        }
    }
}
