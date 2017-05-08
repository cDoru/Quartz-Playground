using System;
using Quartz;

namespace QuartzWebTemplate.Quartz
{
    public interface ISelfDescribingJob : IJob
    {
        JobInfo Describe { get; }
        Action<SimpleScheduleBuilder> Cron { get; }

        bool HasActiveSchedule { get; }
    }
}