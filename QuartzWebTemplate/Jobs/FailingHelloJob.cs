using Quartz;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Services;

namespace QuartzWebTemplate.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FailingHelloJob : ISelfDescribingJob
    {
        private readonly IFailingHelloService _failingHelloService;
        public FailingHelloJob(IFailingHelloService failingHelloService)
        {
            _failingHelloService = failingHelloService;
        }


        void IJob.Execute(IJobExecutionContext context)
        {
            _failingHelloService.FailToSayHello();
        }

        JobInfo ISelfDescribingJob.Describe
        {
            get
            {
                return new JobInfo
                    {
                        JobGroup = "FailingJobGroup",
                        JobName = "FailingJobName"
                    };
            }
        }
    }
}