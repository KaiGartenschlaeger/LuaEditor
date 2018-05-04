namespace LuaEditor.Dialogs
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.statusBarMain = new System.Windows.Forms.StatusStrip();
            this.slblGeneral = new System.Windows.Forms.ToolStripStatusLabel();
            this.slblEditorColumn = new System.Windows.Forms.ToolStripStatusLabel();
            this.slblEditorLine = new System.Windows.Forms.ToolStripStatusLabel();
            this.slblEditorChar = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mniProject_Title = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProject_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProject_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProject_Last = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProject_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.mniProject_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniProject_End = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Title = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Replace = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_Separator = new System.Windows.Forms.ToolStripSeparator();
            this.mniEdit_Themes = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_ThemeReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEdit_ThemeSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mniEdit_Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHelp_Info = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarMain = new System.Windows.Forms.ToolStrip();
            this.tbbProject_Open = new System.Windows.Forms.ToolStripButton();
            this.tbbProject_Close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbRun = new System.Windows.Forms.ToolStripButton();
            this.editorTabs = new LuaEditor.Dialogs.Controls.EditorTabsControl();
            this.console = new LuaEditor.Dialogs.Controls.ConsoleControl();
            this.splitterProjectTree = new System.Windows.Forms.Splitter();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.splitterConsole = new System.Windows.Forms.Splitter();
            this.panelProject = new System.Windows.Forms.Panel();
            this.projectTree = new LuaEditor.Dialogs.Controls.ProjectTreeControl();
            this.statusBarMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.toolbarMain.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.panelProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBarMain
            // 
            this.statusBarMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusBarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slblGeneral,
            this.slblEditorColumn,
            this.slblEditorLine,
            this.slblEditorChar});
            this.statusBarMain.Location = new System.Drawing.Point(0, 707);
            this.statusBarMain.Name = "statusBarMain";
            this.statusBarMain.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusBarMain.Size = new System.Drawing.Size(1008, 22);
            this.statusBarMain.TabIndex = 0;
            // 
            // slblGeneral
            // 
            this.slblGeneral.AutoSize = false;
            this.slblGeneral.Name = "slblGeneral";
            this.slblGeneral.Size = new System.Drawing.Size(80, 17);
            this.slblGeneral.Text = "{General}";
            this.slblGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slblEditorColumn
            // 
            this.slblEditorColumn.AutoSize = false;
            this.slblEditorColumn.Name = "slblEditorColumn";
            this.slblEditorColumn.Size = new System.Drawing.Size(120, 17);
            this.slblEditorColumn.Text = "{ColumnIndex}";
            this.slblEditorColumn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // slblEditorLine
            // 
            this.slblEditorLine.AutoSize = false;
            this.slblEditorLine.Name = "slblEditorLine";
            this.slblEditorLine.Size = new System.Drawing.Size(120, 17);
            this.slblEditorLine.Text = "{LineIndex}";
            this.slblEditorLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // slblEditorChar
            // 
            this.slblEditorChar.AutoSize = false;
            this.slblEditorChar.Name = "slblEditorChar";
            this.slblEditorChar.Size = new System.Drawing.Size(120, 17);
            this.slblEditorChar.Text = "{currentChar}";
            this.slblEditorChar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniProject_Title,
            this.mniEdit_Title,
            this.hilfeToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(3);
            this.menuMain.Size = new System.Drawing.Size(1008, 29);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // mniProject_Title
            // 
            this.mniProject_Title.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniProject_New,
            this.mniProject_Open,
            this.mniProject_Last,
            this.mniProject_Save,
            this.mniProject_Close,
            this.toolStripMenuItem2,
            this.mniProject_End});
            this.mniProject_Title.Name = "mniProject_Title";
            this.mniProject_Title.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.mniProject_Title.Size = new System.Drawing.Size(56, 23);
            this.mniProject_Title.Text = "Projekt";
            this.mniProject_Title.DropDownOpening += new System.EventHandler(this.mniProject_Title_DropDownOpening);
            // 
            // mniProject_New
            // 
            this.mniProject_New.Name = "mniProject_New";
            this.mniProject_New.Size = new System.Drawing.Size(158, 22);
            this.mniProject_New.Text = "Neu";
            this.mniProject_New.Click += new System.EventHandler(this.mniProject_New_Click);
            // 
            // mniProject_Open
            // 
            this.mniProject_Open.Name = "mniProject_Open";
            this.mniProject_Open.Size = new System.Drawing.Size(158, 22);
            this.mniProject_Open.Text = "Öffnen";
            this.mniProject_Open.Click += new System.EventHandler(this.mniProject_Open_Click);
            // 
            // mniProject_Last
            // 
            this.mniProject_Last.Name = "mniProject_Last";
            this.mniProject_Last.Size = new System.Drawing.Size(158, 22);
            this.mniProject_Last.Text = "Zuletzt geöffnet";
            this.mniProject_Last.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mniProject_Last_DropDownItemClicked);
            // 
            // mniProject_Save
            // 
            this.mniProject_Save.Name = "mniProject_Save";
            this.mniProject_Save.Size = new System.Drawing.Size(158, 22);
            this.mniProject_Save.Text = "Speichern";
            this.mniProject_Save.Click += new System.EventHandler(this.mniProject_Save_Click);
            // 
            // mniProject_Close
            // 
            this.mniProject_Close.Name = "mniProject_Close";
            this.mniProject_Close.Size = new System.Drawing.Size(158, 22);
            this.mniProject_Close.Text = "Schließen";
            this.mniProject_Close.Click += new System.EventHandler(this.mniProject_Close_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // mniProject_End
            // 
            this.mniProject_End.Name = "mniProject_End";
            this.mniProject_End.Size = new System.Drawing.Size(158, 22);
            this.mniProject_End.Text = "Beenden";
            this.mniProject_End.Click += new System.EventHandler(this.mniProject_End_Click);
            // 
            // mniEdit_Title
            // 
            this.mniEdit_Title.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniEdit_Search,
            this.mniEdit_Replace,
            this.mniEdit_Separator,
            this.mniEdit_Themes,
            this.mniEdit_Settings});
            this.mniEdit_Title.Name = "mniEdit_Title";
            this.mniEdit_Title.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.mniEdit_Title.Size = new System.Drawing.Size(75, 23);
            this.mniEdit_Title.Text = "Bearbeiten";
            this.mniEdit_Title.DropDownOpening += new System.EventHandler(this.mniEdit_Title_DropDownOpening);
            // 
            // mniEdit_Search
            // 
            this.mniEdit_Search.Name = "mniEdit_Search";
            this.mniEdit_Search.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Search.Text = "Suchen";
            this.mniEdit_Search.Click += new System.EventHandler(this.mniEdit_Search_Click);
            // 
            // mniEdit_Replace
            // 
            this.mniEdit_Replace.Name = "mniEdit_Replace";
            this.mniEdit_Replace.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Replace.Text = "Ersetzen";
            this.mniEdit_Replace.Click += new System.EventHandler(this.mniEdit_Replace_Click);
            // 
            // mniEdit_Separator
            // 
            this.mniEdit_Separator.Name = "mniEdit_Separator";
            this.mniEdit_Separator.Size = new System.Drawing.Size(149, 6);
            // 
            // mniEdit_Themes
            // 
            this.mniEdit_Themes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniEdit_ThemeReset,
            this.mniEdit_ThemeSeparator});
            this.mniEdit_Themes.Name = "mniEdit_Themes";
            this.mniEdit_Themes.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Themes.Text = "Themes";
            this.mniEdit_Themes.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mniEdit_Themes_DropDownItemClicked);
            // 
            // mniEdit_ThemeReset
            // 
            this.mniEdit_ThemeReset.Name = "mniEdit_ThemeReset";
            this.mniEdit_ThemeReset.Size = new System.Drawing.Size(144, 22);
            this.mniEdit_ThemeReset.Text = "Zurücksetzen";
            this.mniEdit_ThemeReset.Click += new System.EventHandler(this.mniEdit_ThemeReset_Click);
            // 
            // mniEdit_ThemeSeparator
            // 
            this.mniEdit_ThemeSeparator.Name = "mniEdit_ThemeSeparator";
            this.mniEdit_ThemeSeparator.Size = new System.Drawing.Size(141, 6);
            // 
            // mniEdit_Settings
            // 
            this.mniEdit_Settings.Name = "mniEdit_Settings";
            this.mniEdit_Settings.Size = new System.Drawing.Size(152, 22);
            this.mniEdit_Settings.Text = "Einstellungen";
            this.mniEdit_Settings.Click += new System.EventHandler(this.mniEdit_Settings_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniHelp_Info});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // mniHelp_Info
            // 
            this.mniHelp_Info.Name = "mniHelp_Info";
            this.mniHelp_Info.Size = new System.Drawing.Size(150, 22);
            this.mniHelp_Info.Text = "Informationen";
            this.mniHelp_Info.Click += new System.EventHandler(this.mniHelp_Info_Click);
            // 
            // toolbarMain
            // 
            this.toolbarMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolbarMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbProject_Open,
            this.tbbProject_Close,
            this.toolStripSeparator2,
            this.tbbRun});
            this.toolbarMain.Location = new System.Drawing.Point(0, 29);
            this.toolbarMain.Name = "toolbarMain";
            this.toolbarMain.Padding = new System.Windows.Forms.Padding(3);
            this.toolbarMain.Size = new System.Drawing.Size(1008, 33);
            this.toolbarMain.TabIndex = 2;
            this.toolbarMain.Text = "toolStrip1";
            // 
            // tbbProject_Open
            // 
            this.tbbProject_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbProject_Open.Image = global::LuaEditor.Properties.Resources.folder_page;
            this.tbbProject_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbProject_Open.Name = "tbbProject_Open";
            this.tbbProject_Open.Padding = new System.Windows.Forms.Padding(2);
            this.tbbProject_Open.Size = new System.Drawing.Size(24, 24);
            this.tbbProject_Open.Text = "Projekt öffnen";
            this.tbbProject_Open.Click += new System.EventHandler(this.tbbProject_Open_Click);
            // 
            // tbbProject_Close
            // 
            this.tbbProject_Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbProject_Close.Image = global::LuaEditor.Properties.Resources.folder_delete;
            this.tbbProject_Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbProject_Close.Name = "tbbProject_Close";
            this.tbbProject_Close.Padding = new System.Windows.Forms.Padding(2);
            this.tbbProject_Close.Size = new System.Drawing.Size(24, 24);
            this.tbbProject_Close.Text = "Projekt schließen";
            this.tbbProject_Close.Click += new System.EventHandler(this.tbbProject_Close_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tbbRun
            // 
            this.tbbRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbRun.Image = global::LuaEditor.Properties.Resources.play_green;
            this.tbbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbRun.Name = "tbbRun";
            this.tbbRun.Padding = new System.Windows.Forms.Padding(2);
            this.tbbRun.Size = new System.Drawing.Size(24, 24);
            this.tbbRun.Text = "Starten (F5)";
            this.tbbRun.Click += new System.EventHandler(this.tbbRun_Click);
            // 
            // editorTabs
            // 
            this.editorTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorTabs.Location = new System.Drawing.Point(6, 3);
            this.editorTabs.Name = "editorTabs";
            this.editorTabs.Size = new System.Drawing.Size(721, 484);
            this.editorTabs.TabIndex = 0;
            this.editorTabs.TabChanged += new System.EventHandler(this.editorTabs_TabChanged);
            this.editorTabs.EditorPositonChanged += new System.EventHandler<LuaEditor.Dialogs.Controls.ScintillaPosEventArgs>(this.editorTabs_EditorCursorPositionChanged);
            this.editorTabs.EditorTextChanged += new System.EventHandler(this.editorTabs_EditorTextChanged);
            // 
            // console
            // 
            this.console.ConsoleFont = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.console.Location = new System.Drawing.Point(6, 487);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(721, 157);
            this.console.TabIndex = 0;
            // 
            // splitterProjectTree
            // 
            this.splitterProjectTree.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.splitterProjectTree.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterProjectTree.Location = new System.Drawing.Point(727, 62);
            this.splitterProjectTree.Name = "splitterProjectTree";
            this.splitterProjectTree.Size = new System.Drawing.Size(8, 645);
            this.splitterProjectTree.TabIndex = 3;
            this.splitterProjectTree.TabStop = false;
            // 
            // panelEditor
            // 
            this.panelEditor.Controls.Add(this.splitterConsole);
            this.panelEditor.Controls.Add(this.editorTabs);
            this.panelEditor.Controls.Add(this.console);
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 62);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Padding = new System.Windows.Forms.Padding(6, 3, 0, 1);
            this.panelEditor.Size = new System.Drawing.Size(727, 645);
            this.panelEditor.TabIndex = 4;
            // 
            // splitterConsole
            // 
            this.splitterConsole.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.splitterConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterConsole.Location = new System.Drawing.Point(6, 479);
            this.splitterConsole.Name = "splitterConsole";
            this.splitterConsole.Size = new System.Drawing.Size(721, 8);
            this.splitterConsole.TabIndex = 2;
            this.splitterConsole.TabStop = false;
            // 
            // panelProject
            // 
            this.panelProject.Controls.Add(this.projectTree);
            this.panelProject.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProject.Location = new System.Drawing.Point(735, 62);
            this.panelProject.Name = "panelProject";
            this.panelProject.Padding = new System.Windows.Forms.Padding(0, 6, 6, 1);
            this.panelProject.Size = new System.Drawing.Size(273, 645);
            this.panelProject.TabIndex = 5;
            // 
            // projectTree
            // 
            this.projectTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(61)))), ((int)(((byte)(90)))));
            this.projectTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectTree.Location = new System.Drawing.Point(0, 6);
            this.projectTree.Name = "projectTree";
            this.projectTree.Size = new System.Drawing.Size(267, 638);
            this.projectTree.TabIndex = 1;
            this.projectTree.OpenFile += new System.EventHandler<LuaEditor.Dialogs.Controls.ProjectEntryEventArgs>(this.projectTree_OpenFile);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.splitterProjectTree);
            this.Controls.Add(this.panelProject);
            this.Controls.Add(this.toolbarMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.statusBarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Lua Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.statusBarMain.ResumeLayout(false);
            this.statusBarMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolbarMain.ResumeLayout(false);
            this.toolbarMain.PerformLayout();
            this.panelEditor.ResumeLayout(false);
            this.panelProject.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusBarMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mniProject_Title;
        private System.Windows.Forms.ToolStripMenuItem mniProject_New;
        private System.Windows.Forms.ToolStripMenuItem mniProject_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mniProject_End;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Title;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Settings;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mniHelp_Info;
        private System.Windows.Forms.ToolStripMenuItem mniProject_Open;
        private System.Windows.Forms.ToolStrip toolbarMain;
        private System.Windows.Forms.ToolStripButton tbbRun;
        private System.Windows.Forms.ToolStripMenuItem mniProject_Last;
        private Controls.ConsoleControl console;
        private System.Windows.Forms.ToolStripMenuItem mniProject_Save;
        private System.Windows.Forms.ToolStripButton tbbProject_Open;
        private System.Windows.Forms.ToolStripButton tbbProject_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Search;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Replace;
        private System.Windows.Forms.ToolStripSeparator mniEdit_Separator;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_Themes;
        private System.Windows.Forms.ToolStripMenuItem mniEdit_ThemeReset;
        private System.Windows.Forms.ToolStripSeparator mniEdit_ThemeSeparator;
        private System.Windows.Forms.ToolStripStatusLabel slblGeneral;
        private System.Windows.Forms.ToolStripStatusLabel slblEditorColumn;
        private System.Windows.Forms.ToolStripStatusLabel slblEditorLine;
        private System.Windows.Forms.ToolStripStatusLabel slblEditorChar;
        private Controls.EditorTabsControl editorTabs;
        private System.Windows.Forms.Splitter splitterProjectTree;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Splitter splitterConsole;
        private System.Windows.Forms.Panel panelProject;
        private Controls.ProjectTreeControl projectTree;
    }
}