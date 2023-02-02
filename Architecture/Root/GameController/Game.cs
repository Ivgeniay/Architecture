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
    /// object == controller, LoadingType == stage of loading was passed
    /// </summary>
    public event Action<object, LoadingEventType> OnControllerEvent;
    /// <summary>
    /// An event that allows to get data about which repository was loaded
    /// object == repository, LoadingType == stage of loading was passed
    /// </summary>
    public event Action<object, LoadingEventType> OnRepositoryEvent;

    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created and awaked, but not yet initialized, and started in the project
    /// </summary>
    public event Action OnProjectAwakeEvent;
    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created, awaked and initialized, but not started in the project
    /// </summary>
    public event Action OnProjectInitializedEvent;
    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created, awaked, initialized, and started in the project
    /// </summary>
    public event Action OnProjectStartEvent;

    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created and awaked, but not yet initialized, and started in the scene
    /// </summary>
    public event Action OnSceneAwakeEvent;
    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created, awaked and initialized, but not started in the scene
    /// </summary>
    public event Action OnSceneInitializedEvent;
    /// <summary>
    /// An event that allows you to receive data that all controllers and repositories have been created, awaked, initialized, and started in the scene
    /// </summary>
    public event Action OnSceneStartEvent;

    private List<SceneInstaller> scenes = new List<SceneInstaller>();
    private SceneController sceneController;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);

            SceneManager.activeSceneChanged += OnActiveSceneChangedHandler;
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
            ProjectController.OnProjectResourcesAwakeEvent += OnProjectAwakeHandler;
            ProjectController.OnProjectResourcesInitializedEvent += OnProjectInitializedHandler;
            ProjectController.OnProjectResourcesStartEvent += OnProjectStartHandler;

            ProjectController.OnControllerEvent += OnControllerEventHandler;
            ProjectController.OnRepositoryEvent += OnRepositoryEventHandler;


            var projectInstaller = Instance.transform.GetComponentInChildren<ProjectInstaller>(true);
            if (projectInstaller is not null) {
                yield return ProjectController.InitProjectController(projectInstaller);
            }
        }
        else
        {
            OnProjectAwakeHandler();
            OnProjectInitializedHandler();
            OnProjectStartHandler();
        }
    }
    private IEnumerator SceneInitialize()
    {
        scenes = Instance.transform.GetComponentsInChildren<SceneInstaller>(true).ToList<SceneInstaller>();

        if (scenes.Count != 0)
        {
            sceneController = new SceneController(scenes);

            sceneController.OnControllerEvent += OnControllerEventHandler;
            sceneController.OnRepositoryEvent += OnRepositoryEventHandler;

            sceneController.OnSceneAwakeEvent += OnSceneAwakeHandler;
            sceneController.OnSceneInitializedEvent += OnSceneInitializedHandler;
            sceneController.OnSceneStartEvent += OnSceneStartHandler;

            yield return sceneController.InitCurrentScene();
        }
        else
        {
            OnSceneAwakeHandler();
            OnSceneInitializedHandler();
            OnSceneStartHandler();
        }
    }
    private void UnsubscribeScene()
    {
        sceneController.OnControllerEvent -= OnControllerEventHandler;
        sceneController.OnRepositoryEvent -= OnRepositoryEventHandler;
    }
    private void UnsubscribeProject()
    {
        ProjectController.OnControllerEvent -= OnControllerEventHandler;
        ProjectController.OnRepositoryEvent -= OnRepositoryEventHandler;
    }


    void Update()
    {
        if(isLoaded) {
            ProjectController.Frame();
            sceneController.Frame();
        }

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

    private void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
    private void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
    private void OnActiveSceneChangedHandler(Scene arg0, Scene arg1) => Routine.Instance.StartCoroutine(GameInitialize());

    private void OnProjectAwakeHandler()
    {
        OnProjectAwakeEvent?.Invoke();
        ProjectController.OnProjectResourcesAwakeEvent -= OnProjectAwakeHandler;
    }
    private void OnProjectInitializedHandler()
    {
        OnProjectInitializedEvent?.Invoke();
        ProjectController.OnProjectResourcesInitializedEvent -= OnProjectInitializedHandler;
    }
    private void OnProjectStartHandler() {
        OnProjectStartEvent?.Invoke();
        ProjectController.OnProjectResourcesStartEvent -= OnProjectStartHandler;
        UnsubscribeProject();
    }

    private void OnSceneAwakeHandler() {
        OnSceneAwakeEvent?.Invoke();
        sceneController.OnSceneAwakeEvent -= OnSceneAwakeHandler;
        
    }
    private void OnSceneInitializedHandler() {
        OnSceneInitializedEvent?.Invoke();
        sceneController.OnSceneInitializedEvent -= OnSceneInitializedHandler;
    }
    private void OnSceneStartHandler() {
        isLoaded = true;
        OnSceneStartEvent?.Invoke();
        sceneController.OnSceneStartEvent -= OnSceneStartHandler;
        UnsubscribeScene();
    }

}

