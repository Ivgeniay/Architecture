using Architecture.Root._Scene;
using Assets._Project.Scripts.Player;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root._Controller
{
    internal class ControllersPool
    {
        private Dictionary<Type, Controller> controllersPool;
        private SceneSetting sceneSetting;

        public ControllersPool(SceneSetting sceneSetting) {
            controllersPool = new Dictionary<Type, Controller>();
            this.sceneSetting = sceneSetting;
        }

        public void CreateControllers()
        {
            controllersPool = sceneSetting.BindControllers();
        }

        public IEnumerator OnAwakeControllers() {
            yield return controllersPool.ForEach(el => Routine.StartRoutine(el.Value.OnAwake()));
        }

        public IEnumerator OnInitializeControllers() {
            yield return controllersPool.ForEach(el => Routine.StartRoutine(el.Value.Initialize()));
        }

        public IEnumerator OnStartControllers() {
            yield return controllersPool.ForEach(el => Routine.StartRoutine(el.Value.OnStart()));
        }

        public T GetController<T>() where T : Controller
        {
            var type = typeof(T);
            return (T) controllersPool[type];
        }
    }
}
