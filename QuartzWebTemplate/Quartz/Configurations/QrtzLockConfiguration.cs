using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_LOCKS
    public class QrtzLockConfiguration : EntityTypeConfiguration<QrtzLock>
    {
        public QrtzLockConfiguration()
            : this("dbo")
        {
        }

        public QrtzLockConfiguration(string schema)
        {
            ToTable("QRTZ_LOCKS", schema);
            HasKey(x => new { x.SchedName, x.LockName });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LockName).HasColumnName(@"LOCK_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(40).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}