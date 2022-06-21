using System;
using System.Collections;
using UnityEngine;

namespace Architecture
{
    public abstract class InteractorBase : IInteractor
    {
        public virtual IEnumerator InitializeRepository() 
        {
            yield return null;
        }
        public virtual IEnumerator StartRepository() 
        {
            yield return null;
        }
        public abstract IEnumerator InitializeInteractor();
        public abstract IEnumerator StartInteractor();

    }
}
