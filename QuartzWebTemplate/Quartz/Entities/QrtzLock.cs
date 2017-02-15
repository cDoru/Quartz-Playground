namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_LOCKS

    public class QrtzLock
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string LockName { get; set; } // LOCK_NAME (Primary key) (length: 40)
    }
}