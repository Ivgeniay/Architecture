using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Project;
using System;
using System.Collections.Generic;
using NetWork.Twitch._Client;
using NetWork.Twitch._Auth;
using NetWork.Twitch._PubSub;

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
