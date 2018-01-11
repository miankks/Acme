namespace Acme.Domain.ViewModelBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Acme.Model.ContentTypes;
    using Acme.Model.ContentTypes.Settings;
    using Acme.Model.Navigation;
    using Acme.Model.ViewModels;
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Editor;
    using EPiServer.Web;
    using EPiServer.Web.Routing;
    using NetR.EPi.Extensions;
    using NetR.EPi.SettingsFramework;
    using NetR.EPi.Utils.ImageFiltering;

    // TODO: Fix navigation provider
    public class MetaPageBaseViewModelBuilder<TViewModel, TPageType> : PageViewModelBuilder<TViewModel, TPageType>
        where TViewModel : PageViewModel<TPageType>, new()
        where TPageType : MetaPageBase
    {
        public MetaPageBaseViewModelBuilder(
            ISettingsService settingsService,
            IContentRepository contentRepository,
            IFilteredImageRepository filteredImageRepository,
            UrlResolver urlResolver)
           : base(
                 settingsService,
                 contentRepository,
                 filteredImageRepository,
                 urlResolver)
        {
        }

        public MetaPageBaseViewModelBuilder<TViewModel, TPageType> WithCurrentMetaPageBase(TPageType currentPage)
        {
            this.WithCurrentPage(currentPage);

            return this;
        }

        public override PageViewModelBuilder<TViewModel, TPageType> WithMeta()
        {
            base.WithMeta();

            this.ViewModel.Title = this.GetPageTitleWithSiteName(this.CurrentPage.PageLink, this.CurrentPage.PageTitle ?? this.CurrentPage.PageName);
            this.ViewModel.Description = this.CurrentPage.Description;
            this.ViewModel.Keywords = this.CurrentPage.Keywords;

            if (this.CurrentPage.ExcludePageFromSearch)
            {
                this.ViewModel.RobotsText = "NOINDEX";
            }

            if (PageEditing.PageIsInEditMode == false)
            {
                var siteSettings = this.SettingsService.Get<SiteSettingsBlock>();

                this.ViewModel.GoogleTagManagerId = siteSettings.GoogleTagManagerId;
            }

            return this;
        }

        public virtual MetaPageBaseViewModelBuilder<TViewModel, TPageType> WithBreadcrumbs()
        {
            var pages = this.ContentRepository.GetAncestors(this.CurrentPage.ContentLink)
                .FilterForVisitor()
                .ToList();

            pages.Insert(0, this.CurrentPage);
            pages.Add(this.ContentRepository.Get<IContent>(ContentReference.StartPage));

            for (int i = pages.Count - 1; i-- > 0;)
            {
                // We need to retrieve the page, since GetAncestors() only return pages
                // on the master language.
                var page = this.ContentRepository.Get<IContent>(pages[i].ContentLink);

                var metaPage = page as MetaPageBase;

                if (metaPage != null &&
                    metaPage.ExcludePageFromPath)
                {
                    continue;
                }

                this.ViewModel.Breadcrumbs.Add(new NavigationItem
                {
                    IsSelected = page.ContentLink.Equals(this.CurrentPage.ContentLink, true),
                    IsStartPage = page.ContentLink.Equals(ContentReference.StartPage, true),
                    Reference = page,
                    Text = page.Name,
                    Url = page.ContentLink.GetUrl()
                });
            }

            return this;
        }

        public virtual MetaPageBaseViewModelBuilder<TViewModel, TPageType> WithNavigation()
        {
            var expandedPages = this.GetExpandedNavigationPages(this.CurrentPage.ContentLink);

            var pages = this.GetNavigationPages(ContentReference.StartPage)
                .ToList();

            pages.Insert(0, this.ContentRepository.Get<SitePageBase>(ContentReference.StartPage));

            foreach (var page in pages)
            {
                this.ViewModel.Navigation.Add(new NavigationItem
                {
                    IsExpanded = expandedPages.Any(p => p.ContentLink.Equals(page.ContentLink, true)),
                    IsSelected = page.ContentLink.Equals(this.CurrentPage.ContentLink, true),
                    IsStartPage = page.ContentLink.Equals(ContentReference.StartPage, true),
                    Reference = page,
                    Text = page.Name,
                    Url = page.ContentLink.GetUrl()
                });
            }

            return this;
        }

        public virtual MetaPageBaseViewModelBuilder<TViewModel, TPageType> WithSubNavigation()
        {
            var expandedPages = this.GetExpandedNavigationPages(this.CurrentPage.ContentLink);

            this.ViewModel.SubNavigation = this.GetSubNavigationItem(expandedPages.ElementAt(2), expandedPages);

            return this;
        }

        protected override PageViewModelBuilder<TViewModel, TPageType> WithOpenGraph()
        {
            base.WithOpenGraph();

            this.ViewModel.OpenGraph.Title = string.IsNullOrWhiteSpace(this.CurrentPage.PageTitle)
                ? this.CurrentPage.PageName
                : this.CurrentPage.PageTitle;

            if (string.IsNullOrEmpty(this.CurrentPage.Description) == false)
            {
                this.ViewModel.OpenGraph.Description = this.CurrentPage.Description?.Replace("\n", " ");
            }

            if (string.IsNullOrEmpty(this.CurrentPage.TeaserBody) == false)
            {
                this.ViewModel.OpenGraph.Description = this.CurrentPage.TeaserBody?.Replace("\n", " ");
            }

            if (ContentReference.IsNullOrEmpty(this.CurrentPage.TeaserImage) == false)
            {
                ImageData image;

                if (this.ContentRepository.TryGet(this.CurrentPage.TeaserImage, out image))
                {
                    var resizedImage = this.FilteredImageRepository.Resize(image, 1200, 0, 0, quality: 80);

                    var uriBuilder = new UriBuilder(SiteDefinition.Current.SiteUrl);
                    uriBuilder.Path = resizedImage.Url;

                    this.ViewModel.OpenGraph.Image = uriBuilder.Uri.AbsoluteUri;
                }
            }

            if (string.IsNullOrEmpty(this.ViewModel.OpenGraph.Image))
            {
                // TODO: Fall back on logo?
            }

            return this;
        }

        private HierarchicalNavigationItem GetSubNavigationItem(PageData page, IEnumerable<IContent> expandedPages, int level = 1)
        {
            var children = this.GetNavigationPages(page.ContentLink);

            var navigationItem = new HierarchicalNavigationItem
            {
                HasChildren = children.Any(),
                IsExpanded = expandedPages.Any(p => p.ContentLink.Equals(page.ContentLink, true)),
                IsSelected = page.ContentLink.Equals(this.CurrentPage.ContentLink, true),
                IsStartPage = page.ContentLink.Equals(ContentReference.StartPage, true),
                Level = level,
                Reference = page,
                Text = page.PageName,
                Url = page.ContentLink.GetUrl()
            };

            if (navigationItem.IsExpanded)
            {
                navigationItem.Items = children.Select(p =>
                    this.GetSubNavigationItem(p, expandedPages, navigationItem.Level + 1))
                    .ToList();
            }

            return navigationItem;
        }

        private IEnumerable<SitePageBase> GetNavigationPages(ContentReference contentReference)
        {
            return this.ContentRepository.GetChildren<SitePageBase>(contentReference)
                .FilterForVisitor()
                .Where(p => p.VisibleInMenu);
        }

        private IEnumerable<PageData> GetExpandedNavigationPages(ContentReference contentReference)
        {
            var expandedPages = this.ContentRepository.GetAncestors(contentReference)
                .Reverse()
                .Cast<PageData>()
                .ToList();

            expandedPages.Add(this.CurrentPage);

            return expandedPages;
        }
    }
}
