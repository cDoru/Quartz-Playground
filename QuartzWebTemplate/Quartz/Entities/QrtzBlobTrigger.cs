namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_BLOB_TRIGGERS

    public class QrtzBlobTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerName { get; set; } // TRIGGER_NAME (Primary key) (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
        public byte[] BlobData { get; set; } // BLOB_DATA (length: 2147483647)
    }
}