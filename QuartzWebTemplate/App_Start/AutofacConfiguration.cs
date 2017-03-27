using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using QuartzWebTemplate.Configuration;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Quartz.AutoFacConfiguration;
using QuartzWebTemplate.Quartz.Config;
using QuartzWebTemplate.Quartz.Scheduler;
using QuartzWebTemplate.Quartz.Security;
using QuartzWebTemplate.Services;

namespace QuartzWebTemplate.App_Start
{
    public class AutofacConfiguration
    {
        private static IContainer Container { get; set; }
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(AutofacConfiguration).Assembly);
            builder.RegisterModule(new QuartzFactoryModule());
            builder.RegisterModule(new QuartzJobsModule(typeof(AutofacConfiguration).Assembly));

            RegisterJobsDependencies(builder);
            RegisterJobs(builder);

            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(Container);
        }

        private static void RegisterJobsDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<FailingHelloService>().As<IFailingHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<QuartzConfiguration>().As<IQuartzConfiguration>().InstancePerLifetimeScope();

            builder.Register<Func<IQuartzConfiguration>>(x => () => Container.Resolve<IQuartzConfiguration>());

            builder.RegisterType<BasicAuthentication>().As<IHttpModule>().InstancePerLifetimeScope();
            builder.RegisterType<QuartzRedirectModule>().As<IHttpModule>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultQuartzSchedulerConfiguration>()
                .As<IQuartzConfigurationProvider>()
                .SingleInstance();

            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
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