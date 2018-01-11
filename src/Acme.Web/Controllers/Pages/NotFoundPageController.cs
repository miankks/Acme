namespace Acme.Web.Controllers.Pages
{
    using System.Net;
    using System.Web.Mvc;
    using Acme.Domain.ViewModelBuilders;
    using Acme.Model.ContentTypes.Pages;
    using Acme.Model.ContentTypes.Settings;
    using Acme.Model.ViewModels;
    using NetR.EPi.Extensions;
    using NetR.EPi.SettingsFramework;

    public class NotFoundPageController : PageControllerBase<NotFoundPage>
    {
        private readonly MetaPageBaseViewModelBuilder<PageViewModel<NotFoundPage>, NotFoundPage> viewModelBuilder;
        private readonly ISettingsService settingsService;

        public NotFoundPageController(
            MetaPageBaseViewModelBuilder<PageViewModel<NotFoundPage>, NotFoundPage> viewModelBuilder,
            ISettingsService settingsService)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.settingsService = settingsService;
        }

        public ActionResult Index(NotFoundPage currentPage)
        {
            return this.View(this.GetViewModel(currentPage));
        }

        public ActionResult GetModelFromSettings()
        {
            var siteSettings = this.settingsService.Get<SiteSettingsBlock>();

            NotFoundPage page;

            if (siteSettings.NotFoundPage.TryGetContent(out page) == false)
            {
                return this.HttpNotFound();
            }

            this.Response.StatusCode = (int)HttpStatusCode.NotFound;
            this.Response.TrySkipIisCustomErrors = true;

            return this.View("Index", this.GetViewModel(page));
        }

        private PageViewModel<NotFoundPage> GetViewModel(NotFoundPage currentPage)
        {
            var vm = this.viewModelBuilder
                .WithCurrentMetaPageBase(currentPage)
                .WithNavigation()
                .WithSubNavigation()
                .WithBreadcrumbs()
                .WithMeta()
                .Build();

            return vm;
        }
    }
}
