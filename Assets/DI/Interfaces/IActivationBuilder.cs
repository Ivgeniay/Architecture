using DI.Containers;
using DI.Descriptors;
using System;

namespace DI.Interfaces
{
    public interface IActivationBuilder 
    {
        public Func<IScope, object> BuildActivation(ServiceDescriptor descriptor);
    }
}
