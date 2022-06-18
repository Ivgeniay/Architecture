using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture
{
    public class SceneConfig
    {
        private Scene scene;
        public SceneConfig(Scene scene)
        {
            this.scene = scene;
            interactorsMap = new Dictionary<Type, InteractorBase>();
            repositoriesMap = new Dictionary<Type, RepositoryBase>();
        }

        private Dictionary<Type, InteractorBase> interactorsMap;
        public Dictionary<Type, RepositoryBase> repositoriesMap;

        public Dictionary<Type, InteractorBase> CreateAllInteractors()
        {
            if (scene.SceneName == "Main")
            {
                CreateNewInteractor<PlayerInteractor>();
                Debug.Log("All interactors created");
            }

            return interactorsMap;
        }
        private void CreateNewInteractor<T>() where T : InteractorBase, new()
        {
            var interactor = new T();
            var type = typeof(T);
            interactorsMap[type] = interactor;
        }
    }
}
