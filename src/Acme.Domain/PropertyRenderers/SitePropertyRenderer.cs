namespace Acme.Domain.PropertyRenderers
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Routing;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.Framework.Web;
    using EPiServer.ServiceLocation;
    using EPiServer.Web;
    using EPiServer.Web.Mvc;
    using EPiServer.Web.Mvc.Html;
    using EPiServer.Web.Routing;

    /// <summary>
    ///     Overrides the default renderer of all properties, so if we specify a CustomTag(Name) and optionally CssClass they get
    ///     rendered in view and preview mode as well. The default renderer only wraps the property in edit mode, except for
    ///     content areas which are always wrapped.
    /// </summary>
    public class SitePropertyRenderer : PropertyRenderer
    {
        private readonly CachingViewEnginesWrapper viewResolver;

        public SitePropertyRenderer(CachingViewEnginesWrapper viewResolver)
        {
            this.viewResolver = viewResolver;
        }

        public override MvcHtmlString PropertyFor<TModel, TValue>(HtmlHelper<TModel> html, string viewModelPropertyName, object additionalViewData, object editorSettings, Expression<Func<TModel, TValue>> expression, Func<string, MvcHtmlString> displayForAction)
        {
            var contextMode = html.ViewContext.RequestContext.GetContextMode();

            // Properties are always wrapped in edit mode, so no need for custom rendering
            if (contextMode == ContextMode.Edit)
            {
                return base.PropertyFor(html, viewModelPropertyName, additionalViewData, editorSettings, expression, displayForAction);
            }

            var routeValueDictionaries = new RouteValueDictionary(additionalViewData);
            var templateName = this.ResolveTemplateName(html, routeValueDictionaries, expression);
            var isContentArea = this.PropertyIsContentArea(html, expression);

            // Content areas are always wrapped, so no need for custom rendering in view mode.
            if (isContentArea)
            {
                return displayForAction(templateName);
            }

            string elementName = null;

            if (routeValueDictionaries.ContainsKey("CustomTag"))
            {
                elementName = routeValueDictionaries["CustomTag"] as string;
            }

            // Correctly spelled property as well, since Episerver probably made a mistake here
            if (routeValueDictionaries.ContainsKey("CustomTagName"))
            {
                elementName = routeValueDictionaries["CustomTagName"] as string;
            }

            string cssClass = null;

            if (routeValueDictionaries.ContainsKey("CssClass"))
            {
                cssClass = routeValueDictionaries["CssClass"] as string;
            }

            return this.GetHtmlForDefaultAndPreviewMode(templateName, elementName, cssClass, displayForAction);
        }

        private MvcHtmlString GetHtmlForDefaultAndPreviewMode(string templateName, string elementName, string cssClass, Func<string, MvcHtmlString> displayForAction)
        {
            // Rely on standard behavior if no element is specified
            if (string.IsNullOrEmpty(elementName))
            {
                return displayForAction(templateName);
            }

            var html = displayForAction(templateName).ToHtmlString();

            if (string.IsNullOrEmpty(html))
            {
                return MvcHtmlString.Empty;
            }

            var tag = new TagBuilder(elementName)
            {
                InnerHtml = html
            };

            if (string.IsNullOrEmpty(cssClass) == false)
            {
                tag.AddCssClass(cssClass);
            }

            return new MvcHtmlString(tag.ToString());
        }

        private bool PropertyIsContentArea<TModel, TValue>(HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var contentAreaType = typeof(ContentArea);
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            return contentAreaType.IsAssignableFrom(modelMetadata.ModelType);
        }

        #region Private methods reflected from base class

        private string ResolveTemplateName<TModel, TValue>(HtmlHelper<TModel> html, RouteValueDictionary additionalValues, Expression<Func<TModel, TValue>> expression)
        {
            var modelMetadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var tag = additionalValues["tag"] as string;

            if (string.IsNullOrEmpty(tag) && modelMetadata != null)
            {
                tag = this.GetTagFromModelMetadata(modelMetadata);
            }

            if (string.IsNullOrEmpty(tag) == false && modelMetadata != null)
            {
                var templateResolver = html.ViewData["templateResolver"] as TemplateResolver ?? ServiceLocator.Current.GetInstance<TemplateResolver>();

                var templateModel = templateResolver.Resolve(
                    html.ViewContext.HttpContext,
                    modelMetadata.ModelType,
                    modelMetadata.Model,
                    TemplateTypeCategories.MvcPartialView,
                    tag);

                var templateName = this.GetTemplateName(templateModel, html.ViewContext);

                if (string.IsNullOrEmpty(templateName) == false)
                {
                    return templateName;
                }
            }

            if (this.DisplayTemplateWithNameExists(html.ViewContext, tag) == false)
            {
                return null;
            }

            return tag;
        }

        private string GetTagFromModelMetadata(ModelMetadata metaData)
        {
            if (metaData == null || metaData.ContainerType == null)
            {
                return null;
            }

            var property = metaData.ContainerType.GetProperty(metaData.PropertyName);

            if (property != null)
            {
                var uIHintAttributes = property.GetCustomAttributes(true).OfType<UIHintAttribute>();

                var uIHintAttribute = uIHintAttributes.FirstOrDefault(a => string.Equals(a.PresentationLayer, "website", StringComparison.OrdinalIgnoreCase));

                if (uIHintAttribute != null)
                {
                    return uIHintAttribute.UIHint;
                }

                uIHintAttribute = uIHintAttributes.FirstOrDefault(a => string.IsNullOrEmpty(a.PresentationLayer));

                if (uIHintAttribute != null)
                {
                    return uIHintAttribute.UIHint;
                }
            }

            return null;
        }

        private string GetTemplateName(TemplateModel templateModel, ControllerContext viewContext)
        {
            if (templateModel == null)
            {
                return null;
            }

            if (this.DisplayTemplateWithNameExists(viewContext, templateModel.Name) == false)
            {
                return null;
            }

            return templateModel.Name;
        }

        private bool DisplayTemplateWithNameExists(ControllerContext viewContext, string templateName)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return false;
            }

            var viewEngineResult = this.viewResolver.FindPartialView(viewContext, $"DisplayTemplates/{templateName}");

            if (viewEngineResult == null)
            {
                return false;
            }

            return viewEngineResult.View != null;
        }

        #endregion
    }
}
