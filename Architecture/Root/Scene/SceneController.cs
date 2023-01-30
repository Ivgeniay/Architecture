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

        public event Action OnResourcesCreate;
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private List<SceneInstaller> sceneInstallers;

        private Installer currentScene;
        private string sceneName;
        public SceneController(List<SceneInstaller> sceneInstallers) { 
            this.sceneInstallers = sceneInstallers;
            sceneName = SceneManager.GetActiveScene().name;
        }



        public void InitCurrentScene() {
            currentScene = sceneInstallers.Where(el => el.SceneName == sceneName).First();

            currentScene.OnControllerEvent += OnControllerEvent_;
            currentScene.OnRepositoryEvent += OnRepositoryEvent_;

            currentScene.OnResourcesCreate += OnResourcesCreate_;
            currentScene.OnAwake += OnSceneAwake_;
            currentScene.OnInitialized += OnSceneInitialized_;
            currentScene.OnStart += OnSceneStart_;

            currentScene.InitializeAsync();
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

        private void Unsubscribe()
        {
            currentScene.OnControllerEvent -= OnControllerEvent_;
            currentScene.OnRepositoryEvent -= OnRepositoryEvent_;

            currentScene.OnResourcesCreate -= OnResourcesCreate_;
            currentScene.OnAwake -= OnSceneAwake_;
            currentScene.OnInitialized -= OnSceneInitialized_;
            currentScene.OnStart -= OnSceneStart_;
        }
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnResourcesCreate_() => OnResourcesCreate?.Invoke();
        private void OnSceneAwake_() => OnSceneAwake?.Invoke();
        private void OnSceneInitialized_() => OnSceneInitialized?.Invoke();
        private void OnSceneStart_() {
            OnSceneStart?.Invoke();
            Unsubscribe();
        }
    }
}
