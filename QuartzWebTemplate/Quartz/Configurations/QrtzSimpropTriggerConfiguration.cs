using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_SIMPROP_TRIGGERS
    public class QrtzSimpropTriggerConfiguration : EntityTypeConfiguration<QrtzSimpropTrigger>
    {
        public QrtzSimpropTriggerConfiguration()
            : this("dbo")
        {
        }

        public QrtzSimpropTriggerConfiguration(string schema)
        {
            ToTable("QRTZ_SIMPROP_TRIGGERS", schema);
            HasKey(x => new { x.SchedName, x.TriggerName, x.TriggerGroup });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerName).HasColumnName(@"TRIGGER_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TriggerGroup).HasColumnName(@"TRIGGER_GROUP").HasColumnType("nvarchar").IsRequired().HasMaxLength(150).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.StrProp1).HasColumnName(@"STR_PROP_1").HasColumnType("nvarchar").IsOptional().HasMaxLength(512);
            Property(x => x.StrProp2).HasColumnName(@"STR_PROP_2").HasColumnType("nvarchar").IsOptional().HasMaxLength(512);
            Property(x => x.StrProp3).HasColumnName(@"STR_PROP_3").HasColumnType("nvarchar").IsOptional().HasMaxLength(512);
            Property(x => x.IntProp1).HasColumnName(@"INT_PROP_1").HasColumnType("int").IsOptional();
            Property(x => x.IntProp2).HasColumnName(@"INT_PROP_2").HasColumnType("int").IsOptional();
            Property(x => x.LongProp1).HasColumnName(@"LONG_PROP_1").HasColumnType("bigint").IsOptional();
            Property(x => x.LongProp2).HasColumnName(@"LONG_PROP_2").HasColumnType("bigint").IsOptional();
            Property(x => x.DecProp1).HasColumnName(@"DEC_PROP_1").HasColumnType("numeric").IsOptional().HasPrecision(13, 4);
            Property(x => x.DecProp2).HasColumnName(@"DEC_PROP_2").HasColumnType("numeric").IsOptional().HasPrecision(13, 4);
            Property(x => x.BoolProp1).HasColumnName(@"BOOL_PROP_1").HasColumnType("bit").IsOptional();
            Property(x => x.BoolProp2).HasColumnName(@"BOOL_PROP_2").HasColumnType("bit").IsOptional();

            // Foreign keys
            HasRequired(a => a.QrtzTrigger).WithOptional(b => b.QrtzSimpropTrigger); // FK_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS
        }
    }
}