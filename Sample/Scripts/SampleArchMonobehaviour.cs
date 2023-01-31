using Architecture.Root.ArchBehaviour;
using UnityEngine;

namespace Assets._Project.Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {
        TwitchAuthController twController;
        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {
            twController = Game.Instance.GetController<TwitchAuthController>();
            twController.OnTwitchConnectedEvent += OnTwitchConnectedEvent;
        }

        private void OnTwitchConnectedEvent() {
            var rr = twController.GetSectet();
            Debug.Log(rr);
        }

        private void Update()
        {
            if (Game.Instance.isLoaded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    twController.Connect();
                }                
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    var rr = twController.GetSectet();
                    Debug.Log(rr);
                }
            }
        }
    }
}
