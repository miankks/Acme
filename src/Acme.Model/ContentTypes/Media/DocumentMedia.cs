namespace Acme.Model.ContentTypes.Media
{
    using System.ComponentModel.DataAnnotations;
    using EPiServer.DataAnnotations;
    using EPiServer.Framework.DataAnnotations;

    [ContentType(GUID = "76c1a479-fe6a-4813-9cd1-fae3ce0942e8")]
    [MediaDescriptor(ExtensionString = "pdf,doc,docx,xls,xlsx,pps,ppt,pptx,key,txt,rtf,text,htm,html,pps,7z,zip,zipx,rar,sit")]
    public class DocumentMedia : SiteMediaBase
    {
        [Display(Order = 10)]
        public virtual string Title { get; set; }
    }
}
