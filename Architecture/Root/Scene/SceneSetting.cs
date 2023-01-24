using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture.Root._Scene
{
    internal abstract class SceneSetting : MonoBehaviour
    {
        public abstract Dictionary<Type, Repository> BindRepositories();
        public abstract Dictionary<Type, Controller> BindControllers();

        protected void BindController<T>(Dictionary<Type, Controller> controllers) where T : Controller, new ()
        {
            var controller = new T();
            var type = typeof(T);

            controllers.Add(type, controller);
        }

        protected void BindRepository<T>(Dictionary<Type, Repository> repositories) where T : Repository, new ()
        {
            var repository = new T();
            var type = typeof(T);

            repositories.Add(type, repository);
        }

    }
}
