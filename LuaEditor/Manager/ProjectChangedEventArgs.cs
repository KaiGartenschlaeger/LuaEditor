using LuaEditor.Objetcts;
using System;

namespace LuaEditor.Manager
{
    public class ProjectChangedEventArgs : EventArgs
    {
        #region Fields

        private ProjectSettings _oldProject;
        private ProjectSettings _newProject;

        #endregion

        #region Constructor

        public ProjectChangedEventArgs(ProjectSettings oldProject, ProjectSettings newProject)
        {
            _oldProject = oldProject;
            _newProject = newProject;
        }

        #endregion

        #region Properties

        public ProjectSettings OldProject
        {
            get { return _oldProject; }
        }

        public ProjectSettings NewProject
        {
            get { return _newProject; }
        }

        #endregion
    }
}