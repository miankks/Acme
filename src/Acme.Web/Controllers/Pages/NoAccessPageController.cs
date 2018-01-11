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

    public class NoAccessPageController : PageControllerBase<NoAccessPage>
    {
        private readonly MetaPageBaseViewModelBuilder<PageViewModel<NoAccessPage>, NoAccessPage> viewModelBuilder;
        private readonly ISettingsService settingsService;

        public NoAccessPageController(
            MetaPageBaseViewModelBuilder<PageViewModel<NoAccessPage>, NoAccessPage> viewModelBuilder,
            ISettingsService settingsService)
        {
            this.viewModelBuilder = viewModelBuilder;
            this.settingsService = settingsService;
        }

        public ActionResult Index(NoAccessPage currentPage)
        {
            return this.View(this.GetViewModel(currentPage));
        }

        public ActionResult GetModelFromSettings()
        {
            var siteSettings = this.settingsService.Get<SiteSettingsBlock>();

            NoAccessPage page;

            if (siteSettings.NoAccessPage.TryGetContent(out page) == false)
            {
                return this.HttpNotFound();
            }

            this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            this.Response.TrySkipIisCustomErrors = true;

            return this.View("Index", this.GetViewModel(page));
        }

        private PageViewModel<NoAccessPage> GetViewModel(NoAccessPage currentPage)
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
