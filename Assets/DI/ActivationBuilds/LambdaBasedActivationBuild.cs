using DI.Containers;
using DI.Descriptors;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System;

namespace DI.ActivationBuilds
{
    internal class LambdaBasedActivationBuild : BaseActivationBuilder
    {
        private static readonly MethodInfo ResolveMethod = typeof(IScope).GetMethod("Resolve");
        protected override Func<IScope, object> BuildActivationInternal(TypeBasedServiceDescriptor tb, ConstructorInfo ctor, ParameterInfo[] parameters, ServiceDescriptor descriptor)
        {
            var scopeParameter = Expression.Parameter(typeof(IScope), "scope");

            //new Contoller(scope.Resolve<IService>(), ...);

            var ctorArgs = parameters.Select(el =>
                Expression.Convert(Expression.Call(scopeParameter, ResolveMethod, Expression.Constant(el.ParameterType)), el.ParameterType));
                //Expression.Call(scopeParameter, ResolveMethod, Expression.Convert(Expression.Constant(el.ParameterType), el.ParameterType)));
            var @new = Expression.New(ctor, ctorArgs);

            var lambda = Expression.Lambda<Func<IScope, object>>(@new, scopeParameter);
            return lambda.Compile();
        }
    }
}
