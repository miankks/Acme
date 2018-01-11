namespace Acme.Model.ContentTypes.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;

    [ContentType(
        GUID = "96b6131f-ef01-439f-8a08-6e2a75ebf401",
        GroupName = GroupNames.Content.LocalBlocks,
        AvailableInEditMode = false)]
    public class TextLinkBlock : SiteBlockBase
    {
        [CultureSpecific]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 10)]
        public virtual string LinkText { get; set; }

        [Display(GroupName = GroupNames.Tabs.Content, Order = 20)]
        public virtual Url LinkUrl { get; set; }
    }
}
