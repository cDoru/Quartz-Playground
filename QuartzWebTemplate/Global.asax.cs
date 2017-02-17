using System.Security.AccessControl;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QuartzWebTemplate.App_Start;
using QuartzWebTemplate.Quartz.Scheduler;

namespace QuartzWebTemplate
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfiguration.ConfigureContainer();
            SchedulerUtils.ConfigureScheduler();
            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.TraceLoggerFactoryAdapter
            {
                Level = Common.Logging.LogLevel.Info
            };
        }
    }
}
