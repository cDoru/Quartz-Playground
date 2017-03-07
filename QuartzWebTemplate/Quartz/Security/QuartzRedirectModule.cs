using System;
using System.Web;
using QuartzWebTemplate.Quartz.Config;

namespace QuartzWebTemplate.Quartz.Security
{
    /// <summary>
    /// Very simple module for quartz to redirect if quartz is not on
    /// </summary>
    public class QuartzRedirectModule : IHttpModule
    {
        private readonly Func<IQuartzConfiguration> _configuration;

        public QuartzRedirectModule(Func<IQuartzConfiguration> configuration)
        {
            _configuration = configuration;
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        private void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            var request = context.Request;

            if (IsQuartz(request))
            {
                // check quartz is enabled

                var enabled = _configuration().GetConfiguration().QuartzAuthenticationEnabled;

                if (enabled) return;
                
                context.Response.Redirect("/", true);
                return;
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