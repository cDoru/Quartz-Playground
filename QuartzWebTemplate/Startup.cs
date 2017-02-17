using Microsoft.Owin;
using Owin;
using QuartzWebTemplate;

[assembly: OwinStartup(typeof(Startup))]

namespace QuartzWebTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }

}
