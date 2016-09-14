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

/*
 * 由于Info的值约束类型默认就是MANY，所以无需特意写一个MANY创建方法。
 */

using System;

namespace LuaContainer.Container
{
    public class BindingFactory : IBindingFactory
    {
        public IBinder binder;

        /// <summary>
        /// binding 数组
        /// </summary>
        public IBinding[] bindings
        {
            get { return _bindings; }
        }
        protected IBinding[] _bindings;

        #region constructor

        public BindingFactory(IBinder binder) { this.binder = binder; }

        #endregion

        #region Create

        #region Create default (MANY)

        /// <summary>
        /// 创建并返回指定类型的 Binding 实例，ConstraintType 为 MULTIPLE
        /// </summary>
        virtual public IBinding Create<T>(BindingType bindingType)
        {
            return Create(typeof(T), bindingType, ConstraintType.MULTIPLE);
        }

        /// <summary>
        /// 创建并返回指定类型的 Binding 实例，ConstraintType 为 MULTIPLE
        /// </summary>
        virtual public IBinding Create(Type type, BindingType bindingType)
        {
            return Create(type, bindingType, ConstraintType.MULTIPLE);
        }

        #endregion

        #region Create SINGLE

        /// <summary>
        /// 创建并返回指定类型的 Binding 实例，ConstraintType 为 SINGLE
        /// </summary>
        virtual public IBinding CreateSingle<T>(BindingType bindingType)
        {
            return Create(typeof(T), bindingType, ConstraintType.SINGLE);
        }

        /// <summary>
        /// 创建并返回指定类型的 Binding 实例，ConstraintType 为 SINGLE
        /// </summary>
        virtual public IBinding CreateSingle(Type type, BindingType bindingType)
        {
            return Create(type, bindingType, ConstraintType.SINGLE);
        }

        #endregion

        /// <summary>
        /// 创建指定类型的多个 Binding 实例，ConstraintType 为 MULTIPLE，并返回 IBindingFactory
        /// </summary>
        virtual public IBindingFactory MultipleCreate(
            Type[] types,
            BindingType[] bindingType)
        {
            int length = types.Length;
            _bindings = new IBinding[length];

            for (int i = 0; i < length; i++)
            {
                ConstraintType cti = ConstraintType.MULTIPLE;
                if (bindingType[i] != BindingType.ADDRESS && 
                    bindingType[i] != BindingType.MULTITON)
                {
                    cti = ConstraintType.SINGLE;
                }

                bindings[i] = Create(
                    types[i],
                    bindingType[i],
                    cti);
            }

            return this;
        }

        /// <summary>
        /// 创建并返回指定类型的Binding实例
        /// </summary>
        virtual public IBinding Create<T>(BindingType bindingType, ConstraintType constraint)
        {
            return Create(typeof(T), bindingType, constraint);
        }

        /// <summary>
        /// 创建并返回指定类型的Binding实例
        /// </summary>
        virtual public IBinding Create(
            Type type,
            BindingType bindingType,
            ConstraintType constraint)
        {
            IBinding binding = new Binding(binder, type, bindingType, constraint);

            return binding;
        }

        #endregion

        #region Binding System Function

        /// <summary>
        /// 为多个 binding 添加值，如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        virtual public IBindingFactory To(object[] os)
        {
            // 不允许参数长度大于 bindings 长度
            if (os.Length > bindings.Length)
            {
                throw new BindingSystemException(BindingSystemException.PARAMETERS_LENGTH_ERROR);
            }

            int length = bindings.Length;
            int osLength = os.Length;
            int osi = 0;
            for (int i = 0; i < length; i++)
            {
                if(os[osi] is Array)
                {
                    object[] oa = (object[])os[osi];
                    bindings[i].To(oa);
                }
                else { bindings[i].To(os[osi]); }
                if (osi < osLength - 1) { osi++; }
            }

            return this;
        }

        /// <summary>
        /// 设置多个 binding 的 id 属性
        /// </summary>
        virtual public IBindingFactory As(object[] os)
        {
            // 由于 id 必须是唯一的，所以如果参数和 binding的数量不同则将抛出异常
            if (os.Length != bindings.Length)
            {
                throw new BindingSystemException(BindingSystemException.PARAMETERS_LENGTH_ERROR);
            }

            int length = bindings.Length;
            for (int i = 0; i < length; i++)
            {
                bindings[i].As(os[i]);
            }

            return this;
        }

        /// <summary>
        /// 设置多个 binding 的 condition 属性
        /// 如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        virtual public IBindingFactory When(Condition[] cs)
        {
            // 不允许参数长度大于 bindings 长度
            if (cs.Length > bindings.Length)
            {
                throw new BindingSystemException(BindingSystemException.PARAMETERS_LENGTH_ERROR);
            }

            int length = bindings.Length;
            int csLength = cs.Length;
            int csi = 0;
            for (int i = 0; i < length; i++)
            {
                bindings[i].condition = cs[csi];
                if (csi < csLength - 1) { csi++; }
            }

            return this;
        }

        /// <summary>
        /// 设置多个 binding 的 condition 属性为 context.parentType 与指定类型相等
        /// 如果参数长度短于 binding 数量，参数的最后一个元素将被重复使用
        /// </summary>
        virtual public IBindingFactory Into(Type[] ts)
        {
            // 不允许参数长度大于 bindings 长度
            if (ts.Length > bindings.Length)
            {
                throw new BindingSystemException(BindingSystemException.PARAMETERS_LENGTH_ERROR);
            }

            int length = bindings.Length;
            int tsLength = ts.Length;
            int tsi = 0;
            for (int i = 0; i < length; i++)
            {
                bindings[i].condition = context => context.parentType == ts[tsi];
                if (tsi < tsLength - 1) { tsi++; }
            }

            return this;
        }

        /// <summary>
        /// 返回一个新的Binding实例,并设置指定类型给 type, BindingType 为 ADDRESS，值约束为 MULTIPLE
        /// </summary>
        virtual public IBinding Bind<T>()
        {
            return binder.Bind<T>();
        }

        /// <summary>
        /// 返回一个新的Binding实例，并设置指定类型给 type, BindingType 为 SINGLETON，值约束为 SINGLE
        /// </summary>
        virtual public IBinding BindSingleton<T>()
        {
            return binder.Bind<T>();
        }

        /// <summary>
        /// 返回一个新的Binding实例，并设置指定类型给 type, BindingType 为 FACTORY，值约束为 SINGLE
        /// </summary>
        virtual public IBinding BindFactory<T>()
        {
            return binder.Bind<T>();
        }

        /// <summary>
        /// 创建多个指定类型的 binding，并返回 IBindingFactory
        /// </summary>
        virtual public IBindingFactory MultipleBind(Type[] types, BindingType[] bindingTypes)
        {
            if (types == null || types.Length == 0)
            {
                throw new ArgumentNullException("types");
            }
            if (bindingTypes == null || bindingTypes.Length == 0)
            {
                throw new ArgumentNullException("bindingTypes");
            }

            if (types.Length != bindingTypes.Length)
            {
                throw new BindingSystemException(BindingSystemException.PARAMETERS_LENGTH_ERROR);
            }

            return MultipleCreate(types, bindingTypes);
        }

        #endregion
    }
}