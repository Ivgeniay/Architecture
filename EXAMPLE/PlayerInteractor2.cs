using UnityEngine;
using Architecture;
using System.Collections;
using System;

public class PlayerInteractor : InteractorBase
{
    public PlayerInteractor()
    {
        this.repository = new PlayerRepository();
    }

    private PlayerRepository repository;

    public override IEnumerator InitializeInteractor()
    {
        yield return Routine.StartRoutine(TestInitializeInteractor());
        yield return Routine.StartRoutine(this.repository.InitializeRepository());
    }

        private IEnumerator TestInitializeInteractor()
        {
            yield return new WaitForSeconds(2);
            Debug.Log($"HEY {this} is initialized!");
        }

    public override IEnumerator StartInteractor()
    {
        yield return Routine.StartRoutine(TestStartInteractor());
        yield return Routine.StartRoutine(this.repository.InitializeRepository());
    }
        private IEnumerator TestStartInteractor()
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"HEY {this} is started!");
        }

    public void TakeDamage(int dmg)
    {
        if (repository.Health - dmg < 0) repository.Health = 0;
        else repository.Health = repository.Health - dmg;
    }
    public void TakeCure(int cure)
    {
        if (repository.Health - cure > 100) repository.Health = 100;
        else repository.Health = repository.Health + cure;
    }
    public int GetHealth()
    {
        return repository.Health;
    }
}