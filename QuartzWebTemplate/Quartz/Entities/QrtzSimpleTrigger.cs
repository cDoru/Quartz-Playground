namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_SIMPLE_TRIGGERS

    public class QrtzSimpleTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerName { get; set; } // TRIGGER_NAME (Primary key) (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
        public int RepeatCount { get; set; } // REPEAT_COUNT
        public long RepeatInterval { get; set; } // REPEAT_INTERVAL
        public int TimesTriggered { get; set; } // TIMES_TRIGGERED

        // Foreign keys
        public virtual QrtzTrigger QrtzTrigger { get; set; } // FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS
    }
}