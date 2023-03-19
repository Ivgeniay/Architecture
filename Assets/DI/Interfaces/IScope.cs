﻿using System;

namespace DI.Containers
{
    internal interface IScope : IDisposable, IAsyncDisposable
    {
        object Resolve(Type service);
    }
}