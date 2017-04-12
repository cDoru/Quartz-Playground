namespace QuartzWebTemplate.Quartz.Locking
{
    public class LockReleaseResult
    {
        public bool Success { get; set; }
        public ReleaseLockFailure Reason { get; set; }
    }
}