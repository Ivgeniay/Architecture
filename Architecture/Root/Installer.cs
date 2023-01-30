using Architecture.Root._Controller;
using Architecture.Root._Repository;
using System;
using System.Collections;
using UnityEngine;

namespace Architecture.Root
{
    internal class Installer : MonoBehaviour
    {
        [SerializeField] protected InstallerSetting Setting;

        protected RepositoriesPool repositoriesPool;
        protected ControllersPool controllersPool;

        public virtual event Action<object, LoadingEventType> OnControllerEvent;
        public virtual event Action<object, LoadingEventType> OnRepositoryEvent;

        public virtual event Action OnResourcesCreate;
        public virtual event Action OnAwake;
        public virtual event Action OnInitialized;
        public virtual event Action OnStart;
        public virtual void InitializeAsync() => this.StartCoroutine(Initialize());
        public virtual T GetRepository<T>() where T : Repository { return null; }
        public virtual T GetController<T>() where T : Controller { return null; }
        protected virtual IEnumerator Initialize() { return null; }
        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) => OnRepositoryEvent?.Invoke(arg1, arg2);
        private void OnControllerEvent_(object arg1, LoadingEventType arg2) => OnControllerEvent?.Invoke(arg1, arg2);

    }
}
