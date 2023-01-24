using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Scene;
using Architecture.Root.Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

namespace Architecture.Root.GameController
{
    internal class Game : MonoBehaviour
    {
        public static Game Instance { get; private set; }
        
        public event Action OnSceneAwake;
        public event Action OnSceneInitialized;
        public event Action OnSceneStart;

        private List<SceneInstaller> scenes = new List<SceneInstaller>();
        private SceneController sceneController;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);

                SceneManager.activeSceneChanged += OnActiveSceneChanged;
                return;
            }


            Destroy(this);
        }

        public void GameInitialize()
        {
            scenes = Instance.transform.GetComponentsInChildren<SceneInstaller>(true).ToList<SceneInstaller>();
            sceneController = new SceneController(scenes);

            sceneController.OnSceneAwake += OnSceneAwake_;
            sceneController.OnSceneInitialized += OnSceneInitialized_;
            sceneController.OnSceneStart += OnSceneStart_;

            sceneController.InitCurrentScene();
        }

        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1) => GameInitialize();
        private void OnSceneAwake_() => OnSceneAwake?.Invoke();
        private void OnSceneInitialized_() => OnSceneInitialized?.Invoke();
        private void OnSceneStart_() => OnSceneStart?.Invoke();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) SceneManager.LoadScene("TestScene");
            if (Input.GetKeyDown(KeyCode.LeftArrow)) SceneManager.LoadScene("LoadScene");
        }

        public T GetRepository<T>() where T : Repository
        {
            return sceneController.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller
        {
            return sceneController.GetController<T>();
        }
    }
}
