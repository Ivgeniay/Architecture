using Architecture.Root._Repository;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root._Controller
{
    internal class ControllersPool
    {
        public event Action<object, LoadingEventType> OnControllerEvent;

        private Dictionary<Type, Controller> controllersPool;
        private InstallerSetting sceneSetting;

        public ControllersPool(InstallerSetting sceneSetting) {
            controllersPool = new Dictionary<Type, Controller>();
            this.sceneSetting = sceneSetting;
        }

        public void CreateControllers()
        {
            controllersPool = sceneSetting.BindControllers();
        }

        public IEnumerator OnAwakeControllers() {

            foreach(var contoller in controllersPool)
            {
                yield return contoller.Value.OnAwake();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeControllers() {

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.Initialize();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartControllers() {

            foreach (var contoller in controllersPool)
            {
                yield return contoller.Value.OnStart();
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Start);
            }
        }
        public void Frame()
        {
            if (controllersPool == null || controllersPool.Count == 0) return;
            foreach (var controller in controllersPool) {
                controller.Value.Frame();
            }
        }

        public T GetController<T>() where T : Controller
        {
            var type = typeof(T);
            controllersPool.TryGetValue(type, out var controller);
            return (T) controller;
        }
    }
}
