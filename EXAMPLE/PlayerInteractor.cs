using Architecture;

public class PlayerInteractor : InteractorBase
{
    public PlayerInteractor()
    {
        this.repository = new PlayerRepository();
    }

    //private RepositoryBase repository 
    private PlayerRepository repository;

    public override void Initialization()
    {
        this.repository.Initialization();
        //throw new System.NotImplementedException();
    }

    public override void Start()
    {
        //throw new System.NotImplementedException();
    }

    public void TakeDamage(int dmg)
    {
        repository.AddDamage(dmg);
    }
    public void TakeCure(int cure)
    {
        repository.AddCure(cure);
    }
    public int GetHealth()
    {
        return repository.Health;
    }
}
