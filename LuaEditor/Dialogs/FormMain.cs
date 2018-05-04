using LuaEditor.Dialogs.Controls;
using LuaEditor.Manager;
using LuaEditor.Objetcts;
using PureSoft.Controls.VisualStudio.Colors;
using PureSoft.Controls.VisualStudio.Renderer;
using System;
using System.IO;
using System.Windows.Forms;

namespace LuaEditor.Dialogs
{
    public partial class FormMain : FormBase
    {
        #region Consts

        private const string ProjFileFilter = "Projekt Datei|*.luaproj";

        #endregion

        #region Fields

        private readonly ProjectManager _projectManager;
        private readonly LuaRunner _runner;
        private readonly PluginManager _pluginManager;

        private EditorTheme _currentTheme;

        #endregion

        #region Constructor

        public FormMain()
        {

        }

        public FormMain(EditorSettings settings)
            : base(settings)
        {
            InitializeComponent();

            _projectManager = new ProjectManager();
            _projectManager.ProjectChanged += _projectManager_ProjectChanged;

            _runner = new LuaRunner();

            var colors = new Vs2010DefaultToolStripColorTable();
            BackColor = colors.WindowBackground;

            splitterConsole.BackColor = colors.WindowBackground;
            splitterProjectTree.BackColor = colors.WindowBackground;

            menuMain.Renderer = new Vs2010MenuStripRenderer();
            slblEditorColumn.Text = string.Empty;

            foreach (ToolStripItem item in statusBarMain.Items)
            {
                item.Text = string.Empty;
            }

            slblGeneral.Text = "Bereit";

            var toolStripRenderer = new Vs2010ToolStripRenderer();
            toolStripRenderer.RoundedEdges = false;
            toolbarMain.Renderer = toolStripRenderer;

            statusBarMain.Renderer = new Vs2010StatusStripRenderer();

            projectTree.SetProjectManager(_projectManager);

            if (Settings != null)
                RestoreSettings();

            editorTabs.Settings = Settings;
            projectTree.Settings = Settings;

            RefreshToolbar();
            RefreshMenu();

            ApplyFontSettings();

            InitAvailableThemes();
            ApplyEditorTheme(Settings.EditorTheme);

            InitLastProjectsMenu();

            _pluginManager = new PluginManager();
            _pluginManager.StartPlugins();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pluginManager.ShutdownPlugins();

            StoreSettings();
        }

        private void ApplyFontSettings()
        {
            console.ConsoleFont = Settings.ConsoleFont;
        }

        private void ApplyEditorTheme(string filename)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                string path = Path.Combine(
                    Application.StartupPath, "Themes", filename);

                if (!File.Exists(path))
                    return;

                _currentTheme = EditorTheme.FromFile(path);
            }
            else
            {
                _currentTheme = EditorTheme.Default;
            }

            Settings.EditorTheme = filename;

            // apply to all editor tab instances
            editorTabs.SetTheme(_currentTheme);

            // check the selected theme
            for (int i = 2; i < mniEdit_Themes.DropDownItems.Count; i++)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)mniEdit_Themes.DropDownItems[i];
                item.Checked = item.Tag.ToString().Equals(filename, StringComparison.OrdinalIgnoreCase);
            }
        }

        private void RestoreSettings()
        {
            // runner settings
            _runner.ApplicationPath = Settings.RunnerSettings.Path;
            _runner.ApplicationArguments = Settings.RunnerSettings.Arguments;
            _runner.HideWindow = Settings.RunnerSettings.HideWindow;
            _runner.RedirectOutput = Settings.RunnerSettings.RedirectOutput;

            // main form settings
            RestorePosition(Settings);

            // fonts
            console.ConsoleFont = Settings.ConsoleFont;

            // splitter settings
            splitterProjectTree.SplitPosition = Settings.GetSplitterSettings("project", Width - 320);
            splitterConsole.SplitPosition = Settings.GetSplitterSettings("console", Height - 320);
            projectTree.SplitterDistance = Settings.GetSplitterSettings("projectTree", Height - 320);

            // last projects
            foreach (string path in Settings.LastProjects)
            {
                _projectManager.AddToLastProjects(path, moveToTop: false);
            }
        }
        private void StoreSettings()
        {
            // form settings
            EditorFormSettings mainFormSettings = Settings.FormSettings["FormMain"];
            mainFormSettings.Maximized = (WindowState == FormWindowState.Maximized);
            mainFormSettings.Bounds = (WindowState == FormWindowState.Maximized) ? RestoreBounds : Bounds;

            // splitter settings
            Settings.SplitterSettings["project"] = splitterProjectTree.SplitPosition;
            Settings.SplitterSettings["console"] = splitterConsole.SplitPosition;
            Settings.SplitterSettings["projectTree"] = projectTree.SplitterDistance;

            // last projects
            Settings.LastProjects.Clear();
            Settings.LastProjects.AddRange(_projectManager.LastProjects);
        }

        private void InitAvailableThemes()
        {
            while (mniEdit_Themes.DropDownItems.Count > 2)
                mniEdit_Themes.DropDownItems.RemoveAt(mniEdit_Themes.DropDownItems.Count - 2);

            string themesDirectory = Path.Combine(Application.StartupPath, "Themes");
            if (Directory.Exists(themesDirectory))
            {
                foreach (string path in Directory.GetFiles(themesDirectory, "*.xml"))
                {
                    string themeName = EditorTheme.NameFromFile(path);
                    if (themeName != null)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem();
                        item.Text = themeName;
                        item.Tag = Path.GetFileName(path);

                        mniEdit_Themes.DropDownItems.Add(item);
                    }
                }
            }

            mniEdit_ThemeSeparator.Available = mniEdit_Themes.DropDownItems.Count > 2;
        }

        private void Run()
        {
            if (_projectManager.CurrentProject == null)
                return;

            if (string.IsNullOrEmpty(_runner.ApplicationPath))
            {
                MessageBox.Show("Der Interpreter muss erst in den Einstellungen festgelegt werden.", "Stop",

                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (!File.Exists(_runner.ApplicationPath))
            {
                MessageBox.Show($"Die Datei \"{_runner.ApplicationPath}\" konnte nicht gefunden werden.", "Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            if (_projectManager.CurrentProject.StartEntry == null)
            {
                MessageBox.Show($"Es wurde noch keine Startdatei definiert.\nLegen Sie in der Projektmappe eine Startdatei fest.", "Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            // save all files
            foreach (var editor in editorTabs.Editors)
            {
                editor.Save();
            }

            // run
            console.Clear();
            console.AppendLine($"Starte \"{_projectManager.CurrentProject.StartEntry.GetAbsolutePath()}\"..");
            console.AppendLine(_runner.Run(_projectManager.CurrentProject));
        }

        private void OpenProjectDialog()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Projekt öffnen";
                dialog.Filter = ProjFileFilter;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    mniProject_Title.DropDown.Close();

                    OpenProject(dialog.FileName);

                    _projectManager.AddToLastProjects(dialog.FileName);
                    InitLastProjectsMenu();
                }

                _projectManager.AddToLastProjects(dialog.FileName);
            }
        }

        private void CloseProject()
        {
            _projectManager.CloseProject();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine(keyData);

            if (keyData == Keys.F5)
            {
                Run();
            }
            else if (keyData == (Keys.Tab | Keys.Shift))
            {
                editorTabs.NextTab();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitLastProjectsMenu()
        {
            mniProject_Last.DropDownItems.Clear();
            foreach (string project in _projectManager.LastProjects)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = project;

                mniProject_Last.DropDownItems.Add(item);
            }
        }

        #endregion

        #region Manager events

        private void _projectManager_ProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            projectTree.SetProject(e.NewProject);

            editorTabs.Clear();
            console.Clear();

            RefreshToolbar();
            RefreshMenu();
        }

        #endregion

        #region Helper

        private void RefreshToolbar()
        {
            ProjectEntry selectedEntry = projectTree.GetSelectededEntry();

            tbbRun.Enabled = _projectManager.HasProject;
            tbbProject_Close.Enabled = _projectManager.HasProject;
        }

        private void RefreshMenu()
        {
            mniEdit_Title.Visible = editorTabs.TabIndex != -1;
        }

        #endregion

        #region Control events

        private void splitContainer_MouseUp(object sender, MouseEventArgs e)
        {
            console.Focus();
        }

        private void mniProject_Title_DropDownOpening(object sender, EventArgs e)
        {
            mniProject_Save.Enabled = _projectManager.HasProject;
            mniProject_Close.Enabled = _projectManager.HasProject;
            mniProject_Last.Enabled = mniProject_Last.DropDownItems.Count > 0;
        }

        private void mniProject_End_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mniProject_Close_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void mniProject_Save_Click(object sender, EventArgs e)
        {
            mniProject_Title.DropDown.Close();

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Projekt speichern";
                dialog.Filter = ProjFileFilter;
                dialog.InitialDirectory = _projectManager.CurrentProject.RootDirectory;

                if (!string.IsNullOrEmpty(_projectManager.CurrentProject.ProjectFilePath))
                    dialog.FileName = _projectManager.CurrentProject.ProjectFilePath;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        _projectManager.Save(dialog.FileName);

                        _projectManager.AddToLastProjects(dialog.FileName);
                        InitLastProjectsMenu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim speichern der Projektdatei:\n\n" + ex.Message, "Fehler",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mniProject_Open_Click(object sender, EventArgs e)
        {
            OpenProjectDialog();
        }

        private void OpenProject(string projectFilePath)
        {
            try
            {
                var proj = _projectManager.Open(projectFilePath);

                _projectManager.AddToLastProjects(projectFilePath);
                InitLastProjectsMenu();

                _projectManager.ChangeProject(proj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim öffnen der Projektdatei:\n\n" + ex.Message, "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mniProject_New_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Title = "Neues Projekt";
                dialog.Filter = ProjFileFilter;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string name = Path.GetFileNameWithoutExtension(dialog.FileName);

                    ProjectSettings proj = _projectManager.Create(
                        name,
                        dialog.FileName);

                    _projectManager.ChangeProject(proj);
                    _projectManager.AddToLastProjects(dialog.FileName);
                }
            }
        }

        private void mniEdit_Title_DropDownOpening(object sender, EventArgs e)
        {
            mniEdit_Search.Enabled = editorTabs.EditorOpen;
            mniEdit_Replace.Enabled = editorTabs.EditorOpen;

            mniEdit_Themes.Enabled = (mniEdit_Themes.DropDownItems.Count > 0);
        }

        private void mniEdit_Search_Click(object sender, EventArgs e)
        {
            if (editorTabs.Current != null)
                editorTabs.Current.ShowSearchDialog();
        }

        private void mniEdit_Replace_Click(object sender, EventArgs e)
        {
            using (FormReplace dialog = new FormReplace())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    // todo: replace
                    throw new NotImplementedException();
                }
            }
        }

        private void mniEdit_Themes_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
                ApplyEditorTheme((string)e.ClickedItem.Tag);
        }

        private void mniEdit_ThemeReset_Click(object sender, EventArgs e)
        {
            ApplyEditorTheme(string.Empty);
        }

        private void mniEdit_Settings_Click(object sender, EventArgs e)
        {
            using (FormSettings dialog = new FormSettings())
            {
                dialog.ApplicationPath = _runner.ApplicationPath;
                dialog.ApplicationArguments = _runner.ApplicationArguments;
                dialog.HideApplicationWindow = _runner.HideWindow;
                dialog.RedirectOutput = _runner.RedirectOutput;

                dialog.EditorFont = Settings.EditorFont;
                dialog.ConsoleFont = console.ConsoleFont;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _runner.ApplicationPath = dialog.ApplicationPath;
                    _runner.ApplicationArguments = dialog.ApplicationArguments;
                    _runner.HideWindow = dialog.HideApplicationWindow;
                    _runner.RedirectOutput = dialog.RedirectOutput;

                    Settings.EditorFont = dialog.EditorFont;
                    Settings.ConsoleFont = dialog.ConsoleFont;

                    ApplyFontSettings();
                }
            }
        }

        private void mniHelp_Info_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Lua Editor\n" +
                "Copyright Kai Gartenschläger, 2017", "Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mniProject_Last_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            mniProject_Title.DropDown.Close();

            string projectFilePath = e.ClickedItem.Text;
            if (!File.Exists(projectFilePath))
            {
                if (MessageBox.Show(
                    text: "Die Projektdatei \"\" konnte nicht gefunden werden.\n" +
                          "Soll der Eintrag entfernt werden?",
                    caption: "Datei nicht gefunden",
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    _projectManager.RemoveLastProject(projectFilePath);
                    InitLastProjectsMenu();
                }

                return;
            }

            OpenProject(e.ClickedItem.Text);
        }

        #endregion

        private void projectTree_OpenFile(object sender, ProjectEntryEventArgs e)
        {
            // check if the file exists
            string absolutePath = e.Entry.GetAbsolutePath();
            if (!File.Exists(absolutePath))
            {
                MessageBox.Show($"Die Datei \"{absolutePath}\" konnte nicht gefunden werden.", "Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return;
            }

            editorTabs.Add(e.Entry);
        }

        private void RefreshEditorInformations()
        {
            var editor = editorTabs.Current;
            if (editor != null)
            {
                char ch = editor.GetCurrentChar();
                int chAscii = (int)ch;

                slblEditorColumn.Text = "Spalte " + (editor.GetColumnIndex() + 1);
                slblEditorLine.Text = "Zeile " + (editor.GetLineIndex() + 1);

                if (chAscii >= 32)
                    slblEditorChar.Text = $"{(int)ch} ('{ch}')";
                else
                    slblEditorChar.Text = string.Empty;
            }
            else
            {
                slblEditorColumn.Text = string.Empty;
                slblEditorLine.Text = string.Empty;
                slblEditorChar.Text = string.Empty;
            }
        }

        private void editorTabs_TabChanged(object sender, EventArgs e)
        {
            RefreshToolbar();
            RefreshMenu();
            RefreshEditorInformations();
        }

        private void editorTabs_EditorCursorPositionChanged(object sender, ScintillaPosEventArgs e)
        {
            RefreshEditorInformations();
        }

        private void editorTabs_EditorTextChanged(object sender, EventArgs e)
        {
            RefreshEditorInformations();
        }

        private void projectTree_SelectedEntryChanged(object sender, EventArgs e)
        {
            RefreshToolbar();
            RefreshMenu();
        }

        #region Toolbar events

        private void tbbProject_Open_Click(object sender, EventArgs e)
        {
            OpenProjectDialog();
        }

        private void tbbProject_Close_Click(object sender, EventArgs e)
        {
            CloseProject();
        }

        private void tbbRun_Click(object sender, EventArgs e)
        {
            Run();
        }

        #endregion
    }
}