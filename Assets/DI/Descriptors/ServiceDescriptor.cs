using System;

namespace DI.Descriptors
{
    internal abstract class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        public Lifetime Lifetime { get; set; }
    }
}
