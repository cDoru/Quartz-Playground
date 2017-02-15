namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_JOB_DETAILS

    public class QrtzJobDetail
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string JobName { get; set; } // JOB_NAME (Primary key) (length: 150)
        public string JobGroup { get; set; } // JOB_GROUP (Primary key) (length: 150)
        public string Description { get; set; } // DESCRIPTION (length: 250)
        public string JobClassName { get; set; } // JOB_CLASS_NAME (length: 250)
        public bool IsDurable { get; set; } // IS_DURABLE
        public bool IsNonconcurrent { get; set; } // IS_NONCONCURRENT
        public bool IsUpdateData { get; set; } // IS_UPDATE_DATA
        public bool RequestsRecovery { get; set; } // REQUESTS_RECOVERY
        public byte[] JobData { get; set; } // JOB_DATA (length: 2147483647)

        // Reverse navigation
        public virtual System.Collections.Generic.ICollection<QrtzTrigger> QrtzTriggers { get; set; } // QRTZ_TRIGGERS.FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS

        public QrtzJobDetail()
        {
            QrtzTriggers = new System.Collections.Generic.List<QrtzTrigger>();
        }
    }
}