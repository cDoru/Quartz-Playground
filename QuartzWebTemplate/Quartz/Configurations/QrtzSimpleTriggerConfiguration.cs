using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_SIMPLE_TRIGGERS
    public class QrtzSimpleTriggerConfiguration : EntityTypeConfiguration<QrtzSimpleTrigger>
    {
        public QrtzSimpleTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzSimpleTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_SIMPLE_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.TriggerName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RepeatCount).HasColumnName(@"REPEAT_COUNT").HasColumnType("int").IsRequired();
            Property(x => x.RepeatInterval).HasColumnName(@"REPEAT_INTERVAL").HasColumnType("bigint").IsRequired();
            Property(x => x.TimesTriggered).HasColumnName(@"TIMES_TRIGGERED").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.QrtzTrigger).WithOptional(b => b.QrtzSimpleTrigger); // FK_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS
        }
    }
}