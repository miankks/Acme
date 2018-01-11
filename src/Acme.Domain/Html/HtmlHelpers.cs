namespace Acme.Domain.Html
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.WebPages;
    using Acme.Model.Navigation;
    using Acme.Models.Constants;
    using EPiServer.Core;
    using EPiServer.Web;
    using EPiServer.Web.Mvc.Html;
    using EPiServer.Web.Routing;
    using NetR.Base.GlobalFunctions;
    using NetR.EPi.Filters.Response.HtmlFilters;

    public static class HtmlHelpers
    {
        /// <summary>
        ///     Renders an img element. Supported ViewData settings in <paramref name="additionalViewData"/>:
        ///         - ImageId
        ///         - ImageCssClass
        ///         - ImageWidth
        ///         - ImageHeight
        ///         - ImageMaxHeight
        ///         - ImageQuality
        ///     This method "overloads" PropertyFor without the need to specify a Tag for the image display template view.
        /// </summary>
        /// <typeparam name="TModel">The content data type.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the image content reference to display.</param>
        /// <param name="additionalViewData">
        ///     An anonymous object that can contain additional view data that will be merged
        ///     into the ViewDataDictionary instance that is created for the template.
        /// </param>
        /// <returns>Returns an img element with dimension attributes.</returns>
        public static MvcHtmlString PropertyImageFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object additionalViewData = null)
            where TValue : ContentReference
        {
            var propertyName = ExpressionFunctions.GetPropertyName(expression);

            return PropertyExtensions.PropertyRenderer.PropertyFor(html, propertyName, additionalViewData, null, expression, (string templateName) =>
            {
                return DisplayExtensions.DisplayFor(html, expression, RenderingTags.DisplayTemplates.Image, additionalViewData);
            });
        }

        /// <summary>
        ///     Uses <see cref="XhtmlStringExtensions.XhtmlString(HtmlHelper, XhtmlString)"/> to render the input,
        ///     but will also apply filters to the rendered output.
        /// </summary>
        /// <param name="htmlHelper">>HtmlHelper instance.</param>
        /// <param name="input">The property to render and apply the filters to.</param>
        /// <returns>Returns a rendered xhtml string that has filters applied.</returns>
        public static IHtmlString XhtmlStringFiltered(this HtmlHelper htmlHelper, XhtmlString input)
        {
            if (input == null || input.IsEmpty)
            {
                return MvcHtmlString.Empty;
            }

            var contextMode = htmlHelper.ViewContext.RequestContext.GetContextMode();

            if (contextMode == ContextMode.Default || contextMode == ContextMode.Preview)
            {
                var rendered = htmlHelper.XhtmlString(input);
                var html = rendered.ToHtmlString();

                var filteredHtml = new ImageCaptionFilter().Filter(html, null);

                return MvcHtmlString.Create(filteredHtml);
            }
            else
            {
                return htmlHelper.XhtmlString(input);
            }
        }

        /// <summary>
        ///     Render items of type <see cref="NavigationItem"/> using supplied helper methods.
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper instance.</param>
        /// <param name="breadcrumbs">The items to render.</param>
        /// <param name="item">
        ///     The helper method to render every item that's not selected or hidden
        ///     beacause the <paramref name="pageCount"/> is reached.
        /// </param>
        /// <param name="selectedItem">The helper method to render the item that is selected (should be the last item).</param>
        /// <param name="separator">The helper method to render separators between each item.</param>
        /// <param name="ellipsis">
        ///     The helper method to render ellipsis for every item that is hidden because
        ///     the <paramref name="pageCount"/> is reached.
        /// </param>
        /// <param name="pageCount">
        ///     Number of items to render, excluding the first and selected item.
        /// </param>
        /// <returns>A <see cref="IHtmlString"/>containing the rendered output.</returns>
        public static IHtmlString Breadcrumbs(
           this HtmlHelper htmlHelper,
           IEnumerable<NavigationItem> breadcrumbs,
           Func<NavigationItem, int, HelperResult> item,
           Func<NavigationItem, int, HelperResult> selectedItem,
           Func<HelperResult> separator,
           Func<NavigationItem, int, HelperResult> ellipsis = null,
           int pageCount = 0)
        {
            if (breadcrumbs == null || breadcrumbs.Any() == false)
            {
                return MvcHtmlString.Empty;
            }

            var buffer = new StringBuilder();
            var writer = new StringWriter(buffer);

            var counter = 0;
            var totalCount = breadcrumbs.Count();

            // Do not add elipsis for start- and last page.
            pageCount = pageCount - 2;

            using (writer)
            {
                for (int i = 0; i < totalCount; i++)
                {
                    var breadcrumb = breadcrumbs.ElementAt(i);
                    var isLastItem = i == totalCount - 1;

                    if (ellipsis != null &&
                        breadcrumb.IsStartPage == false &&
                        isLastItem == false &&
                        i > pageCount)
                    {
                        ellipsis(breadcrumb, counter).WriteTo(writer);
                    }
                    else if (breadcrumb.IsSelected)
                    {
                        selectedItem(breadcrumb, counter).WriteTo(writer);
                    }
                    else
                    {
                        item(breadcrumb, counter).WriteTo(writer);
                    }

                    if (isLastItem == false)
                    {
                        separator().WriteTo(writer);
                    }

                    counter++;
                }
            }

            return new MvcHtmlString(buffer.ToString());
        }
    }
}
