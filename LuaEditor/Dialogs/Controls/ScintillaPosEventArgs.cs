using System;

namespace LuaEditor.Dialogs.Controls
{
    public class ScintillaPosEventArgs : EventArgs
    {
        #region Fields

        private int _columnIndex;
        private int _lineIndex;

        #endregion

        #region Constructor

        public ScintillaPosEventArgs(int columnIndex, int lineIndex)
        {
            _columnIndex = columnIndex;
            _lineIndex = lineIndex;
        }

        #endregion

        #region Properties

        public int ColumnIndex
        {
            get { return _columnIndex; }
        }

        public int LineIndex
        {
            get { return _lineIndex; }
        }

        #endregion
    }
}