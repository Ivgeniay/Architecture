﻿using Architecture.Root.Controllers;
using Architecture.Root.Repositories;
using Architecture.Root.Scenes;
using System;
using System.Collections.Generic;

namespace Assets.MainProject.Scripts.Architecture.Root.Scene
{
    internal class TestScene : SceneSetting
    {
        public override Dictionary<Type, Controller> BindControllers()
        {
            var controller = new Dictionary<Type, Controller>();

            BindController<PlayerController>(controller);

            return controller;
        }

        public override Dictionary<Type, Repository> BindRepositories()
        {
            var repos = new Dictionary<Type, Repository>();

            BindRepository<PlayerRepository>(repos);

            return repos;
        }
    }
}
