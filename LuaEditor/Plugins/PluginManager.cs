using LuaEditor.Plugin;
using LuaEditor.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LuaEditor.Manager
{
    internal class PluginManager
    {
        #region Fields

        private readonly IPluginHost _host;
        private readonly List<PluginInfo> _plugins;

        #endregion

        #region Constructor

        public PluginManager()
        {
            _host = new PluginHost();
            _plugins = new List<PluginInfo>();
        }

        #endregion

        #region Methods

        public void StartPlugins()
        {
            var path = Path.Combine(Application.StartupPath, "Plugins");
            var pluginsDirectory = new DirectoryInfo(path);

            if (!pluginsDirectory.Exists)
                return;

            foreach (var pluginPath in pluginsDirectory.GetFiles("*.dll"))
            {
                try
                {
                    Assembly pluginAssembly = Assembly.LoadFile(pluginPath.FullName);
                    Type[] pluginTypes = pluginAssembly.GetTypes();

                    Type pluginInfoType = pluginTypes.Where(t =>
                    {
                        return t.IsPublic &&
                              !t.IsAbstract &&
                              typeof(IPlugin).IsAssignableFrom(t);

                    }).FirstOrDefault();

                    IPlugin pluginInstance = (IPlugin)Activator.CreateInstance(
                        pluginAssembly.GetType(pluginInfoType.FullName));

                    PluginInfo plugin = new PluginInfo(pluginInstance);
                    _plugins.Add(plugin);

                    plugin.Instance.Start(_host);
                }
                catch (Exception)
                { }
            }
        }

        public void ShutdownPlugins()
        {
            foreach (var plugin in _plugins)
            {
                plugin.Instance.Shutdown(_host);
            }
        }

        #endregion

        #region Properties

        #endregion
    }
}