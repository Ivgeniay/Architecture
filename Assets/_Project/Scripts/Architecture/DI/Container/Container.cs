using Architecture.DI.Descriptors;
using Architecture.DI.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture.DI.Containers
{
    internal class Container : IContainer, IDisposable, IAsyncDisposable
    {
        private readonly IActivationBuilder builder;
        private readonly Dictionary<Type, ServiceDescriptor> descriptors = new ();
        private readonly ConcurrentDictionary<Type, Func<IScope, object>> builActivators = new();
        private readonly Scope rootScope;
        
        public Container(IEnumerable<ServiceDescriptor> serviceDescriptors, IActivationBuilder activationBuilder)
        {
            builder = activationBuilder;
            descriptors = serviceDescriptors.ToDictionary(el => el.ServiceType);
            rootScope = new Scope(this);
        }

        public IScope CreateScope()
        {
            return new Scope(this);
        }

        private Func<IScope, object> BuildActivation(Type service)
        {
            if (! descriptors.TryGetValue(service, out ServiceDescriptor descriptor)) {
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

            return builder.BuildActivation(descriptor);
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

        public void Dispose() => rootScope.Dispose();
        public ValueTask DisposeAsync() => rootScope.DisposeAsync();
        



        private class Scope : IScope
        {
            private readonly Container container;
            private readonly ConcurrentDictionary<Type, object> scopedInstances = new();
            private readonly ConcurrentStack<object> disposables = new ();
            public Scope(Container container) {
                this.container = container;
            }

            public object Resolve(Type service)
            {
                var descriptor = container.FindDescriptor(service);
                if (descriptor.Lifetime == Lifetime.Transient)
                    return CreateInstanceInternal(service);

                if (descriptor.Lifetime == Lifetime.Scoped || container.rootScope == this) {
                    return scopedInstances.GetOrAdd(service, s => CreateInstanceInternal(service));
                }
                else  {
                    return container.rootScope.Resolve(service);
                }
            }

            private object CreateInstanceInternal(Type service)
            {
                var result = container.CreateInstance(service, this);
                if (result is IDisposable || result is IAsyncDisposable)
                    disposables.Push(result);

                return result;
            }

            public void Dispose()
            {
                foreach (var instance in disposables)
                {
                    if (instance is IDisposable dis)
                        dis.Dispose();
                    else if (instance is IAsyncDisposable asyncDis)                    
                        asyncDis.DisposeAsync().GetAwaiter().GetResult();
                    
                }
            }

            public async ValueTask DisposeAsync()
            {
                foreach (var instance in disposables)
                {
                    if (instance is IAsyncDisposable asyncDis)
                        await asyncDis.DisposeAsync();
                    else if (instance is IDisposable dis)                    
                        dis.Dispose();
                    
                }
            }
        }

    }
}
