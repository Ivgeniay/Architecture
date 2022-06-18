using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public abstract class RepositoryBase : IRepository
    {
        public void Initialization()
        {
            throw new System.NotImplementedException();
        }

        abstract public void Save();

        public void Start()
        {
            throw new System.NotImplementedException();
        }
    }
}
