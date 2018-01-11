namespace Acme.Model.ContentTypes.Blocks
{
    using System.ComponentModel.DataAnnotations;
    using Acme.Model.PropertySettings;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    [ContentType(
        GUID = "08aeabee-0ec5-4b44-b21f-da20cf5b4e99",
        GroupName = GroupNames.Content.GeneralContent)]
    public class TeaserBlock : SiteBlockBase
    {
        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 10)]
        public virtual string Heading { get; set; }

        [UIHint(UIHint.Image)]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 20)]
        public virtual ContentReference Image { get; set; }

        [CultureSpecific]
        [Searchable]
        [PropertySettings(typeof(SimpleTinyMceSettings))]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 30)]
        public virtual XhtmlString Text { get; set; }

        [Display(GroupName = GroupNames.Tabs.Content, Order = 40)]
        public virtual TextLinkBlock Link { get; set; }
    }
}
