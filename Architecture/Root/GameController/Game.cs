using Architecture.Root._Controller;
using Architecture.Root._Repository;
using Architecture.Root._Scene;
using Architecture.Root._Project;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Architecture.Root;
using System.Collections;

internal class Game : MonoBehaviour
{
    private static Game _instance;

    /// <summary>
    /// Singleton of game machine state
    /// </summary>
    public static Game Instance {
        get {
            return _instance;
        }
        private set {
            _instance = value;
        }
    }

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
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
            return;
        }
        Destroy(gameObject);
    }
    public IEnumerator GameInitialize()
    {
        yield return ProjectInitialize();
        yield return SceneInitialize();
    }

    private IEnumerator ProjectInitialize()
    {
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
                yield return ProjectController.InitProjectController(projectInstaller);
            }
        }
        else
        {
            OnProjectResourcesCreate_();
            OnProjectResourcesAwake_();
            OnProjectResourcesInitialized_();
            OnProjectResourcesStart_();
        }
        yield return null;
    }
    private IEnumerator SceneInitialize()
    {
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

            yield return sceneController.InitCurrentScene();
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
        T repository = ProjectController.GetRepository<T>();
        if (repository is null) repository = sceneController.GetRepository<T>();

        if (repository is null) {
            var type = typeof(T);
            throw new Exception($"There is no {type} repository");
        }

        return repository;
    }

    public T GetController<T>() where T : Controller
    {
        T controller = ProjectController.GetController<T>();
        if (controller is null) controller = sceneController.GetController<T>();

        if (controller is null) {
            var type = typeof(T);
            throw new Exception($"There is no {type} controller");
        }

        return controller;
    }

    private void OnApplicationQuit()
    {
        sceneController.ExitCurrentScene();
    }
    private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
    private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
    private void OnResourcesCreate_() => OnResourcesCreate?.Invoke();
    private void OnActiveSceneChanged(Scene arg0, Scene arg1) => Routine.instance.StartCoroutine(GameInitialize());
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

