namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_FIRED_TRIGGERS
    public class QrtzFiredTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string EntryId { get; set; } // ENTRY_ID (Primary key) (length: 95)
        public string TriggerName { get; set; } // TRIGGER_NAME (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (length: 150)
        public string InstanceName { get; set; } // INSTANCE_NAME (length: 200)
        public long FiredTime { get; set; } // FIRED_TIME
        public long SchedTime { get; set; } // SCHED_TIME
        public int Priority { get; set; } // PRIORITY
        public string State { get; set; } // STATE (length: 16)
        public string JobName { get; set; } // JOB_NAME (length: 150)
        public string JobGroup { get; set; } // JOB_GROUP (length: 150)
        public bool? IsNonconcurrent { get; set; } // IS_NONCONCURRENT
        public bool? RequestsRecovery { get; set; } // REQUESTS_RECOVERY
    }
}