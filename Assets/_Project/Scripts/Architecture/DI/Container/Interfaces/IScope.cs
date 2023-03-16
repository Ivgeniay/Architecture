using System;

namespace Architecture.DI.Containers
{
    internal interface IScope
    {
        object Resolve(Type service);
    }
}
