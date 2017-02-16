using System.Web.Http;
using System.Web.Mvc;
using Autofac;
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

            RegisterDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<FailingHelloService>().As<IFailingHelloService>().InstancePerLifetimeScope();

            builder.RegisterType<HelloJob>().As<ISelfDescribingJob>().Keyed<ISelfDescribingJob>(JobConstants.HelloJobKey).InstancePerLifetimeScope();
            builder.RegisterType<FailingHelloJob>().As<ISelfDescribingJob>().Keyed<ISelfDescribingJob>(JobConstants.FailingHelloJobKey).InstancePerLifetimeScope();
        }
    }
}