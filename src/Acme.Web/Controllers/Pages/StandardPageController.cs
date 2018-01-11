namespace Acme.Web.Controllers.Pages
{
    using System.Web.Mvc;
    using Acme.Domain.ViewModelBuilders;
    using Acme.Model.ContentTypes.Pages;
    using Acme.Model.ViewModels;

    public class StandardPageController : PageControllerBase<StandardPage>
    {
        private readonly MetaPageBaseViewModelBuilder<PageViewModel<StandardPage>, StandardPage> viewModelBuilder;

        public StandardPageController(MetaPageBaseViewModelBuilder<PageViewModel<StandardPage>, StandardPage> viewModelBuilder)
        {
            this.viewModelBuilder = viewModelBuilder;
        }

        public ActionResult Index(StandardPage currentPage)
        {
            var vm = this.viewModelBuilder
                .WithCurrentMetaPageBase(currentPage)
                .WithNavigation()
                .WithSubNavigation()
                .WithBreadcrumbs()
                .WithMeta()
                .Build();

            return this.View(vm);
        }
    }
}
