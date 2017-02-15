using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Autofac;
using Quartz;
using Module = Autofac.Module;

namespace QuartzWebTemplate.AutoFacConfiguration
{
    /// <summary>
    ///     Registers Quartz jobs from specified assemblies.
    /// </summary>
    public class QuartzJobsModule : Module
    {
        readonly Assembly[] _assembliesToScan;


        /// <summary>
        ///     Initializes a new instance of the <see cref="QuartzJobsModule" /> class.
        /// </summary>
        /// <param name="assembliesToScan">The assemblies to scan for jobs.</param>
        /// <exception cref="System.ArgumentNullException">assembliesToScan</exception>
        public QuartzJobsModule(params Assembly[] assembliesToScan)
        {
            if (assembliesToScan == null) throw new ArgumentNullException("assembliesToScan");
            _assembliesToScan = assembliesToScan;
            PropertyWiringOptions = PropertyWiringOptions.None;
        }

        /// <summary>
        ///     Instructs Autofac whether registered types should be injected into properties.
        /// </summary>
        /// <remarks>
        ///     Default is <c>false</c>.
        /// </remarks>
        public bool AutoWireProperties { get; set; }

        /// <summary>
        ///     Property wiring options.
        ///     Used if <see cref="AutoWireProperties" /> is <c>true</c>.
        /// </summary>
        /// <remarks>
        ///     See Autofac API documentation http://autofac.org/apidoc/html/33ED0D92.htm for details.
        /// </remarks>
        public PropertyWiringOptions PropertyWiringOptions { get; private set; } 

        /// <summary>
        ///     Job registration filter callback.
        /// </summary>
        /// <seealso cref="JobRegistrationFilter" />
        public JobRegistrationFilter JobFilter { get; set; }

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
            var registrationBuilder = builder.RegisterAssemblyTypes(_assembliesToScan)
                .Where(type => !type.IsAbstract && typeof(IJob).IsAssignableFrom(type) && FilterJob(type))
                .AsSelf().InstancePerLifetimeScope();

            if (AutoWireProperties)
                registrationBuilder.PropertiesAutowired(PropertyWiringOptions);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool FilterJob(Type jobType)
        {
            return JobFilter == null || JobFilter(jobType);
        }
    }
}