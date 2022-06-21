

using System.Collections;
namespace Architecture
{
    public abstract class RepositoryBase : IRepository
    {
        public abstract IEnumerator InitializeRepository();
        public abstract IEnumerator Save();
        public abstract IEnumerator StartRepository();
    }
}
