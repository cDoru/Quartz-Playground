using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Autofac;

namespace QuartzWebTemplate.App_Start
{
    public class AutofacDependencyScope : IDependencyScope
    {
        private ILifetimeScope _scope;

        public AutofacDependencyScope(ILifetimeScope scope)
        {
            if (scope == null)
                throw new ArgumentNullException("scope");

            _scope = scope;
        }

        public void Dispose()
        {
            _scope.Dispose();
            _scope = null;
        }

        public object GetService(Type serviceType)
        {
            if (_scope == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            if (!_scope.IsRegistered(serviceType))
                return null;

            return _scope.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_scope == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed.");

            if (!_scope.IsRegistered(serviceType))
                return Enumerable.Empty<object>();

            return (IEnumerable<object>)_scope.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }
}