using System;
using Quartz;
using Quartz.Impl;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    public static class QuartzHelper
    {
        public static void MaybeRegister<T>(IScheduler scheduler, T job) where T : ISelfDescribingJob
        {
            var description = job.Describe;
            var jobKey = new JobKey(description.JobName, description.JobGroup);

            if (scheduler.CheckExists(jobKey)) return;
            

            var jobDetail = JobBuilder.Create<T>()
                .WithIdentity(description.JobName, description.JobGroup)
                .StoreDurably()
                .Build();

            var jobTrigger = TriggerBuilder.Create()
                .WithIdentity(description.JobName, description.JobGroup)
                .WithSimpleSchedule(job.Cron)
                
                .StartAt(DateTime.Now)
                .Build();

            scheduler.ScheduleJob(jobDetail, jobTrigger);
        }
    }
}