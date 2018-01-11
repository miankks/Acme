namespace Acme.Model.ViewModels
{
    using Acme.Model.ContentTypes;

    public class PageViewModel<TPageType> : LayoutViewModel
        where TPageType : SitePageBase
    {
        public TPageType CurrentPage { get; set; }
    }
}
