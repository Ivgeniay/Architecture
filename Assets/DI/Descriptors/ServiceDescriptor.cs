﻿using System;

namespace DI.Descriptors
{
    public abstract class ServiceDescriptor
    {
        public Type ServiceType { get; set; }
        internal Lifetime Lifetime { get; set; }
    }
}
