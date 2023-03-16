using Architecture.DI;
using Architecture.DI.Containers;
using Architecture.DI.Containers.Extensions;
using UnityEngine;

namespace Assets.MainProject.Scripts
{
    internal class TestScr : MonoBehaviour
    {
        IContainerBuilder builder;

        private void Awake()
        {
            builder = new ContainerBuilder();
            var counainer = builder
                .RegistrationSingleton<IGovno, Govno>()
                .RegistrationSingleton<MainViewModel, MainViewModel>()
                .Build();

            var scope = counainer.CreateScope();
            var service = scope.Resolve(typeof(MainViewModel));
            var srt = (MainViewModel)service;
            srt.ff();
            var service2 = scope.Resolve(typeof(MainViewModel));
            srt = (MainViewModel)service2;
            srt.ff();

            Debug.Log(service);
        }
        //ContainerBuilder ioc = new ContainerBuilder();

        //private void Awake()
        //{
        //    ioc.Register<IGovno, Govno>();
        //    ioc.Register<MainViewModel, MainViewModel>();

        //    var view = ioc.Resolve<MainViewModel>();
        //    Debug.Log(view);
        //}
    }
}
