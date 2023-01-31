using System.Collections;

namespace Architecture.Root._Controller
{
    public abstract class Controller
    {
        public virtual IEnumerator OnAwake() { yield return null; }
        public virtual IEnumerator Initialize() { yield return null; }
        public virtual IEnumerator OnStart() { yield return null; }
        public virtual void Frame() { }
    }
}
