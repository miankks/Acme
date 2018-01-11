namespace Acme.Domain.PropertyRenderers
{
    using System;
    using System.Web.Mvc;
    using Acme.Models.Constants;
    using EPiServer.Core;
    using EPiServer.Web;
    using EPiServer.Web.Mvc.Html;
    using NetR.Base.Extensions;
    using NetR.EPi.Extensions;

    public class SiteContentAreaRenderer : ContentAreaRenderer
    {
        private readonly string gridCssClass = "o-grid";

        private readonly string columnCssClass = "o-grid__column";

        public SiteContentAreaRenderer()
        {
        }

        public string CssClass { get; private set; }

        public int ColumnSpan { get; private set; }

        public int Columns { get; private set; }

        public bool IsGridEnabled => this.CssClass.IsNullOrWhiteSpace() == false && this.CssClass.Contains(this.gridCssClass);

        /// <summary>
        ///     First method we can override where we have access to the HtmlHelper.
        ///     Here we retrieve the settings for the content area property and store them
        ///     in properties so we can use them later on where we don't have access to the HtmlHelper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper to use.</param>
        /// <param name="contentArea">The content area property to render.</param>
        public override void Render(HtmlHelper htmlHelper, ContentArea contentArea)
        {
            var cssClass = htmlHelper.ViewContext.ViewData.GetRenderSettings<string>("CssClass");
            var columnSpan = htmlHelper.ViewContext.ViewData.GetRenderSettings<int>("ColumnSpan", int.TryParse);
            var columns = htmlHelper.ViewContext.ViewData.GetRenderSettings<int>("Columns", int.TryParse);

            this.CssClass = cssClass;
            this.ColumnSpan = columnSpan;
            this.Columns = columns;

            base.Render(htmlHelper, contentArea);
        }

        protected override void BeforeRenderContentAreaItemStartTag(TagBuilder tagBuilder, ContentAreaItem contentAreaItem)
        {
            if (this.IsGridEnabled == false)
            {
                base.BeforeRenderContentAreaItemStartTag(tagBuilder, contentAreaItem);

                return;
            }

            var gridSize = 24;
            var size = gridSize;

            if (this.Columns > 0)
            {
                size = gridSize / this.Columns;
            }
            else if (this.ColumnSpan > 0)
            {
                size = this.ColumnSpan;
            }
            else
            {
                var displayOption = contentAreaItem.LoadDisplayOption();

                if (displayOption != null)
                {
                    size = this.GetDisplayOptionColumnSpan(displayOption);
                }
            }

            tagBuilder.Attributes.Add("data-size", $"sm:24 md:{size} lg:{size}");
        }

        protected override string GetContentAreaItemCssClass(HtmlHelper htmlHelper, ContentAreaItem contentAreaItem)
        {
            if (this.IsGridEnabled)
            {
                var baseClasses = base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);

                return $"{this.columnCssClass} {baseClasses}";
            }

            return base.GetContentAreaItemCssClass(htmlHelper, contentAreaItem);
        }

        private int GetDisplayOptionColumnSpan(DisplayOption displayOption, int gridSize = 24)
        {
            switch (displayOption.Id)
            {
                case RenderingTags.DisplayOptionTags.ThreeQuarters:
                    return (int)Math.Round(gridSize * 0.75);
                case RenderingTags.DisplayOptionTags.TwoThirds:
                    return (int)Math.Round(gridSize * 0.66);
                case RenderingTags.DisplayOptionTags.Half:
                    return (int)Math.Round(gridSize * 0.5);
                case RenderingTags.DisplayOptionTags.Third:
                    return (int)Math.Round(gridSize * 0.33);
                case RenderingTags.DisplayOptionTags.Quarter:
                    return (int)Math.Round(gridSize * 0.25);
                default:
                    return gridSize;
            }
        }
    }
}
