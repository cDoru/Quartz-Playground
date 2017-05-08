namespace QuartzWebTemplate.Jobs
{
    public class JobKeys
    {
        // Autofac keys
        public const string ConcurrentJobAutofacKey = "concurrent-job";
        public const string SpinningJobAutofacKey = "spinner";



        // Job groups

        public const string SpinnerGroup = "job-spin";

        // job map data items 

        public const string JobDataName = "jobdata-name";
        public const string JobDataColor = "jobdata-color";
    }
}