
namespace QuartzWebTemplate.Configuration
{
    public class ConfigurationKeys
    {
        private const string InstanceNameKey = "InstanceName";
        private const string QuartzConnectionKey = "QuartzConnection";

        private const string EncryptKeyKey = "ENCRYPT_KEY";
        private const string EncryptVectorKey = "ENCRYPT_VECTOR";

        public string Key { get; private set; }

        private ConfigurationKeys(string key)
        {
            Key = key;
        }

        public static ConfigurationKeys InstanceName
        {
            get { return new ConfigurationKeys(InstanceNameKey);}
        }

        public static ConfigurationKeys EncryptKey
        {
            get { return new ConfigurationKeys(EncryptKeyKey); }
        }

        public static ConfigurationKeys EncryptVector
        {
            get { return new ConfigurationKeys(EncryptVectorKey); }
        }

        public static ConfigurationKeys QuartzSqlConnection
        {
            get { return new ConfigurationKeys(QuartzConnectionKey);}
        }
    }
}