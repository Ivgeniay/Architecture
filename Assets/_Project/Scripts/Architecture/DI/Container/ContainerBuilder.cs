﻿namespace Architecture.DI.Containers
{
    internal class ContainerBuilder : IContainerBuilder
    {




        //private readonly Dictionary<Type, Type> map = new Dictionary<Type, Type>();

        //public void Register<TFrom, TTo>()
        //{
        //    map.Add(typeof(TFrom), typeof(TTo));
        //}

        //public T Resolve<T>()
        //{
        //    return (T)Resolve(typeof(T));
        //}

        //private object Resolve(Type type)
        //{
        //    Type resolveType = null;
        //    if (map.TryGetValue(type, out resolveType)) {
        //    }
        //    else {
        //        throw new InvalidOperationException($"There are no registered type {type}");
        //    }

        //    var ctorLength = resolveType.GetConstructors().Length;

        //    var ctor = resolveType.GetConstructors().First();
        //    var ctorParameters = ctor.GetParameters();
        //    if (ctorParameters.Length == 0)
        //    {
        //        return Activator.CreateInstance(resolveType);
        //    }

        //    var parameters = new List<object>();
        //    foreach ( var parameterToResolve in ctorParameters )
        //    {
        //        parameters.Add(Resolve(parameterToResolve.ParameterType));
        //    }

        //    return ctor.Invoke(parameters.ToArray());
        //}
    }
}