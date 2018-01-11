namespace Acme.Model.ViewModels
{
    using Acme.Model.ContentTypes;
    using EPiServer.Core;

    public class PreviewModel<TPageType> : PageViewModel<TPageType>
        where TPageType : SitePageBase
    {
        public ContentArea ContentArea { get; set; }

        public IContent PreviewContent { get; set; }
    }
}
