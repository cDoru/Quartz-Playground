namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_PAUSED_TRIGGER_GRPS

    public class QrtzPausedTriggerGrp
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
    }
}