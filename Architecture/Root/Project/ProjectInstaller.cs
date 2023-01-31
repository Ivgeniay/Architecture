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

        public override event Action OnResourcesCreate;
        public override event Action OnAwake;
        public override event Action OnInitialized;
        public override event Action OnStart;


        private void Awake()
        {
            controllersPool = new ControllersPool(Setting);
            repositoriesPool = new RepositoriesPool(Setting);

            controllersPool.OnControllerEvent += OnControllerEvent_;
            repositoriesPool.OnRepositoryEvent += OnRepositoryEvent_;
        }

        public override IEnumerator InitializeAsync() { yield return Initialize(); }
        protected override IEnumerator Initialize()
        {
            yield return null;
            repositoriesPool.CreateRepositories();
            controllersPool.CreateControllers();
            OnResourcesCreate?.Invoke();

            yield return repositoriesPool.OnAwakeRepositories();
            yield return controllersPool.OnAwakeControllers();
            OnAwake?.Invoke();

            yield return repositoriesPool.OnInitializeRepositories();
            yield return controllersPool.OnInitializeControllers();
            OnInitialized?.Invoke();

            yield return repositoriesPool.OnStartRepositories();
            yield return controllersPool.OnStartControllers();
            OnStart?.Invoke();

        }

        public override void Frame()
        {
            repositoriesPool.Frame();
            controllersPool.Frame();
        }

        public override T GetRepository<T>() => repositoriesPool.GetRepository<T>();
        public override T GetController<T>() => controllersPool.GetController<T>();
        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
    }
}
