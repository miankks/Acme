namespace Acme.Model.ContentTypes.Pages
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using NetR.EPi.DataAnnotations;

    [ContentType(
        GUID = "7ae4e3c3-d873-4e0b-a4ca-be7435bfd124",
        GroupName = GroupNames.Content.SpecialContent)]
    [AvailableContentTypes(
        Availability.None,
        IncludeOn = new[]
        {
            typeof(HomePage)
        })]
    [ContentIcon(ContentIcons.Stop)]
    public class NotFoundPage : ContentPageBase
    {
        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            this.VisibleInMenu = false;
            this.ExcludePageFromSearch = true;
        }
    }
}
