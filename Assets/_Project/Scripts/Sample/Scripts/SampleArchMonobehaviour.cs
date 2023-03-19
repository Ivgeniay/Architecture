using Architecture.Root.ArchBehaviour;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {
        [SerializeField] private string loadSceneName;
        [SerializeField] private string testSceneName;

        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs)
        {
            //Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {

        }


        private void Update()
        {
            if (Engine.Instance.isLoaded) 
            {
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    SceneManager.LoadScene(testSceneName);
                }                
                if (Input.GetKeyDown(KeyCode.LeftArrow)){
                    SceneManager.LoadScene(loadSceneName);
                    
                }
            }
        }
    }
}
