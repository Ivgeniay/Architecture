using Assets.MainProject.Scripts;

namespace Architecture.DI.Containers
{
    internal class Registration
    {
        public Container ConfigureServices()
        {
            var builder = new ContainerBuilder();
            builder.Register<IGovno, Govno>();
            builder.Register<MainViewModel, MainViewModel>();
            return builder.Build();
        }
    }
}
