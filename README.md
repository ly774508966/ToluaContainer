# ToluaContainer 

ToluaContainer （https://github.com/joey1258/ToluaContainer） 是基于 adic （https://github.com/intentor/adic） 进行改造，再并结合 Tolua （https://github.com/topameng/tolua） 的版本。

对比 adic 做了如下的修改：

一、 修改了 binding 系统。

* 与 adic 的 BindingInfo、BindingFactory、IBindingConditionFactory 等“多段式” binding 不同，不再使用一个只储存字段、属性的 BindingInfo， 把各种方法分离到 BindingFactory、IBindingConditionFactory，而是将它们一体化（类似 strangeioc 的形式，但没有 SemiBinding）。

* 添加了 MULTITON 类型的 binding————在 adic 中，每个 binding 只能有一个值（当然也可以把数组或者 list 装箱为 object，但对于本身就已经装箱为 object 的值组织为数组或者 list 之后再度装箱为 object未免太绕了一点），然后以类型作为 key 储存在 binder 中，当需要取用时对改类型的 binding 进行遍历，取出合适的。在一个 binding 需要多个值的场景下，如果想避免拆装箱，就需要为其绑定多个 binding 分别储存在 binder 中，而储存过多的 binding 对 binder 的拣选机制将造成负担，因此为其添加一种可以储存不用拆装箱就可以储存多个值的 binding。

二、 添加 PrefabInfo 类，为加载 prefab 提供异步和携程方法。

三、 添加 AssetBundleInfo 类，提供 AssetBundle 相关方法以及异步、携程方法。

对比不支持热更新的 SimpleContainer 的不同：

一、不建议使用 AssetBundleInfo 相关方法，因为 Tolua 自带相关方法，并且更有效率

二、由于 lua 的 table 是基础 c 实现的，比 c# 的字典效率更高，因此 binding 系统在 lua 中重新实现（未完成，暂未上传）

请移动到 https://github.com/joey1258/ToluaContainer/wiki 了解更多
