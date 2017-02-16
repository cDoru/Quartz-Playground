using System;
using System.Collections.Specialized;
using Autofac;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace QuartzWebTemplate.Quartz.AutoFacConfiguration
{
    /// <summary>
    ///     Registers <see cref="ISchedulerFactory" /> and default <see cref="IScheduler" />.
    /// </summary>
    public class QuartzFactoryModule : Module
    {
        /// <summary>
        ///     Default name for nested lifetime scope.
        /// </summary>
        public const string LifetimeScopeName = "quartz.job";

        readonly string _lifetimeScopeName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartzFactoryModule" /> class with a default lifetime scope
        ///     name.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">lifetimeScopeName</exception>
        public QuartzFactoryModule()
            : this(LifetimeScopeName)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartzFactoryModule" /> class.
        /// </summary>
        /// <param name="lifetimeScopeName">Name of the lifetime scope to wrap job resolution and execution.</param>
        /// <exception cref="System.ArgumentNullException">lifetimeScopeName</exception>
        public QuartzFactoryModule(string lifetimeScopeName)
        {
            if (lifetimeScopeName == null) throw new ArgumentNullException("lifetimeScopeName");
            _lifetimeScopeName = lifetimeScopeName;
        }

        /// <summary>
        ///     Provides custom configuration for Scheduler.
        ///     Returns <see cref="NameValueCollection" /> with custom Quartz settings.
        ///     <para>See http://quartz-scheduler.org/documentation/quartz-2.x/configuration/ for settings description.</para>
        ///     <seealso cref="StdSchedulerFactory" /> for some configuration property names.
        /// </summary>
        public Func<IComponentContext, NameValueCollection> ConfigurationProvider { get; set; }

        /// <summary>
        ///     Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        ///     Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">
        ///     The builder through which components can be
        ///     registered.
        /// </param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new JobFactory(c.Resolve<ILifetimeScope>(), _lifetimeScopeName))
                .AsSelf()
                .As<IJobFactory>()
                .SingleInstance();

            builder.Register<ISchedulerFactory>(c =>
            {
                var cfgProvider = ConfigurationProvider;

                var autofacSchedulerFactory = cfgProvider != null
                    ? new SchedulerFactory(cfgProvider(c), c.Resolve<JobFactory>())
                    : new SchedulerFactory(c.Resolve<JobFactory>());
                return autofacSchedulerFactory;
            })
                .SingleInstance();

            builder.Register(c => c.Resolve<ISchedulerFactory>().GetScheduler())
                .SingleInstance();
        }
    }
}