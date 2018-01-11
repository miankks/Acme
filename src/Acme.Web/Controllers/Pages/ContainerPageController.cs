namespace Acme.Web.Controllers.Pages
{
    using System.Web.Mvc;
    using Acme.Model.ContentTypes;
    using EPiServer.Editor;
    using NetR.EPi.Extensions;

    public class ContainerPageController : PageControllerBase<SitePageBase>
    {
        public ActionResult Index(SitePageBase currentPage)
        {
            if (PageEditing.PageIsInEditMode == false)
            {
                var parentUrl = currentPage.GetParent().ExternalUrl(false);

                return this.Redirect(parentUrl);
            }

            return this.View("/Views/ContainerPage/Index.cshtml", currentPage);
        }
    }
}
