namespace QuartzWebTemplate.Jobs
{
    //[PersistJobDataAfterExecution]
    //[DisallowConcurrentExecution]
    //// ReSharper disable once ClassNeverInstantiated.Global
    //public class FailingHelloJob : SelfDescribingJobBase
    //{
    //    private readonly IFailingHelloService _failingHelloService;

    //    public FailingHelloJob(IFailingHelloService failingHelloService)
    //    {
    //        _failingHelloService = failingHelloService;
    //    }

    //    public override void Execute(IJobExecutionContext context)
    //    {
    //        _failingHelloService.FailToSayHello();
    //    }

    //    public override Action<SimpleScheduleBuilder> Cron
    //    {
    //        get
    //        {
    //            return x =>
    //                x.WithIntervalInMinutes(1)
    //                    .WithMisfireHandlingInstructionNextWithRemainingCount()
    //                    .RepeatForever();
    //        }
    //    }

    //    protected override string Group
    //    {
    //        get { return "FailingGroup"; }
    //    }
    //}
}