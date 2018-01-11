namespace Acme.Web.Controllers.PagePartials
{
    using System.Web.Mvc;
    using Acme.Model.ContentTypes;
    using EPiServer.Framework.DataAnnotations;
    using EPiServer.Framework.Web;

    [TemplateDescriptor(
        TemplateTypeCategory = TemplateTypeCategories.MvcPartialController,
        Inherited = true)]
    public class MetaPageBaseController : PageControllerBase<MetaPageBase>
    {
        public ActionResult Index(MetaPageBase currentPage)
        {
            return this.PartialView("MetaPageBase", currentPage);
        }
    }
}
