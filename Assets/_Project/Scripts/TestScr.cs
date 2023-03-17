using Architecture.DI;
using Architecture.DI.ActivationBuilds;
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
            builder = new ContainerBuilder(new LambdaBasedActivationBuild());
            var counainer = builder
                .RegistrationSingleton<ITestInterface, TestClass>()
                .RegistrationScoped<MainViewModel, MainViewModel>()
                .Build();

            var scope = counainer.CreateScope(); 
            var service = scope.Resolve(typeof(MainViewModel));
            var srt = (MainViewModel)service;
            srt.ff();
            var service2 = scope.Resolve(typeof(MainViewModel));
            var srt2 = (MainViewModel)service2;
            srt2.ff();

            if (srt == srt2)
            {

            }

            var scope2 = counainer.CreateScope();

            var service3 = scope2.Resolve(typeof(MainViewModel));
            var srt3 = (MainViewModel)service3;
            srt3.ff();

            Debug.Log(service);
        }
        //ContainerBuilder ioc = new ContainerBuilder();

        //private void Awake()
        //{
        //    ioc.Register<ITestInterface, TestClass>();
        //    ioc.Register<MainViewModel, MainViewModel>();

        //    var view = ioc.Resolve<MainViewModel>();
        //    Debug.Log(view);
        //}
    }
}
