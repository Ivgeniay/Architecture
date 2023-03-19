using System;

namespace DI.Containers
{
    internal interface IContainer : IDisposable, IAsyncDisposable
    {
        IScope CreateScope();
    }
}
