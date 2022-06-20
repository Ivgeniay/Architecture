namespace Architecture
{
    public interface IRepository
    {
        abstract void InitializeRepository();
        abstract void Start();
        abstract void Save();

    }
}
