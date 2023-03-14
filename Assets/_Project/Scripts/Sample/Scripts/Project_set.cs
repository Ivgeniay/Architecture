using Architecture.Root.Controllers;
using Architecture.Root.Repositories;
using Architecture.Root.MainProject;
using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts
{
    internal class Project_set : ProjectSetting
    {
        public override Dictionary<Type, Controller> BindControllers()
        {
            var controllers = new Dictionary<Type, Controller>();

            BindController<GameController>(controllers);

            return controllers;
        }

        public override Dictionary<Type, Repository> BindRepositories()
        {
            var repositories = new Dictionary<Type, Repository>();

            BindRepository<GameRepository>(repositories);

            return repositories;
        }
    }
}
