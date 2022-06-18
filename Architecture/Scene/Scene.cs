using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TaskQueues;

namespace Architecture
{
    public class Scene
    {
        public Scene()
        {
            SceneName = SceneManager.GetActiveScene().name;
            interactorsPool = new InteractorsPool();
            repositoryPool = new RepositoriesPool();
            config = new SceneConfig(this);
            interactorsPool.interactorsMap = config.CreateAllInteractors();
            InitializeInteractors(interactorsPool.interactorsMap);
            StartInteractors(interactorsPool.interactorsMap);

        }
        private string sceneName;
        public string SceneName 
        {
            get => sceneName;
            private set => sceneName = value;
        }
        private SceneConfig config;
        private InteractorsPool interactorsPool;
        private RepositoriesPool repositoryPool;

        public T GetInteractor<T>() where T: InteractorBase
        {
            var type = typeof(T);
            return (T)interactorsPool.interactorsMap[type];
        }

        private void InitializeInteractors(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.Initialization();
                pair.Value.InitializeRepository();
            }
        }
        private void StartInteractors(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.Start();
                pair.Value.StartRepository();
            }
        }

    }
}
