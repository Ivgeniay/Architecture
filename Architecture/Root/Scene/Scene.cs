using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Architecture.Root._Scene
{
    internal class Scene 
    {
        private RepositoriesPool repositoriesPool;
        private ControllersPool controllersPool;

        public SceneSetting sceneSetting;

        public Scene(SceneSetting sceneSetting)
        {
            this.sceneSetting = sceneSetting;
            controllersPool = new ControllersPool(sceneSetting);
            repositoriesPool = new RepositoriesPool(sceneSetting);
        }

        public IEnumerator Initialize()
        {
            controllersPool.CreateControllers();
            repositoriesPool.CreateRepositories();
            yield return null;

            Routine.StartRoutine(controllersPool.OnAwakeControllers());
            Routine.StartRoutine(repositoriesPool.OnAwakeRepositories());
            yield return null;

            Routine.StartRoutine(controllersPool.OnInitializeControllers());
            Routine.StartRoutine(repositoriesPool.OnInitializeRepositories());
            yield return null;

            Routine.StartRoutine(controllersPool.OnStartControllers());
            Routine.StartRoutine(repositoriesPool.OnStartRepositories());
            yield return null;

        }

        public T GetRepository<T>() where T : Repository
        {
            return repositoriesPool.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller
        {
            return controllersPool.GetController<T>();
        }

    }
}
