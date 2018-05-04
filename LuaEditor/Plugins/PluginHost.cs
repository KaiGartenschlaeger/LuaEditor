using LuaEditor.Plugin;
using System;

namespace LuaEditor.Plugins
{
    public class PluginHost : IPluginHost
    {
        #region Properties

        public IEditor Editor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEditorTabs Tabs
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEditorUI UI
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}