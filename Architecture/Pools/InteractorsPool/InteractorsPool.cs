using System.Collections.Generic;
using Architecture;
using System;

public sealed class InteractorsPool
{
    public InteractorsPool()
    {
        interactorsMap = new Dictionary<Type, InteractorBase>();
    }

    public Dictionary<Type, InteractorBase> interactorsMap;
}
