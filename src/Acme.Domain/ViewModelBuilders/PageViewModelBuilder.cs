namespace Acme.Domain.ViewModelBuilders
{
    using System;
    using Acme.Model.ContentTypes;
    using Acme.Model.ViewModels;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.Web;
    using EPiServer.Web.Routing;
    using NetR.EPi.Extensions;
    using NetR.EPi.SettingsFramework;
    using NetR.EPi.Utils.ImageFiltering;

    public class PageViewModelBuilder<TViewModel, TPageType> : LayoutViewModelBuilder<TViewModel>
       where TViewModel : PageViewModel<TPageType>, new()
       where TPageType : SitePageBase
    {
        private readonly ISettingsService settingsService;
        private readonly IContentRepository contentRepository;
        private readonly IFilteredImageRepository filteredImageRepository;
        private readonly UrlResolver urlResolver;

        public PageViewModelBuilder(
            ISettingsService settingsService,
            IContentRepository contentRepository,
            IFilteredImageRepository filteredImageRepository,
            UrlResolver urlResolver)
           : base()
        {
            this.settingsService = settingsService;
            this.contentRepository = contentRepository;
            this.filteredImageRepository = filteredImageRepository;
            this.urlResolver = urlResolver;
        }

        protected TPageType CurrentPage { get; private set; }

        protected ISettingsService SettingsService { get => this.settingsService; }

        protected IContentRepository ContentRepository { get => this.contentRepository; }

        protected IFilteredImageRepository FilteredImageRepository { get => this.filteredImageRepository; }

        protected UrlResolver UrlResolver { get => this.urlResolver; }

        public PageViewModelBuilder<TViewModel, TPageType> WithCurrentPage(TPageType currentPage)
        {
            this.CurrentPage = currentPage;
            this.ViewModel.CurrentPage = currentPage;

            return this;
        }

        public virtual PageViewModelBuilder<TViewModel, TPageType> WithMeta()
        {
            this.ViewModel.Title = this.GetPageTitleWithSiteName(this.CurrentPage.PageLink, this.CurrentPage.Name);
            this.ViewModel.Language = this.CurrentPage.Language.ToString();

            this.WithCanonicalUrl();
            this.WithOpenGraph();

            return this;
        }

        protected virtual PageViewModelBuilder<TViewModel, TPageType> WithCanonicalUrl()
        {
            var shortcutProperty = this.CurrentPage.Property[MetaDataProperties.PageShortcutLink] as PropertyPageReference;

            if (shortcutProperty != null &&
                ContentReference.IsNullOrEmpty(shortcutProperty.ContentLink) == false)
            {
                var page = this.ContentRepository.Get<PageData>(shortcutProperty.ContentLink);

                this.ViewModel.CanonicalUrl = new Uri(page.ExternalUrl(true));
            }
            else
            {
                this.ViewModel.CanonicalUrl = new Uri(this.CurrentPage.ExternalUrl(true));
            }

            return this;
        }

        protected virtual PageViewModelBuilder<TViewModel, TPageType> WithOpenGraph()
        {
            this.ViewModel.OpenGraph.SiteName = SiteDefinition.Current.Name;
            this.ViewModel.OpenGraph.Title = this.CurrentPage.Name;
            this.ViewModel.OpenGraph.Type = "article";

            var uri = new Uri(this.CurrentPage.ExternalUrl(true));

            this.ViewModel.OpenGraph.Url = uri.GetComponents(
                    UriComponents.Scheme |
                    UriComponents.Host |
                    UriComponents.PathAndQuery,
                    UriFormat.UriEscaped);

            return this;
        }

        protected string GetPageTitleWithSiteName(ContentReference currentPageLink, string title)
        {
            if (ContentReference.StartPage.Equals(currentPageLink, true))
            {
                // This is the start page. Render site name first.
                return $"{SiteDefinition.Current.Name} — {title}";
            }
            else
            {
                // This is not the start page. Render site name last.
                return $"{title} — {SiteDefinition.Current.Name}";
            }
        }
    }
}
