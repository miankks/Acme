namespace Acme.Web.Controllers
{
    using System.Web.Mvc;

    public class RobotsTxtController : Controller
    {
        public ActionResult Index()
        {
            string content;

            // Disallow for all enviroments but production
            if (this.HttpContext.IsDebuggingEnabled)
            {
                content = "User-agent: *\r\nDisallow: /";
            }
            else
            {
                content = "User-agent: *\r\nAllow: /";
            }

            return this.Content(content, "text/plain");
        }
    }
}