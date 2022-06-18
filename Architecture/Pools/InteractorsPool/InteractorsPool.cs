using System.Collections.Generic;
using System;

namespace Architecture
{
    public sealed class InteractorsPool
    {
        public InteractorsPool()
        {
            interactorsMap = new Dictionary<Type, InteractorBase>();
        }

        public Dictionary<Type, InteractorBase> interactorsMap;
    }
}
