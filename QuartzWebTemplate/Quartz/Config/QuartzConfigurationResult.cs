namespace QuartzWebTemplate.Quartz.Config
{
    public class QuartzConfigurationResult
    {
        public bool QuartzAuthenticationEnabled { get; set; }
        public string QuartzAuthenticationUsername { get; set; }

        public string QuartzAuthenticationPassword { get; set; }

        public bool QuartzAuthenticationRequired { get; set; }
    }
}