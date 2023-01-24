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

        public event Action OnResourceCreate;
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private RepositoriesPool repositoriesPool;
        private ControllersPool controllersPool;


        private void Awake() {
            controllersPool = new ControllersPool(sceneSetting);
            repositoriesPool = new RepositoriesPool(sceneSetting);
        }

        public void InitializeSceneAsync() => this.StartCoroutine(Initialize());

        private IEnumerator Initialize()
        {
            yield return null;

            repositoriesPool.CreateRepositories();
            controllersPool.CreateControllers();
            OnResourceCreate?.Invoke();

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

    }
}
