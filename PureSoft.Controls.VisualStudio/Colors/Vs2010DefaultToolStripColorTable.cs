using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public class Vs2010DefaultToolStripColorTable : Vs2010ToolStripColorTable
    {
        private Vs2010CommonColorTable _CommonColorTable;

        public Vs2010DefaultToolStripColorTable()
        {
            _CommonColorTable = new Vs2010DefaultCommonColorTable();
        }

        public override Color WindowBackground
        {
            get { return Color.FromArgb(41, 57, 85); }
        }

        public override Vs2010CommonColorTable CommonColorTable
        {
            get { return _CommonColorTable; }
        }

        public override Color Background
        {
            get { return Color.FromArgb(188, 199, 216); }
        }

        public override Color Border
        {
            get { return Color.FromArgb(201, 210, 225); }
        }

        public override Color Grip
        {
            get { return Color.FromArgb(98, 115, 141); }
        }

        public override Color ParentBackground
        {
            get { return Color.FromArgb(156, 170, 193); }
        }

        public override Color Separator
        {
            get { return Color.FromArgb(133, 145, 162); }
        }

        public override Color OverflowBackground
        {
            get { return Color.FromArgb(213, 220, 232); }
        }
    }
}