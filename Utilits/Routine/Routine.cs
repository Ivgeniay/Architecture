using System.Collections;
using UnityEngine;


public sealed class Routine : MonoBehaviour
{
    private static Routine _instance;
    private static Routine instance {
        get {
            if (_instance == null)
            {
                var go = new GameObject("[COROUTINE]");
                _instance = go.AddComponent<Routine>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }
    
    public static Coroutine StartRoutine(IEnumerator ienumerator) =>
        instance.StartCoroutine(ienumerator);

    public static void StopRoutine(Coroutine coroutine)
    {
        instance.StopCoroutine(coroutine);
    }
}
