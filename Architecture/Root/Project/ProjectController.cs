using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections;

namespace Architecture.Root._Project
{
    internal static class ProjectController
    {
        public static bool isLoaded { get; private set; } = false;

        public static event Action<object, LoadingEventType> OnControllerEvent;
        public static event Action<object, LoadingEventType> OnRepositoryEvent;

        public static event Action OnProjectResourcesAwakeEvent;
        public static event Action OnProjectResourcesInitializedEvent;
        public static event Action OnProjectResourcesStartEvent;

        private static ProjectInstaller _projectInstaller = null;

        public static IEnumerator InitProjectController(ProjectInstaller projectInstaller)
        {
            if (_projectInstaller != null)
                throw new Exception("Project resources were installed");

            if (projectInstaller == null)
                throw new NullReferenceException();

            _projectInstaller = projectInstaller;

            _projectInstaller.OnControllerEvent += OnControllerEventHandler;
            _projectInstaller.OnRepositoryEvent += OnRepositoryEventHandler;

            _projectInstaller.OnAwakeEvent += OnProjectResourcesAwakeHandler;
            _projectInstaller.OnInitializedEvent += OnProjectResourcesInitializedHandler;
            _projectInstaller.OnStartEvent += OnProjectResourcesStartHandler;
            
            yield return _projectInstaller.InitializeAsync();
        }


        private static void Unsubscribe(ProjectInstaller projectInstaller)
        {
            projectInstaller.OnControllerEvent -= OnControllerEventHandler;
            projectInstaller.OnRepositoryEvent -= OnRepositoryEventHandler;

            projectInstaller.OnAwakeEvent -= OnProjectResourcesAwakeHandler;
            projectInstaller.OnInitializedEvent -= OnProjectResourcesInitializedHandler;
            projectInstaller.OnStartEvent -= OnProjectResourcesStartHandler;
            
        }

        public static void Frame() =>
            _projectInstaller.Frame();
        
        public static T GetRepository<T>() where T : Repository => _projectInstaller.GetRepository<T>();
        public static T GetController<T>() where T : Controller => _projectInstaller.GetController<T>();
        

        private static void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private static void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private static void OnProjectResourcesAwakeHandler() => OnProjectResourcesAwakeEvent?.Invoke();
        private static void OnProjectResourcesInitializedHandler() => OnProjectResourcesInitializedEvent?.Invoke();
        private static void OnProjectResourcesStartHandler() {
            isLoaded = true;
            OnProjectResourcesStartEvent?.Invoke();
            Unsubscribe(_projectInstaller);
        }
    }
}
