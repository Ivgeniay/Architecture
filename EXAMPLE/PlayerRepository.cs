using System.Collections.Generic;
using Architecture;

public class PlayerRepository : RepositoryBase
{
    public override void Initialization()
    {
        //throw new System.NotImplementedException();
    }

    public override void Save()
    {
        //throw new System.NotImplementedException();
    }

    public override void Start()
    {
        //throw new System.NotImplementedException();
    }

    private int _health;

    public int Health{
        get => _health;
        private set {
            _health = value;
        }
    }

    private List<string> inventory;

    public void AddDamage(int dmg)
    {
        if (Health - dmg < 0) Health = 0;
        else Health = Health - dmg;
    }
    public void AddCure(int cure)
    {
        if (Health - cure > 100) _health = 100;
        else _health = _health + cure;
    }

}
