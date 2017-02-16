using System;
using System.Collections.Specialized;
using Quartz;
using Quartz.Core;
using Quartz.Impl;

namespace QuartzWebTemplate.Quartz.AutoFacConfiguration
{
    /// <summary>
    ///     Scheduler factory which uses Autofac to instantiate jobs.
    /// </summary>
    public class SchedulerFactory : StdSchedulerFactory
    {
        readonly JobFactory _jobFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Quartz.Impl.StdSchedulerFactory" /> class.
        /// </summary>
        /// <param name="jobFactory">Job factory.</param>
        /// <exception cref="ArgumentNullException"><paramref name="jobFactory" /> is <see langword="null" />.</exception>
        public SchedulerFactory(JobFactory jobFactory)
        {
            if (jobFactory == null) throw new ArgumentNullException("jobFactory");
            _jobFactory = jobFactory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Quartz.Impl.StdSchedulerFactory" /> class.
        /// </summary>
        /// <param name="props">The properties.</param>
        /// <param name="jobFactory">Job factory</param>
        /// <exception cref="ArgumentNullException"><paramref name="jobFactory" /> is <see langword="null" />.</exception>
        public SchedulerFactory(NameValueCollection props, JobFactory jobFactory)
            : base(props)
        {
            if (jobFactory == null) throw new ArgumentNullException("jobFactory");
            _jobFactory = jobFactory;
        }

        /// <summary>
        ///     Instantiates the scheduler.
        /// </summary>
        /// <param name="rsrcs">The resources.</param>
        /// <param name="qs">The scheduler.</param>
        /// <returns>Scheduler.</returns>
        protected override IScheduler Instantiate(QuartzSchedulerResources rsrcs, QuartzScheduler qs)
        {
            var scheduler = base.Instantiate(rsrcs, qs);
            scheduler.JobFactory = _jobFactory;
            return scheduler;
        }
    }
}