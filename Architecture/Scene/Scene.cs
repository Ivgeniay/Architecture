using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Review;

namespace Architecture
{
    public sealed class Scene
    {
        /// <summary>  
        ///  Event fired when all interactors and their repositories have finished loading.
        /// </summary> 
        public event Action<string> onLoadedEvent;
        public Scene()
        {
            interactorsPool = new();
            repositoryPool = new();
            NumOfInteractorsWereLoaded = new();

            SceneName = SceneManager.GetActiveScene().name;
            config = new SceneConfig(this);

            interactorsPool.interactorsMap = config.CreateAllInteractors();
            NumOfInteractors = interactorsPool.interactorsMap.Count;
            
            Routine.StartRoutine(StartArchitecture(interactorsPool.interactorsMap));
        }

        /// <summary>  
        ///  The loading state of the application architecture.
        /// </summary> 
        public bool IsLoaded{get; private set;}
        public string SceneName {get; private set;}
        public ReviewVariable<int> NumOfInteractorsWereLoaded;
        public int NumOfInteractors{get; private set;}
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
            Debug.Log("All interactors are initialized");
            yield return Routine.StartRoutine(StartInteractorsRoutine(map));
            Debug.Log("All interactors started");

            this.IsLoaded = true;
            onLoadedEvent?.Invoke(SceneName);
        }

        private IEnumerator InitializeInteractorsRoutine(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                yield return Routine.StartRoutine(pair.Value.InitializeInteractor());
            }

            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                yield return Routine.StartRoutine(pair.Value.InitializeRepository());
            }
        }

        private IEnumerator StartInteractorsRoutine(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                yield return Routine.StartRoutine(pair.Value.StartInteractor());
            }

            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                yield return Routine.StartRoutine(pair.Value.StartRepository());
                NumOfInteractorsWereLoaded.Value++;
            }
        }
    }
}
