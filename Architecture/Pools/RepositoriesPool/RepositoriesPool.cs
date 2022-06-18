using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
