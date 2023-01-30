using Architecture.Root._Controller;
using System.Collections;
using UnityEngine;


internal class GameController : Controller
{
    public override IEnumerator OnAwake()
    {
        yield return null;
    }

    public override IEnumerator OnStart()
    {
        yield return null;
    }

    public override IEnumerator Initialize()
    {
        yield return null;
    }

    public void PublicMethod() {
        Debug.Log("Public method from GameController");
    }
    
}
