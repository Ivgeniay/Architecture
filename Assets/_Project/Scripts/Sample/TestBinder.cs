using DI.Containers;
using DI.Containers.Extensions;
using DI.MonoDI;
using UnityEngine;

namespace Assets._Project.Scripts.Sample
{
    internal class TestBinder : Binder
    {
        [SerializeField] private TestClass testClass;
        protected override IContainerBuilder BindContainer(IContainerBuilder builder)
        {
            builder.RegistrationSingletonFromInstance<TestClass>(testClass);
            return builder;
        }
    }
}
