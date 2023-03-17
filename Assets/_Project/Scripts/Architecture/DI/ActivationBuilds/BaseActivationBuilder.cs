using Architecture.DI.Containers;
using Architecture.DI.Descriptors;
using Architecture.DI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Architecture.DI.ActivationBuilds
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
