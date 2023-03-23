using System;

namespace DI.Containers
{
    public interface IScope : IDisposable, IAsyncDisposable
    {
        object Resolve(Type service);
    }
}
