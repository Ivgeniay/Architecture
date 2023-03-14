using Architecture.Root.Repositories;
using System.Collections;
using UnityEngine;


internal class PlayerRepository : Repository
{
    private int _coins;
    public int coins
    {
        get => _coins;
        set {
            _coins = value;
            if (_coins < 0) _coins = 0;
        }
    }

    private const string KEY = "COINS"; 
    public PlayerRepository() { }

    public override IEnumerator OnAwakeRoutine()
    {
        yield return null;
    }

    public override IEnumerator InitializeRoutine()
    {
        coins = PlayerPrefs.GetInt(KEY);
        yield return null;
    }
    public override IEnumerator OnStartRoutine()
    {
        yield return null;
    }


    public override IEnumerator SaveRoutine()
    {
        yield return null;
        PlayerPrefs.SetInt(KEY, coins);
    }
}

