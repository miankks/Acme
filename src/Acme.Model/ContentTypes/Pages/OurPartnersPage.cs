namespace Acme.Model.ContentTypes.Pages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAnnotations;
    using EPiServer.SpecializedProperties;
    using NetR.EPi.SettingsFramework;

    [ContentType(DisplayName = "OurPartners", GUID = "59d41e99-b0b9-4e23-93c9-bfa4c65ca187", Description = "")]
    [AvailableContentTypes(
        Availability.Specific,
        Include = new[]
                  {
                      typeof(StandardPage),
                      typeof(SettingsFolder),
                      typeof(HomePage)
                  })]
    
    public class OurPartnersPage : ContentPageBase
    {
        /*
                [CultureSpecific]
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
    }
}
