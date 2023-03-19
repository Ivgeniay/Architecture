using Architecture.Root.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root.Repositories
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

        public void CreateRepositories()
        {
            repositoriesPool = sceneSetting.BindRepositories();
        }

        public IEnumerator OnAwakeRepositoriesRoutine()
        {
            if (repositoriesPool.Count == 0) yield break;
            foreach (var repository in repositoriesPool)
            {
                yield return repository.Value.OnAwakeRoutine();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeRepositoriesRoutine()
        {
            if (repositoriesPool.Count == 0) yield break;
            foreach (var repository in repositoriesPool)
            {
                yield return repository.Value.InitializeRoutine();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartRepositoriesRoutine()
        {
            if (repositoriesPool.Count == 0) yield break;
            foreach (var repository in repositoriesPool)
            {
                yield return repository.Value.OnStartRoutine();
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Start);
            }
        }

        public void Frame()
        {
            if (repositoriesPool == null || repositoriesPool.Count == 0) return;
            foreach (var repository in repositoriesPool)
            {
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
