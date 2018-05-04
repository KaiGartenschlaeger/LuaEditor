using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace LuaEditor.Objetcts
{
    public class ProjectEntry
    {
        #region Fields

        private readonly ProjectSettings _project;
        private readonly ProjectEntryType _type;
        private string _location;
        private bool _isExpanded;
        private string _id;
        private ProjectEntry _parent;
        private readonly List<ProjectEntry> _children;

        #endregion

        #region Constructor

        public ProjectEntry(ProjectSettings project, ProjectEntryType type, string path, string id, bool isExpanded)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            _project = project;
            _type = type;
            _location = path;
            _id = id;
            _isExpanded = isExpanded;

            _children = new List<ProjectEntry>();
        }

        #endregion

        public void Add(ProjectEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            entry._parent = this;

            _children.Add(entry);
        }

        public bool Remove()
        {
            if (_parent == null)
                return false;

            return _parent._children.Remove(this);
        }

        public string GetAbsolutePath()
        {
            switch (_type)
            {
                case ProjectEntryType.ProjectFolder:
                case ProjectEntryType.LinkedFile:
                    return _location;

                case ProjectEntryType.Folder:
                case ProjectEntryType.File:
                    return Path.Combine(_parent.GetAbsolutePath(), _location);

                default:
                    throw new NotImplementedException();
            }
        }

        #region Properties

        [Browsable(true)]
        public string Location
        {
            get { return _location; }
        }

        [Browsable(true)]
        public ProjectEntryType Type
        {
            get { return _type; }
        }

        [Browsable(false)]
        public IReadOnlyCollection<ProjectEntry> Children
        {
            get { return _children; }
        }

        [Browsable(false)]
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { _isExpanded = value; }
        }

        [Browsable(false)]
        public string Id
        {
            get { return _id; }
        }

        [Browsable(false)]
        public ProjectEntry Parent
        {
            get { return _parent; }
        }

        [Browsable(false)]
        public ProjectSettings Project
        {
            get { return _project; }
        }

        #endregion
    }
}