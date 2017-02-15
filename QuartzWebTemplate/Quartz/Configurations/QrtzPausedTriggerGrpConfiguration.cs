using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_PAUSED_TRIGGER_GRPS

    public class QrtzPausedTriggerGrpConfiguration : EntityTypeConfiguration<QrtzPausedTriggerGrp>
    {
        public QrtzPausedTriggerGrpConfiguration()
            : this("dbo")
        {
        }

        public QrtzPausedTriggerGrpConfiguration(string schema)
        {
            ToTable("QRTZ_PAUSED_TRIGGER_GRPS", schema);
            HasKey(x => new { x.SchedName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}