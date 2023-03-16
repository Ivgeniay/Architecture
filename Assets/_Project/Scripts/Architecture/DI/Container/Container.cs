using Architecture.DI.Descriptors;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Architecture.DI.Containers
{
    internal class Container : IContainer
    {
        private readonly Dictionary<Type, ServiceDescriptor> descriptors = new ();
        private readonly ConcurrentDictionary<Type, Func<IScope, object>> builActivators = new();
        private readonly Scope rootScope;
        
        public Container(IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            descriptors = serviceDescriptors.ToDictionary(el => el.ServiceType);
            rootScope = new Scope(this);
        }

        public IScope CreateScope()
        {
            return new Scope(this);
        }

        private Func<IScope, object> BuildActivation(Type service)
        {
            if (! descriptors.TryGetValue(service, out ServiceDescriptor descriptor))
            {
                throw new InvalidOperationException($"There are no {service}. Not registered");
            }

            if (descriptor is InstanceBasedServiceDescriptor baseInst)
            {
                return _ => baseInst.Instance;
            }

            if (descriptor is FactoryBasedServiceDescriptor fb)
            {
                return fb.Factory;
            }

            var tb = (TypeBasedServiceDescriptor)descriptor;
            var ctor = tb.ImplementationType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var parameters = ctor.GetParameters();


            return s =>
            {
                var argsForCtor = new Object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    argsForCtor[i] = this.CreateInstance(parameters[i].ParameterType, s);
                }

                return ctor.Invoke(argsForCtor);
            };
        }

        private object CreateInstance(Type service, IScope scope)
        {
            return builActivators.GetOrAdd(service, BuildActivation)(scope);
        }
        private ServiceDescriptor FindDescriptor(Type service)
        {
            descriptors.TryGetValue(service, out var descriptor);
            return descriptor;
        }



        private class Scope : IScope
        {
            private readonly Container container;
            private readonly ConcurrentDictionary<Type, object> scopedInstances = new();
            public Scope(Container container) {
                this.container = container;
            }

            public object Resolve(Type service)
            {
                var descriptor = container.FindDescriptor(service);
                if (descriptor.Lifetime == Lifetime.Transient)
                    return container.CreateInstance(service, this);

                if (descriptor.Lifetime == Lifetime.Scoped || container.rootScope == this)
                {
                    return scopedInstances.GetOrAdd(service, s => container.CreateInstance(s, this));
                }
                else 
                {
                    return container.rootScope.Resolve(service);
                }
            }
        }

    }
}
