using System.Web.Http;

namespace QuartzWebTemplate.Controllers
{
    public class KeepAliveController : ApiController
    {
        // GET api/values
        public string Get()
        {
            return "alive";
        } 
    }
}