using Architecture.Root._Repository;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
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
            base.OnAwake();
            Debug.Log("Repository OnAwake");
            yield return null;
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Repository Initialized");
            coins = PlayerPrefs.GetInt(KEY);
            yield return null;
        }
        public override IEnumerator OnStart()
        {
            base.OnStart();
            Debug.Log("Repository OnStart");
            yield return null;
        }


        public override IEnumerator Save()
        {
            yield return null;
            PlayerPrefs.SetInt(KEY, coins);
        }
    }
}
