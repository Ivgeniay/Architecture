using UnityEngine;
using Architecture;
using System.Collections;
using System;

public class PlayerInteractor2 : InteractorBase
{
    public PlayerInteractor2()
    {
        this.repository = new PlayerRepository2();
    }

    private PlayerRepository2 repository;

    public override IEnumerator InitializeInteractor()
    {
        yield return Routine.StartRoutine(TestInitializeInteractor());
        yield return Routine.StartRoutine(this.repository.InitializeRepository());
    }

        private IEnumerator TestInitializeInteractor()
        {
            yield return new WaitForSeconds(2);
            Debug.Log($"{this} is initialized!");
        }

    public override IEnumerator StartInteractor()
    {
        yield return Routine.StartRoutine(TestStartInteractor());
        yield return Routine.StartRoutine(this.repository.StartRepository());
    }
        private IEnumerator TestStartInteractor()
        {
            yield return new WaitForSeconds(1);
            Debug.Log($"{this} is started!");
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
