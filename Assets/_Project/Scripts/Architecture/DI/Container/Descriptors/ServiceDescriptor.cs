using System;

namespace Architecture.DI.Descriptors
{
    internal abstract class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        public Lifetime Lifetime { get; set; }
    }

    public enum Lifetime
    {
        Transient,
        Scoped,
        Singleton
    }
}
