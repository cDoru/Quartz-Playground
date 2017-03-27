using System.Collections.Specialized;

namespace QuartzWebTemplate.Quartz.Scheduler
{
    public interface IQuartzConfigurationProvider
    {
        NameValueCollection GetConfiguration();
    }
}