namespace Acme.Model.PropertySettings
{
    using System;
    using System.Collections.Generic;
    using EPiServer.Core.PropertySettings;
    using EPiServer.Editor.TinyMCE;
    using EPiServer.ServiceLocation;

    /// <summary>
    /// Settings for a reduced TinyMCE tool set.
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(PropertySettings))]
    public class SimpleTinyMceSettings : PropertySettings<TinyMCESettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTinyMceSettings"/> class.
        /// </summary>
        public SimpleTinyMceSettings()
        {
            this.DisplayName = "Ingress [NetRelations]";
        }

        /// <summary>
        /// Gets the identifier for this specific settings.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public override Guid ID
        {
            get
            {
                return new Guid("326c8ae8-685d-4387-872a-006fb45818d5");
            }
        }

        /// <summary>
        /// Gets the property settings.
        /// </summary>
        /// <returns>The property settings.</returns>
        public override TinyMCESettings GetPropertySettings()
        {
            return new TinyMCESettings
            {
                ContentCss = "/gui/css/editor.min.css",
                Width = 580,
                Height = 150,
                NonVisualPlugins = new[] { "optimizededitor", "contextmenu" },
                ToolbarRows = new List<ToolbarRow>
                {
                    new ToolbarRow(new[]
                    {
                        TinyMCEButtons.Bold,
                        TinyMCEButtons.Italic,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Undo,
                        TinyMCEButtons.Redo,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.EPiServerLink,
                        TinyMCEButtons.Unlink,
                        TinyMCEButtons.Anchor,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.PasteText,
                        TinyMCEButtons.CleanUp,
                        TinyMCEButtons.RemoveFormat,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Search,
                        TinyMCEButtons.Replace,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Code,
                        TinyMCEButtons.Fullscreen
                    })
                }
            };
        }
    }
}
