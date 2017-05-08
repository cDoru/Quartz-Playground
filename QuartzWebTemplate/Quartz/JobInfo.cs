namespace QuartzWebTemplate.Quartz
{
    public class JobInfo
    {
        public string JobName { get; set; }
        public string JobGroup { get; set; }

        public bool HasActiveSchedule { get; set; }
    }
}