
namespace Acme.Web.Controllers.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Acme.Domain.ViewModelBuilders;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Framework.DataAnnotations;
    using EPiServer.Web.Mvc;
    using Acme.Model.ContentTypes.Pages;
    using Acme.Model.ViewModels;

    public class OurPartnersPageController : PageControllerBase<OurPartnersPage>
    {
        private readonly MetaPageBaseViewModelBuilder<PageViewModel<OurPartnersPage>, OurPartnersPage> viewModelBuilder;

        public OurPartnersPageController(MetaPageBaseViewModelBuilder<PageViewModel<OurPartnersPage>, OurPartnersPage> viewModelBuilder)
        {
            this.viewModelBuilder = viewModelBuilder;
        }

        public ActionResult Index(OurPartnersPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            var vm = this.viewModelBuilder
                .WithCurrentMetaPageBase(currentPage)
                .WithNavigation()
                .WithBreadcrumbs()
                .WithMeta().Build();
            return View(vm);
        }
    }
}
