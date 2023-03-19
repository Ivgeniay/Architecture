using DI.Containers;
using System;

namespace DI.Descriptors
{
    internal class FactoryBasedServiceDescriptor : ServiceDescriptor
    {
        public Func<IScope, object> Factory { get; set; }
    }
}
