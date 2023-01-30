using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Scene;
using Architecture.Root.Project;
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
        /// <summary>
        /// Singleton of game machine state
        /// </summary>
        public static Game Instance { get; private set; }

        public bool isLoaded { get; private set; } = false;

        /// <summary>
        /// An event that allows to get data about which controller was loaded
        /// </summary>
        public event Action<object, LoadingEventType> OnControllerEvent;
        /// <summary>
        /// An event that allows to get data about which repository was loaded
        /// </summary>
        public event Action<object, LoadingEventType> OnRepositoryEvent;

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, but have not yet awaked, initialized, and started in the project
        /// </summary>
        public event Action OnProjectResourcesCreate;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created and awaked, but not yet initialized, and started in the project
        /// </summary>
        public event Action OnProjectResourcesAwake;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked and initialized, but not started in the project
        /// </summary>
        public event Action OnProjectResourcesInitialized;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked, initialized, and started in the project
        /// </summary>
        public event Action OnProjectResourcesStart;

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, but have not yet awaked, initialized, and started in the scene
        /// </summary>
        public event Action OnResourcesCreate;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created and awaked, but not yet initialized, and started in the scene
        /// </summary>
        public event Action OnSceneAwake;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked and initialized, but not started in the scene
        /// </summary>
        public event Action OnSceneInitialized;
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked, initialized, and started in the scene
        /// </summary>
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


            Destroy(gameObject);
        }

        public void GameInitialize()
        {
            //Project init
            if (ProjectController.isLoaded == false)
            {
                ProjectController.OnProjectResourcesCreate += OnProjectResourcesCreate_;
                ProjectController.OnProjectResourcesAwake += OnProjectResourcesAwake_;
                ProjectController.OnProjectResourcesInitialized += OnProjectResourcesInitialized_;
                ProjectController.OnProjectResourcesStart += OnProjectResourcesStart_;

                ProjectController.OnControllerEvent += OnControllerEvent_;
                ProjectController.OnRepositoryEvent += OnRepositoryEvent_;


                var projectInstaller = Instance.transform.GetComponentInChildren<ProjectInstaller>(true);
                if (projectInstaller is not null) {
                    ProjectController.InitProjectController(projectInstaller);
                }
            }
            else
            {
                OnProjectResourcesCreate_();
                OnProjectResourcesAwake_();
                OnProjectResourcesInitialized_();
                OnProjectResourcesStart_();
            }

            //Scene init
            scenes = Instance.transform.GetComponentsInChildren<SceneInstaller>(true).ToList<SceneInstaller>();

            if (scenes.Count != 0)
            {
                sceneController = new SceneController(scenes);

                sceneController.OnControllerEvent += OnControllerEvent_;
                sceneController.OnRepositoryEvent += OnRepositoryEvent_;

                sceneController.OnResourcesCreate += OnResourcesCreate_;
                sceneController.OnSceneAwake += OnSceneAwake_;
                sceneController.OnSceneInitialized += OnSceneInitialized_;
                sceneController.OnSceneStart += OnSceneStart_;

                sceneController.InitCurrentScene();
            }
            else
            {
                OnResourcesCreate_();
                OnSceneAwake_();
                OnSceneInitialized_();
                OnSceneStart_();
            }
        }

        private void UnsubscribeScene()
        {
            sceneController.OnControllerEvent -= OnControllerEvent_;
            sceneController.OnRepositoryEvent -= OnRepositoryEvent_;

            sceneController.OnResourcesCreate -= OnResourcesCreate_;
            sceneController.OnSceneAwake -= OnSceneAwake_;
            sceneController.OnSceneInitialized -= OnSceneInitialized_;
            sceneController.OnSceneStart -= OnSceneStart_;
        }
        private void UnsubscribeProject()
        {
            ProjectController.OnProjectResourcesCreate -= OnProjectResourcesCreate_;
            ProjectController.OnProjectResourcesAwake -= OnProjectResourcesAwake_;
            ProjectController.OnProjectResourcesInitialized -= OnProjectResourcesInitialized_;
            ProjectController.OnProjectResourcesStart -= OnProjectResourcesStart_;

            ProjectController.OnControllerEvent -= OnControllerEvent_;
            ProjectController.OnRepositoryEvent -= OnRepositoryEvent_;
        }


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

        private void OnApplicationQuit()
        {
            sceneController.ExitCurrentScene();
        }
        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private void OnResourcesCreate_() => OnResourcesCreate?.Invoke();
        private void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1) => GameInitialize();
        private void OnSceneAwake_() => OnSceneAwake?.Invoke();
        private void OnSceneInitialized_() => OnSceneInitialized?.Invoke();
        private void OnSceneStart_() {
            isLoaded = true;
            OnSceneStart?.Invoke();
        }

        private void OnProjectResourcesCreate_() => OnProjectResourcesCreate?.Invoke();
        private void OnProjectResourcesAwake_() => OnProjectResourcesAwake?.Invoke();
        private void OnProjectResourcesInitialized_() => OnProjectResourcesInitialized?.Invoke();
        private void OnProjectResourcesStart_() => OnProjectResourcesStart?.Invoke();
        
    }
}
