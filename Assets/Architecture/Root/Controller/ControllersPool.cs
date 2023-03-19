using Architecture.Root.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root.Controllers
{
    internal class ControllersPool
    {
        public event Action<object, LoadingEventType> OnControllerEvent;

        private Dictionary<Type, Controller> controllersPool;
        private InstallerSetting sceneSetting;

        public ControllersPool(InstallerSetting sceneSetting)
        {
            controllersPool = new Dictionary<Type, Controller>();
            this.sceneSetting = sceneSetting;
        }

        public void CreateControllers()
        {
            controllersPool = sceneSetting.BindControllers();
        }

        public IEnumerator OnAwakeControllersRoutine()
        {
            if (controllersPool.Count == 0) yield break;

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.OnAwakeRoutine();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeControllersRoutine()
        {
            if (controllersPool.Count == 0) yield break;

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.InitializeRoutine();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartControllersRoutine()
        {
            if (controllersPool.Count == 0) yield break;

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.OnStartRoutine();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Start);
            }
        }

        public IEnumerator ExitCurrentSceneRoutine()
        {
            if (controllersPool.Count == 0) yield break;

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.OnExitRoutine();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Exit);
            }
        }

        public void Frame()
        {
            if (controllersPool == null || controllersPool.Count == 0) return;
            foreach (var controller in controllersPool)
            {
                controller.Value.Frame();
            }
        }

        public T GetController<T>() where T : Controller
        {
            var type = typeof(T);
            controllersPool.TryGetValue(type, out var controller);
            return (T)controller;
        }
    }
}
