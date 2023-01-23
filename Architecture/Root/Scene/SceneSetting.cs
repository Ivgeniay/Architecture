using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections.Generic;

namespace Architecture.Root._Scene
{
    internal abstract class SceneSetting
    {
        public abstract Dictionary<Type, Repository> CreateRepositories();
        public abstract Dictionary<Type, Controller> CreateControllers();

        public void CreateController<T>(Dictionary<Type, Controller> controllers) where T : Controller, new ()
        {
            var controller = new T();
            var type = typeof(T);
            controllers.Add(type, controller);
        }

        public void CreateRepository<T>(Dictionary<Type, Repository> repositories) where T : Repository, new ()
        {
            var repository = new T();
            var type = typeof(T);

            repositories.Add(type, repository);
        }
    }
}
