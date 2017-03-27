namespace QuartzWebTemplate.Configuration
{
    public interface IConfigurationProvider
    {
        string Get(ConfigurationKeys key);
    }
}