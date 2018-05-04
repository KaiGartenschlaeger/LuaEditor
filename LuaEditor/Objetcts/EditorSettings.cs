using PureSoft.Configuration;
using PureSoft.Configuration.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace LuaEditor.Objetcts
{
    public class EditorSettings
    {
        #region Fields

        private Font _editorFont;
        private Font _consoleFont;
        private readonly Dictionary<string, EditorFormSettings> _formSettings;
        private readonly EditorRunnerSettings _runnerSettings;
        private readonly List<string> _lastProjects;
        private readonly Dictionary<string, int> _splitterSettings;
        private string _editorTheme;

        #endregion

        #region Constructor

        public EditorSettings()
        {
            _formSettings = new Dictionary<string, EditorFormSettings>();
            _runnerSettings = new EditorRunnerSettings();
            _lastProjects = new List<string>();
            _splitterSettings = new Dictionary<string, int>();
        }

        #endregion

        #region Methods

        public static EditorSettings Read(string path)
        {
            ConfigurationSet set = new JsonConfigurationSet();
            if (File.Exists(path))
            {
                set.Read(path);
            }

            EditorSettings settings = new EditorSettings();

            // editor theme
            settings.EditorTheme = set.Get<string>("editor.theme", null);

            // runner settings
            settings.RunnerSettings.Path = set.Get("start.path", string.Empty);
            settings.RunnerSettings.Arguments = set.Get("start.arguments", string.Empty);
            settings.RunnerSettings.HideWindow = set.Get("start.hide", true);
            settings.RunnerSettings.RedirectOutput = set.Get("start.redirect", true);

            // form settings
            int formSettingsCount = set.Get("forms.count", 0);
            for (int i = 0; i < formSettingsCount; i++)
            {
                string name = set.Get<string>("forms.setting[" + i + "].name", null);
                if (!string.IsNullOrEmpty(name))
                {
                    EditorFormSettings s = new EditorFormSettings();

                    s.IsAbsolutePos = set.Get("forms.setting[" + i + "].absolutePos", true);

                    s.Bounds = new Rectangle(
                        set.Get("forms.setting[" + i + "].left", 0),
                        set.Get("forms.setting[" + i + "].top", 0),
                        set.Get("forms.setting[" + i + "].width", 0),
                        set.Get("forms.setting[" + i + "].height", 0));

                    s.Maximized = set.Get("forms.setting[" + i + "].maximized", false);

                    settings.FormSettings.Add(name, s);
                }
            }

            // fonts
            settings.EditorFont = new Font(
                set.Get("editor.font.name", "Consolas"),
                set.Get("editor.font.size", 12));
            settings.ConsoleFont = new Font(
                set.Get("console.font.name", "Consolas"),
                set.Get("console.font.size", 12));

            // splitter
            int splitterSettingsCount = set.Get("splitter.count", 0);
            for (int i = 0; i < splitterSettingsCount; i++)
            {
                string settingKey = set.Get<string>("splitter.settings[" + i + "].name", null);
                if (!string.IsNullOrEmpty(settingKey)
                    && !settings.SplitterSettings.ContainsKey(settingKey))
                {
                    settings.SplitterSettings.Add(settingKey,
                        set.Get("splitter.settings[" + i + "].distance", -1));
                }
            }

            // last projects
            int lastProjectsCount = set.Get("lastProjects.count", 0);
            for (int i = 0; i < lastProjectsCount; i++)
            {
                settings.LastProjects.Add(
                    set.Get<string>("lastProjects.project[" + i + "]"));
            }

            return settings;
        }

        public void Save(string path)
        {
            ConfigurationSet set = new JsonConfigurationSet();

            // editor theme
            set.Set("editor.theme", _editorTheme);

            // runner settings
            set.Set("start.path", _runnerSettings.Path);
            set.Set("start.arguments", _runnerSettings.Arguments);
            set.Set("start.hide", _runnerSettings.HideWindow);
            set.Set("start.redirect", _runnerSettings.RedirectOutput);

            // form settings
            set.Set("forms.count", _formSettings.Count);

            int formSettingsCounter = 0;
            foreach (var fs in _formSettings)
            {
                set.Set("forms.setting[" + formSettingsCounter + "].absolutePos", fs.Value.IsAbsolutePos);
                set.Set("forms.setting[" + formSettingsCounter + "].name", fs.Key);
                set.Set("forms.setting[" + formSettingsCounter + "].left", fs.Value.Bounds.Left);
                set.Set("forms.setting[" + formSettingsCounter + "].top", fs.Value.Bounds.Top);
                set.Set("forms.setting[" + formSettingsCounter + "].width", fs.Value.Bounds.Width);
                set.Set("forms.setting[" + formSettingsCounter + "].height", fs.Value.Bounds.Height);
                set.Set("forms.setting[" + formSettingsCounter + "].maximized", fs.Value.Maximized);

                formSettingsCounter++;
            }

            // fonts
            set.Set("editor.font.name", _editorFont.Name);
            set.Set("editor.font.size", (int)_editorFont.Size);

            set.Set("console.font.name", _consoleFont.Name);
            set.Set("console.font.size", (int)_consoleFont.Size);

            // splitter
            set.Set("splitter.count", _splitterSettings.Count);
            int splitterSettingsCounter = 0;
            foreach (var splitterSettings in _splitterSettings)
            {
                set.Set("splitter.settings[" + splitterSettingsCounter + "].name",
                    splitterSettings.Key);
                set.Set("splitter.settings[" + splitterSettingsCounter + "].distance",
                    splitterSettings.Value);

                splitterSettingsCounter++;
            }

            // last projects
            set.Set("lastProjects.count", _lastProjects.Count);
            for (int i = 0; i < _lastProjects.Count; i++)
            {
                set.Set("lastProjects.project[" + i + "]", _lastProjects[i]);
            }

            set.Save(path);
        }

        public int GetSplitterSettings(string identifier, int fallbackValue)
        {
            if (_splitterSettings.ContainsKey(identifier) && _splitterSettings[identifier] != -1)
                return _splitterSettings[identifier];

            return fallbackValue;
        }

        #endregion

        #region Properties

        public Font EditorFont
        {
            get { return _editorFont; }
            set { _editorFont = value; }
        }

        public Font ConsoleFont
        {
            get { return _consoleFont; }
            set { _consoleFont = value; }
        }

        public Dictionary<string, EditorFormSettings> FormSettings
        {
            get { return _formSettings; }
        }

        public EditorRunnerSettings RunnerSettings
        {
            get { return _runnerSettings; }
        }

        public List<string> LastProjects
        {
            get { return _lastProjects; }
        }

        public Dictionary<string, int> SplitterSettings
        {
            get { return _splitterSettings; }
        }

        public string EditorTheme
        {
            get { return _editorTheme; }
            set { _editorTheme = value; }
        }

        #endregion
    }
}