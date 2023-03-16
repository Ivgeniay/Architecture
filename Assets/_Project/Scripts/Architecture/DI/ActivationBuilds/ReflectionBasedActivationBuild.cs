using Architecture.DI.Containers;
using Architecture.DI.Descriptors;
using System.Reflection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Architecture.DI.Interfaces;

namespace Architecture.DI.ActivationBuilds
{
    internal class ReflectionBasedActivationBuild : IActivationBuilder
    {

        public Func<IScope, object> BuildActivation(ServiceDescriptor descriptor)
        {

            var tb = (TypeBasedServiceDescriptor)descriptor;
            var ctor = tb.ImplementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var parameters = ctor.GetParameters();


            return s =>
            {
                var argsForCtor = new Object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    argsForCtor[i] = s.Resolve(parameters[i].ParameterType);
                }

                return ctor.Invoke(argsForCtor);
            };
        }

    }
}
