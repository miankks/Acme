namespace Acme.Model.ContentTypes.Settings
{
    using System.ComponentModel.DataAnnotations;
    using ContentTypes;
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using NetR.EPi.ContentTypes;
    using NetR.EPi.SettingsFramework;
    using Pages;

    [EPiSettings(
        true,
        GroupName = GroupNames.Content.SettingsBlocks,
        GUID = "256ac770-64d6-4f99-b8e9-8619e72d1a62")]
    public class SiteSettingsBlock : BlockData, IFormEditDefault
    {
        [AllowedTypes(typeof(NotFoundPage))]
        [Display(GroupName = GroupNames.SettingsTabs.General, Order = 10)]
        public virtual PageReference NotFoundPage { get; set; }

        [AllowedTypes(typeof(NoAccessPage))]
        [Display(GroupName = GroupNames.SettingsTabs.General, Order = 20)]
        public virtual PageReference NoAccessPage { get; set; }

        [Display(GroupName = GroupNames.SettingsTabs.General, Order = 30)]
        public virtual string GoogleTagManagerId { get; set; }
    }
}
