using System;
using System.Configuration;
using System.Web;

namespace QuartzWebTemplate.Quartz.Security
{
    /// <summary>
    /// Very simple module for quartz to redirect if quartz is not on
    /// </summary>
    public class QuartzRedirectModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        static void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            var request = context.Request;

            if (IsQuartz(request))
            {
                // check quartz is enabled

                var enabledSetting = ConfigurationManager.AppSettings["QuartzAuthentication.Enabled"];

                bool enabled;
                if (!bool.TryParse(enabledSetting, out enabled))
                {
                    context.Response.Redirect("/", true);
                    return;
                };

                if (!enabled)
                {
                    context.Response.Redirect("/", true);
                    return;
                }
            }
        }

        private static bool IsQuartz(HttpRequest request)
        {
            return request.RawUrl.Contains("CrystalQuartzPanel.axd");
        }

        public void Dispose()
        {
        }
    }
}