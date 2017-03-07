using System.Configuration;

namespace QuartzWebTemplate.Quartz.Config
{
    public class QuartzConfiguration : IQuartzConfiguration
    {
        private QuartzConfigurationResult _result;
        private readonly object _lockObject = new object();
        private bool _initialized;

        public QuartzConfigurationResult GetConfiguration()
        {
            if (!_initialized)
            {
                lock (_lockObject)
                {
                    if (!_initialized)
                    {
                        var enabledSetting = ConfigurationManager.AppSettings["QuartzAuthentication.Enabled"];

                        bool enabled;
                        bool.TryParse(enabledSetting, out enabled);

                        var requiredSetting = ConfigurationManager.AppSettings["QuartzAuthentication.Required"];
                        bool required;
                        bool.TryParse(requiredSetting, out required);

                        _result = new QuartzConfigurationResult
                        {
                            QuartzAuthenticationEnabled = enabled,
                            QuartzAuthenticationRequired = required,
                            QuartzAuthenticationUsername =
                                ConfigurationManager.AppSettings["QuartzAuthentication.Username"],
                            QuartzAuthenticationPassword =
                                ConfigurationManager.AppSettings["QuartzAuthentication.Password"]
                        };

                        _initialized = true;
                    }
                }
            }

            return _result;
        }
    }
}