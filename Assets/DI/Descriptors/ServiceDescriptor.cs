using System;

namespace DI.Descriptors
{
    public abstract class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        public Lifetime Lifetime { get; set; }
    }
}
