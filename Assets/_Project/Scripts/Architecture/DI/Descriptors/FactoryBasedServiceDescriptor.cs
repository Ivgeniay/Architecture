using Architecture.DI.Containers;
using System;

namespace Architecture.DI.Descriptors
{
    internal class FactoryBasedServiceDescriptor : ServiceDescriptor
    {
        public Func<IScope, object> Factory { get; set; }
    }
}
