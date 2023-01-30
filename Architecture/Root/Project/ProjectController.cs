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

        public static event Action OnProjectResourcesCreate;
        public static event Action OnProjectResourcesAwake;
        public static event Action OnProjectResourcesInitialized;
        public static event Action OnProjectResourcesStart;

        private static ProjectInstaller _projectInstaller = null;

        public static IEnumerator InitProjectController(ProjectInstaller projectInstaller)
        {
            if (_projectInstaller != null)
                throw new Exception("Project resources were installed");

            if (projectInstaller == null)
                throw new NullReferenceException();

            _projectInstaller = projectInstaller;

            _projectInstaller.OnControllerEvent += OnControllerEvent_;
            _projectInstaller.OnRepositoryEvent += OnRepositoryEvent_;

            _projectInstaller.OnResourcesCreate += OnProjectResourcesCreate_;
            _projectInstaller.OnAwake += OnProjectResourcesAwake_;
            _projectInstaller.OnInitialized += OnProjectResourcesInitialized_;
            _projectInstaller.OnStart += OnProjectResourcesStart_;
            
            yield return _projectInstaller.InitializeAsync();
        }


        private static void Unsubscribe(ProjectInstaller projectInstaller)
        {
            projectInstaller.OnControllerEvent -= OnControllerEvent_;
            projectInstaller.OnRepositoryEvent -= OnRepositoryEvent_;

            projectInstaller.OnResourcesCreate -= OnProjectResourcesCreate_;
            projectInstaller.OnAwake -= OnProjectResourcesAwake_;
            projectInstaller.OnInitialized -= OnProjectResourcesInitialized_;
            projectInstaller.OnStart -= OnProjectResourcesStart_;
            
        }

        public static T GetRepository<T>() where T : Repository => _projectInstaller.GetRepository<T>();
        public static T GetController<T>() where T : Controller => _projectInstaller.GetController<T>();
        

        private static void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private static void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private static void OnProjectResourcesCreate_() => OnProjectResourcesCreate?.Invoke();
        private static void OnProjectResourcesAwake_() => OnProjectResourcesAwake?.Invoke();
        private static void OnProjectResourcesInitialized_() => OnProjectResourcesInitialized?.Invoke();
        private static void OnProjectResourcesStart_() {
            isLoaded = true;
            OnProjectResourcesStart?.Invoke();
            Unsubscribe(_projectInstaller);
        }
    }
}
