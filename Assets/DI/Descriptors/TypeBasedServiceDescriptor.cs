using System;

namespace DI.Descriptors
{
    internal class TypeBasedServiceDescriptor : ServiceDescriptor
    {
        public Type ImplementationType { get; set; }
    }
}
