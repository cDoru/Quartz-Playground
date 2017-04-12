using System;
using Quartz;

namespace QuartzWebTemplate.Quartz
{
    public abstract class SelfDescribingJobBase : ISelfDescribingJob
    {
        public abstract void Execute(IJobExecutionContext context);

        public JobInfo Describe
        {
            get
            {
                return new JobInfo
                {
                    JobName = GetType().Name,
                    JobGroup = Group
                };
            }
        }

        public abstract Action<SimpleScheduleBuilder> Cron { get; }
        protected abstract string Group { get; }
    }
}