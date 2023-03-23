using DI.Descriptors;

namespace DI.Containers
{
    public interface IContainerBuilder
    {
        public void Register(ServiceDescriptor descriptor);
        public IContainer Build();
    }
}
