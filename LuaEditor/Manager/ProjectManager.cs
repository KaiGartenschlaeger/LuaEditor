using LuaEditor.Helper;
using LuaEditor.Objetcts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

namespace LuaEditor.Manager
{
    public class ProjectManager
    {
        #region Consts

        private const int MaxLastProjects = 10;
        private const int CurrentVersion = 2;

        #endregion

        #region Fields

        private ProjectSettings _currentProject;
        private readonly List<string> _lastProjects;

        #endregion

        #region Constructor

        public ProjectManager()
        {
            _lastProjects = new List<string>();
        }

        #endregion

        #region Helper

        private void RaiseProjectChangedEvent(ProjectSettings oldProject, ProjectSettings newProject)
        {
            if (ProjectChanged != null)
            {
                ProjectChanged(this,
                    new ProjectChangedEventArgs(oldProject, newProject));
            }
        }

        #endregion

        #region Methods

        public void AddToLastProjects(string projectFile, bool moveToTop = true)
        {
            if (string.IsNullOrEmpty(projectFile))
                return;

            int index = _lastProjects.IndexOf(projectFile);
            if (index == -1)
            {
                while (_lastProjects.Count > MaxLastProjects)
                {
                    _lastProjects.RemoveAt(_lastProjects.Count - 1);
                }
            }

            if (moveToTop)
            {
                if (index != -1)
                {
                    _lastProjects.RemoveAt(index);
                }

                _lastProjects.Insert(0, projectFile);
            }
            else
            {
                if (index == -1)
                {
                    _lastProjects.Add(projectFile);
                }
            }
        }

        public void RemoveLastProject(string projectFilePath)
        {
            int index = _lastProjects.IndexOf(projectFilePath);
            if (index != -1)
            {
                _lastProjects.RemoveAt(index);
            }
        }

        public void Save(string projectFilePath)
        {
            if (_currentProject == null)
                throw new Exception("Speichern kann nicht aufgerufen werden, wenn kein Projekt geöffnet ist.");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;

            using (FileStream stream = new FileStream(projectFilePath, FileMode.Create))
            {
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    writer.WriteStartDocument(true);

                    writer.WriteStartElement("Project");

                    writer.WriteElementString("Version", $"{CurrentVersion}.00");
                    writer.WriteElementString("Name", _currentProject.Name);
                    writer.WriteElementString("Id", _currentProject.Id);

                    if (_currentProject.StartEntry != null)
                        writer.WriteElementString("StartElement", _currentProject.StartEntry.Id);

                    WriteEntriesToXml(writer, _currentProject.Entries[0].Children);

                    writer.WriteEndElement();
                }
            }
        }

        private void WriteEntriesToXml(XmlWriter writer, IReadOnlyCollection<ProjectEntry> entries)
        {
            writer.WriteStartElement("Entries");
            foreach (ProjectEntry entry in entries)
            {
                writer.WriteStartElement("Entry");
                writer.WriteElementString("Type", entry.Type.ToString());
                writer.WriteElementString("Id", entry.Id);
                writer.WriteElementString("Path", entry.Location);

                if (entry.Type == ProjectEntryType.Folder)
                {
                    writer.WriteElementString("Expanded", entry.IsExpanded.ToString());
                }

                foreach (var childEntry in entry.Children)
                {
                    WriteEntriesToXml(writer, entry.Children);
                }

                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        public ProjectSettings Open(string projectFilePath)
        {
            // open file
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(File.ReadAllText(projectFilePath, Encoding.UTF8));

            // find required nodes
            XmlNode projNode = XmlDocumentHelper.GetNode(doc, "Project");
            XmlNode versionNode = XmlDocumentHelper.GetNode(projNode, "Version");
            XmlNode nodeId = XmlDocumentHelper.GetNode(projNode, "Id");
            XmlNode nameNode = XmlDocumentHelper.GetNode(projNode, "Name");
            XmlNode entriesNode = XmlDocumentHelper.GetNode(projNode, "Entries");
            XmlNode startElementNode = XmlDocumentHelper.GetNode(projNode, "StartElement", isRequired: false);

            // check version
            Version projectVersion = Version.Parse(versionNode.InnerText);
            if (projectVersion.Major < 1 || projectVersion.Major > CurrentVersion)
            {
                throw new Exception("Die Version der Projektdatei wird nicht unterstützt");
            }

            // create project settings object
            ProjectSettings proj = new ProjectSettings(
                nameNode.InnerText,
                projectFilePath,
                nodeId.InnerText);

            ReadEntries(
                proj,
                projectVersion,
                proj.Entries[0],
                entriesNode.ChildNodes,
                startElementNode != null ? startElementNode.InnerText : null);

            return proj;
        }

        private void ReadEntries(ProjectSettings proj, Version projectVersion, ProjectEntry parentEntry, XmlNodeList nodes, string startElementId)
        {
            foreach (XmlNode node in nodes)
            {
                ProjectEntryType entryType = (ProjectEntryType)Enum.Parse(typeof(ProjectEntryType),
                    XmlDocumentHelper.GetNode(node, "Type").InnerText);

                string entryPath = XmlDocumentHelper.GetNode(node, "Path").InnerText;
                string entryId = XmlDocumentHelper.GetNode(node, "Id").InnerText;

                XmlNode nodeExpanded = XmlDocumentHelper.GetNode(node, "Expanded", isRequired: false);
                XmlNode nodeEntries = XmlDocumentHelper.GetNode(node, "Entries", isRequired: false);

                bool isExpanded = false;
                if (nodeExpanded != null)
                {
                    isExpanded = Convert.ToBoolean(nodeExpanded.InnerText);
                }

                if (projectVersion.Major < 2)
                {
                    // old projects have used absolute paths, so we need to convert them
                    if (parentEntry != null)
                    {
                        if (entryPath.StartsWith(proj.RootDirectory, StringComparison.OrdinalIgnoreCase))
                        {
                            entryPath = PathHelper.ToRelativePath(entryPath,
                                parentEntry.GetAbsolutePath());
                        }
                        else
                        {
                            entryType = ProjectEntryType.LinkedFile;
                        }
                    }
                }

                ProjectEntry entry = new ProjectEntry(
                    project: proj,
                    type: entryType,
                    path: entryPath,
                    id: entryId,
                    isExpanded: isExpanded);

                if (entry.Id == startElementId)
                    proj.StartEntry = entry;

                parentEntry.Add(entry);

                if (nodeEntries != null)
                {
                    ReadEntries(proj, projectVersion, entry, nodeEntries.ChildNodes, startElementId);
                }
            }
        }

        public ProjectSettings Create(string projectName, string projectFilePath)
        {
            return new ProjectSettings(
                projectName, projectFilePath, GuidHelper.Create());
        }

        public void ChangeProject(ProjectSettings newProject)
        {
            if (newProject == null)
            {
                throw new ArgumentNullException(nameof(newProject));
            }

            if (_currentProject != newProject)
            {
                ProjectSettings oldProject = _currentProject;
                _currentProject = newProject;

                RaiseProjectChangedEvent(oldProject, newProject);
            }
        }

        public void CloseProject()
        {
            if (_currentProject != null)
            {
                _currentProject = null;

                RaiseProjectChangedEvent(_currentProject, null);
            }
        }

        #endregion

        #region Events

        public event EventHandler<ProjectChangedEventArgs> ProjectChanged;

        #endregion

        #region Properties

        public ProjectSettings CurrentProject
        {
            get { return _currentProject; }
        }

        public bool HasProject
        {
            get { return _currentProject != null; }
        }

        public ReadOnlyCollection<string> LastProjects
        {
            get { return _lastProjects.AsReadOnly(); }
        }

        #endregion
    }
}