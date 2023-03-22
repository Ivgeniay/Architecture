using System.Collections;
using UnityEngine;

namespace Utilits.Routine
{
    public sealed class Routine : MonoBehaviour
    {
        private static Routine _instance = null;
        public static Routine Instance {
            get {
                if (_instance == null) {
                    var go = new GameObject("[COROUTINE]");
                    _instance = go.AddComponent<Routine>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }
    
        public static Coroutine StartRoutine(IEnumerator ienumerator) =>
            Instance.StartCoroutine(ienumerator);

        public static void StopRoutine(Coroutine coroutine) =>
            Instance.StopCoroutine(coroutine);
    
    }
}
