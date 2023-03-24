using System;

namespace DI.Containers
{
    public interface IScope : IDisposable, IAsyncDisposable
    {
        public object Resolve(Type service, int id = 0);
    }
}
