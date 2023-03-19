using Architecture.DI;
using Architecture.DI.ActivationBuilds;
using UnityEngine;
using Utilits.Extensions;

namespace Assets.MainProject.Scripts
{ 

    internal class TestScr : MonoBehaviour
    {
        [SerializeField] private MainViewModel prefab;
        private void Awake()
        {
            var go = Instantiate(prefab)
                .With(el => el.transform.position = new Vector3(10, 15, 25))
                .With(el => el.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f))
                .With(el => el.isEnable = false)
                .With(
                    apply: el => Debug.Log("HYEHOP"), 
                    @if: el => el.isEnable == true, 
                    elseif: el => false,
                    @else: el => Debug.Log("Else"));
        }
    }
}

 