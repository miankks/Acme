namespace Acme.Model.ContentTypes.Pages
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.SpecializedProperties;
    using NetR.EPi.SettingsFramework;

    [ContentType(
        GUID = "49f5cb3c-c25b-4cb0-a25c-162c502e5ed6",
        GroupName = GroupNames.Content.SpecialContent)]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[]
        {
            typeof(StandardPage),
            typeof(SettingsFolder)
        })]
    public class HomePage : ContentPageBase
    {
        [CultureSpecific]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1130)]
        public virtual ContentArea Teasers { get; set; }

        [CultureSpecific]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1130)]
        public virtual ContentArea ReferenceTeasers { get; set; }

        [CultureSpecific]
        [Display(GroupName = GroupNames.Tabs.Header, Order = 5010)]
        public virtual LinkItemCollection SupplementalNavigation { get; set; }

        [CultureSpecific]
        [Display(GroupName = GroupNames.Tabs.Footer, Order = 6010)]
        public virtual LinkItemCollection HelpLinks { get; set; }

        public override void SetDefaultValues(ContentType contentType)
        {
            base.SetDefaultValues(contentType);

            this.ExcludePageFromSearch = true;
        }
    }
}
