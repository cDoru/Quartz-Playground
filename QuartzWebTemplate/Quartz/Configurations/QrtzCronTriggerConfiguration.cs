using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_CRON_TRIGGERS

    public class QrtzCronTriggerConfiguration : EntityTypeConfiguration<QrtzCronTrigger>
    {
        public QrtzCronTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzCronTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_CRON_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.TriggerName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CronExpression).HasColumnName(@"CRON_EXPRESSION").HasColumnType("nvarchar").IsRequired().HasMaxLength(120);
            Property(x => x.TimeZoneId).HasColumnName(@"TIME_ZONE_ID").HasColumnType("nvarchar").IsOptional().HasMaxLength(80);

            // Foreign keys
            HasRequired(a => a.QrtzTrigger).WithOptional(b => b.QrtzCronTrigger); // FK_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS
        }
    }
}