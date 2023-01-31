using System.Collections;

namespace Architecture.Root._Repository
{
    public abstract class Repository
    {
        public virtual IEnumerator OnAwake() { yield return null; }
        public virtual IEnumerator Initialize() { yield return null; }
        public virtual IEnumerator OnStart() { yield return null; }
        public virtual void Frame() { }
        public abstract IEnumerator Save();
    }
}
