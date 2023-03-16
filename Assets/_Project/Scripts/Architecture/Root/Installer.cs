﻿using Architecture.Root.Controllers;
using Architecture.Root.Repositories;
using System;
using System.Collections;
using UnityEngine;

namespace Architecture.Root
{
    public class Installer : MonoBehaviour
    {
        [SerializeField] protected InstallerSetting Setting;

        protected RepositoriesPool repositoriesPool;
        protected ControllersPool controllersPool;

        public virtual event Action<object, LoadingEventType> OnControllerEvent;
        public virtual event Action<object, LoadingEventType> OnRepositoryEvent;

        public virtual event Action OnAwakeEvent;
        public virtual event Action OnInitializedEvent;
        public virtual event Action OnStartEvent;
        public virtual IEnumerator InitializeRoutine() { yield return Initialize(); }
        public virtual T GetRepository<T>() where T : Repository { return null; }
        public virtual T GetController<T>() where T : Controller { return null; }
        protected virtual IEnumerator Initialize() { return null; }
        public virtual IEnumerator ExitCurrentScene() { yield break; }
        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);
        public virtual void Frame() { }

    }
}