using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public abstract class InteractorBase : IInteractor
    {
        public abstract void Initialization();
        public abstract void Start();
    }
}
