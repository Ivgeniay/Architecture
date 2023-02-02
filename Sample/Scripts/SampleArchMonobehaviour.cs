using Architecture.ProjectResources.TwitchFacade;
using Architecture.Root.ArchBehaviour;
using NetWork.Twitch._Auth;
using NetWork.Twitch._Client;
using TwitchLib.Client;
using UnityEngine;

namespace Assets._Project.Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {
        TwitchAuthController twController;
        TwitchClientController twitchClient;
        TwitchFacadeController twitchFacade;

        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs)
        {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {
            twController = Game.Instance.GetController<TwitchAuthController>();
            twitchClient = Game.Instance.GetController<TwitchClientController>();
            twitchFacade = Game.Instance.GetController<TwitchFacadeController>();

            twController.OnTwitchConnectedEvent += OnTwitchConnectedHandler;
        }

        private void OnTwitchConnectedHandler() {
            var rr = twController.GetSectet();
            Debug.Log(rr);
        }

        private void Update()
        {
            if (Game.Instance.isLoaded) 
            {
                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                    twitchFacade.Connect();
                }                
                if (Input.GetKeyDown(KeyCode.A)){
                    twitchClient.SendMessage("Hello");
                }
            }
        }
    }
}
