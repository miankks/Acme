namespace Acme.Web
{
    using System.Linq;
    using System.Web.Mvc;

    /// <summary>
    ///     Extends the Razor view engine to include the folders ~/Views/Shared/Blocks/ and ~/Views/Shared/PagePartials/
    ///     when looking for partial views.
    /// </summary>
    public class ViewEngine : RazorViewEngine
    {
        private static readonly string[] AdditionalPartialViewFormats =
        {
            "~/Views/{0}/Partials/Index.cshtml",
            "~/Views/Shared/PagePartials/{0}.cshtml",
            "~/Views/Shared/Partials/{0}.cshtml",
            "~/Views/Shared/Blocks/{0}.cshtml",
        };

        public ViewEngine()
        {
            this.PartialViewLocationFormats = this.PartialViewLocationFormats.Union(AdditionalPartialViewFormats).ToArray();
        }
    }
}