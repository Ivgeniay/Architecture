﻿using Architecture.Root.Controllers;
using Architecture.Root.Repositories;
using System;
using System.Collections;
using Utilits.Routine;

namespace Architecture.Root.MainProject
{
    internal static class ProjectController
    {
        public static bool isLoaded { get; private set; } = false;

        public static event Action<object, LoadingEventType> OnControllerEvent;
        public static event Action<object, LoadingEventType> OnRepositoryEvent;

        public static event Action OnProjectAwakeEvent;
        public static event Action OnProjectInitializedEvent;
        public static event Action OnProjectStartEvent;

        private static ProjectInstaller _projectInstaller = null;

        public static IEnumerator InitProjectController(ProjectInstaller projectInstaller)
        {
            if (_projectInstaller != null)
                throw new Exception("Project resources were installed");

            if (projectInstaller is not null)
            {
                _projectInstaller = projectInstaller;

                _projectInstaller.OnControllerEvent += OnControllerEventHandler;
                _projectInstaller.OnRepositoryEvent += OnRepositoryEventHandler;

                _projectInstaller.OnAwakeEvent += OnProjectResourcesAwakeHandler;
                _projectInstaller.OnInitializedEvent += OnProjectResourcesInitializedHandler;
                _projectInstaller.OnStartEvent += OnProjectResourcesStartHandler;

                yield return _projectInstaller.InitializeRoutine();
            }
            else
            {
                OnProjectAwakeEvent?.Invoke();
                OnProjectInitializedEvent?.Invoke();
                OnProjectStartEvent?.Invoke();
            }

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


        public static void ExitProgramm()
        {
            if (_projectInstaller != null)
                Routine.Instance.StartCoroutine(_projectInstaller.ExitCurrentScene());
        }


        private static void OnControllerEventHandler(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        private static void OnRepositoryEventHandler(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private static void OnProjectResourcesAwakeHandler() => OnProjectAwakeEvent?.Invoke();
        private static void OnProjectResourcesInitializedHandler() => OnProjectInitializedEvent?.Invoke();
        private static void OnProjectResourcesStartHandler()
        {
            isLoaded = true;
            OnProjectStartEvent?.Invoke();
            Unsubscribe(_projectInstaller);
        }
    }
}
