namespace Acme.Domain.Initialization
{
    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Framework;
    using EPiServer.Framework.Initialization;
    using EPiServer.ServiceLocation;
    using NetR.EPi.Filters.Editor;

    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class EditorFilterInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();

            contentEvents.CreatingContent += this.CreatingContent;
            contentEvents.SavingContent += this.SavingContent;
        }

        public void Uninitialize(InitializationEngine context)
        {
            var contentEvents = ServiceLocator.Current.GetInstance<IContentEvents>();

            contentEvents.CreatingContent -= this.CreatingContent;
            contentEvents.SavingContent -= this.SavingContent;
        }

        private void CreatingContent(object sender, ContentEventArgs e)
        {
            EditorFilter.FilterContent(sender, e);
        }

        private void SavingContent(object sender, ContentEventArgs e)
        {
            EditorFilter.FilterContent(sender, e);
        }
    }
}
