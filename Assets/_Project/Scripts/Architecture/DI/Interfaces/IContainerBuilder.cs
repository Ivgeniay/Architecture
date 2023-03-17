using Architecture.DI.Descriptors;
using Architecture.DI.Interfaces;

namespace Architecture.DI.Containers
{
    internal interface IContainerBuilder
    {
        public void Register(ServiceDescriptor descriptor);
        IContainer Build();
    }
}
