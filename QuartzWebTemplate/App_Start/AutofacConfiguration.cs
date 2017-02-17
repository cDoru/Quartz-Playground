using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.WebApi;
using QuartzWebTemplate.Jobs;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Quartz.AutoFacConfiguration;
using QuartzWebTemplate.Services;

namespace QuartzWebTemplate.App_Start
{
    public class AutofacConfiguration
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(AutofacConfiguration).Assembly);
            builder.RegisterModule(new QuartzFactoryModule());
            builder.RegisterModule(new QuartzJobsModule(typeof(AutofacConfiguration).Assembly));

            RegisterJobsDependencies(builder);
            RegisterJobs(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterJobsDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<FailingHelloService>().As<IFailingHelloService>().InstancePerLifetimeScope();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            var type = typeof(ISelfDescribingJob);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract).ToList();
            
            foreach (var implementation in types)
            {
                builder.RegisterType(implementation)
                    .As<ISelfDescribingJob>()
                    .Keyed<ISelfDescribingJob>(implementation.Name)
                    .InstancePerLifetimeScope();
            }
        }
    }
}