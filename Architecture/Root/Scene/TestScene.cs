using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Scene;
using Assets._Project.Scripts.Player;
using System;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Architecture.Root.Scene
{
    internal class TestScene : SceneSetting
    {
        public override Dictionary<Type, Controller> CreateControllers()
        {
            var controller = new Dictionary<Type, Controller>();

            CreateController<PlayerController>(controller);

            return controller;
        }

        public override Dictionary<Type, Repository> CreateRepositories()
        {
            var repos = new Dictionary<Type, Repository>();

            CreateRepository<PlayerRepository>(repos);

            return repos;
        }
    }
}
