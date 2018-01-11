namespace Acme.Model.ContentTypes
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.Security;

    /// <summary>
    ///     Tab names and page type groups used in this site.
    ///     Localize using EPiServer.xml
    /// </summary>
    public static class GroupNames
    {
        /// <summary>
        ///     Defines the group names for content types used in this solution.
        ///     Note that these are localized through the lang file NetR.EPi.xx.xml
        /// </summary>
        [GroupDefinitions]
        public static class Content
        {
            [Display(Order = 10)]
            public const string GeneralContent = "General";

            [Display(Order = 20)]
            public const string SpecialContent = "Special";

            [Display(Order = 30)]
            public const string LocalBlocks = "Local";

            [Display(Order = 40)]
            public const string SettingsBlocks = "Settings";
        }

        /// <summary>
        ///     Defines the names of the tab group names that are available througout the solution.
        ///     Note that these are localized through the lang file EPiServer.xml
        /// </summary>
        [GroupDefinitions]
        public static class Tabs
        {
            [Display(Order = 10)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Content = SystemTabNames.Content;

            [Display(Order = 20)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string TeaserInformation = "TeaserInformation";

            [Display(Order = 30)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string SEO = "SEO";

            [Display(Order = 40)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Settings = SystemTabNames.Settings;

            [Display(Order = 50)]
            [RequiredAccess(AccessLevel.Edit)]
            public const string Form = "Form";

            [Display(Order = 60)]
            [RequiredAccess(AccessLevel.Administer)]
            public const string Header = "Header";

            [Display(Order = 70)]
            [RequiredAccess(AccessLevel.Administer)]
            public const string Footer = "Footer";

            [Display(Order = 80)]
            [RequiredAccess(AccessLevel.Administer)]
            public const string SiteSettings = "SiteSettings";

            // This is not a tab, but rather the settings panel that shows
            // up when you scroll up at the top of a page in edit mode.
            [Display]
            public const string PageHeader = SystemTabNames.PageHeader;
        }

        /// <summary>
        ///     Defines the names of tab groups intended to use in the site settings block.
        /// </summary>
        public static class SettingsTabs
        {
            [Display(Order = 10)]
            public const string General = "GeneralSettings";
        }
    }
}
