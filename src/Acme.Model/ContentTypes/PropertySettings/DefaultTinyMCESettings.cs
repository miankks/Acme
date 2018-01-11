namespace Acme.Model.PropertySettings
{
    using System;
    using System.Collections.Generic;
    using EPiServer.Core.PropertySettings;
    using EPiServer.Editor.TinyMCE;
    using EPiServer.ServiceLocation;

    /// <summary>
    /// The default settings for TinyMCE
    /// </summary>
    [ServiceConfiguration(ServiceType = typeof(PropertySettings))]
    public class DefaultTinyMceSettings : PropertySettings<TinyMCESettings>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultTinyMceSettings" /> class.
        /// </summary>
        public DefaultTinyMceSettings()
        {
            this.IsDefault = true;
            this.DisplayName = "Grundinställning [NetRelations]";
        }

        /// <summary>
        ///     Gets the identifier for this specific settings.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public override Guid ID
        {
            get
            {
                return new Guid("9d776ac1-4dc7-4789-ac9c-124f34b8c275");
            }
        }

        /// <summary>
        ///     Gets the property settings.
        /// </summary>
        /// <returns>The property settings.</returns>
        public override TinyMCESettings GetPropertySettings()
        {
            return new TinyMCESettings
            {
                ContentCss = "/gui/css/editor.min.css",
                Width = 580,
                Height = 300,
                NonVisualPlugins = new[] { "optimizededitor", "contextmenu" },
                ToolbarRows = new List<ToolbarRow>
                {
                    new ToolbarRow(new[]
                    {
                        TinyMCEButtons.Bold,
                        TinyMCEButtons.Italic,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.NumericList,
                        TinyMCEButtons.BulletedList,
                        TinyMCEButtons.Outdent,
                        TinyMCEButtons.Indent,
                        TinyMCEButtons.EPiServerQuote,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Undo,
                        TinyMCEButtons.Redo,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.EPiServerLink,
                        TinyMCEButtons.Unlink,
                        TinyMCEButtons.Anchor,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Image,
                        TinyMCEButtons.EPiServerImageEditor,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.PasteText,
                        TinyMCEButtons.CleanUp,
                        TinyMCEButtons.RemoveFormat,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.Code,
                        TinyMCEButtons.Fullscreen
                    }),
                    new ToolbarRow(new[]
                    {
                        TinyMCEButtons.FormatSelect,
                        TinyMCEButtons.StyleSelect,
                        TinyMCEButtons.TableButtons.Table,
                        TinyMCEButtons.TableButtons.RowProperties,
                        TinyMCEButtons.TableButtons.CellProperties,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.TableButtons.InsertRowBefore,
                        TinyMCEButtons.TableButtons.InsertRowAfter,
                        TinyMCEButtons.TableButtons.DeleteRow,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.TableButtons.InsertColumnBefore,
                        TinyMCEButtons.TableButtons.InsertColumnsAfter,
                        TinyMCEButtons.TableButtons.DeleteColumns,
                        TinyMCEButtons.Separator,
                        TinyMCEButtons.TableButtons.SplitCells,
                        TinyMCEButtons.TableButtons.MergeCells
                    })
                }
            };
        }
    }
}
