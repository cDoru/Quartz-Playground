namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_CRON_TRIGGERS

    public class QrtzCronTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerName { get; set; } // TRIGGER_NAME (Primary key) (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
        public string CronExpression { get; set; } // CRON_EXPRESSION (length: 120)
        public string TimeZoneId { get; set; } // TIME_ZONE_ID (length: 80)

        // Foreign keys
        public virtual QrtzTrigger QrtzTrigger { get; set; } // FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS
    }
}