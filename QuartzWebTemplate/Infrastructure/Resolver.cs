using System;
using Autofac;
using QuartzWebTemplate.Exceptions;
using QuartzWebTemplate.Infrastructure.Contracts;

namespace QuartzWebTemplate.Infrastructure
{
    /// <summary>
    /// Resolves any dependency registered in autofac
    /// </summary>
    public class Resolver : IResolver
    {
        private readonly ILifetimeScope _scope;
        private const string ResolverFailure = "Failed to resolve type {0}. More details in the underlying exception.";

        public Resolver(ILifetimeScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Resolves simple dependency based on the autofac di container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            try
            {
                return _scope.Resolve<T>();
            }
            catch (Exception exception)
            {
                throw new ResolverException(string.Format(ResolverFailure, typeof(T).FullName), exception);
            }
        }

        public T ResolveKeyed<T>(string key)
        {
            try
            {
                return _scope.ResolveKeyed<T>(key);
            }
            catch (Exception exception)
            {
                throw new ResolverException(string.Format(ResolverFailure, typeof(T).FullName), exception);
            }
        }
    }
}