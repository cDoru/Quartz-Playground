
namespace QuartzWebTemplate.Configuration
{
    public class ConfigurationKeys
    {
        private const string InstanceNameKey = "InstanceName";
        private const string QuartzConnectionKey = "QuartzConnection";

        public string Key { get; private set; }

        private ConfigurationKeys(string key)
        {
            Key = key;
        }

        public static ConfigurationKeys InstanceName
        {
            get { return new ConfigurationKeys(InstanceNameKey);}
        }

        public static ConfigurationKeys QuartzSqlConnection
        {
            get { return new ConfigurationKeys(QuartzConnectionKey);}
        }
    }
}