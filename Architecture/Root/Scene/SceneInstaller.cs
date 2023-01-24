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

            controllersPool.CreateControllers();
            repositoriesPool.CreateRepositories();

            yield return controllersPool.OnAwakeControllers();
            yield return repositoriesPool.OnAwakeRepositories();
            OnSceneAwake?.Invoke();

            yield return controllersPool.OnInitializeControllers();
            yield return repositoriesPool.OnInitializeRepositories();
            OnSceneInitialized?.Invoke();

            yield return controllersPool.OnStartControllers();
            yield return repositoriesPool.OnStartRepositories();
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
