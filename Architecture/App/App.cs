using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public static class App
    {
        public static void Initialize() 
        {
            CreateScene();
        }
        


        private static Scene scene;
        private static void CreateScene()
        {
            if (scene == null)
                scene = new Scene();
        }

        public static T GetInteractor<T>() where T: InteractorBase
        {
            return scene.GetInteractor<T>();
        }
    }
}
