using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CrystalQuartz.Web.Configuration;
using Quartz;
using QuartzWebTemplate.AutoFacConfiguration;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    public static class SchedulerUtils
    {
        private static void Start(IScheduler provider)
        {
            provider.Start();
        }

        public static void StartScheduler()
        {
            // perform some initialization - pass the job factory
            var configuredScheduler = ConfigUtils.SchedulerProvider.Scheduler;

            if (!configuredScheduler.IsStarted)
            {
                var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver as AutofacWebApiDependencyResolver;

                if (dependencyResolver != null)
                {
                    JobFactory factory;
                    if (TryResolve(dependencyResolver, out factory))
                    {
                        configuredScheduler.JobFactory = factory;
                    }
                }


                Start(configuredScheduler);
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