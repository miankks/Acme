namespace Acme.Domain.Html
{
    using System.Web.Mvc;

    /// <summary>
    ///     Helper class for rendering of components.
    ///     This class wraps the <see cref="HtmlHelper"/> class so we
    ///     can "categorize" components in a namespace.
    /// </summary>
    public class ComponentsHtmlHelper
    {
        internal ComponentsHtmlHelper(HtmlHelper helper)
        {
            this.Helper = helper;
        }

        internal HtmlHelper Helper { get; }
    }
}
