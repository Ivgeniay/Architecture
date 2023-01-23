using System.Collections;

namespace Architecture.Root._Controller
{
    internal abstract class Controller
    {
        public virtual IEnumerator OnAwake() { return null; }
        public virtual IEnumerator Initialize() { return null; }
        public virtual IEnumerator OnStart() { return null; }
    }
}
