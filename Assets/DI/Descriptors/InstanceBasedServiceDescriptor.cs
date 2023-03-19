using System;

namespace DI.Descriptors
{
    internal class InstanceBasedServiceDescriptor : ServiceDescriptor
    {
        public object Instance { get; set; }

        public InstanceBasedServiceDescriptor(Type serviceType, object instance) {
            Lifetime = Lifetime.Singleton;
            ServiceType = serviceType;
            Instance = instance;
        }
    }
}
