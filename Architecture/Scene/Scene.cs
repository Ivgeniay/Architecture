using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture
{
    public class Scene
    {
        /// <summary>  
        ///  Event fired when all interactors and their repositories have finished loading.
        /// </summary> 
        public event Action<string> onLoadedEvent;
        public Scene()
        {
            interactorsPool = new();
            repositoryPool = new();

            SceneName = SceneManager.GetActiveScene().name;
            config = new SceneConfig(this);

            interactorsPool.interactorsMap = config.CreateAllInteractors();
            
            Routine.StartRoutine(StartArchitecture(interactorsPool.interactorsMap));
        }

        /// <summary>  
        ///  The loading state of the application architecture.
        /// </summary> 
        public bool IsLoaded{get; private set;}
        public string SceneName {get; private set;}
        private SceneConfig config;
        private InteractorsPool interactorsPool;
        private RepositoriesPool repositoryPool;

        public T GetInteractor<T>() where T: InteractorBase
        {
            var type = typeof(T);
            return (T)interactorsPool.interactorsMap[type];
        }

        private IEnumerator StartArchitecture(Dictionary<Type, InteractorBase> map)
        {
            this.IsLoaded = false;

            yield return Routine.StartRoutine(InitializeInteractorsRoutine(map));
            yield return Routine.StartRoutine(StartInteractorsRoutine(map));

            yield return new WaitForSeconds(2);

            this.IsLoaded = true;
            onLoadedEvent?.Invoke(SceneName);
        }


        private IEnumerator InitializeInteractorsRoutine(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.InitializeInteractor();
            }
            yield return null;

            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.InitializeRepository();
            }
            yield return null;
        }

        private IEnumerator StartInteractorsRoutine(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.StartInteractor();
            }
            yield return null;

            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                pair.Value.StartRepository();
            }
            yield return null;
        }


    }
}
