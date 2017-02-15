namespace QuartzWebTemplate.Quartz.Entities
{
    // QRTZ_CALENDARS

    public class QrtzCalendar
    {
        public string SchedName { get; set; } // SCHED_NAME (Primary key) (length: 100)
        public string CalendarName { get; set; } // CALENDAR_NAME (Primary key) (length: 200)
        public byte[] Calendar { get; set; } // CALENDAR (length: 2147483647)
    }
}