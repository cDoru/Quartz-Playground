using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using QuartzWebTemplate.Exceptions;
using QuartzWebTemplate.Jobs.Attributes;
using QuartzWebTemplate.Quartz;

namespace QuartzWebTemplate.App_Start
{
    public class JobsConfiguration
    {
        public static void RegisterJobs(ContainerBuilder builder)
        {
            // filter out types/jobs that have DontRegister on then
            var type = typeof(ISelfDescribingJob);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract /*&& p.GetCustomAttribute<DontRegisterAttribute>() == null*/ &&
                    p.GetCustomAttribute<AutofacJobKeyAttribute>() != null).ToList();

            foreach (var implementation in types)
            {
                var attr = implementation.GetCustomAttribute<AutofacJobKeyAttribute>();
                var lifetime = implementation.GetCustomAttribute<LifetimeSelectAttribute>();

                var selection = LifetimeSelection.PerLifetime;
                if (lifetime != null)
                {
                    selection = lifetime.Selection;
                }

                if (attr == null)
                {
                    throw new AutofacJobKeyMissingException();
                }

                var implementationKey = attr.Key;

                if (string.IsNullOrEmpty(implementationKey))
                {
                    throw new AutofacJobKeyEmptyException();
                }

                switch (selection)
                {
                    case LifetimeSelection.PerContext:
                        {
                            builder.RegisterType(implementation)
                                   .As<ISelfDescribingJob>()
                                   .Keyed<ISelfDescribingJob>(implementationKey)
                                   .InstancePerDependency();

                            break;
                        }
                    case LifetimeSelection.PerLifetime:
                        {
                            builder.RegisterType(implementation)
                                   .As<ISelfDescribingJob>()
                                   .Keyed<ISelfDescribingJob>(implementationKey)
                                   .InstancePerLifetimeScope();
                            break;
                        }
                    case LifetimeSelection.Singleton:
                        {
                            builder.RegisterType(implementation)
                                   .As<ISelfDescribingJob>()
                                   .Keyed<ISelfDescribingJob>(implementationKey)
                                   .SingleInstance();
                            break;
                        }
                    default:
                        {
                            builder.RegisterType(implementation)
                                   .As<ISelfDescribingJob>()
                                   .Keyed<ISelfDescribingJob>(implementationKey)
                                   .InstancePerLifetimeScope();
                            break;
                        }
                }
            }
        }
    }
}