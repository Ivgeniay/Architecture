using UnityEngine;
using System;
using Review;

namespace Architecture
{
    /// <summary>  
    ///  Basic element of architecture. Used to initialize all interectors "App.Initialize()" and access interactors "App.GetInteractor".
    /// </summary> 
    public static class App
    {
        /// <summary>  
        ///  Initialize all interactors at the scene. Use once on scene in the Awake method;
        /// </summary> 
        public static void Initialize() 
        {
            CreateScene();
        }

        /// <summary>  
        /// Event reporting the end of loading all interactors and their repositories
        /// </summary>
        public static event Action onLoadedAppEvent;
        /// <summary>  
        ///  The loading state of the application architecture.
        /// </summary> 
        public static bool IsLoaded() => scene.IsLoaded;
        private static Scene scene;
        /// <summary>  
        ///  Observable variable of type int32. The Value property reflects the number of initialized interactors. There is a subscription to the value change event - OnChange.
        /// </summary> 
        public static ReviewVariable<int> NumInteractorsAlreadyLoaded { get; private set;}
        /// <summary>  
        ///  Return num of interactors in library.
        /// </summary> 
        public static int NumOfInteractors{get; private set;}



        /// <summary>  
        ///  Return interactor. typeof(InteractorBase);
        /// </summary> 
        public static T GetInteractor<T>() where T: InteractorBase
        {
            return scene.IsLoaded? scene.GetInteractor<T>() : null;
        }


        private static void CreateScene()
        {
            if (scene != null) return;

            scene = new Scene();
            scene.onLoadedEvent += onLoaded;
            NumInteractorsAlreadyLoaded = new();
            scene.NumOfInteractorsWereLoaded.OnChange += NumOfInteractorsAlreadyLoaded;
            NumOfInteractors = scene.NumOfInteractors;
        }

        private static void NumOfInteractorsAlreadyLoaded(object obj)
        {
            NumInteractorsAlreadyLoaded.Value = scene.NumOfInteractorsWereLoaded.Value;
        }

        private static void onLoaded(string SceneName)
        {
            onLoadedAppEvent?.Invoke();
            Debug.Log($"HEY, Scene:\"{SceneName}\" is loaded!");
        }

    }
}
