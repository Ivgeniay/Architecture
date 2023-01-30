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
            playerRepository = Game.Instance.GetRepository<PlayerRepository>();
            yield return null;
            Debug.Log("PlayerController OnAwake");
        }

        public override IEnumerator Initialize()
        {
            yield return null;
            Debug.Log("PlayerController OnInitialized");
        }

        public override IEnumerator OnStart()
        {
            yield return null;
            Debug.Log("PlayerController OnStart");
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
