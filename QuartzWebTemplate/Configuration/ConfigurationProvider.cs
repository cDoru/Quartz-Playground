using System.Configuration;

namespace QuartzWebTemplate.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string Get(ConfigurationKeys key)
        {
            return ConfigurationManager.AppSettings[key.Key];
        }
    }
}