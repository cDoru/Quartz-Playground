using System;
using Quartz;
using QuartzWebTemplate.Quartz;
using QuartzWebTemplate.Services;

namespace QuartzWebTemplate.Jobs
{
    [PersistJobDataAfterExecution]
    [DisallowConcurrentExecution]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class HelloJob : SelfDescribingJobBase
    {
        private readonly IHelloService _service;

        public HelloJob(IHelloService service)
        {
            _service = service;
        }

        public override void Execute(IJobExecutionContext context)
        {
            _service.SayHello();
        }


        public override Action<SimpleScheduleBuilder> Cron
        {
            get
            {
                return x =>
                    x.WithIntervalInMinutes(1)
                        .WithMisfireHandlingInstructionNextWithRemainingCount()
                        .RepeatForever();
            }
        }

        protected override string Group
        {
            get { return "HelloGroup"; }
        }
    }
}