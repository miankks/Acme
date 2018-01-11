namespace Acme.Web.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Acme.Model.ContentTypes;
    using EPiServer.Web.Mvc;

    public abstract class PageControllerBase<TPageType> : PageController<TPageType>
        where TPageType : SitePageBase
    {
        #region Debug information

        [Authorize(Roles = "CmsAdmins")]
        public ActionResult ListAppSettings()
        {
            var sb = new StringBuilder();

            foreach (var key in ConfigurationManager.AppSettings.AllKeys.OrderBy(x => x))
            {
                sb.AppendLine($"{key}: {ConfigurationManager.AppSettings.Get(key)}");
            }

            return this.Content(sb.ToString(), "text/plain");
        }

        [Authorize(Roles = "CmsAdmins")]
        public ActionResult ListEnvironmentData()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Request.Url: {this.Request.Url}");

            sb.AppendLine($"Request.UserHostAddress: {this.Request.UserHostAddress}");

            sb.AppendLine($"Request.IsSecureConnection: {this.Request.IsSecureConnection}");

            foreach (var hdr in this.Request.Headers.AllKeys.Where(x => x != "Cookie").OrderBy(x => x))
            {
                sb.AppendLine($"Request.Headers[\"{hdr}\"]: {this.Request.Headers[hdr]}");
            }

            return this.Content(sb.ToString(), "text/plain");
        }

        #endregion
    }
}
