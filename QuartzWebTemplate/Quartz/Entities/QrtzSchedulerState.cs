namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_SCHEDULER_STATE

    public class QrtzSchedulerState
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string InstanceName { get; set; } // INSTANCE_NAME (Primary key) (length: 200)
        public long LastCheckinTime { get; set; } // LAST_CHECKIN_TIME
        public long CheckinInterval { get; set; } // CHECKIN_INTERVAL
    }
}