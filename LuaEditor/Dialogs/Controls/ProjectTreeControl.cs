using LuaEditor.Helper;
using LuaEditor.Manager;
using LuaEditor.Objetcts;
using PureSoft.Controls.VisualStudio.Colors;
using PureSoft.Controls.VisualStudio.Renderer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LuaEditor.Dialogs.Controls
{
    public partial class ProjectTreeControl : UserControl
    {
        #region Fields

        private EditorSettings _settings;
        private ProjectManager _projectManager;

        #endregion

        #region Constructor

        public ProjectTreeControl()
        {
            InitializeComponent();

            contextMenuFile.Renderer = new Vs2010MenuStripRenderer();

            Vs2010ToolStripRenderer toolStripRender = new Vs2010ToolStripRenderer();
            toolStripRender.RoundedEdges = false;
            toolStrip1.Renderer = toolStripRender;

            Vs2010CommonColorTable colorTable = new Vs2010DefaultCommonColorTable();
            BackColor = colorTable.WindowBackground;

            //splitContainer1.SplitterWidth = 8;

            trvEntities.TreeViewNodeSorter = new NodeSorter();

            RefreshToolbar();

            propEntry.Enabled = false;
        }

        #endregion

        #region Helper

        class NodeSorter : IComparer
        {
            private int GetSortOrderByType(ProjectEntryType type)
            {
                switch (type)
                {
                    case ProjectEntryType.ProjectFolder:
                        return 1;
                    case ProjectEntryType.Folder:
                        return 2;
                    case ProjectEntryType.File:
                    case ProjectEntryType.LinkedFile:
                        return 3;

                    default:
                        throw new NotImplementedException();
                }
            }

            public int Compare(object a, object b)
            {
                TreeNode nodeA = (TreeNode)a;
                TreeNode nodeB = (TreeNode)b;

                ProjectEntry entryA = (ProjectEntry)nodeA.Tag;
                ProjectEntry entryB = (ProjectEntry)nodeB.Tag;

                int sortOrderA = GetSortOrderByType(entryA.Type);
                int sortOrderB = GetSortOrderByType(entryB.Type);

                if (sortOrderA == sortOrderB)
                    return nodeA.Text.CompareTo(nodeB.Text);
                else
                    return sortOrderA - sortOrderB;
            }
        }

        private TreeNode FindNodeById(TreeNodeCollection nodes, string id)
        {
            Stack<TreeNodeCollection> nodesToSearch = new Stack<TreeNodeCollection>();
            nodesToSearch.Push(nodes);

            while (nodesToSearch.Count > 0)
            {
                TreeNodeCollection current = nodesToSearch.Pop();
                foreach (TreeNode node in current)
                {
                    if (((ProjectEntry)node.Tag).Id == id)
                    {
                        return node;
                    }
                    else
                    {
                        if (node.Nodes.Count > 0)
                        {
                            nodesToSearch.Push(node.Nodes);
                        }
                    }
                }
            }

            return null;
        }

        private TreeNode FindNodeById(string id)
        {
            return FindNodeById(trvEntities.Nodes, id);
        }

        public ProjectEntry FindEntryById(string id)
        {
            TreeNode node = FindNodeById(id);
            if (node != null)
                return (ProjectEntry)node.Tag;

            return null;
        }

        public ProjectEntry GetSelectededEntry()
        {
            TreeNode selectedNode = trvEntities.SelectedNode;
            if (selectedNode != null)
            {
                return (ProjectEntry)selectedNode.Tag;
            }

            return null;
        }

        #endregion

        #region Actions

        private void AddFile()
        {
            ProjectEntry folderEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            using (FormNewFile dialog = new FormNewFile(_settings, folderEntry, ".lua"))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string folderPath = folderEntry.GetAbsolutePath();
                    string filePath = Path.Combine(folderPath, dialog.Filename);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                        {
                            writer.WriteLine("-- Erstellt: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                        }
                    }

                    ProjectEntry entry = new ProjectEntry(
                        project: _projectManager.CurrentProject,
                        type: ProjectEntryType.File,
                        path: dialog.Filename,
                        id: GuidHelper.Create(),
                        isExpanded: false);

                    folderEntry.Add(entry);
                    folderEntry.IsExpanded = true;

                    _projectManager.Save(
                        folderEntry.Project.ProjectFilePath);

                    TreeNode addedNode = AddEntryToTree(trvEntities.SelectedNode.Nodes, entry);
                    trvEntities.SelectedNode = addedNode;

                    if (OpenFile != null)
                        OpenFile(this, new ProjectEntryEventArgs(entry));
                }
            }
        }

        private void AddFolder()
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            using (FormNewFolder dialog = new FormNewFolder(_settings, selectedEntry))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string fullPath = Path.Combine(
                        selectedEntry.GetAbsolutePath(), dialog.Foldername);

                    Directory.CreateDirectory(fullPath);

                    ProjectEntry entry = new ProjectEntry(
                        project: _projectManager.CurrentProject,
                        type: ProjectEntryType.Folder,
                        path: dialog.Foldername,
                        id: GuidHelper.Create(),
                        isExpanded: true);

                    selectedEntry.Add(entry);
                    _projectManager.Save(entry.Project.ProjectFilePath);

                    TreeNode addedNode = AddEntryToTree(trvEntities.SelectedNode.Nodes, entry);
                    trvEntities.SelectedNode = addedNode;
                }
            }
        }

        #endregion

        #region Context menu events

        private void contextMenuFile_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshContextMenu();
        }

        private void mniFile_AddFile_Click(object sender, EventArgs e)
        {
            AddFile();
        }

        private void mniFile_AddExistingFile_Click(object sender, EventArgs e)
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Datei hinzufügen";
                dialog.InitialDirectory = _projectManager.CurrentProject.RootDirectory;

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    string filename = Path.GetFileName(dialog.FileName);

                    ProjectEntry entry = new ProjectEntry(
                        project: _projectManager.CurrentProject,
                        type: ProjectEntryType.LinkedFile,
                        path: filename,
                        id: GuidHelper.Create(),
                        isExpanded: false);

                    selectedEntry.Add(entry);
                    selectedEntry.IsExpanded = true;

                    AddEntryToTree(trvEntities.SelectedNode.Nodes, entry);

                    _projectManager.Save(entry.Project.ProjectFilePath);
                }
            }
        }

        private void mniFile_AddFolder_Click(object sender, EventArgs e)
        {
            AddFolder();
        }

        private void mniFile_OpenInExplorer_Click(object sender, EventArgs e)
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            FileHelper.OpenInExplorer(selectedEntry.Location, selectedEntry.Type != ProjectEntryType.Folder);
        }

        private void mniFile_Delete_Click(object sender, EventArgs e)
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            if (selectedEntry.Parent == null) // root element
                return;

            if (MessageBox.Show($"Soll der Eintrag \"{selectedEntry.Location}\" wirklich entfernt werden?", "Eintrag entfernen",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            TreeNode node = FindNodeById(selectedEntry.Id);
            if (node == null)
            {
                MessageBox.Show("Das TreeNode Element konnte nicht gefunden werden.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!selectedEntry.Remove())
            {
                MessageBox.Show("Das Element konnte nicht entfernt werden.", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            node.Remove();

            _projectManager.Save(
                selectedEntry.Project.ProjectFilePath);
        }

        private void mniFile_Startentry_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = trvEntities.SelectedNode;
            ProjectEntry selectedEntry = (ProjectEntry)selectedNode.Tag;

            // remove old start entry
            if (_projectManager.CurrentProject.StartEntry != null)
            {
                TreeNode oldStartNode = FindNodeById(_projectManager.CurrentProject.StartEntry.Id);
                _projectManager.CurrentProject.StartEntry = null;
                RefreshNodeIcon(oldStartNode);
            }

            // set new start entry
            _projectManager.CurrentProject.StartEntry = selectedEntry;
            _projectManager.Save(_projectManager.CurrentProject.ProjectFilePath);

            RefreshNodeIcon(selectedNode);
        }

        #endregion

        #region TreeView events

        private void trvEntities_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propEntry.SelectedObject = e.Node.Tag;

            if (SelectedEntryChanged != null)
                SelectedEntryChanged(this, EventArgs.Empty);

            RefreshToolbar();
        }

        private void tvwEntries_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = trvEntities.GetNodeAt(e.Location);
            if (clickedNode != null)
            {
                trvEntities.SelectedNode = clickedNode;
            }

            if (_projectManager.HasProject && e.Button == MouseButtons.Right)
            {
                contextMenuFile.Show(MousePosition);
            }
        }

        private void trvEntities_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ((ProjectEntry)trvEntities.SelectedNode.Tag).IsExpanded = true;
        }

        private void trvEntities_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ((ProjectEntry)trvEntities.SelectedNode.Tag).IsExpanded = false;
        }

        private void trvEntities_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level == 0)
                e.Cancel = true;
        }

        private void trvEntities_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;
            switch (selectedEntry.Type)
            {
                case ProjectEntryType.Folder:
                case ProjectEntryType.ProjectFolder:
                    return;

                case ProjectEntryType.File:
                case ProjectEntryType.LinkedFile:
                    if (OpenFile != null)
                        OpenFile(this, new ProjectEntryEventArgs(selectedEntry));

                    return;

                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Toolbar events

        private void tbbAddFile_Click(object sender, EventArgs e)
        {
            AddFile();
        }

        private void tbbAddFolder_Click(object sender, EventArgs e)
        {
            AddFolder();
        }

        #endregion

        #region Events2

        public event EventHandler<ProjectEntryEventArgs> OpenFile;
        public event EventHandler SelectedEntryChanged;

        #endregion

        #region Project manager events

        private void _projectManager_ProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            RefreshToolbar();

            if (_projectManager != null)
                propEntry.Enabled = _projectManager.HasProject;
        }

        #endregion

        #region Methods

        private void RefreshToolbar()
        {
            if (trvEntities.SelectedNode == null)
            {
                tbbAddFile.Enabled = false;
                tbbAddFolder.Enabled = false;
            }
            else
            {
                ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;

                bool isFolder = (selectedEntry.Type == ProjectEntryType.Folder || selectedEntry.Type == ProjectEntryType.ProjectFolder);
                bool isFile = (selectedEntry.Type == ProjectEntryType.File);

                tbbAddFile.Enabled = isFolder;
                tbbAddFolder.Enabled = isFolder;
            }
        }

        private void RefreshContextMenu()
        {
            ProjectEntry selectedEntry = (ProjectEntry)trvEntities.SelectedNode.Tag;

            bool isFolder = (selectedEntry.Type == ProjectEntryType.Folder || selectedEntry.Type == ProjectEntryType.ProjectFolder);
            bool isFile = (selectedEntry.Type == ProjectEntryType.File);

            mniFile_AddFile.Enabled = isFolder;
            mniFile_AddExistingFile.Enabled = isFolder;
            mniFile_AddFolder.Enabled = isFolder;

            mniFile_Startentry.Enabled = isFile;
        }

        public void SetProjectManager(ProjectManager projectManager)
        {
            if (projectManager == null)
                throw new ArgumentNullException(nameof(projectManager));

            _projectManager = projectManager;
            _projectManager.ProjectChanged += _projectManager_ProjectChanged;
        }

        private int GetImageIndexForEntry(ProjectEntry entry)
        {
            switch (entry.Type)
            {
                case ProjectEntryType.Folder:
                case ProjectEntryType.ProjectFolder:
                    return 2;

                case ProjectEntryType.File:
                    if (entry.Project.StartEntry != null && entry.Project.StartEntry.Id == entry.Id)
                    {
                        return 5;
                    }
                    else
                    {
                        return 1;
                    }

                case ProjectEntryType.LinkedFile:
                    return 4;

                default:
                    throw new NotImplementedException();
            }
        }

        private string GetEntryNodeName(ProjectEntry entry)
        {
            switch (entry.Type)
            {
                case ProjectEntryType.Folder:
                case ProjectEntryType.File:
                    return entry.Location;

                case ProjectEntryType.LinkedFile:
                    return Path.GetFileName(entry.Location);

                case ProjectEntryType.ProjectFolder:
                    return entry.Project.Name;

                default:
                    throw new NotImplementedException();
            }
        }

        private void RefreshNodeIcon(TreeNode node)
        {
            ProjectEntry entry = (ProjectEntry)node.Tag;

            int imageIndex = GetImageIndexForEntry(entry);

            node.ImageIndex = imageIndex;
            node.SelectedImageIndex = imageIndex;
        }

        private TreeNode AddEntryToTree(TreeNodeCollection parentNodes, ProjectEntry entry)
        {
            TreeNode node = new TreeNode(GetEntryNodeName(entry));
            node.Tag = entry;
            node.ImageIndex = GetImageIndexForEntry(entry);
            node.SelectedImageIndex = GetImageIndexForEntry(entry);

            parentNodes.Add(node);

            if (entry.Type == ProjectEntryType.Folder
                && entry.IsExpanded)
            {
                node.Expand();
            }

            return node;
        }

        private void InitTree(TreeNode parentNode, IReadOnlyCollection<ProjectEntry> entries)
        {
            TreeNodeCollection parentCollection;
            if (parentNode == null)
            {
                parentCollection = trvEntities.Nodes;
            }
            else
            {
                parentCollection = parentNode.Nodes;
            }

            foreach (var entry in entries)
            {
                TreeNode node = AddEntryToTree(parentCollection, entry);
                InitTree(node, entry.Children);
            }
        }

        public void SetProject(ProjectSettings settings)
        {
            if (settings == null)
            {
                // close
                trvEntities.Nodes.Clear();
                propEntry.SelectedObject = null;
            }
            else
            {
                // open
                trvEntities.BeginUpdate();
                trvEntities.Nodes.Clear();

                InitTree(null, settings.Entries);
                trvEntities.SelectedNode = trvEntities.Nodes[0];

                ExpandTree(trvEntities.Nodes[0]);

                trvEntities.EndUpdate();
            }
        }

        private void ExpandTree(TreeNode node)
        {
            if (((ProjectEntry)node.Tag).IsExpanded)
            {
                node.Expand();
            }

            foreach (TreeNode childNode in node.Nodes)
            {
                ExpandTree(childNode);
            }
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SplitterDistance
        {
            get { return splitterProperties.SplitPosition; }
            set { splitterProperties.SplitPosition = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EditorSettings Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        #endregion
    }
}