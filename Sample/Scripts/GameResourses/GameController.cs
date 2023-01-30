using Architecture.Root._Controller;
using System.Collections;
using UnityEngine;


internal class GameController : Controller
{
    public override IEnumerator OnAwake()
    {
        Debug.Log("GameController OnAwake");
        return null;
    }

    public override IEnumerator OnStart()
    {
        Debug.Log("GameController OnStart");
        return null;
    }

    public override IEnumerator Initialize()
    {
        Debug.Log("GameController OnInit");
        return null;
    }
    
}
