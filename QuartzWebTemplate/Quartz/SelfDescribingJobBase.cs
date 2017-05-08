using System;
using System.Threading.Tasks;
using Quartz;

namespace QuartzWebTemplate.Quartz
{
    public abstract class SelfDescribingJobBase : ISelfDescribingJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => ExecuteInner(context));
        }



        protected abstract void ExecuteInner(IJobExecutionContext context);

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

        public abstract bool HasActiveSchedule { get; }
        protected abstract string Group { get; }
        protected abstract bool HasActiveJobSchedule { get; }
    }
}