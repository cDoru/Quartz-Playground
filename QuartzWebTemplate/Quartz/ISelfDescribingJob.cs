using System;
using Quartz;

namespace QuartzWebTemplate.Quartz
{
    public interface ISelfDescribingJob : IJob
    {
        JobInfo Describe { get; }
        Action<SimpleScheduleBuilder> Cron { get; }
    }

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