using System;

namespace Architecture.DI.Containers
{
    internal interface IContainer : IDisposable, IAsyncDisposable
    {
        IScope CreateScope();
    }
}
