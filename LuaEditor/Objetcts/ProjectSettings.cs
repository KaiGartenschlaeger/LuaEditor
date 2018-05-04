using System;
using System.Collections.Generic;
using System.IO;

namespace LuaEditor.Objetcts
{
    public class ProjectSettings
    {
        #region Fields

        private string _name;
        private string _id;
        private string _projectFilePath;
        private string _rootDirectory;

        private readonly List<ProjectEntry> _entries;

        private ProjectEntry _startEntry;

        #endregion

        #region Constructor

        public ProjectSettings(string projectName, string projectFile, string projectId)
        {
            if (string.IsNullOrEmpty(projectName))
                throw new ArgumentNullException(nameof(projectName));

            _name = projectName;
            _projectFilePath = projectFile;
            _rootDirectory = Path.GetDirectoryName(projectFile);
            _id = projectId;

            _entries = new List<ProjectEntry>();
            _entries.Add(new ProjectEntry(
                project: this,
                type: ProjectEntryType.ProjectFolder,
                path: _rootDirectory,
                id: _id,
                isExpanded: true));
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(Name));

                _name = value;
            }
        }

        public string RootDirectory
        {
            get { return _rootDirectory; }
        }

        public List<ProjectEntry> Entries
        {
            get { return _entries; }
        }

        public ProjectEntry StartEntry
        {
            get { return _startEntry; }
            set { _startEntry = value; }
        }

        public string Id
        {
            get { return _id; }
        }

        public string ProjectFilePath
        {
            get { return _projectFilePath; }
        }

        #endregion
    }
}