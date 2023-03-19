using UnityEngine;

namespace Architecture.Root.ArchBehaviour
{
    public abstract class MonoArchBehaviour : MonoBehaviour
    {
        private void OnEnable() =>
            Subscribe();
        private void OnDisable() =>
            Unsubscribe();



        #region ProjectEvents
        private void OnProjectAwakeHandler()
        {
            SendMessageUpwards("OnProjectAwake", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnProjectInitializedHandler()
        {
            SendMessageUpwards("OnProjectInitialized", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnProjectStartHandler()
        {
            SendMessageUpwards("OnProjectStart", null, SendMessageOptions.DontRequireReceiver);
        }
        #endregion

        #region ScenesEvents
        private void OnSceneAwakeHandler()
        {
            SendMessageUpwards("OnSceneAwake", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnSceneInitializedHandler()
        {
            SendMessageUpwards("OnSceneInitialized", null, SendMessageOptions.DontRequireReceiver);
        }
        private void OnSceneStartHandler()
        {
            SendMessageUpwards("OnSceneStart", null, SendMessageOptions.DontRequireReceiver);
        }

        private void OnSceneUnloadHandler()
        {
            Unsubscribe();
            SendMessageUpwards("OnSceneUnload", null, SendMessageOptions.DontRequireReceiver);
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
        /// <summary>
        /// An event that scen was exit
        /// </summary>
        protected virtual void OnSceneUnload() { }

        #endregion

        #region CommonMono
        /// <summary>
        /// An event that allows to get data about which controller or repository was loaded on different stage
        /// </summary>
        /// <param name="onResourceEventArgs">Resource == controller or repository, LoadingType == stage of loading was passed </param>
        protected virtual void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) { }
        #endregion

        private void Subscribe()
        {
            Engine.Instance.OnProjectAwakeEvent += OnProjectAwakeHandler;
            Engine.Instance.OnProjectInitializedEvent += OnProjectInitializedHandler;
            Engine.Instance.OnProjectStartEvent += OnProjectStartHandler;

            Engine.Instance.OnSceneAwakeEvent += OnSceneAwakeHandler;
            Engine.Instance.OnSceneInitializedEvent += OnSceneInitializedHandler;
            Engine.Instance.OnSceneStartEvent += OnSceneStartHandler;

            Engine.Instance.OnControllerEvent += OnControllerHandler;
            Engine.Instance.OnRepositoryEvent += OnRepositoryHandler;

            Engine.Instance.OnSceneUnloadEvent += OnSceneUnloadHandler;
        }
        private void Unsubscribe()
        {
            Engine.Instance.OnProjectAwakeEvent -= OnProjectAwakeHandler;
            Engine.Instance.OnProjectInitializedEvent -= OnProjectInitializedHandler;
            Engine.Instance.OnProjectStartEvent -= OnProjectStartHandler;

            Engine.Instance.OnSceneAwakeEvent -= OnSceneAwakeHandler;
            Engine.Instance.OnSceneInitializedEvent -= OnSceneInitializedHandler;
            Engine.Instance.OnSceneStartEvent -= OnSceneStartHandler;

            Engine.Instance.OnControllerEvent -= OnControllerHandler;
            Engine.Instance.OnRepositoryEvent -= OnRepositoryHandler;

            Engine.Instance.OnSceneUnloadEvent -= OnSceneUnloadHandler;
        }
    }
}
