using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Scene;
using System;
using System.Collections.Generic;

internal class LoadScene : SceneSetting
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

