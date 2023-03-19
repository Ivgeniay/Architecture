using Architecture.Root.Repositories;
using Architecture.Root.MainProject;
using Architecture.Root.Controllers;
using Architecture.Root.Scenes;
using Architecture.Root;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System;

public class Engine : MonoBehaviour
{
    private static Engine _instance;

    /// <summary>
    /// Singleton of game machine state
    /// </summary>
    public static Engine Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
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
    /// <summary>
    /// An event that scen was exit
    /// </summary>
    public event Action OnSceneUnloadEvent;

    private List<SceneInstaller> scenes = new List<SceneInstaller>();
    private SceneController sceneController;


    private void Awake()
    {
        if (Instance == null)
        {
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
            ProjectController.OnProjectAwakeEvent += OnProjectAwakeHandler;
            ProjectController.OnProjectInitializedEvent += OnProjectInitializedHandler;
            ProjectController.OnProjectStartEvent += OnProjectStartHandler;

            ProjectController.OnControllerEvent += OnControllerEventHandler;
            ProjectController.OnRepositoryEvent += OnRepositoryEventHandler;


            var projectInstaller = Instance.transform.GetComponentInChildren<ProjectInstaller>(true);
            if (projectInstaller is not null)
            {
                yield return ProjectController.InitProjectController(projectInstaller);
            }
        }
        //else
        //{
        //    OnProjectAwakeHandler();
        //    OnProjectInitializedHandler();
        //    OnProjectStartHandler();
        //}
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
        //else {
        //    OnSceneAwakeHandler();
        //    OnSceneInitializedHandler();
        //    OnSceneStartHandler();
        //}
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
        if (isLoaded)
        {
            ProjectController.Frame();
            sceneController.Frame();
        }

        //if (Input.GetKeyDown(KeyCode.RightArrow)) SceneManager.LoadScene("MainMenu");
        //if (Input.GetKeyDown(KeyCode.LeftArrow)) SceneManager.LoadScene("LoadScene");
        //if (Input.GetKeyDown(KeyCode.Space)) GetController<TwitchFacadeController>().Connect();
        //if (Input.GetKeyDown(KeyCode.A)) GetController<TwitchFacadeController>().SendChatMessage("Hello");
        //if (Input.GetKeyDown(KeyCode.D)) GetController<TwitchFacadeController>().GetChatters().data.ForEach(el => Debug.Log($"{el.user_id}: {el.user_name} "));
    }

    public T GetRepository<T>() where T : Repository
    {
        T repository = ProjectController.GetRepository<T>();
        if (repository is null) repository = sceneController.GetRepository<T>();

        if (repository is null)
        {
            var type = typeof(T);
            throw new Exception($"There is no {type} repository");
        }

        return repository;
    }

    public T GetController<T>() where T : Controller
    {
        T controller = ProjectController.GetController<T>();
        if (controller is null) controller = sceneController.GetController<T>();

        if (controller is null)
        {
            var type = typeof(T);
            throw new Exception($"There is no {type} controller");
        }

        return controller;
    }

    public IEnumerator SceneUnload()
    {
        OnSceneUnloadHandler();
        yield return sceneController.ExitCurrentScene();
    }

    private void OnApplicationQuit()
    {
        ProjectController.ExitProgramm();
    }

    private void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
    private void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
    private void OnActiveSceneChangedHandler(Scene arg0, Scene arg1) => Routine.Instance.StartCoroutine(GameInitialize());

    #region Events
    private void OnProjectAwakeHandler()
    {
        OnProjectAwakeEvent?.Invoke();
        ProjectController.OnProjectAwakeEvent -= OnProjectAwakeHandler;
    }
    private void OnProjectInitializedHandler()
    {
        OnProjectInitializedEvent?.Invoke();
        ProjectController.OnProjectInitializedEvent -= OnProjectInitializedHandler;
    }
    private void OnProjectStartHandler()
    {
        OnProjectStartEvent?.Invoke();
        ProjectController.OnProjectStartEvent -= OnProjectStartHandler;
        UnsubscribeProject();
    }

    private void OnSceneAwakeHandler()
    {
        OnSceneAwakeEvent?.Invoke();
        sceneController.OnSceneAwakeEvent -= OnSceneAwakeHandler;

    }
    private void OnSceneInitializedHandler()
    {
        OnSceneInitializedEvent?.Invoke();
        sceneController.OnSceneInitializedEvent -= OnSceneInitializedHandler;
    }
    private void OnSceneStartHandler()
    {
        isLoaded = true;
        OnSceneStartEvent?.Invoke();
        sceneController.OnSceneStartEvent -= OnSceneStartHandler;
        UnsubscribeScene();
    }

    private void OnSceneUnloadHandler() =>
        OnSceneUnloadEvent?.Invoke();
    #endregion

}

