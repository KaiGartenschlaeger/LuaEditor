namespace LuaEditor.Plugin
{
    /// <summary>
    /// Stellt den Host der Erweiterung dar.
    /// </summary>
    public interface IPluginHost
    {
        IEditorTabs Tabs { get; }

        IEditor Editor { get; }

        IEditorUI UI { get; }
    }
}