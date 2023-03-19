using Architecture.Root.Controllers;
using Architecture.Root.Repositories;
using System;
using System.Collections;
using UnityEngine;

namespace Architecture.Root.MainProject
{
    internal class ProjectInstaller : Installer
    {
        public override event Action<object, LoadingEventType> OnControllerEvent;
        public override event Action<object, LoadingEventType> OnRepositoryEvent;

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

        public override IEnumerator InitializeRoutine() { yield return Initialize(); }
        protected override IEnumerator Initialize()
        {
            yield return null;
            repositoriesPool.CreateRepositories();
            controllersPool.CreateControllers();

            yield return repositoriesPool.OnAwakeRepositoriesRoutine();
            yield return controllersPool.OnAwakeControllersRoutine();
            OnAwakeEvent?.Invoke();

            yield return repositoriesPool.OnInitializeRepositoriesRoutine();
            yield return controllersPool.OnInitializeControllersRoutine();
            OnInitializedEvent?.Invoke();

            yield return repositoriesPool.OnStartRepositoriesRoutine();
            yield return controllersPool.OnStartControllersRoutine();
            OnStartEvent?.Invoke();

        }

        public override IEnumerator ExitCurrentScene()
        {
            yield return controllersPool.ExitCurrentSceneRoutine();
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
