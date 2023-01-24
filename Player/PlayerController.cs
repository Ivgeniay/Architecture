using Architecture.Root._Controller;
using Architecture.Root.GameController;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    internal class PlayerController : Controller
    {
        private PlayerRepository playerRepository;

        public PlayerController()
        {
            
        }

        public override IEnumerator OnAwake()
        {
            base.OnAwake();
            Debug.Log("Controller OnAwake");
            playerRepository = Game.Instance.GetRepository<PlayerRepository>();
            yield return null;
        }

        public override IEnumerator Initialize()
        {
            base.Initialize();
            Debug.Log("Controller Initialized");
            yield return null;
        }

        public override IEnumerator OnStart()
        {
            base.OnStart();
            Debug.Log("Controller OnStart");
            yield return null;
        }


        public int GetNumCoins() => playerRepository.coins;
        public void AddCoins(object sender, int value)
        {
            playerRepository.coins += value;
            playerRepository.Save();
        }

        public void SpendCoins(object sender, int value)
        {
            playerRepository.coins -= value;
            playerRepository.Save();
        }
    }
}
