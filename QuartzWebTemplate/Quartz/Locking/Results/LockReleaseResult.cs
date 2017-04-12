namespace QuartzWebTemplate.Quartz.Locking.Results
{
    public class LockReleaseResult
    {
        public bool Success { get; set; }
        public ReleaseLockFailure Reason { get; set; }
    }
}