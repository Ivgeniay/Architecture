using DI.Descriptors;
using DI.Interfaces;

namespace DI.Containers
{
    internal interface IContainerBuilder
    {
        public void Register(ServiceDescriptor descriptor);
        IContainer Build();
    }
}
