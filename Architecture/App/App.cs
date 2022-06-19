namespace Architecture
{
    /// <summary>  
    ///  Basic element of architecture. Used to initialize all interectors "App.Initialize()" and access interactors "App.GetInteractor".
    /// </summary> 
    public static class App
    {
        /// <summary>  
        ///  Initialize all interactors at the scene. Use once on scene in the Awake method;
        /// </summary> 
        public static void Initialize() 
        {
            CreateScene();
        }
        

///  Return interactor "App.GetInteractor<\typeof(InteractorBase)>";
        private static Scene scene;
        private static void CreateScene()
        {
            if (scene == null)
                scene = new Scene();
        }

        /// <summary>  
        ///  Return interactor. typeof(InteractorBase);
        /// </summary> 
        public static T GetInteractor<T>() where T: InteractorBase
        {
            return scene.GetInteractor<T>();
        }
    }
}
