using Architecture.Root._Scene;
using Assets._Project.Scripts.Player;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Architecture.Root._Repository
{
    internal class RepositoriesPool
    {
        private Dictionary<Type, Repository> repositoriesPool;
        private SceneSetting sceneSetting;

        public RepositoriesPool(SceneSetting sceneSetting)
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
            yield return repositoriesPool.ForEach(el => Routine.StartRoutine(el.Value.OnAwake()));
        }

        public IEnumerator OnInitializeRepositories()
        {
            yield return repositoriesPool.ForEach(el => Routine.StartRoutine(el.Value.Initialize()));
        }

        public IEnumerator OnStartRepositories()
        {
            yield return repositoriesPool.ForEach(el => Routine.StartRoutine(el.Value.OnStart()));
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T)repositoriesPool[type];
        }
    }
}
