using UnityEngine;
using ToluaContainer;
using ToluaContainer.Container;
using Utils;
using System;
using System.Collections.Generic;
using LuaInterface;

using BindType = ToLuaMenu.BindType;
using System.Reflection;

/// <summary>
/// 设置相关路径、需要导出的类型、委托类型 list 以及动态类型 list
/// </summary>
public static class CustomSettings
{
    /// <summary>
    /// 导出 Wrap 文件路径
    /// </summary>
    public static string saveDir = Application.dataPath + "/Source/Generate/";

    /// <summary>
    /// lua 文件路径
    /// </summary>   
    public static string toluaBaseType = Application.dataPath + "/ToLua/BaseType/";

    /// <summary>
    /// 导出时强制做为静态类的类型(注意 customTypeList 还要添加这个类型才能导出)
    /// unity 有些类作为 sealed class, 其实完全等价于静态类
    /// </summary>
    public static List<Type> staticClassTypes = new List<Type>
    {        
        typeof(UnityEngine.Application),
        typeof(UnityEngine.Time),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.SleepTimeout),
        typeof(UnityEngine.Input),
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.Physics),
        typeof(UnityEngine.RenderSettings),
        typeof(UnityEngine.QualitySettings),
        typeof(UnityEngine.GL),
    };

    /// <summary>
    /// 附加导出委托类型 list,(在导出委托时, customTypeList 中牵扯的委托类型都会导出， 无需写在这里)
    /// </summary>
    public static DelegateType[] customDelegateList = 
    {        
        _DT(typeof(Action)),                
        _DT(typeof(UnityEngine.Events.UnityAction)),
        _DT(typeof(System.Predicate<int>)),
        _DT(typeof(System.Action<int>)),
        _DT(typeof(System.Comparison<int>)),
    };

    /// <summary>
    /// 需要导出的类型列表，在这里添加你要导出注册到 lua 的类型
    /// </summary>
    public static BindType[] customTypeList =
    {                
        //------------------------为例子导出--------------------------------
        //_GT(typeof(TestEventListener)),
        //_GT(typeof(TestProtol)),
        //_GT(typeof(TestAccount)),
        //_GT(typeof(Dictionary<int, TestAccount>)).SetLibName("AccountMap"),
        //_GT(typeof(KeyValuePair<int, TestAccount>)),    
        //_GT(typeof(TestExport)),
        //_GT(typeof(TestExport.Space)),
        //-------------------------------------------------------------------        
                
        _GT(typeof(Debugger)).SetNameSpace(null),        

#if USING_DOTWEENING
        _GT(typeof(DG.Tweening.DOTween)),
        _GT(typeof(DG.Tweening.Tween)).SetBaseType(typeof(System.Object)).AddExtendType(typeof(DG.Tweening.TweenExtensions)),
        _GT(typeof(DG.Tweening.Sequence)).AddExtendType(typeof(DG.Tweening.TweenSettingsExtensions)),
        _GT(typeof(DG.Tweening.Tweener)).AddExtendType(typeof(DG.Tweening.TweenSettingsExtensions)),
        _GT(typeof(DG.Tweening.LoopType)),
        _GT(typeof(DG.Tweening.PathMode)),
        _GT(typeof(DG.Tweening.PathType)),
        _GT(typeof(DG.Tweening.RotateMode)),
        _GT(typeof(Component)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Transform)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Light)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Material)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Rigidbody)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(Camera)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        _GT(typeof(AudioSource)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        //_GT(typeof(LineRenderer)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),
        //_GT(typeof(TrailRenderer)).AddExtendType(typeof(DG.Tweening.ShortcutExtensions)),    
#else
                                         
        _GT(typeof(Component)),
        _GT(typeof(Transform)),
        _GT(typeof(Material)),
        _GT(typeof(Light)),
        _GT(typeof(Rigidbody)),
        _GT(typeof(Camera)),
        _GT(typeof(AudioSource)),
        //_GT(typeof(LineRenderer))
        //_GT(typeof(TrailRenderer))
#endif
      
        _GT(typeof(Behaviour)),
        _GT(typeof(MonoBehaviour)),        
        _GT(typeof(GameObject)),
        _GT(typeof(TrackedReference)),
        _GT(typeof(Application)),
        _GT(typeof(Physics)),
        _GT(typeof(Collider)),
        _GT(typeof(Time)),        
        _GT(typeof(Texture)),
        _GT(typeof(Texture2D)),
        _GT(typeof(Shader)),        
        _GT(typeof(Renderer)),
        _GT(typeof(WWW)),
        _GT(typeof(Screen)),        
        _GT(typeof(CameraClearFlags)),
        _GT(typeof(AudioClip)),        
        _GT(typeof(AssetBundle)),
        _GT(typeof(ParticleSystem)),
        _GT(typeof(AsyncOperation)).SetBaseType(typeof(System.Object)),        
        _GT(typeof(LightType)),
        _GT(typeof(SleepTimeout)),
        _GT(typeof(Animator)),
        _GT(typeof(Input)),
        _GT(typeof(KeyCode)),
        _GT(typeof(SkinnedMeshRenderer)),
        _GT(typeof(Space)),      
       

        _GT(typeof(MeshRenderer)),            
        _GT(typeof(ParticleEmitter)),
        _GT(typeof(ParticleRenderer)),
        _GT(typeof(ParticleAnimator)), 
                              
        _GT(typeof(BoxCollider)),
        _GT(typeof(MeshCollider)),
        _GT(typeof(SphereCollider)),        
        _GT(typeof(CharacterController)),
        _GT(typeof(CapsuleCollider)),
        
        _GT(typeof(Animation)),        
        _GT(typeof(AnimationClip)).SetBaseType(typeof(UnityEngine.Object)),        
        _GT(typeof(AnimationState)),
        _GT(typeof(AnimationBlendMode)),
        _GT(typeof(QueueMode)),  
        _GT(typeof(PlayMode)),
        _GT(typeof(WrapMode)),

        _GT(typeof(QualitySettings)),
        _GT(typeof(RenderSettings)),                                                   
        _GT(typeof(BlendWeights)),           
        _GT(typeof(RenderTexture)),	
        
        // ToluaContainer
        _GT(typeof(AppDefine)),
        _GT(typeof(ToLuaConst)),
        //_GT(typeof(Inject)),
        //_GT(typeof(Priority)),
        //_GT(typeof(InjectFromContainer)),
        _GT(typeof(ToluaContainer.Container.Binder)),
        _GT(typeof(Binding)),
        _GT(typeof(BindingFactory)),
        _GT(typeof(InjectionContainer)),
        //_GT(typeof(Injector)),
        //_GT(typeof(InjectionInfo)),
        //_GT(typeof(ReflectionCache)),
        //_GT(typeof(ReflectionFactory)),
        //_GT(typeof(ReflectionInfo)),
        //_GT(typeof(SetterInfo)),
        //_GT(typeof(ToluaContainer.Container.ParameterInfo)),
        //_GT(typeof(ToluaContainer.Container.MethodInfo)),
        _GT(typeof(Command)),
        _GT(typeof(CommandDispatch)),
        _GT(typeof(CommandDispatcher)),
        _GT(typeof(CommanderContainerAOT)),
        _GT(typeof(CommanderContainerExtension)),
        _GT(typeof(CommanderUtils)),
        _GT(typeof(CommandException)),
        _GT(typeof(CommandReference)),
        _GT(typeof(NamespaceCommandBehaviour)),
        _GT(typeof(TimedCommandDispatch)),
        _GT(typeof(ContextRoot)),
        //_GT(typeof(SceneInjector)),
        _GT(typeof(EventBehaviour)),
        _GT(typeof(EventContainerAOT)),
        //_GT(typeof(InjectionUtil)),
        //_GT(typeof(MonoInjection)),
        //_GT(typeof(StateInjectionExtension)),
        _GT(typeof(AssetBundleInfo)),
        _GT(typeof(PrefabInfo)),
        _GT(typeof(UnityBindingExtension)),
        _GT(typeof(UnityContainerAOT)),
        _GT(typeof(CompareUtils)),
        _GT(typeof(MethodUtils)),
        _GT(typeof(Exceptions)),
        _GT(typeof(TypeUtils)),
    };

    /// <summary>
    /// 动态类型 list 
    /// </summary>
    public static List<Type> dynamicList = new List<Type>()
    {
        typeof(MeshRenderer),
        typeof(ParticleEmitter),
        typeof(ParticleRenderer),
        typeof(ParticleAnimator),

        typeof(BoxCollider),
        typeof(MeshCollider),
        typeof(SphereCollider),
        typeof(CharacterController),
        typeof(CapsuleCollider),

        typeof(Animation),
        typeof(AnimationClip),
        typeof(AnimationState),

        typeof(BlendWeights),
        typeof(RenderTexture),
        typeof(Rigidbody),
    };

    /// <summary>
    /// 重载函数，相同参数个数，相同位置out参数匹配出问题时, 需要强制匹配解决
    /// 使用方法参见例子14
    /// </summary>
    public static List<Type> outList = new List<Type>()
    {
        
    };

    /// <summary>
    /// 生成指定类型 BindType 实例
    /// </summary>
    public static BindType _GT(Type t)
    {
        return new BindType(t);
    }

    /// <summary>
    /// 生成指定类型 DelegateType 实例
    /// </summary>
    public static DelegateType _DT(Type t)
    {
        return new DelegateType(t);
    }    
}
