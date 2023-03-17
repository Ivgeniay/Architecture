using System;

namespace Architecture.DI.Descriptors
{
    internal class TypeBasedServiceDescriptor : ServiceDescriptor
    {
        public Type ImplementationType { get; set; }
    }
}
