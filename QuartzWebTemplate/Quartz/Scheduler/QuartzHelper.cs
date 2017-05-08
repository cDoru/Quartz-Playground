using System;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    public static class QuartzHelper
    {
        public static async Task MaybeRegister<T>(IScheduler scheduler, T job) where T : ISelfDescribingJob
        {
            var description = job.Describe;
            var jobKey = new JobKey(description.JobName, description.JobGroup);

            if (await scheduler.CheckExists(jobKey)) return;
            

            var jobDetail = JobBuilder.Create<T>()
                .WithIdentity(description.JobName, description.JobGroup)
                .StoreDurably()
                .Build();

            var jobTrigger = TriggerBuilder.Create()
                .WithIdentity(description.JobName, description.JobGroup)
                .WithSimpleSchedule(job.Cron)
                
                .StartAt(DateTime.Now)
                .Build();

            await scheduler.ScheduleJob(jobDetail, jobTrigger);
        }

        public static async Task TriggerNow<T>(IScheduler scheduler, T job, DateTime start, params Tuple<string, string>[] variables) where T : ISelfDescribingJob
        {
            var description = job.Describe;

            // form the job key
            var jobKey = new JobKey(description.JobName, description.JobGroup);

            var dataMapFeed = variables.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
            JobDataMap dataMap = new JobDataMap(dataMapFeed);
            await scheduler.TriggerJob(jobKey, dataMap);
        }
    }
}