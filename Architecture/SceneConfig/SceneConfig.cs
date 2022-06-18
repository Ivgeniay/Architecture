using System;
using System.Collections.Generic;
using UnityEngine;

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
            switch (scene.SceneName)
            {
                case "Main":
                    CreateNewInteractor<PlayerInteractor>();
                    Debug.Log("All interactors created");
                    break;
                
                default:
                    Debug.Log("There are not interactors in the scene");
                    break;
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
