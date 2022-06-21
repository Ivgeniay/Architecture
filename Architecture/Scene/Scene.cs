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
        public bool IsLoaded{get; private set;}                     //
        public string SceneName {get; private set;}                 //Current scene name.
        public ReviewVariable<int> NumOfInteractorsWereLoaded;      //Number of interactor who was already loaded.
        public int NumOfInteractors{get; private set;}              //Number of interactor in the dictionary.
        private SceneConfig config;                                 //Scene configuration. There are all interactors.
        private InteractorsPool interactorsPool;                    //There is dictionary of interactors



        public T GetInteractor<T>() where T: InteractorBase
        {
            var type = typeof(T);
            return interactorsPool.interactorsMap.ContainsKey(type) ? (T)interactorsPool.interactorsMap[type] : null;
        }

        private IEnumerator StartArchitecture(Dictionary<Type, InteractorBase> map)
        {

            yield return Routine.StartRoutine(InitializeInteractorsRoutine(map));

            this.IsLoaded = true;
            onLoadedEvent?.Invoke(SceneName);
        }

        private IEnumerator InitializeInteractorsRoutine(Dictionary<Type, InteractorBase> map)
        {
            foreach(KeyValuePair<Type, InteractorBase> pair in map)
            {
                yield return Routine.StartRoutine(pair.Value.InitializeInteractor());
                yield return Routine.StartRoutine(pair.Value.InitializeRepository());
                yield return Routine.StartRoutine(pair.Value.StartInteractor());
                yield return Routine.StartRoutine(pair.Value.StartRepository());
                NumOfInteractorsWereLoaded.Value++;
            }
        }
    }
}
