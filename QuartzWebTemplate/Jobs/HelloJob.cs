using Quartz;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Services;

namespace QuartzWebTemplate.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    // ReSharper disable once ClassNeverInstantiated.Global
    public  class HelloJob : ISelfDescribingJob
    {
        private readonly IHelloService _service;

        public HelloJob(IHelloService service)
        {
            _service = service;
        }

        void IJob.Execute(IJobExecutionContext context)
        {
            _service.SayHello();
        }

        JobInfo ISelfDescribingJob.Describe
        {
            get
            {
                return new JobInfo
                {
                    JobGroup = "JobGroup",
                    JobName = "JobName"
                };
            }
        }
    }
}