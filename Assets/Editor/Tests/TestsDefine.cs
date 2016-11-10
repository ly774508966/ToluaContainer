/**
 * This file is part of ToluaContainer.
 *
 * Licensed under The MIT License
 * For full copyright and license information, please see the MIT-LICENSE.txt
 * Redistributions of files must retain the above copyright notice.
 *
 * @copyright Joey1258
 * @link https://github.com/joey1258/ToluaContainer
 * @license http://www.opensource.org/licenses/mit-license.php MIT License
 */


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using ToluaContainer;
using ToluaContainer.Container;

namespace ToluaContainer_NUitTests
{
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
            num ++;
            ((someClass)parameters[0]).id = num;
        }
    }
}