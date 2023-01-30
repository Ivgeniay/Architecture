using Architecture.Root._Repository;
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

    public override IEnumerator OnAwake()
    {
        yield return null;
    }

    public override IEnumerator Initialize()
    {
        coins = PlayerPrefs.GetInt(KEY);
        yield return null;
    }
    public override IEnumerator OnStart()
    {
        yield return null;
    }


    public override IEnumerator Save()
    {
        yield return null;
        PlayerPrefs.SetInt(KEY, coins);
    }
}

