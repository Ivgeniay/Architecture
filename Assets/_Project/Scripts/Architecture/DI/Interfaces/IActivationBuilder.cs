using Architecture.DI.Containers;
using Architecture.DI.Descriptors;
using System;

namespace Architecture.DI.Interfaces
{
    internal interface IActivationBuilder 
    {
        public Func<IScope, object> BuildActivation(ServiceDescriptor descriptor);
    }
}
