using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture.Root.ArchBehaviour
{
    public abstract class MonoArchBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            Game.Instance.OnProjectAwakeEvent += OnProjectAwakeHandler;
            Game.Instance.OnProjectInitializedEvent += OnProjectInitializedHandler;
            Game.Instance.OnProjectStartEvent += OnProjectStartHandler;

            Game.Instance.OnSceneAwakeEvent += OnSceneAwakeHandler;
            Game.Instance.OnSceneInitializedEvent += OnSceneInitializedHandler;
            Game.Instance.OnSceneStartEvent += OnSceneStartHandler;

            Game.Instance.OnControllerEvent += OnControllerHandler;
            Game.Instance.OnRepositoryEvent += OnRepositoryHandler;

            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }




        /// <summary>
        /// Mandatory using base 
        /// </summary>
        /// <param name="arg0"></param>
        /// <param name="arg1">Next scene</param>
        protected virtual void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }


        #region ProjectEvents
        private void OnProjectAwakeHandler() {
            SendMessageUpwards("OnProjectAwake", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnProjectAwakeEvent -= OnProjectAwakeHandler;
        }
        private void OnProjectInitializedHandler() {
            SendMessageUpwards("OnProjectInitialized", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnProjectInitializedEvent -= OnProjectInitializedHandler;
        }
        private void OnProjectStartHandler() {
            SendMessageUpwards("OnProjectStart", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnProjectStartEvent -= OnProjectStartHandler;
        }
        #endregion

        #region ScenesEvents
        private void OnSceneAwakeHandler() {
            SendMessageUpwards("OnSceneAwake", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnSceneAwakeEvent -= OnSceneAwakeHandler;
        }
        private void OnSceneInitializedHandler() {
            SendMessageUpwards("OnSceneInitialized", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnSceneInitializedEvent -= OnSceneInitializedHandler;
        }
        private void OnSceneStartHandler()
        {
            SendMessageUpwards("OnSceneStart", null, SendMessageOptions.DontRequireReceiver);
            Game.Instance.OnSceneStartEvent -= OnSceneStartHandler;

            Game.Instance.OnControllerEvent -= OnControllerHandler;
            Game.Instance.OnRepositoryEvent -= OnRepositoryHandler;
        }

        #endregion

        #region CommonEvents
        private void OnControllerHandler(object arg1, LoadingEventType arg2) =>
            SendMessageUpwards("OnResourceEvent", new OnResourceEventArgs() { Resource = arg1, LoadingType = arg2 }, SendMessageOptions.DontRequireReceiver);
        private void OnRepositoryHandler(object arg1, LoadingEventType arg2) => 
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
        /// An event that allows you to receive data that all scene's controllers and repositories have been created and awaked, but not yet initialized, and started.
        /// </summary>
        protected virtual void OnSceneAwake() { }
        /// <summary>
        /// An event that allows you to receive data that all scene's controllers and repositories have been created, awaked and initialized, but not started
        /// </summary>
        protected virtual void OnSceneInitialized() { }
        /// <summary>
        /// An event that allows you to receive data that all scene's controllers and repositories have been created, awaked, initialized, and started.
        /// </summary>
        protected virtual void OnSceneStart() { }


        #endregion

        #region CommonMono
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
