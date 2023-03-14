using Architecture.Root.ArchBehaviour;
using UnityEngine;

namespace Assets._Project.Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {

        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs)
        {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {

        }


        private void Update()
        {
            if (Game.Instance.isLoaded) 
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    
                }                
                if (Input.GetKeyDown(KeyCode.A)){
                    
                }
            }
        }
    }
}
