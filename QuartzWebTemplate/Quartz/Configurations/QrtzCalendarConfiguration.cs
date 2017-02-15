using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using QuartzWebTemplate.Quartz.Entities;

namespace QuartzWebTemplate.Quartz.Configurations
{
    // QRTZ_CALENDARS

    public class QrtzCalendarConfiguration : EntityTypeConfiguration<QrtzCalendar>
    {
        public QrtzCalendarConfiguration()
            : this("dbo")
        {
        }

        public QrtzCalendarConfiguration(string schema)
        {
            ToTable("QRTZ_CALENDARS", schema);
            HasKey(x => new { x.SchedName, x.CalendarName });

            Property(x => x.SchedName).HasColumnName(@"SCHED_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CalendarName).HasColumnName(@"CALENDAR_NAME").HasColumnType("nvarchar").IsRequired().HasMaxLength(200).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Calendar).HasColumnName(@"CALENDAR").HasColumnType("image").IsRequired().HasMaxLength(2147483647);
        }
    }
}