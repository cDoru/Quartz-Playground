using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.WebApi;
using QuartzWebTemplate.Configuration;
using QuartzWebTemplate.Infrastructure.Contracts;
using QuartzWebTemplate.Quartz.AutoFacConfiguration;
using QuartzWebTemplate.Quartz.Config;
using QuartzWebTemplate.Quartz.Locking.Contracts;
using QuartzWebTemplate.Quartz.Locking.Impl;
using QuartzWebTemplate.Quartz.Scheduler;
using QuartzWebTemplate.Quartz.Security;
using QuartzWebTemplate.Services;
using QuartzWebTemplate.Infrastructure;

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
            JobsConfiguration.RegisterJobs(builder);

            AutowireProperties(builder);
            builder.Register<IResolver>(c => new Resolver(Container));
            Container = builder.Build();
            DependencyResolver.SetResolver(new AutofacResolver(Container));
            GlobalConfiguration.Configuration.DependencyResolver =
                 new AutofacWebApiDependencyResolver(Container);

            // inject autofac into ef's resolver
            DbConfiguration.Loaded += DbConfiguration_Loaded;
        }

        private static void AutowireProperties(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(typeof(Inner).Assembly)
                .PropertiesAutowired();

            builder.RegisterType<WebApiApplication>()
                .PropertiesAutowired();
        }

        private static void RegisterJobsDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<HelloService>().As<IHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<FailingHelloService>().As<IFailingHelloService>().InstancePerLifetimeScope();
            builder.RegisterType<QuartzConfiguration>().As<IQuartzConfiguration>().InstancePerLifetimeScope();
            builder.RegisterType<NowImplementation>().As<INow>().InstancePerLifetimeScope();
            builder.Register<Func<IQuartzConfiguration>>(x => () => Container.Resolve<IQuartzConfiguration>());
            builder.RegisterType<ISocket>().As<SocketImplementation>().SingleInstance();
            builder.RegisterType<BasicAuthentication>().As<IHttpModule>().InstancePerLifetimeScope();
            builder.RegisterType<QuartzRedirectModule>().As<IHttpModule>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultQuartzSchedulerConfiguration>()
                .As<IQuartzConfigurationProvider>()
                .SingleInstance();

            builder.RegisterType<ConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();

            builder.RegisterType<Encryptor>().As<IEncryptor>().SingleInstance();
            builder.RegisterType<SqlAppLock>().As<IAppLock>().InstancePerLifetimeScope();
            builder.RegisterType<Lock>().As<ILock>().InstancePerLifetimeScope();
            builder.RegisterType<SynchronizationTokenHolder>().As<ISynchronizationTokenHolder>().SingleInstance();
        }


        static void DbConfiguration_Loaded(object sender, DbConfigurationLoadedEventArgs e)
        {
            e.AddDependencyResolver(new WrappingEfAutofacResolver(e.DependencyResolver, Container), true);
        }
    }

    internal sealed class Inner { }
}