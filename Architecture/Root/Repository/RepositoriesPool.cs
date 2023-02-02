using Architecture.Root._Controller;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root._Repository
{
    internal class RepositoriesPool
    {
        public event Action<object, LoadingEventType> OnRepositoryEvent;

        private Dictionary<Type, Repository> repositoriesPool;
        private InstallerSetting sceneSetting;

        public RepositoriesPool(InstallerSetting sceneSetting)
        {
            repositoriesPool = new Dictionary<Type, Repository>();
            this.sceneSetting = sceneSetting;
        }

        public void CreateRepositories() {
            repositoriesPool = sceneSetting.BindRepositories();
        }

        public IEnumerator OnAwakeRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                //yield return Routine.Instance.StartCoroutine(repository.Value.OnAwakeEvent());
                yield return repository.Value.OnAwake();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                yield return repository.Value.Initialize();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                yield return repository.Value.OnStart();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Start);
            }
        }

        public void Frame()
        {
            if (repositoriesPool == null || repositoriesPool.Count == 0) return;
            foreach(var repository in repositoriesPool) {
                repository.Value.Frame();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            repositoriesPool.TryGetValue(type, out var repository);
            return (T)repository;
        }
    }
}
