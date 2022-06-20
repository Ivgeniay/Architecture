using UnityEngine;

namespace Architecture
{
    public abstract class InteractorBase : IInteractor
    {
        public virtual void InitializeRepository() {}
        public virtual void StartRepository() {}
        public abstract void Start();
        public abstract void Initialization();

    }
}
