

namespace Architecture
{
    public abstract class RepositoryBase : IRepository
    {
        public abstract void InitializeRepository();
        public abstract void Save();
        public abstract void Start();
    }
}
