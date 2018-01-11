namespace Acme.Model.ContentTypes.Pages
{
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    [ContentType(
        GUID = "D7563725-B619-4EC7-92B6-3B93D82B3179",
        GroupName = GroupNames.Content.GeneralContent)]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[]
        {
            typeof(StandardPage)
        })]
    public class StandardPage : ContentPageBase
    {
    }
}
