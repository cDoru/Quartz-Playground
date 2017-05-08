using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using QuartzWebTemplate.Infrastructure.Contracts;
using QuartzWebTemplate.Jobs.Attributes;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Quartz.Scheduler;

namespace QuartzWebTemplate.Jobs
{

    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    [AutofacJobKey(JobKeys.SpinningJobAutofacKey)]
    public class SpinJob : ISelfDescribingJob
    {
        private const int NumberOfJobs = 5;
        private readonly IResolver _resolver;
        private readonly INow _now;

        public SpinJob(IResolver resolver, INow now)
        {
            _resolver = resolver;
            _now = now;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            if (MissfireHelper.IsMissedFire(context))
            {
                return;
            }

            const string jobName = "{0}Job";
            Func<int, ConsoleColor> getColor = x => (ConsoleColor)x + 1;

            for (var i = 0; i < NumberOfJobs; i++)
            {
                var utcNow = _now.UtcNow;
                var then = utcNow.AddSeconds(5);

                // spin a job
                ISelfDescribingJob job;

                if (!TryResolveKeyed(_resolver, JobKeys.ConcurrentJobAutofacKey, out job))
                {
                    throw new Exception("Could not resolve the calendar job");
                }

                var variables = new List<Tuple<string, string>>
                {
                    new Tuple<string, string>(JobKeys.JobDataName, string.Format(jobName, i)),
                    new Tuple<string, string>(JobKeys.JobDataColor, getColor(i).ToString())
                };

                await QuartzHelper.TriggerNow(context.Scheduler, job, then, variables.ToArray());
            }
        }

        public JobInfo Describe
        {
            get
            {
                return new JobInfo
                    {
                        JobGroup = JobKeys.SpinnerGroup,
                        JobName = GetType().Name,
                        HasActiveSchedule = true
                    };
            }
        }

        public Action<SimpleScheduleBuilder> Cron
        {
            get
            {
                return x =>
                    x.WithIntervalInMinutes(3)
                     .WithMisfireHandlingInstructionNextWithRemainingCount()
                     .RepeatForever();
            }
        }
        private static bool TryResolveKeyed<T>(IResolver resolver, string key, out T instance)
        {
            instance = default(T);

            try
            {
                instance = resolver.ResolveKeyed<T>(key);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool HasActiveSchedule
        {
            get { return true; }
        }
    }
}