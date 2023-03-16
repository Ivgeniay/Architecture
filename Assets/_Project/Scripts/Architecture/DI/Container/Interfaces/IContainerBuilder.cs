using Architecture.DI.Descriptors;

namespace Architecture.DI.Containers
{
    internal interface IContainerBuilder
    {
        public void Register(ServiceDescriptor descriptor);
        IContainer Build();
    }
}
