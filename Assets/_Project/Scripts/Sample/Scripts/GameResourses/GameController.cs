using Architecture.Root.Controllers;
using System.Collections;
using UnityEngine;


internal class GameController : Controller
{
    public override IEnumerator OnAwakeRoutine()
    {
        yield return null;
    }

    public override IEnumerator OnStartRoutine()
    {
        yield return null;
    }

    public override IEnumerator InitializeRoutine()
    {
        yield return null;
    }

    public void PublicMethod() {
        Debug.Log("Public method from GameController");
    }
    
}
