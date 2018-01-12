namespace Acme.Model.ContentTypes.Blocks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Acme.Model.PropertySettings;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    [ContentType(DisplayName = "ReferenceBlock", GUID = "663b4592-5904-48db-bcbc-a6e65ca88f45", Description = "")]
    public class ReferenceBlock : SiteBlockBase
    {
        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 10)]
        public virtual string CoachName { get; set; }

        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 10)]
        public virtual string CompanyName { get; set; }

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
