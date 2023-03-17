using Architecture.DI.Containers;
using Architecture.DI.Descriptors;
using System.Reflection;
using System;
namespace Architecture.DI.ActivationBuilds
{
    internal class ReflectionBasedActivationBuild : BaseActivationBuilder
    {
        protected override Func<IScope, object> BuildActivationInternal(TypeBasedServiceDescriptor tb, ConstructorInfo ctor, ParameterInfo[] parameters, ServiceDescriptor descriptor)
        {
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
