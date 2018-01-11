namespace Acme.Model.ContentTypes
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    /// <summary>
    /// Base class for pages that contain meta information. Should be used for most surfable pages of the site.
    /// </summary>
    public abstract class MetaPageBase : SitePageBase
    {
        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1010)]
        public virtual string PageTitle { get; set; }

        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1020)]
        public virtual string PageHeading
        {
            get
            {
                var heading = this.GetPropertyValue(p => p.PageHeading);

                if (string.IsNullOrWhiteSpace(heading))
                {
                    return this.Name;
                }

                return heading;
            }

            set
            {
                this.SetPropertyValue(p => p.PageHeading, value);
            }
        }

        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.TeaserInformation, Order = 2010)]
        public virtual string TeaserHeading
        {
            get
            {
                var heading = this.GetPropertyValue(p => p.TeaserHeading);

                if (string.IsNullOrWhiteSpace(heading))
                {
                    return this.PageHeading;
                }

                return heading;
            }

            set
            {
                this.SetPropertyValue(p => p.TeaserHeading, value);
            }
        }

        [UIHint(UIHint.Image)]
        [Display(GroupName = GroupNames.Tabs.TeaserInformation, Order = 2020)]
        public virtual ContentReference TeaserImage { get; set; }

        [CultureSpecific]
        [Searchable]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = GroupNames.Tabs.TeaserInformation, Order = 2030)]
        public virtual string TeaserBody { get; set; }

        [CultureSpecific]
        [Searchable]
        [Display(GroupName = GroupNames.Tabs.SEO, Order = 3010)]
        public virtual string Keywords { get; set; }

        [CultureSpecific]
        [Searchable]
        [UIHint(UIHint.Textarea)]
        [Display(GroupName = GroupNames.Tabs.SEO, Order = 3020)]
        public virtual string Description { get; set; }

        [Display(GroupName = GroupNames.Tabs.SEO, Order = 3030)]
        public virtual bool ExcludePageFromSearch { get; set; }

        [Display(GroupName = GroupNames.Tabs.Settings, Order = 3040)]
        public virtual bool ExcludePageFromPath { get; set; }
    }
}
