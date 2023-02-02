using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Architecture.Root._Scene
{
    internal class SceneController
    {
        public event Action<object, LoadingEventType> OnControllerEvent;
        public event Action<object, LoadingEventType> OnRepositoryEvent;

        public event Action OnSceneAwakeEvent;
        public event Action OnSceneInitializedEvent;
        public event Action OnSceneStartEvent;

        private List<SceneInstaller> sceneInstallers;

        private Installer currentScene;
        private string sceneName;
        public SceneController(List<SceneInstaller> sceneInstallers) { 
            this.sceneInstallers = sceneInstallers;
            sceneName = SceneManager.GetActiveScene().name;
        }



        public IEnumerator InitCurrentScene() {
            currentScene = sceneInstallers.Where(el => el.SceneName == sceneName).First();

            currentScene.OnControllerEvent += OnControllerEventHandler;
            currentScene.OnRepositoryEvent += OnRepositoryEventHandler;

            currentScene.OnAwakeEvent += OnSceneAwakeHandler;
            currentScene.OnInitializedEvent += OnSceneInitializedHandler;
            currentScene.OnStartEvent += OnSceneStartHandler;

            yield return currentScene.InitializeAsync();
        }


        public void ExitCurrentScene()
        {
        }


        public T GetRepository<T>() where T : Repository
        {
            return currentScene.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller
        {
            return currentScene.GetController<T>();
        }

        public void Frame() =>
            currentScene.Frame();
        

        private void Unsubscribe()
        {
            currentScene.OnControllerEvent -= OnControllerEventHandler;
            currentScene.OnRepositoryEvent -= OnRepositoryEventHandler;

            currentScene.OnAwakeEvent -= OnSceneAwakeHandler;
            currentScene.OnInitializedEvent -= OnSceneInitializedHandler;
            currentScene.OnStartEvent -= OnSceneStartHandler;
        }
        private void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnSceneAwakeHandler() => OnSceneAwakeEvent?.Invoke();
        private void OnSceneInitializedHandler() => OnSceneInitializedEvent?.Invoke();
        private void OnSceneStartHandler() {
            OnSceneStartEvent?.Invoke();
            Unsubscribe();
        }
    }
}
