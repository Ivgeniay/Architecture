using System;

namespace Architecture.DI.Containers
{
    internal interface IScope : IDisposable, IAsyncDisposable
    {
        object Resolve(Type service);
    }
}
