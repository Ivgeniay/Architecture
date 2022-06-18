namespace Architecture
{
    public abstract class RepositoryBase : IRepository
    {
        public abstract void Initialization();
        public abstract void Save();
        public abstract void Start();
    }
}
