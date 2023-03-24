using DI.Containers;
using DI.Descriptors;
using System;

namespace DI.Containers.Extensions
{
    public static class ContainerBuilderExtensions
    {
        private static IContainerBuilder RegisterType(
            this IContainerBuilder builder, 
            Type serviseInterface, 
            Type serviceImplement,
            Lifetime lifetime,
            int _id = 0)
        {
            builder.Register(new TypeBasedServiceDescriptor() { 
                id = _id,
                ImplementationType = serviceImplement, 
                ServiceType = serviseInterface,
                Lifetime = lifetime
            });
            return builder;
        }

        private static IContainerBuilder RegisterFactory(
            this IContainerBuilder builder, 
            Type service, 
            Func<IScope, object> factory, 
            Lifetime lifetime,
            int _id = 0)
        {
            builder.Register(new FactoryBasedServiceDescriptor() { Factory = factory, ServiceType = service, Lifetime = lifetime, id = _id });
            return builder;
        }

        private static IContainerBuilder RegisterInstance(this IContainerBuilder builder, Type service, object instance, int id = 0) {
            builder.Register(new InstanceBasedServiceDescriptor(service, instance));
            return builder;
        }

        public static IContainerBuilder RegistrationSingleton(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement, int id = 0)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Singleton, id);
        public static IContainerBuilder RegistrationTransient(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement, int id = 0)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Transient, id);
        public static IContainerBuilder RegistrationScoped(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement, int id = 0)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Scoped, id);

        public static IContainerBuilder RegistrationSingleton<TService, TImplement> (this IContainerBuilder builder, int id = 0) where TImplement : TService
           => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Singleton, id);
        public static IContainerBuilder RegistrationTransient<TService, TImplement>(this IContainerBuilder builder, int id = 0) where TImplement : TService
            => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Transient, id);
        public static IContainerBuilder RegistrationScoped<TService, TImplement>(this IContainerBuilder builder, int id = 0) where TImplement : TService
            => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Scoped, id);

        public static IContainerBuilder RegistrationSingleton<TService>(this IContainerBuilder builder, Func<IScope, object> factory, int id = 0)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Singleton, id);
        public static IContainerBuilder RegistrationTransient<TService>(this IContainerBuilder builder, Func<IScope, object> factory, int id = 0)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Transient, id);
        public static IContainerBuilder RegistrationScoped<TService>(this IContainerBuilder builder, Func<IScope, object> factory, int id = 0)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Scoped, id);

        public static IContainerBuilder RegistrationSingletonFromInstance<TService>(this IContainerBuilder builder, object instance, int id = 0)
            => builder.RegisterInstance(typeof(TService), instance, id);

    }
}
