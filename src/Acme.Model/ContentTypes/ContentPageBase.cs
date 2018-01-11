namespace Acme.Model.ContentTypes
{
    using System.ComponentModel.DataAnnotations;
    using Acme.Model.PropertySettings;
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Web;

    /// <summary>
    /// Base class for pages that contain basic content information.
    /// </summary>
    public abstract class ContentPageBase : MetaPageBase
    {
        [CultureSpecific]
        [Searchable]
        [UIHint(UIHint.LongString)]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1110)]
        public virtual string MainIntro { get; set; }

        [CultureSpecific]
        [Searchable]
        [PropertySettings(typeof(DefaultTinyMceSettings))]
        [Display(GroupName = GroupNames.Tabs.Content, Order = 1120)]
        public virtual XhtmlString MainBody { get; set; }

        public override string TeaserBody
        {
            get
            {
                var body = this.GetPropertyValue(p => p.TeaserBody);

                if (string.IsNullOrWhiteSpace(body))
                {
                    return this.MainIntro;
                }

                return body;
            }

            set
            {
                this.SetPropertyValue(p => p.TeaserBody, value);
            }
        }
    }
}
