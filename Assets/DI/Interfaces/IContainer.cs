using System;

namespace DI.Containers
{
    public interface IContainer : IDisposable, IAsyncDisposable
    {
        IScope CreateScope();
    }
}
