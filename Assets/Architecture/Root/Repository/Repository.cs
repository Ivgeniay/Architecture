using System.Collections;

namespace Architecture.Root.Repositories
{
    public abstract class Repository
    {
        public virtual IEnumerator OnAwakeRoutine() { yield break; }
        public virtual IEnumerator InitializeRoutine() { yield break; }
        public virtual IEnumerator OnStartRoutine() { yield break; }
        public virtual void Frame() { }
        public abstract IEnumerator SaveRoutine();
    }
}
