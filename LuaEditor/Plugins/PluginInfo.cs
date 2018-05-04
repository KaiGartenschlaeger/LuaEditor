using LuaEditor.Plugin;

namespace LuaEditor.Plugins
{
    internal class PluginInfo
    {
        #region Fields

        private IPlugin _instance;

        #endregion

        #region Constructor

        public PluginInfo(IPlugin instance)
        {
            _instance = instance;
        }

        #endregion

        #region Properties

        public IPlugin Instance
        {
            get { return _instance; }
        }

        #endregion
    }
}