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
                yield return Routine.instance.StartCoroutine(contoller.Value.OnAwake());
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeControllers() {

            foreach (var contoller in controllersPool)
            {
                yield return Routine.instance.StartCoroutine(contoller.Value.Initialize());
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartControllers() {

            foreach (var contoller in controllersPool)
            {
                yield return Routine.instance.StartCoroutine(contoller.Value.OnStart());
                OnControllerEvent?.Invoke(contoller.Value, LoadingEventType.Start);
            }
        }

        public T GetController<T>() where T : Controller
        {
            var type = typeof(T);
            return (T) controllersPool[type];
        }
    }
}
