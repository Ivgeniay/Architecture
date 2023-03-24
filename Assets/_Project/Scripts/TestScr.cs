using UnityEngine;

namespace Assets.MainProject.Scripts
{
    internal class TestScr : MonoBehaviour
    {
        private void Awake()
        {
            var tt = Engine.Instance.GetController<GameController>();
            Debug.Log(tt);
        }

    }
}

 