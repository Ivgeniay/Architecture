using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.Root.ArchBehaviour
{
    public abstract class MonoArchBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            Game.Instance.OnResourcesCreateEvent += OnResourcesCreateEvent;

            Game.Instance.OnProjectResourcesAwakeEvent += OnProjectResourcesAwakeEvent;
            Game.Instance.OnProjectResourcesInitializedEvent += OnProjectResourcesInitializedEvent;
            Game.Instance.OnProjectResourcesStartEvent += OnProjectResourcesStartEvent;

            Game.Instance.OnSceneAwakeEvent += OnSceneAwakeEvent;
            Game.Instance.OnSceneInitializedEvent += OnSceneInitializedEvent;
            Game.Instance.OnSceneStartEvent += OnSceneStartEvent;

            Game.Instance.OnControllerEvent += OnControllerEvent;
            Game.Instance.OnRepositoryEvent += OnRepositoryEvent;

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

        private void OnSceneStartEvent()
        {
            SendMessageUpwards("OnSceneStartEvent", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnResourcesCreateEvent -= OnResourcesCreateEvent;

            Game.Instance.OnProjectResourcesAwakeEvent -= OnProjectResourcesAwakeEvent;
            Game.Instance.OnProjectResourcesInitializedEvent -= OnProjectResourcesInitializedEvent;
            Game.Instance.OnProjectResourcesStartEvent -= OnProjectResourcesStartEvent;

            Game.Instance.OnSceneAwakeEvent -= OnSceneAwakeEvent;
            Game.Instance.OnSceneInitializedEvent -= OnSceneInitializedEvent;
            Game.Instance.OnSceneStartEvent -= OnSceneStartEvent;

            Game.Instance.OnControllerEvent -= OnControllerEvent;
            Game.Instance.OnRepositoryEvent -= OnRepositoryEvent;

            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        #region ProjectEvents
        private void OnProjectResourcesCreateEvent() =>
            SendMessageUpwards("OnProjectInitialized", null, SendMessageOptions.DontRequireReceiver);
        private void OnProjectResourcesStartEvent() =>
            SendMessageUpwards("OnProjectStart", null, SendMessageOptions.DontRequireReceiver);
        private void OnProjectResourcesInitializedEvent() =>
            SendMessageUpwards("OnProjectInitialized", null, SendMessageOptions.DontRequireReceiver);
        private void OnProjectResourcesAwakeEvent() =>
            SendMessageUpwards("OnProjectInitialized", null, SendMessageOptions.DontRequireReceiver);
        #endregion

        #region ScenesEvents
        private void OnSceneInitializedEvent() =>
            SendMessageUpwards("OnSceneInitializedEvent", null, SendMessageOptions.DontRequireReceiver);
        private void OnSceneAwakeEvent() => 
            SendMessageUpwards("OnSceneAwakeEvent", null, SendMessageOptions.DontRequireReceiver);
        private void OnResourcesCreateEvent() => 
            SendMessageUpwards("OnResourcesCreateEvent", null, SendMessageOptions.DontRequireReceiver);
        #endregion

        #region CommonEvents
        private void OnControllerEvent(object arg1, LoadingEventType arg2) =>
            SendMessageUpwards("OnResourceEvent", new OnResourceEventArgs() { Resource = arg1, LoadingType = arg2 }, SendMessageOptions.DontRequireReceiver);
        private void OnRepositoryEvent(object arg1, LoadingEventType arg2) => 
            SendMessageUpwards("OnResourceEvent", new OnResourceEventArgs() { Resource = arg1, LoadingType = arg2 }, SendMessageOptions.DontRequireReceiver);
        #endregion

        #region ProjectMono
        /// <summary>
        /// An event that allows you to receive data that all project's controllers and repositories have been created and awaked, but not yet initialized, and started.
        /// </summary>
        protected virtual void OnProjectAwake() { }
        /// <summary>
        /// An event that allows you to receive data that all project's controllers and repositories have been created, awaked and initialized, but not started
        /// </summary>
        protected virtual void OnProjectInitialized() { }
        /// <summary>
        /// An event that allows you to receive data that all project's controllers and repositories have been created, awaked, initialized, and started.
        /// </summary>
        protected virtual void OnProjectStart() { }
        #endregion

        #region ScenesMono
        /// <summary>
        /// An event that allows you to receive data that all scene's controllers and repositories have been created, awaked, initialized, and started.
        /// </summary>
        protected virtual void OnSceneStart() { }

        /// <summary>
        /// An event that allows you to receive data that all scene's controllers and repositories have been created, awaked and initialized, but not started
        /// </summary>
        protected virtual void OnSceneInitialized() { }

        /// <summary>
        /// An event that allows you to receive data that all scene's controllers and repositories have been created and awaked, but not yet initialized, and started.
        /// </summary>
        protected virtual void OnSceneAwake() { }
        #endregion

        #region CommonMono
        /// <summary>
        /// An event that allows you to receive data that all controllers and repositories have been created, but have not yet awaked, initialized, and started
        /// </summary>
        protected virtual void OnResourcesCreate() { }

        /// <summary>
        /// An event that allows to get data about which controller or repository was loaded on different stage
        /// </summary>
        /// <param name="onResourceEventArgs">Resource == controller or repository, LoadingType == stage of loading was passed </param>
        protected virtual void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) { }
        #endregion
    }

    public class OnResourceEventArgs
    {
        public object Resource;
        public LoadingEventType LoadingType;
    }
}
