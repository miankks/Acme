namespace Acme.Web.Controllers.Pages
{
    using System.Web.Mvc;
    using Acme.Domain.ViewModelBuilders;
    using Acme.Model.ContentTypes.Pages;
    using Acme.Model.ViewModels;

    public class HomePageController : PageControllerBase<HomePage>
    {
        private readonly MetaPageBaseViewModelBuilder<PageViewModel<HomePage>, HomePage> viewModelBuilder;

        public HomePageController(MetaPageBaseViewModelBuilder<PageViewModel<HomePage>, HomePage> viewModelBuilder)
        {
            this.viewModelBuilder = viewModelBuilder;
        }

        public ActionResult Index(HomePage currentPage)
        {
            var vm = this.viewModelBuilder
                .WithCurrentMetaPageBase(currentPage)
                .WithNavigation()
                .WithBreadcrumbs()
                .WithMeta()
                .Build();

            return this.View(vm);
        }
    }
}
