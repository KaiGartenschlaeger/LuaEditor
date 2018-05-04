using LuaEditor.Objetcts;
using System;

namespace LuaEditor.Dialogs.Controls
{
    public class ProjectEntryEventArgs : EventArgs
    {
        #region Fields

        private ProjectEntry _entry;

        #endregion

        #region Constructor

        public ProjectEntryEventArgs(ProjectEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            _entry = entry;
        }

        #endregion

        #region Properties

        public ProjectEntry Entry
        {
            get { return _entry; }
        }

        #endregion
    }
}