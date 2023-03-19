using System.Collections;

namespace Architecture.Root.Controllers
{
    public abstract class Controller
    {
        public virtual IEnumerator OnAwakeRoutine() { yield break; }
        public virtual IEnumerator InitializeRoutine() { yield break; }
        public virtual IEnumerator OnStartRoutine() { yield break; }
        public virtual IEnumerable OnExitRoutine() { yield break; }
        public virtual void Frame() { }
    }
}
