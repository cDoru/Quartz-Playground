using System.Web.Http.Dependencies;
using Autofac;

namespace QuartzWebTemplate.App_Start
{
    public class AutofacResolver : AutofacDependencyScope, System.Web.Mvc.IDependencyResolver
    {
        private readonly IContainer _container;

        public AutofacResolver(IContainer container)
            : base(container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new AutofacDependencyScope(_container.BeginLifetimeScope());
        }
    }
}