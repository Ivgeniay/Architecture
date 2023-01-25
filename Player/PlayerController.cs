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
        }

        public override IEnumerator Initialize()
        {
            yield return null;
        }

        public override IEnumerator OnStart()
        {
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
