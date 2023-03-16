using Architecture.DI;
using UnityEngine;

namespace Assets.MainProject.Scripts
{
    internal class TestScr : MonoBehaviour
    {

        ContainerBuilder ioc = new ContainerBuilder();

        private void Awake()
        {
            ioc.Register<IGovno, Govno>();
            ioc.Register<MainViewModel, MainViewModel>();

            var view = ioc.Resolve<MainViewModel>();
            Debug.Log(view);
        }
    }
}
