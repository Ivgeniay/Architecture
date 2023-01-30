using Architecture.Root._Repository;
using System.Collections;
using UnityEngine;

internal class GameRepository : Repository
{
    public override IEnumerator OnAwake()
    {
        Debug.Log("GameRepository OnAwake");
        return null;
    }

    public override IEnumerator Initialize()
    {
        Debug.Log("GameRepository OnInit");
        return null;
    }

    public override IEnumerator OnStart()
    {
        Debug.Log("GameRepository OnStart");
        return null;
    }

    public override IEnumerator Save()
    {
        return null;
    }
    
}
