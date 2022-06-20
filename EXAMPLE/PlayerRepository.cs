using System.Collections.Generic;
using UnityEngine;
using Architecture;

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
    public override void InitializeRepository()
    {
        //throw new System.NotImplementedException();
        Debug.Log($"HEY {this} is initialized!");
    }

    public override void Save()
    {
        //throw new System.NotImplementedException();
    }

    public override void Start()
    {
        //throw new System.NotImplementedException();
    }


}
