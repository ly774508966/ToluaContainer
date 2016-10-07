using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ToluaContainer;
using ToluaContainer.Container;

public class NewBehaviourScript : MonoBehaviour
{
    void Start()
    {
        //Arrange 
        IBinder binder = new Binder();
        //Act
        binder.MultipleBind(
            new Type[] { typeof(someClass_b), typeof(int), typeof(someClass) },
            new BindingType[] {
                    BindingType.ADDRESS,
                    BindingType.SINGLETON,
                    BindingType.FACTORY })
                .To(new object[] { typeof(someClass_b), 1, new someClass() })
                .As(new object[] { null, 1, 2 })
                .MultipleBind(
            new Type[] { typeof(someClass_b), typeof(int), typeof(someClass) },
            new BindingType[] {
                    BindingType.ADDRESS,
                    BindingType.SINGLETON,
                    BindingType.FACTORY })
                .To(new object[] { typeof(someClass_b), 1, new someClass() })
                .MultipleBind(
            new Type[] { typeof(someClass_b), typeof(int), typeof(someClass) },
            new BindingType[] {
                    BindingType.ADDRESS,
                    BindingType.SINGLETON,
                    BindingType.FACTORY })
                .To(new object[] { typeof(someClass_b), 1, new someClass() })
                .As(new object[] { null, 3, 4 });

        Debug.Log(binder.GetAll().Count == 9);
        Debug.Log(binder.GetBinding<int>(1) != null);
        Debug.Log(binder.GetBinding<someClass>(2) != null);
        Debug.Log(binder.GetBinding<int>(3) != null);
        Debug.Log(binder.GetBinding<someClass>(4) != null);
    }
}
public class someClass : IInjectionFactory
{
    public int id;
    virtual public object Create(InjectionInfo context) { return this; }
}

public class someClass_b : someClass
{
    override public object Create(InjectionInfo context) { return 1; }
}

public class someClass_c : someClass
{
    [Inject]
    public someClass_b b;

    override public object Create(InjectionInfo context) { return 2; }
}

public class TestCommand1 : Command
{
    public int num = 0;

    public override void Execute(params object[] parameters)
    {
        num++;
        ((someClass)parameters[0]).id = num;
    }
}
