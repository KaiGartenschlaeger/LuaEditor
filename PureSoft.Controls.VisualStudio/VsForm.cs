using PureSoft.Controls.VisualStudio.Colors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio
{
    public class VsForm : Form
    {
        #region Fields

        private Vs2010CommonColorTable _colorTable;

        #endregion

        #region Constructor

        public VsForm()
            : this(new Vs2010DefaultCommonColorTable())
        {
        }

        public VsForm(Vs2010CommonColorTable colorTable)
        {
            if (colorTable == null)
                throw new ArgumentNullException(nameof(colorTable));

            _colorTable = colorTable;

            BackColor = _colorTable.WindowBackground;
        }

        #endregion

        #region Properties

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Vs2010CommonColorTable ColorTable
        {
            get { return _colorTable; }
        }

        #endregion
    }
}