using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections;
using UnityEngine;

namespace Architecture.Root._Project
{
    internal class ProjectInstaller : Installer
    {
        public override event Action<object, LoadingEventType> OnControllerEvent;
        public override event Action<object, LoadingEventType> OnRepositoryEvent;

        public override event Action OnResourcesCreateEvent;
        public override event Action OnAwakeEvent;
        public override event Action OnInitializedEvent;
        public override event Action OnStartEvent;


        private void Awake()
        {
            controllersPool = new ControllersPool(Setting);
            repositoriesPool = new RepositoriesPool(Setting);

            controllersPool.OnControllerEvent += OnControllerEventHandler;
            repositoriesPool.OnRepositoryEvent += OnRepositoryEventHandler;
        }

        public override IEnumerator InitializeAsync() { yield return Initialize(); }
        protected override IEnumerator Initialize()
        {
            yield return null;
            repositoriesPool.CreateRepositories();
            controllersPool.CreateControllers();
            OnResourcesCreateEvent?.Invoke();

            yield return repositoriesPool.OnAwakeRepositories();
            yield return controllersPool.OnAwakeControllers();
            OnAwakeEvent?.Invoke();

            yield return repositoriesPool.OnInitializeRepositories();
            yield return controllersPool.OnInitializeControllers();
            OnInitializedEvent?.Invoke();

            yield return repositoriesPool.OnStartRepositories();
            yield return controllersPool.OnStartControllers();
            OnStartEvent?.Invoke();

        }

        public override void Frame()
        {
            repositoriesPool.Frame();
            controllersPool.Frame();
        }

        public override T GetRepository<T>() => repositoriesPool.GetRepository<T>();
        public override T GetController<T>() => controllersPool.GetController<T>();
        private void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
    }
}
