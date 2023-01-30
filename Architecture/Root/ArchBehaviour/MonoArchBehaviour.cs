using Architecture.Root.GameController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.Root.ArchBehaviour
{
    public abstract class MonoArchBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            Game.Instance.OnResourcesCreate += OnResourcesCreate_;
            Game.Instance.OnSceneAwake += OnSceneAwake_;
            Game.Instance.OnSceneInitialized += OnSceneInitialized_;
            Game.Instance.OnSceneStart += OnSceneStart_;

            Game.Instance.OnControllerEvent += OnControllerEvent_;
            Game.Instance.OnRepositoryEvent += OnRepositoryEvent_;

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        /// <summary>
        /// Mandatory using base 
        /// </summary>
        /// <param name="arg0"></param>
        /// <param name="arg1">Next scene</param>
        protected virtual void OnActiveSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnSceneStart_()
        {
            SendMessageUpwards("OnSceneStart", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnResourcesCreate -= OnResourcesCreate;
            Game.Instance.OnSceneAwake -= OnSceneAwake;
            Game.Instance.OnSceneInitialized -= OnSceneInitialized;
            Game.Instance.OnSceneStart -= OnSceneStart;
        }
        private void OnSceneInitialized_() {
            SendMessageUpwards("OnSceneInitialized", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnSceneAwake_() {
            SendMessageUpwards("OnSceneAwake", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnResourcesCreate_() {
            SendMessageUpwards("OnResourcesCreate", null, SendMessageOptions.DontRequireReceiver);
        }

        private void OnControllerEvent_(object arg1, LoadingEventType arg2) {
            SendMessageUpwards("OnResourceEvent", new OnResourceEventArgs() { Resource = arg1, LoadingType = arg2 }, SendMessageOptions.DontRequireReceiver);
        }

        private void OnRepositoryEvent_(object arg1, LoadingEventType arg2) { 
            SendMessageUpwards("OnResourceEvent", new OnResourceEventArgs() { Resource = arg1, LoadingType = arg2 }, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked, initialized, and started.
        /// </summary>
        protected virtual void OnSceneStart() { }

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, awaked and initialized, but not started
        /// </summary>
        protected virtual void OnSceneInitialized() { }

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created and awaked, but not yet initialized, and started.
        /// </summary>
        protected virtual void OnSceneAwake() { }

        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, but have not yet awaked, initialized, and started
        /// </summary>
        protected virtual void OnResourcesCreate() { }

        /// <summary>
        /// An event that allows to get data about which controller or repository was loaded on different stage
        /// </summary>
        /// <param name="onResourceEventArgs">Resource == controller or repository, LoadingType == stage of loading was passed </param>
        protected virtual void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) { }
    }

    public class OnResourceEventArgs
    {
        public object Resource;
        public LoadingEventType LoadingType;
    }
}
