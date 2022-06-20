using UnityEngine;
using System;

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
        public static bool IsLoaded() => scene.IsLoaded;
        private static Scene scene;



        /// <summary>  
        ///  Return interactor. typeof(InteractorBase);
        /// </summary> 
        public static T GetInteractor<T>() where T: InteractorBase
        {
            return scene.IsLoaded? scene.GetInteractor<T>() : null;
        }

        private static void CreateScene()
        {
            if (scene == null)
                scene = new Scene();
            scene.onLoadedEvent += onLoaded;
        }

        private static void onLoaded(string SceneName)
        {
            onLoadedAppEvent?.Invoke();
            Debug.Log($"HEY, Scene:\"{SceneName}\" is loaded!");
        }

    }
}
