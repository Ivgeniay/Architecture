using System;
using System.Collections.Generic;

namespace Architecture
{
    public sealed class RepositoriesPool
    {
        public RepositoriesPool()
        {
            repositoriesMap = new Dictionary<Type, RepositoryBase>();
        }

        public Dictionary<Type, RepositoryBase> repositoriesMap;
    }
}
