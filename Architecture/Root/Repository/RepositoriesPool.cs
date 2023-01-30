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

        public void CreateRepositories()
        {
            repositoriesPool = sceneSetting.BindRepositories();
        }

        public IEnumerator OnAwakeRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                yield return Routine.StartRoutine(repository.Value.OnAwake());
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Awake);
            }
        }

        public IEnumerator OnInitializeRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                yield return Routine.StartRoutine(repository.Value.Initialize());
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Initialized);
            }
        }

        public IEnumerator OnStartRepositories()
        {
            foreach (var repository in repositoriesPool)
            {
                yield return Routine.StartRoutine(repository.Value.OnStart());
                OnRepositoryEvent?.Invoke(repository.Value, LoadingEventType.Start);
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T)repositoriesPool[type];
        }
    }
}
