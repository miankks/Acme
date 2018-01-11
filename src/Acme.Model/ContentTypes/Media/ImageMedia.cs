namespace Acme.Model.ContentTypes.Media
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.Core;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;

    [ContentType(GUID = "CFA90F9C-D812-4A20-886D-B9386629C750")]
    [MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,gif,bmp,png")]
    public class ImageMedia : ImageData
    {
        [Display(Order = 10)]
        public virtual string AlternativeText { get; set; }
    }
}
