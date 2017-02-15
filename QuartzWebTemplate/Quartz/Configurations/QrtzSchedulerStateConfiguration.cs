using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_SCHEDULER_STATE
    public class QrtzSchedulerStateConfiguration : EntityTypeConfiguration<QrtzSchedulerState>
    {
        public QrtzSchedulerStateConfiguration()
            : this("dbo")
        {
        }

        public QrtzSchedulerStateConfiguration(string schema)
        {
            ToTable("QRTZ_SCHEDULER_STATE", schema);
            HasKey(x => new { x.SchedName, x.InstanceName });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.InstanceName).HasColumnName(@"INSTANCE_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LastCheckinTime).HasColumnName(@"LAST_CHECKIN_TIME").HasColumnType("bigint").IsRequired();
            Property(x => x.CheckinInterval).HasColumnName(@"CHECKIN_INTERVAL").HasColumnType("bigint").IsRequired();
        }
    }
}