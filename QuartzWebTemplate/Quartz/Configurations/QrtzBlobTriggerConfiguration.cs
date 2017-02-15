using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_BLOB_TRIGGERS

    public class QrtzBlobTriggerConfiguration : EntityTypeConfiguration<QrtzBlobTrigger>
    {
        public QrtzBlobTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzBlobTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_BLOB_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.TriggerName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.BlobData).HasColumnName(@"BLOB_DATA").HasColumnType("image").IsOptional().HasMaxLength(2147483647);
        }
    }
}