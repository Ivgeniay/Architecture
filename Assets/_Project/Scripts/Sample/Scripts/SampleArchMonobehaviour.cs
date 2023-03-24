using Architecture.Root.ArchBehaviour;
using Assets.MainProject.Scripts;
using DI.MonoDI;
using UnityEngine;

namespace Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : EventMonoBehaviour
    {
        [SerializeField] private string loadSceneName;
        [SerializeField] private string testSceneName;
        [SerializeField] private MainViewModel mainViewModel;

        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs)
        {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {

        }
        protected override void OnProjectAwake()
        {
            var tt = Engine.Instance.GetController<GameController>();
            Debug.Log(tt);
        }


        private void Update()
        {
            if (Engine.Instance.isLoaded) 
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    Engine.Instance.NextScene(loadSceneName);
                }                
                if (Input.GetKeyDown(KeyCode.LeftArrow)){
                    Engine.Instance.NextScene(testSceneName);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    MonoDI.Instance.Instantiate(mainViewModel);
                }
            }
        }

    }
}
