using UnityEngine;
using Architecture;

public class PlayerInteractor : InteractorBase
{
    public PlayerInteractor()
    {
        this.repository = new PlayerRepository();
    }

    //private RepositoryBase repository 
    private PlayerRepository repository;

    public override void InitializeInteractor()
    {
        Debug.Log($"HEY {this} is initialized!");
        this.repository.InitializeRepository();
        //throw new System.NotImplementedException();
    }

    public override void StartInteractor()
    {
        //throw new System.NotImplementedException();
    }

    public void AddCure(int cure)
    {
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
