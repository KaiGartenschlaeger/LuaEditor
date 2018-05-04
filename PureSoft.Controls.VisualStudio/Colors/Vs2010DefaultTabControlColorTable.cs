using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public class Vs2010DefaultTabControlColorTable : Vs2010TabControlColorTable
    {
        private Vs2010CommonColorTable _CommonColorTable;

        public Vs2010DefaultTabControlColorTable()
        {
            _CommonColorTable = new Vs2010DefaultCommonColorTable();
        }

        public override Vs2010CommonColorTable CommonColorTable
        {
            get { return _CommonColorTable; }
        }

        public override Color CloseButtonBorder
        {
            get { return Color.FromArgb(229, 195, 101); }
        }

        public override Color CloseButtonClickedBackColor
        {
            get { return Color.FromArgb(255, 232, 166); }
        }

        public override Color CloseButtonHoverBackColor
        {
            get { return Color.FromArgb(255, 252, 244); }
        }

        public override Color Background
        {
            get { return Color.FromArgb(43, 60, 88); }
        }

        public override Color BackgroundDotsLight
        {
            get { return Color.FromArgb(53, 73, 106); }
        }

        public override Color BackgroundDotsDark
        {
            get { return Color.FromArgb(41, 57, 85); }
        }

        public override Color InactiveColor
        {
            get { return Color.FromArgb(69, 89, 125); }
        }

        public override Color Border
        {
            get { return Color.FromArgb(255, 236, 181); }
        }

        public override Color HoveringHeaderGradientBottom
        {
            get { return Color.FromArgb(53, 73, 106); }
        }

        public override Color HoveringHeaderGradientTop
        {
            get { return Color.FromArgb(111, 119, 118); }
        }

        public override Color SelectedHeaderGradientBottom
        {
            get { return Color.FromArgb(255, 236, 181); }
        }

        public override Color SelectedHeaderGradientMiddle
        {
            get { return Color.FromArgb(255, 244, 208); }
        }

        public override Color SelectedHeaderGradientTop
        {
            get { return Color.FromArgb(255, 252, 242); }
        }

        public override Color HoveringHeaderBorder
        {
            get { return Color.FromArgb(155, 167, 183); }
        }
    }
}