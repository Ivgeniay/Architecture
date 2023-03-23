using DI.Descriptors;
using DI.Interfaces;

namespace DI.Containers
{
    public interface IContainerBuilder
    {
        public void Register(ServiceDescriptor descriptor);
        IContainer Build();
    }
}
