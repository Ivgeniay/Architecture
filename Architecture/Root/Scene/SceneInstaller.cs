using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Architecture.Root._Scene
{
    internal class SceneInstaller : MonoBehaviour
    {
        [SerializeField] public string SceneName;
        [SerializeField] public SceneSetting sceneSetting;


        public event Action<object, LoadingEventType> OnControllerEvent;
        public event Action<object, LoadingEventType> OnRepositoryEvent;

        public event Action OnResourcesCreate;
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private RepositoriesPool repositoriesPool;
        private ControllersPool controllersPool;


        private void Awake() {
            controllersPool = new ControllersPool(sceneSetting);
            repositoriesPool = new RepositoriesPool(sceneSetting);

            controllersPool.OnControllerEvent += OnControllerEvent_;
            repositoriesPool.OnRepositoryEvent += OnRepositoryEvent_;
        }

        public void InitializeSceneAsync() => this.StartCoroutine(Initialize());

        private IEnumerator Initialize()
        {
            yield return null;

            repositoriesPool.CreateRepositories();
            controllersPool.CreateControllers();
            OnResourcesCreate?.Invoke();

            yield return repositoriesPool.OnAwakeRepositories();
            yield return controllersPool.OnAwakeControllers();
            OnSceneAwake?.Invoke();

            yield return repositoriesPool.OnInitializeRepositories();
            yield return controllersPool.OnInitializeControllers();
            OnSceneInitialized?.Invoke();

            yield return repositoriesPool.OnStartRepositories();
            yield return controllersPool.OnStartControllers();
            OnSceneStart?.Invoke();

        }

        public T GetRepository<T>() where T : Repository {
            return repositoriesPool.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller {
            return controllersPool.GetController<T>();
        }

        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);

    }
}
