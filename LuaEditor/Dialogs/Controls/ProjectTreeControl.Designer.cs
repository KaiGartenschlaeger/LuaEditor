namespace LuaEditor.Dialogs.Controls
{
    partial class ProjectTreeControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectTreeControl));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuFile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniFile_AddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFile_AddExistingFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFile_AddFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mniFile_OpenInExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniFile_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniFile_Startentry = new System.Windows.Forms.ToolStripMenuItem();
            this.toolPaneLineControl1 = new PureSoft.Controls.VisualStudio.Vs2010ToolPaneHeader();
            this.trvEntities = new PureSoft.Controls.VisualStudio.Vs2010TreeView();
            this.propEntry = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbbAddFolder = new System.Windows.Forms.ToolStripButton();
            this.tbbAddFile = new System.Windows.Forms.ToolStripButton();
            this.splitterProperties = new System.Windows.Forms.Splitter();
            this.contextMenuFile.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "page_white.png");
            this.imageList1.Images.SetKeyName(2, "folder_closed.png");
            this.imageList1.Images.SetKeyName(3, "folder_link.png");
            this.imageList1.Images.SetKeyName(4, "page_white_link.png");
            this.imageList1.Images.SetKeyName(5, "page_white_lightning.png");
            // 
            // contextMenuFile
            // 
            this.contextMenuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFile_AddFile,
            this.mniFile_AddExistingFile,
            this.mniFile_AddFolder,
            this.mniFile_OpenInExplorer,
            this.toolStripMenuItem1,
            this.mniFile_Delete,
            this.toolStripMenuItem2,
            this.mniFile_Startentry});
            this.contextMenuFile.Name = "contextMenuFile";
            this.contextMenuFile.Size = new System.Drawing.Size(231, 170);
            this.contextMenuFile.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuFile_Opening);
            // 
            // mniFile_AddFile
            // 
            this.mniFile_AddFile.Image = global::LuaEditor.Properties.Resources.page_white_add;
            this.mniFile_AddFile.Name = "mniFile_AddFile";
            this.mniFile_AddFile.Size = new System.Drawing.Size(230, 22);
            this.mniFile_AddFile.Text = "Datei hinzufügen";
            this.mniFile_AddFile.Click += new System.EventHandler(this.mniFile_AddFile_Click);
            // 
            // mniFile_AddExistingFile
            // 
            this.mniFile_AddExistingFile.Image = global::LuaEditor.Properties.Resources.page_white_link;
            this.mniFile_AddExistingFile.Name = "mniFile_AddExistingFile";
            this.mniFile_AddExistingFile.Size = new System.Drawing.Size(230, 22);
            this.mniFile_AddExistingFile.Text = "Vorhandene Datei hinzufügen";
            this.mniFile_AddExistingFile.Click += new System.EventHandler(this.mniFile_AddExistingFile_Click);
            // 
            // mniFile_AddFolder
            // 
            this.mniFile_AddFolder.Image = global::LuaEditor.Properties.Resources.folder_add;
            this.mniFile_AddFolder.Name = "mniFile_AddFolder";
            this.mniFile_AddFolder.Size = new System.Drawing.Size(230, 22);
            this.mniFile_AddFolder.Text = "Ordner hinzufügen";
            this.mniFile_AddFolder.Click += new System.EventHandler(this.mniFile_AddFolder_Click);
            // 
            // mniFile_OpenInExplorer
            // 
            this.mniFile_OpenInExplorer.Name = "mniFile_OpenInExplorer";
            this.mniFile_OpenInExplorer.Size = new System.Drawing.Size(230, 22);
            this.mniFile_OpenInExplorer.Text = "Im Explorer öffnen";
            this.mniFile_OpenInExplorer.Click += new System.EventHandler(this.mniFile_OpenInExplorer_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(227, 6);
            // 
            // mniFile_Delete
            // 
            this.mniFile_Delete.Image = global::LuaEditor.Properties.Resources.cancel;
            this.mniFile_Delete.Name = "mniFile_Delete";
            this.mniFile_Delete.Size = new System.Drawing.Size(230, 22);
            this.mniFile_Delete.Text = "Entfernen";
            this.mniFile_Delete.Click += new System.EventHandler(this.mniFile_Delete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(227, 6);
            // 
            // mniFile_Startentry
            // 
            this.mniFile_Startentry.Image = global::LuaEditor.Properties.Resources.lightning;
            this.mniFile_Startentry.Name = "mniFile_Startentry";
            this.mniFile_Startentry.Size = new System.Drawing.Size(230, 22);
            this.mniFile_Startentry.Text = "Startdatei";
            this.mniFile_Startentry.Click += new System.EventHandler(this.mniFile_Startentry_Click);
            // 
            // toolPaneLineControl1
            // 
            this.toolPaneLineControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolPaneLineControl1.Location = new System.Drawing.Point(0, 0);
            this.toolPaneLineControl1.Margin = new System.Windows.Forms.Padding(0);
            this.toolPaneLineControl1.Name = "toolPaneLineControl1";
            this.toolPaneLineControl1.Size = new System.Drawing.Size(318, 22);
            this.toolPaneLineControl1.TabIndex = 1;
            this.toolPaneLineControl1.Text = "Projekt";
            // 
            // trvEntities
            // 
            this.trvEntities.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvEntities.FullRowSelect = true;
            this.trvEntities.HideSelection = false;
            this.trvEntities.ImageIndex = 0;
            this.trvEntities.ImageList = this.imageList1;
            this.trvEntities.ItemHeight = 22;
            this.trvEntities.Location = new System.Drawing.Point(0, 52);
            this.trvEntities.Margin = new System.Windows.Forms.Padding(0);
            this.trvEntities.Name = "trvEntities";
            this.trvEntities.SelectedImageIndex = 0;
            this.trvEntities.ShowLines = false;
            this.trvEntities.ShowNodeToolTips = true;
            this.trvEntities.ShowRootLines = false;
            this.trvEntities.Size = new System.Drawing.Size(318, 308);
            this.trvEntities.TabIndex = 0;
            this.trvEntities.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.trvEntities_BeforeCollapse);
            this.trvEntities.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvEntities_AfterCollapse);
            this.trvEntities.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvEntities_AfterExpand);
            this.trvEntities.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvEntities_AfterSelect);
            this.trvEntities.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trvEntities_NodeMouseDoubleClick);
            this.trvEntities.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwEntries_MouseUp);
            // 
            // propEntry
            // 
            this.propEntry.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.propEntry.HelpVisible = false;
            this.propEntry.LineColor = System.Drawing.SystemColors.ControlDark;
            this.propEntry.Location = new System.Drawing.Point(0, 368);
            this.propEntry.Name = "propEntry";
            this.propEntry.Size = new System.Drawing.Size(318, 157);
            this.propEntry.TabIndex = 3;
            this.propEntry.ViewBorderColor = System.Drawing.SystemColors.Window;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbAddFolder,
            this.tbbAddFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 22);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(3, 3, 2, 2);
            this.toolStrip1.Size = new System.Drawing.Size(318, 30);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbbAddFolder
            // 
            this.tbbAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbAddFolder.Image = global::LuaEditor.Properties.Resources.folder_add;
            this.tbbAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbAddFolder.Name = "tbbAddFolder";
            this.tbbAddFolder.Padding = new System.Windows.Forms.Padding(1);
            this.tbbAddFolder.Size = new System.Drawing.Size(23, 22);
            this.tbbAddFolder.Text = "Ordner hinzufügen";
            this.tbbAddFolder.Click += new System.EventHandler(this.tbbAddFolder_Click);
            // 
            // tbbAddFile
            // 
            this.tbbAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbAddFile.Image = global::LuaEditor.Properties.Resources.page_white_add;
            this.tbbAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbAddFile.Name = "tbbAddFile";
            this.tbbAddFile.Padding = new System.Windows.Forms.Padding(1);
            this.tbbAddFile.Size = new System.Drawing.Size(23, 22);
            this.tbbAddFile.Text = "Datei hinzufügen";
            this.tbbAddFile.Click += new System.EventHandler(this.tbbAddFile_Click);
            // 
            // splitterProperties
            // 
            this.splitterProperties.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.splitterProperties.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterProperties.Location = new System.Drawing.Point(0, 360);
            this.splitterProperties.Name = "splitterProperties";
            this.splitterProperties.Size = new System.Drawing.Size(318, 8);
            this.splitterProperties.TabIndex = 4;
            this.splitterProperties.TabStop = false;
            // 
            // ProjectTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trvEntities);
            this.Controls.Add(this.splitterProperties);
            this.Controls.Add(this.propEntry);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolPaneLineControl1);
            this.Name = "ProjectTreeControl";
            this.Size = new System.Drawing.Size(318, 525);
            this.contextMenuFile.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PureSoft.Controls.VisualStudio.Vs2010TreeView trvEntities;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuFile;
        private System.Windows.Forms.ToolStripMenuItem mniFile_AddFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mniFile_AddFolder;
        private System.Windows.Forms.ToolStripMenuItem mniFile_Delete;
        private System.Windows.Forms.ToolStripMenuItem mniFile_AddExistingFile;
        private System.Windows.Forms.ToolStripMenuItem mniFile_OpenInExplorer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mniFile_Startentry;
        private PureSoft.Controls.VisualStudio.Vs2010ToolPaneHeader toolPaneLineControl1;
        private System.Windows.Forms.PropertyGrid propEntry;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Splitter splitterProperties;
        private System.Windows.Forms.ToolStripButton tbbAddFolder;
        private System.Windows.Forms.ToolStripButton tbbAddFile;
    }
}
