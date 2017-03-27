using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using Common.Logging.Simple;
using QuartzWebTemplate.App_Start;
using QuartzWebTemplate.KeepAlive;
using QuartzWebTemplate.Quartz.Scheduler;

namespace QuartzWebTemplate
{
    public class WebApiApplication : HttpApplication
    {
        private Scheduler _keepAliveScheduler;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfiguration.ConfigureContainer();
            SchedulerUtils.ConfigureScheduler();
            LogManager.Adapter = new TraceLoggerFactoryAdapter
            {
                Level = LogLevel.Info
            };

            // start the keep-alive scheduler 
            _keepAliveScheduler = new Scheduler();
            _keepAliveScheduler.Start(KeepAliveConstants.CheckFrequency);
        }

        protected void Application_BeginRequest()
        {
            if (BasePathHolder.NeedsBasePath)
            {
                var path = KeepAliveUtils.FullyQualifiedApplicationPath;
                BasePathHolder.BasePath = path;
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (_keepAliveScheduler != null)
            {
                _keepAliveScheduler.Dispose();
                _keepAliveScheduler = null;
            }

            new Scheduler().PingServer();
        }
    }
}
