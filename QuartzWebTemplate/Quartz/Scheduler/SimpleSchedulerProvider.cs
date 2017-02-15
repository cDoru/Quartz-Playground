using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CrystalQuartz.Core.SchedulerProviders;
using Quartz;
using QuartzWebTemplate.Jobs;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    // ReSharper disable once UnusedMember.Global
    public class SimpleSchedulerProvider : StdSchedulerProvider
    {
        protected override System.Collections.Specialized.NameValueCollection GetSchedulerProperties()
        {
            return DefaultQuartzSchedulerConfiguration.GetConfiguration();
            
        }

        protected override void InitScheduler(IScheduler scheduler)
        {
            // Put jobs creation code here
            var dependencyResolver = GlobalConfiguration.Configuration.DependencyResolver as AutofacWebApiDependencyResolver;
            if (dependencyResolver != null)
            {
                ISelfDescribingJob helloJob;
                if (TryResolve(dependencyResolver, JobConstants.HelloJobKey, out helloJob))
                {
                    QuartzHelper.MaybeRegister(scheduler, helloJob, x => 
                        x.WithIntervalInMinutes(1)
                         .WithMisfireHandlingInstructionIgnoreMisfires()
                         .RepeatForever());
                }

                ISelfDescribingJob failingHelloJob;
                if (TryResolve(dependencyResolver, JobConstants.FailingHelloJobKey, out failingHelloJob))
                {
                    QuartzHelper.MaybeRegister(scheduler, failingHelloJob, x => 
                        x.WithIntervalInMinutes(1)
                         .WithMisfireHandlingInstructionIgnoreMisfires()
                         .RepeatForever());
                }
            }
        }

        private static bool TryResolve<T>(AutofacWebApiDependencyResolver resolver, string key, out T instance)
        {
            instance = default(T);

            try
            {
                instance = resolver.Container.ResolveKeyed<T>(key);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}