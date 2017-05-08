using System;
using System.Threading.Tasks;
using Quartz;
using QuartzWebTemplate.Exceptions;
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
        public async Task Execute(IJobExecutionContext context)
        {
            if (MissfireHelper.IsMissedFire(context))
            {
                return;
            }
            
            var dataMap = context.MergedJobDataMap;
            var myName = dataMap[JobKeys.JobDataName] as string;
            if (myName == null)
            {
                throw new DataMapItemMissingException(JobKeys.JobDataName);
            }

            var colorRaw = dataMap[JobKeys.JobDataColor] as string;
            if (colorRaw == null)
            {
                throw new DataMapItemMissingException(JobKeys.JobDataColor);
            }

            var color = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), colorRaw);




           /* throw new NotImplementedException();*/
            await Task.Delay(10000);
        }

        public JobInfo Describe
        {
            get
            {
                return new JobInfo
                {
                    JobName = GetType().Name,
                    JobGroup = "ConcurrentGroup",
                    HasActiveSchedule = false
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