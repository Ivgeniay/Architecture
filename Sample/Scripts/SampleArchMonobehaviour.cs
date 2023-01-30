using Architecture.Root.ArchBehaviour;
using UnityEngine;

namespace Assets._Project.Scripts.Sample.Scripts
{
    internal class SampleArchMonobehaviour : MonoArchBehaviour
    {
        protected override void OnResourceEvent(OnResourceEventArgs onResourceEventArgs) {
            Debug.Log(onResourceEventArgs.Resource.ToString() + onResourceEventArgs.LoadingType);
        }

        protected override void OnSceneStart()
        {
            var rr = Game.Instance.GetController<GameController>();
            rr.PublicMethod();
            var pp = Game.Instance.GetController<PlayerController>();
            Debug.Log($"Coins: {pp.GetNumCoins()}");
        }

    }
}
