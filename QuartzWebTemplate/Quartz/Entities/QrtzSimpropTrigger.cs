namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_SIMPROP_TRIGGERS

    public class QrtzSimpropTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerName { get; set; } // TRIGGER_NAME (Primary key) (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
        public string StrProp1 { get; set; } // STR_PROP_1 (length: 512)
        public string StrProp2 { get; set; } // STR_PROP_2 (length: 512)
        public string StrProp3 { get; set; } // STR_PROP_3 (length: 512)
        public int? IntProp1 { get; set; } // INT_PROP_1
        public int? IntProp2 { get; set; } // INT_PROP_2
        public long? LongProp1 { get; set; } // LONG_PROP_1
        public long? LongProp2 { get; set; } // LONG_PROP_2
        public decimal? DecProp1 { get; set; } // DEC_PROP_1
        public decimal? DecProp2 { get; set; } // DEC_PROP_2
        public bool? BoolProp1 { get; set; } // BOOL_PROP_1
        public bool? BoolProp2 { get; set; } // BOOL_PROP_2

        // Foreign keys
        public virtual QrtzTrigger QrtzTrigger { get; set; } // FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS
    }
}