using System.Collections;

namespace Architecture.Root._Repository
{
    internal abstract class Repository
    {
        public virtual IEnumerator OnAwake() { return null; }
        public virtual IEnumerator Initialize() { return null; }
        public virtual IEnumerator OnStart() { return null; }
        public abstract IEnumerator Save();
    }
}
