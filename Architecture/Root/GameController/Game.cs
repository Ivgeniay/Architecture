using Architecture.Root._Scene;
using Architecture.Root.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Architecture.Root.GameController
{
    internal class Game : MonoBehaviour
    {
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private List<SceneInstaller> scenes = new List<SceneInstaller>();
        private SceneController sceneController;

        private void Awake()
        {
            scenes = transform.GetComponentsInChildren<SceneInstaller>(true).ToList<SceneInstaller>();
            sceneController = new SceneController(scenes);

            sceneController.OnSceneAwake += OnSceneAwake_;
            sceneController.OnSceneInitialized += OnSceneInitialized_;
            sceneController.OnSceneStart += OnSceneStart_;

            sceneController.InitCurrentScene();
        }

        private void OnSceneAwake_() => OnSceneAwake?.Invoke();
        private void OnSceneInitialized_() => OnSceneInitialized?.Invoke();
        private void OnSceneStart_() => OnSceneStart?.Invoke();
    }
}
