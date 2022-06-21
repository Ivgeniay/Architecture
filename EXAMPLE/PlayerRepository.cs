using System.Collections.Generic;
using UnityEngine;
using Architecture;
using System.Collections;

public class PlayerRepository : RepositoryBase
{
    private List<string> inventory;
    private int _health;
    public int Health{
        get => _health;
        set {
            _health = value;
        }
    }
    public override IEnumerator InitializeRepository()
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"{this} is initialized!");
    }

    public override IEnumerator Save()
    {
        yield return null;
    }

    public override IEnumerator StartRepository()
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"{this} is started!");
    }


}
