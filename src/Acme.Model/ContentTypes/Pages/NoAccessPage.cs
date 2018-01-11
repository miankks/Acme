namespace Acme.Model.ContentTypes.Pages
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using NetR.EPi.DataAnnotations;

    [ContentType(
        GUID = "b1ece21a-584a-4f01-89a6-eb3aa484e841",
        GroupName = GroupNames.Content.SpecialContent)]
    [AvailableContentTypes(
        Availability.None,
        IncludeOn = new[]
        {
            typeof(HomePage)
        })]
    [ContentIcon(ContentIcons.Stop)]
    public class NoAccessPage : ContentPageBase
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            this.VisibleInMenu = false;
            this.ExcludePageFromSearch = true;
        }
    }
}
