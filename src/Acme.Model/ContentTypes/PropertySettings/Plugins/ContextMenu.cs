namespace Acme.Model.PropertySettings.Plugins
{
    using EPiServer.Editor.TinyMCE;

    /// <summary>
    /// TinyMCE Context menu, NetRelations style
    /// </summary>
    [TinyMCEPluginNonVisual(
        AlwaysEnabled = false,
        PlugInName = "contextmenu",
        DisplayName = "NetRelations Context Menu",
        Description = "Adds some right click menu options for the editor.")]
    public class ContextMenu
    {
    }
}
