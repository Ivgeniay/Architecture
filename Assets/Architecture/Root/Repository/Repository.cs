using System.Collections;

namespace Architecture.Root.Repositories
{
    public abstract class Repository
    {
        public virtual IEnumerator OnAwakeRoutine() { yield return null; }
        public virtual IEnumerator InitializeRoutine() { yield return null; }
        public virtual IEnumerator OnStartRoutine() { yield return null; }
        public virtual void Frame() { }
        public abstract IEnumerator SaveRoutine();
    }
}
