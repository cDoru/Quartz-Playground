using System;
using System.Configuration;
using System.Text;
using System.Web;

namespace QuartzWebTemplate.Quartz.Security
{
    public class BasicAuthentication : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        private void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = ((HttpApplication)sender).Context;
            var request = context.Request;
            
            if (!IsQuartz(request))
            {
                return;
            }


            if (!IsRequired())
            {
                return;
            }

            if (ValidateCredentials())
            {
                return;
            }
                
            var httpApplication = (HttpApplication)sender;
            httpApplication.Context.Response.Clear();
            httpApplication.Context.Response.Status = "401 Unauthorized";
            httpApplication.Context.Response.StatusCode = 401;
            httpApplication.Context.Response.AddHeader("WWW-Authenticate", "Basic realm=\"" + Request.Url.Host + "\"");
            httpApplication.CompleteRequest();
        }

        private static bool IsQuartz(HttpRequest request)
        {
            return request.RawUrl.Contains("CrystalQuartzPanel.axd");
        }

        private static bool IsRequired()
        {
            var requiredSetting = ConfigurationManager.AppSettings["QuartzAuthentication.Required"];
            if (string.IsNullOrWhiteSpace(requiredSetting)) return false;
            requiredSetting = requiredSetting.Trim().ToLower();
            return requiredSetting == "1" || requiredSetting == "true";
        }

        private static bool ValidateCredentials()
        {
            var validUsername = ConfigurationManager.AppSettings["QuartzAuthentication.Username"];
            if (string.IsNullOrEmpty(validUsername))
                return false;

            var validPassword = ConfigurationManager.AppSettings["QuartzAuthentication.Password"];
            if (string.IsNullOrEmpty(validPassword))
                return false;

            var header = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(header))
                return false;

            header = header.Trim();
            if (header.IndexOf("Basic ", StringComparison.InvariantCultureIgnoreCase) != 0)
                return false;

            var credentials = header.Substring(6);

            // Decode the Base64 encoded credentials
            var credentialsBase64DecodedArray = Convert.FromBase64String(credentials);
            var decodedCredentials = Encoding.UTF8.GetString(credentialsBase64DecodedArray, 0, credentialsBase64DecodedArray.Length);

            // Get username and password
            var separatorPosition = decodedCredentials.IndexOf(':');

            if (separatorPosition <= 0)
                return false;

            var username = decodedCredentials.Substring(0, separatorPosition);
            var password = decodedCredentials.Substring(separatorPosition + 1);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            return String.Equals(username, validUsername, StringComparison.CurrentCultureIgnoreCase) && password == validPassword;
        }

        private static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        public void Dispose()
        {
        }
    }
}