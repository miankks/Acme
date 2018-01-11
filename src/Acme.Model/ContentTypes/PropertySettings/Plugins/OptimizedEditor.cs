namespace Acme.Model.PropertySettings.Plugins
{
    using System.Collections.Generic;
    using EPiServer.Editor.TinyMCE;

    /// <summary>
    /// NetRelations optimized editor
    /// </summary>
    [TinyMCEPluginNonVisual(
        AlwaysEnabled = false,
        PlugInName = "optimizededitor",
        DisplayName = "NetRelations Settings",
        Description = "Optimized init options for the editor.",
        DynamicConfigurationOptionsHandler = typeof(OptimizedEditor),
        ServerSideOnly = true)]
    public class OptimizedEditor : IDynamicConfigurationOptions
    {
        public IDictionary<string, object> GetConfigurationOptions()
        {
            return new Dictionary<string, object>
            {
                { "theme", "advanced" },
                { "mode", "exact" },
                { "body_class", "s-text s-editor" },
                { "cleanup", true },
                { "apply_source_formatting", true },
                { "remove_linebreaks", false },
                { "convert_fonts_to_spans", true },
                { "fix_nesting", true },
                { "theme_advanced_blockformats", "p,h2,h3,h4,h5,h6,blockquote,pre" },
                {
                    "valid_elements",
                    @"@[id|class|title|dir<ltr?rtl|lang|xml::lang],
                      a[rel|rev|hreflang|name|href|target],
                      img[longdesc|usemap|src|title|width|height|alt=], object[width|height|data|type],param[name|value],
                      strong/b,
                      em/i,
                      -p,
                      -ol,
                      -ul,
                      -li,
                      -sub,
                      -sup,
                      -blockquote[cite],
                      -table[summary],
                      -tr[rowspan|align|valign|style],
                      tbody,
                      thead,
                      tfoot,
                      #td[colspan|rowspan|align|valign|scope|headers|style],
                      #th[colspan|rowspan|align|valign|scope|headers|style],
                      -div,
                      -span,
                      -code,
                      -pre,
                      -h1,
                      -h2,
                      -h3,
                      -h4,
                      -h5,
                      -h6,
                      abbr,
                      acronym,
                      address,
                      br,
                      caption,
                      cite,
                      dd,
                      dl,
                      dt,
                      hr,
                      kbd,
                      q[cite]"
                },
                { "valid_child_elements", "h1/h2/h3/h4/h5/h6/a[%istrict], ol/ul[li], table[caption|thead|tbody|tfoot|tr|td], strong/b/p/div/em/i[%istrict|#text], object[param], td[%bstrict|%istrict|#text]" }
                ////This doens't work unfortunately, but it should.
                ////{
                ////    "style_formats",
                ////    new object[]
                ////    {
                ////        new { title = "Images" },
                ////        new { title = "Left", selector = "img", classes = "left" },
                ////        new { title = "Right", selector = "img", classes = "right" },
                ////        new { title = "Full width", selector = "img", classes = "fullwidth" }
                ////    }
                ////},
            };
        }
    }
}
