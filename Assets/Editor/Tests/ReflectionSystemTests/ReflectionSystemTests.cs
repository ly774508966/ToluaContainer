﻿/*
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
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ToluaContainer;
using ToluaContainer.Container;

namespace uMVVMCS_NUitTests
{
    [TestFixture]
    public class ReflectionSystemTests
    {
        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 type 属性是否正确创建
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_typeCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_d));
            //Assert
            Assert.AreEqual(typeof(someClass_d), reflectionInfo.type);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 constructor 属性是否正确创建
        /// 由于 someClass_d 类构造函数有参数，所以无参数构造函数为空
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_constructorCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_d));
            //Assert
            Assert.AreEqual(true, reflectionInfo.constructor == null);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 paramsConstructor 属性是否正确创建
        /// 由于 someClass_d 类构造函数有参数，所以有参数构造函数不为空
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_paramsConstructorCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_d));
            //Assert
            Assert.AreEqual(false, reflectionInfo.paramsConstructor == null);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 constructorParameters 属性是否正确创建
        /// 由于 someClass_e 类附着[Construct]特性的构造函数有两个参数，所以长度为2
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_constructorParametersCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_e));
            //Assert
            Assert.AreEqual(2, reflectionInfo.constructorParameters.Length);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 postConstructors 属性是否正确创建
        /// 由于 someClass_e 类附着[PostConstruct]特性的方法有两个，所以为2
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_postConstructorsCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_e));
            //Assert
            Assert.AreEqual(2, reflectionInfo.methods.Length);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 properties 属性是否正确创建
        /// 由于 someClass_f 类附着[Inject]特性的属性有3个，所以为3
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_propertiesCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_f));
            //Assert
            Assert.AreEqual(3, reflectionInfo.properties.Length);
        }

        /// <summary>
        /// 测试 ReflectionFactory.Create 方法创建的 fields 属性是否正确创建
        /// 由于 someClass_f 类附着[Inject]特性的字典有4个，所以为4
        /// </summary>
        [Test]
        public void ReflectionFactoryCreate_CreateReflectionInfo_fieldsCorrect()
        {
            //Arrange 
            ReflectionFactory reflectionFactory = new ReflectionFactory();
            //Act
            ReflectionInfo reflectionInfo = reflectionFactory.Create(typeof(someClass_f));
            //Assert
            Assert.AreEqual(4, reflectionInfo.fields.Length);
        }

        /// <summary>
        /// 测试 ReflectionCache 自动添加类型是否正确创建
        /// </summary>
        [Test]
        public void ReflectionCacheGetInfo_AutoAdd_Correct()
        {
            //Arrange 
            ReflectionCache reflectionCache = new ReflectionCache();
            //Act
            ReflectionInfo reflectionInfo = reflectionCache.GetInfo(typeof(someClass_f));
            //Assert
            Assert.AreEqual(4, reflectionInfo.fields.Length);
        }

        /// <summary>
        /// 测试 ReflectionCache 移除类型是否正确
        /// </summary>
        [Test]
        public void ReflectionCacheRemove_RemoveType_Correct()
        {
            //Arrange 
            ReflectionCache reflectionCache = new ReflectionCache();
            //Act
            reflectionCache.Remove(typeof(someClass_f));
            //Assert
            Assert.AreEqual(false, reflectionCache.Contains(typeof(someClass_f)));
        }
    }

    public class someClass_d : IInjectionFactory
    {
        public int id;
        public string name;
        public object Create(InjectionInfo context) { return this; }
        //[Construct]
        public someClass_d(int id) { this.id = id; }
    }

    public class someClass_e : IInjectionFactory
    {
        public int id;
        public string name;
        public object Create(InjectionInfo context) { return this; }
        public someClass_e(int id) { this.id = id; }
        [Inject]
        public someClass_e(int id, string name) { this.id = id; this.name = name; }
        [Inject]
        public void TestPostConstruct1(int id) { }
        [Inject]
        public void TestPostConstruct2(int id, string name) { }
    }

    public class someClass_f : IInjectionFactory
    {
        public int id;
        public string name;
        public object Create(InjectionInfo context) { return this; }
        [Inject]
        public string name1 { get; set; }
        [Inject]
        public string name2 { get; set; }
        [Inject]
        public string name3 { get; set; }
        [Inject]
        public string name4;
        [Inject]
        public string name5;
        [Inject]
        public string name6;
        [Inject]
        public string name7;
    }
}
