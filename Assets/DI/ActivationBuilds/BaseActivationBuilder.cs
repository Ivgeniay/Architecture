using DI.Containers;
using DI.Descriptors;
using DI.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace DI.ActivationBuilds
{
    internal abstract class BaseActivationBuilder : IActivationBuilder
    {
        public Func<IScope, object> BuildActivation(ServiceDescriptor descriptor)
        {
            var tb = (TypeBasedServiceDescriptor)descriptor;
            var ctor = tb.ImplementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var parameters = ctor.GetParameters();

            return BuildActivationInternal(tb, ctor, parameters, descriptor);
        }

        protected abstract Func<IScope, object> BuildActivationInternal(TypeBasedServiceDescriptor tb, ConstructorInfo ctor, ParameterInfo[] parameters, ServiceDescriptor descriptor);
    }
}
