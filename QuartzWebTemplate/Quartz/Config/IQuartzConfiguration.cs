using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QuartzWebTemplate.Quartz.Config
{
    public interface IQuartzConfiguration
    {
        QuartzConfigurationResult GetConfiguration();
    }

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

    public class QuartzConfigurationResult
    {
        public bool QuartzAuthenticationEnabled { get; set; }
        public string QuartzAuthenticationUsername { get; set; }

        public string QuartzAuthenticationPassword { get; set; }

        public bool QuartzAuthenticationRequired { get; set; }
    }
}