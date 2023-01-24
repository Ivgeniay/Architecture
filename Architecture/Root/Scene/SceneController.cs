using Architecture.Root._Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Architecture.Root.Scene
{
    internal class SceneController
    {
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private List<SceneInstaller> sceneInstallers;
        private string sceneName;
        public SceneController(List<SceneInstaller> sceneInstallers) { 
            this.sceneInstallers = sceneInstallers;
            sceneName = SceneManager.GetActiveScene().name;
        }



        public void InitCurrentScene() {
            var scene = sceneInstallers.Where(el => el.SceneName == sceneName).First();
            scene.InitializeSceneAsync();

            scene.OnSceneAwake += OnSceneAwake_;
            scene.OnSceneInitialized += OnSceneInitialized_;
            scene.OnSceneStart += OnSceneStart_;
        }

        private void OnSceneAwake_() => OnSceneAwake?.Invoke();
        private void OnSceneInitialized_() => OnSceneInitialized?.Invoke();
        private void OnSceneStart_() => OnSceneStart?.Invoke();
    }
}
