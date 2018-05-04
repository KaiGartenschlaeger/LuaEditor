using LuaEditor.Plugin;

namespace Hashing
{
    public class Plugin : IPlugin
    {
        #region Methods

        public void Start(IPluginHost host)
        {

        }

        public void Shutdown(IPluginHost host)
        {

        }

        #endregion

        #region Properties

        public string Name
        {
            get { return "Hashing"; }
        }

        #endregion
    }
}