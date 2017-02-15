using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_JOB_DETAILS
    public class QrtzJobDetailConfiguration : EntityTypeConfiguration<QrtzJobDetail>
    {
        public QrtzJobDetailConfiguration()
            : this("dbo")
        {
        }

        public QrtzJobDetailConfiguration(string schema)
        {
            ToTable("QRTZ_JOB_DETAILS", schema);
            HasKey(x => new { x.SchedName, x.JobName, x.JobGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JobName).HasColumnName(@"JOB_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.JobGroup).HasColumnName(@"JOB_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Description).HasColumnName(@"DESCRIPTION").HasColumnType("nvarchar").IsOptional().HasMaxLength(250);
            Property(x => x.JobClassName).HasColumnName(@"JOB_CLASS_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(250);
            Property(x => x.IsDurable).HasColumnName(@"IS_DURABLE").HasColumnType("bit").IsRequired();
            Property(x => x.IsNonconcurrent).HasColumnName(@"IS_NONCONCURRENT").HasColumnType("bit").IsRequired();
            Property(x => x.IsUpdateData).HasColumnName(@"IS_UPDATE_DATA").HasColumnType("bit").IsRequired();
            Property(x => x.RequestsRecovery).HasColumnName(@"REQUESTS_RECOVERY").HasColumnType("bit").IsRequired();
            Property(x => x.JobData).HasColumnName(@"JOB_DATA").HasColumnType("image").IsOptional().HasMaxLength(2147483647);
        }
    }
}