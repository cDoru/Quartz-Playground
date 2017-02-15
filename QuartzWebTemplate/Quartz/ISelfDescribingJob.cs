using Quartz;

namespace QuartzWebTemplate.Quartz
{
    public interface ISelfDescribingJob : IJob
    {
        JobInfo Describe { get; }
    }
}