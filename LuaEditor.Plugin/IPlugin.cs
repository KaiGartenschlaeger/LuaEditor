namespace LuaEditor.Plugin
{
    /// <summary>
    /// Stellt eine Erweiterung dar.
    /// </summary>
    public interface IPlugin
    {
        #region Methods

        /// <summary>
        /// Wird aufgerufen, wenn die Erweiterung geladen wird.
        /// </summary>
        void Start(IPluginHost host);

        /// <summary>
        /// Wird aufgerufen, wenn die Erweiterung beendet wird.
        /// </summary>
        void Shutdown(IPluginHost host);

        #endregion

        #region Properties

        /// <summary>
        /// Name der Erweiterung
        /// </summary>
        string Name { get; }

        #endregion
    }
}