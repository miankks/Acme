namespace Acme.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Acme.Domain.ViewModelBuilders;
    using Acme.Model.ContentTypes;
    using Acme.Model.ContentTypes.Pages;
    using Acme.Model.ViewModels;
    using EPiServer.Core;
    using EPiServer.Framework.DataAnnotations;
    using EPiServer.Framework.Web;
    using EPiServer.Web;
    using EPiServer.Web.Mvc;
    using NetR.EPi.Extensions;

    [TemplateDescriptor(
        Inherited = true,
        TemplateTypeCategory = TemplateTypeCategories.MvcController,
        TagString = RenderingTags.Preview,
        AvailableWithoutTag = false)]
    public class PreviewController : ActionControllerBase, IRenderTemplate<SiteBlockBase>
    {
        private readonly MetaPageBaseViewModelBuilder<PreviewModel<HomePage>, HomePage> viewModelBuilder;

        public PreviewController(MetaPageBaseViewModelBuilder<PreviewModel<HomePage>, HomePage> viewModelBuilder)
        {
            this.viewModelBuilder = viewModelBuilder;
        }

        public ActionResult Index(IContent currentContent)
        {
            HomePage startPage;

            if (SiteDefinition.Current.StartPage.TryGetContent(out startPage) == false)
            {
                throw new Exception("StartPage needs to be set for site");
            }

            var vm = this.viewModelBuilder
                .WithCurrentMetaPageBase(startPage)
                .WithMeta()
                .Build();

            var contentArea = new ContentArea();

            contentArea.Items.Add(new ContentAreaItem
            {
                ContentLink = currentContent.ContentLink
            });

            vm.ContentArea = contentArea;
            vm.PreviewContent = currentContent;

            return this.View(vm);
        }
    }
}
