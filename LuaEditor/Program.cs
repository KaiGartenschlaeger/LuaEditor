using LuaEditor.Dialogs;
using LuaEditor.Objetcts;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LuaEditor
{
    static class Program
    {
        static EditorSettings _settings;

        static string EnsureSettingsPath()
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "LuaEditor");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            return Path.Combine(folderPath, "Settings.json");
        }

        static EditorSettings ReadSettings()
        {
            string path = EnsureSettingsPath();
            return EditorSettings.Read(path);
        }

        static void SaveSettings()
        {
            string path = EnsureSettingsPath();
            _settings.Save(path);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += Application_ApplicationExit;
            Application.ThreadException += Application_ThreadException;

            _settings = ReadSettings();

            FormMain main = new FormMain(_settings);

            Application.Run(main);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Ein unerwarteter Fehler ist aufgetreten:\n\n" + e.Exception.Message, "Fehler",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}