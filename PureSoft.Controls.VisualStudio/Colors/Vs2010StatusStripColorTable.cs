using System;
using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public abstract class Vs2010StatusStripColorTable
    {
        private Vs2010CommonColorTable _commonColorTable;

        public Vs2010StatusStripColorTable()
            : this(new Vs2010DefaultCommonColorTable())
        {
        }

        public Vs2010StatusStripColorTable(Vs2010CommonColorTable commonColorsTable)
        {
            if (commonColorsTable == null)
                throw new ArgumentNullException(nameof(commonColorsTable));

            _commonColorTable = commonColorsTable;
        }

        public abstract Color LabelTextColor { get; }

        public abstract Color Grip { get; }

        public Vs2010CommonColorTable Common
        {
            get { return _commonColorTable; }
        }
    }
}