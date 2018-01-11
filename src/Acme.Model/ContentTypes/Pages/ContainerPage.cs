namespace Acme.Model.ContentTypes.Pages
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using NetR.EPi.ContentTypes;

    [ContentType(
        GUID = "df10fa46-5a2b-4191-8285-fe5eab61b90d",
        GroupName = GroupNames.Content.GeneralContent)]
    [AvailableContentTypes(
        Availability.All,
        IncludeOn = new[]
        {
            typeof(SitePageBase)
        })]
    public class ContainerPage : SitePageBase, IContainerPage
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            this.VisibleInMenu = false;
        }
    }
}
