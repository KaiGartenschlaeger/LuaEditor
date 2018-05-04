using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public class Vs2010DefaultMenuStripColorTable : Vs2010MenuStripColorTable
    {
        private Vs2010CommonColorTable _CommonColorTable;

        public Vs2010DefaultMenuStripColorTable()
        {
            _CommonColorTable = new Vs2010DefaultCommonColorTable();
        }

        public override Vs2010CommonColorTable CommonColorTable
        {
            get { return _CommonColorTable; }
        }

        public override Color BottomBorder
        {
            get { return Color.FromArgb(156, 170, 193); }
        }

        public override Color BackgroundGradientBottom
        {
            get { return Color.FromArgb(175, 186, 206); }
        }

        public override Color BackgroundGradientTop
        {
            get { return Color.FromArgb(202, 211, 226); }
        }

        public override Color DropdownGradientBottom
        {
            get { return Color.FromArgb(208, 215, 226); }
        }

        public override Color DropdownGradientTop
        {
            get { return Color.FromArgb(233, 236, 238); }
        }

        public override Color DroppedDownItemBackground
        {
            get { return Color.FromArgb(233, 236, 238); }
        }

        public override Color ImageMargin
        {
            get { return Color.FromArgb(233, 236, 238); }
        }

        public override Color Separator
        {
            get { return Color.FromArgb(190, 195, 203); }
        }

    }
}