namespace Acme.Domain.Html
{
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using Acme.Model.Navigation;

    /// <summary>
    ///     HTML helper extensions for rendering of components.
    /// </summary>
    public static class ComponentsHtmlHelpers
    {
        /// <summary>
        ///     "Namespace" for all components.
        /// </summary>
        /// <param name="helper">The HTML helper instance to extend.</param>
        /// <returns>Returns a <see cref="ComponentsHtmlHelper"/>.</returns>
        public static ComponentsHtmlHelper Components(this HtmlHelper helper)
        {
            return new ComponentsHtmlHelper(helper);
        }

        public static IHtmlString Breadcrumbs(this ComponentsHtmlHelper helper, List<NavigationItem> model)
        {
            if (model == null)
            {
                return MvcHtmlString.Empty;
            }

            return helper.Helper.Partial("Components/Navigation/Breadcrumbs", model);
        }

        public static IHtmlString Navigation(this ComponentsHtmlHelper helper, List<NavigationItem> model)
        {
            if (model == null)
            {
                return MvcHtmlString.Empty;
            }

            return helper.Helper.Partial("Components/Navigation/Navigation", model);
        }

        public static IHtmlString SubNavigation(this ComponentsHtmlHelper helper, HierarchicalNavigationItem model)
        {
            if (model == null)
            {
                return MvcHtmlString.Empty;
            }

            return helper.Helper.Partial("Components/Navigation/SubNavigation", model);
        }
    }
}
