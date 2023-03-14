using Architecture.Root.Controllers;
using System.Collections;
using UnityEngine;

internal class PlayerController : Controller
{
    private PlayerRepository playerRepository;

    public PlayerController()
    {
            
    }

    public override IEnumerator OnAwakeRoutine()
    {
        playerRepository = Game.Instance.GetRepository<PlayerRepository>();
        yield return null;
    }

    public override IEnumerator InitializeRoutine()
    {
        yield return null;
    }

    public override IEnumerator OnStartRoutine()
    {
        yield return null;
    }


    public int GetNumCoins() => playerRepository.coins;
    public void AddCoins(object sender, int value)
    {
        playerRepository.coins += value;
        playerRepository.SaveRoutine();
    }

    public void SpendCoins(object sender, int value)
    {
        playerRepository.coins -= value;
        playerRepository.SaveRoutine();
    }
    
}
