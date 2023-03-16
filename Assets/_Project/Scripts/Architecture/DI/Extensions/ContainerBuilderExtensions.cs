using Architecture.DI.Containers;
using Architecture.DI.Descriptors;
using System;

namespace Architecture.DI.Containers.Extensions
{
    internal static class ContainerBuilderExtensions
    {
        private static IContainerBuilder RegisterType(
            this IContainerBuilder builder, 
            Type serviseInterface, 
            Type serviceImplement,
            Lifetime lifetime)
        {
            builder.Register(new TypeBasedServiceDescriptor() { 
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
            Lifetime lifetime)
        {
            builder.Register(new FactoryBasedServiceDescriptor() { Factory = factory, ServiceType = service, Lifetime = lifetime });
            return builder;
        }

        private static IContainerBuilder RegisterInstance(this IContainerBuilder builder, Type service, object instance) {
            builder.Register(new InstanceBasedServiceDescriptor(service, instance));
            return builder;
        }

        public static IContainerBuilder RegistrationSingleton(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Singleton);
        public static IContainerBuilder RegistrationTransient(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Transient);
        public static IContainerBuilder RegistrationScoped(this IContainerBuilder builder, Type serviceInterface, Type serviceImplement)
            => builder.RegisterType(serviceInterface, serviceImplement, Lifetime.Scoped);

        public static IContainerBuilder RegistrationSingleton<TService, TImplement> (this IContainerBuilder builder) where TImplement : TService
           => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Singleton);
        public static IContainerBuilder RegistrationTransient<TService, TImplement>(this IContainerBuilder builder) where TImplement : TService
            => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Transient);
        public static IContainerBuilder RegistrationScoped<TService, TImplement>(this IContainerBuilder builder) where TImplement : TService
            => builder.RegisterType(typeof(TService), typeof(TImplement), Lifetime.Scoped);

        public static IContainerBuilder RegistrationSingleton<TService>(this IContainerBuilder builder, Func<IScope, object> factory)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Singleton);
        public static IContainerBuilder RegistrationTransient<TService>(this IContainerBuilder builder, Func<IScope, object> factory)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Transient);
        public static IContainerBuilder RegistrationScoped<TService>(this IContainerBuilder builder, Func<IScope, object> factory)
            => builder.RegisterFactory(typeof(TService), factory, Lifetime.Scoped);

        public static IContainerBuilder RegisterSingleton<TService>(this IContainerBuilder builder, object instance)
            => builder.RegisterInstance(typeof(TService), instance);
    }
}
