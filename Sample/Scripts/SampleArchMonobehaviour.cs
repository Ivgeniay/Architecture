using Architecture.Root.ArchBehaviour;
using NetWork.Twitch._Auth;
using NetWork.Twitch._Client;
using TwitchLib.Client;
using UnityEngine;

namespace Assets._Project.Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {

        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }
    }
}
