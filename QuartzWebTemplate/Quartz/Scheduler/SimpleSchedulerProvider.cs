using System;
using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Quartz;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    // ReSharper disable once UnusedMember.Global
    public class SimpleSchedulerProvider : LazySchedulerProvider
    {
        protected override System.Collections.Specialized.NameValueCollection GetSchedulerProperties()
        {
            
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver as AutofacWebApiDependencyResolver;
            if (dependencyResolver != null)
            {
                IQuartzConfigurationProvider provider;
                if (TryResolve(dependencyResolver, out provider))
                {
                    return provider.GetConfiguration();
                }
            }

            throw new Exception("Could not resolve quartz configuration provider service");
        }

        protected override void InitScheduler(IScheduler scheduler)
        {
            // Put jobs creation code here
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver as AutofacWebApiDependencyResolver;
            if (dependencyResolver != null)
            {
                IEnumerable<ISelfDescribingJob> jobs;
                if (TryResolve(dependencyResolver, out jobs))
                {
                    foreach (var job in jobs)
                    {
                        QuartzHelper.MaybeRegister(scheduler, job);
                    }
                }
            }
        }

        private static bool TryResolve<T>(AutofacWebApiDependencyResolver resolver, out T instance)
        {
            instance = default(T);

            try
            {
                instance = resolver.Container.Resolve<T>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}