using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_TRIGGERS
    public class QrtzTrigger
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string TriggerName { get; set; } // TRIGGER_NAME (Primary key) (length: 150)
        public string TriggerGroup { get; set; } // TRIGGER_GROUP (Primary key) (length: 150)
        public string JobName { get; set; } // JOB_NAME (length: 150)
        public string JobGroup { get; set; } // JOB_GROUP (length: 150)
        public string Description { get; set; } // DESCRIPTION (length: 250)
        public long? NextFireTime { get; set; } // NEXT_FIRE_TIME
        public long? PrevFireTime { get; set; } // PREV_FIRE_TIME
        public int? Priority { get; set; } // PRIORITY
        public string TriggerState { get; set; } // TRIGGER_STATE (length: 16)
        public string TriggerType { get; set; } // TRIGGER_TYPE (length: 8)
        public long StartTime { get; set; } // START_TIME
        public long? EndTime { get; set; } // END_TIME
        public string CalendarName { get; set; } // CALENDAR_NAME (length: 200)
        public int? MisfireInstr { get; set; } // MISFIRE_INSTR
        public byte[] JobData { get; set; } // JOB_DATA (length: 2147483647)

        // Reverse navigation
        public virtual QrtzCronTrigger QrtzCronTrigger { get; set; } // QRTZ_CRON_TRIGGERS.FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS
        public virtual QrtzSimpleTrigger QrtzSimpleTrigger { get; set; } // QRTZ_SIMPLE_TRIGGERS.FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS
        public virtual QrtzSimpropTrigger QrtzSimpropTrigger { get; set; } // QRTZ_SIMPROP_TRIGGERS.FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS

        // Foreign keys
        public virtual QrtzJobDetail QrtzJobDetail { get; set; } // FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS
    }

}