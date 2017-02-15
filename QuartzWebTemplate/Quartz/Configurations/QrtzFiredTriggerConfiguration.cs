using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_FIRED_TRIGGERS

    public class QrtzFiredTriggerConfiguration : EntityTypeConfiguration<QrtzFiredTrigger>
    {
        public QrtzFiredTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzFiredTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_FIRED_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.EntryId });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.EntryId).HasColumnName(@"ENTRY_ID").HasColumnType("nvarchar").IsRequired().HasMaxLength(95).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150);
            Property(x => x.InstanceName).HasColumnName(@"INSTANCE_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(200);
            Property(x => x.FiredTime).HasColumnName(@"FIRED_TIME").HasColumnType("bigint").IsRequired();
            Property(x => x.SchedTime).HasColumnName(@"SCHED_TIME").HasColumnType("bigint").IsRequired();
            Property(x => x.Priority).HasColumnName(@"PRIORITY").HasColumnType("int").IsRequired();
            Property(x => x.State).HasColumnName(@"STATE").HasColumnType("nvarchar").IsRequired().HasMaxLength(16);
            Property(x => x.JobName).HasColumnName(@"JOB_NAME").HasColumnType("nvarchar").IsOptional().HasMaxLength(150);
            Property(x => x.JobGroup).HasColumnName(@"JOB_GROUP").HasColumnType("nvarchar").IsOptional().HasMaxLength(150);
            Property(x => x.IsNonconcurrent).HasColumnName(@"IS_NONCONCURRENT").HasColumnType("bit").IsOptional();
            Property(x => x.RequestsRecovery).HasColumnName(@"REQUESTS_RECOVERY").HasColumnType("bit").IsOptional();
        }
    }
}