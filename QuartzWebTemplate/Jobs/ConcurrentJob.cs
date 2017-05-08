using System;
using System.Threading.Tasks;
using Quartz;
using QuartzWebTemplate.Jobs.Attributes;
using QuartzWebTemplate.Quartz;

namespace QuartzWebTemplate.Jobs
{
    [PersistJobDataAfterExecution]
    [AutofacJobKey(JobKeys.ConcurrentJobAutofacKey)]
    [LifetimeSelect(LifetimeSelection.PerContext)]
    [DontRegister]
    public class ConcurrentJob : ISelfDescribingJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public JobInfo Describe
        {
            get
            {
                return new JobInfo
                {
                    JobName = GetType().Name,
                    JobGroup = "ConcurrentGroup"
                };
            }
        }

        public Action<SimpleScheduleBuilder> Cron
        {
            get { return null; }
        }

        public bool HasActiveSchedule
        {
            get { return false; }
        }
    }
}