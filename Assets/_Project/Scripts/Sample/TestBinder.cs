using DI.Containers;
using DI.Containers.Extensions;
using DI.MonoDI;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Sample
{
    internal class TestBinder : Binder
    {
        [SerializeField] private TestClass testClass;
        public override IContainerBuilder BindContainer(IContainerBuilder builder)
        {
            builder.RegistrationSingletonFromInstance<TestClass>(testClass);
            return base.BindContainer(builder);
        }
    }
}
