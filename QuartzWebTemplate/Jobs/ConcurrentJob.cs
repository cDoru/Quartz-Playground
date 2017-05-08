using System;
using System.Threading.Tasks;
using Quartz;
using QuartzWebTemplate.Exceptions;
using QuartzWebTemplate.Infrastructure.Contracts;
using QuartzWebTemplate.Jobs.Attributes;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Quartz.Locking.SemaphoreLocking;

namespace QuartzWebTemplate.Jobs
{
    [PersistJobDataAfterExecution]
    [AutofacJobKey(JobKeys.ConcurrentJobAutofacKey)]
    [LifetimeSelect(LifetimeSelection.PerContext)]
    [DontRegister]
    public class ConcurrentJob : ISelfDescribingJob
    {
        private readonly ISocket _socket;

        public ConcurrentJob(ISocket socket)
        {
            _socket = socket;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (MissfireHelper.IsMissedFire(context))
            {
                return;
            }
            
            var dataMap = context.MergedJobDataMap;
            var taskName = dataMap[JobKeys.JobDataName] as string;
            if (taskName == null)
            {
                throw new DataMapItemMissingException(JobKeys.JobDataName);
            }

            var colorRaw = dataMap[JobKeys.JobDataColor] as string;
            if (colorRaw == null)
            {
                throw new DataMapItemMissingException(JobKeys.JobDataColor);
            }

            var consoleColor = (ConsoleColor) Enum.Parse(typeof (ConsoleColor), colorRaw);

            ColoredConsoleWriteLine(consoleColor, string.Format("Entered {0}. Acquiring lock", taskName));

            for (var j = 0; j < 5; j++)
            {
                ColoredConsoleWriteLine(consoleColor, string.Format("Writing {0} from task {1}", j, taskName));
            }

            using (await CriticalRegionAsyncLock.CreateAsync(TokenHolder.SynchronizationToken))
            {
                ColoredConsoleWriteLine(consoleColor, string.Format("Entered lock from task {0}. Starting critical region", taskName));
                //CRITICAL REGION

                for (var i = 0; i < 10; i++)
                {
                    ColoredConsoleWriteLine(consoleColor, taskName);
                    await Task.Delay(100);
                }

                // END OF CRITICAL REGION
                ColoredConsoleWriteLine(consoleColor, string.Format("Finished critical region. Exiting lock from task {0}", taskName));
            }

            ColoredConsoleWriteLine(consoleColor, string.Format("Exiting task {0}", taskName));
        }

        private void ColoredConsoleWriteLine(ConsoleColor color, string text)
        {
            _socket.Send(text, color);
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