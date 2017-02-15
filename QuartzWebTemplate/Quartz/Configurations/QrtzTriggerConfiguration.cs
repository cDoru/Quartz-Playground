using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_TRIGGERS
    public class QrtzTriggerConfiguration : EntityTypeConfiguration<QrtzTrigger>
    {
        public QrtzTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.TriggerName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JobName).HasColumnName(@"JOB_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.JobGroup).HasColumnName(@"JOB_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.Description).HasColumnName(@"DESCRIPTION").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.NextFireTime).HasColumnName(@"NEXT_FIRE_TIME").HasColumnType("bigint").IsOptional();
            Property(x => x.PrevFireTime).HasColumnName(@"PREV_FIRE_TIME").HasColumnType("bigint").IsOptional();
            Property(x => x.Priority).HasColumnName(@"PRIORITY").HasColumnType("int").IsOptional();
            Property(x => x.TriggerState).HasColumnName(@"TRIGGER_STATE").HasColumnType("nvarchar").IsRequired().HasMaxLength(16);
            Property(x => x.TriggerType).HasColumnName(@"TRIGGER_TYPE").HasColumnType("nvarchar").IsRequired().HasMaxLength(8);
            Property(x => x.StartTime).HasColumnName(@"START_TIME").HasColumnType("bigint").IsRequired();
            Property(x => x.EndTime).HasColumnName(@"END_TIME").HasColumnType("bigint").IsOptional();
            Property(x => x.CalendarName).HasColumnName(@"CALENDAR_NAME").HasColumnType("nvarchar").IsOptional().HasMaxLength(200);
            Property(x => x.MisfireInstr).HasColumnName(@"MISFIRE_INSTR").HasColumnType("int").IsOptional();
            Property(x => x.JobData).HasColumnName(@"JOB_DATA").HasColumnType("image").IsOptional().HasMaxLength(2147483647);

            // Foreign keys
            HasRequired(a => a.QrtzJobDetail).WithMany(b => b.QrtzTriggers).HasForeignKey(c => new { c.SchedName, c.JobName, c.JobGroup }).WillCascadeOnDelete(false); // FK_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS
        }
    }
}