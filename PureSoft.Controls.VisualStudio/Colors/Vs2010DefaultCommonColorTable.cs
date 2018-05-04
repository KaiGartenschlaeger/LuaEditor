using System;
using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public class Vs2010DefaultCommonColorTable : Vs2010CommonColorTable
    {
        public override Color CheckedBackground
        {
            get { return Color.FromArgb(255, 239, 187); }
        }

        public override Color CheckedSelectedBackground
        {
            get { return Color.FromArgb(255, 252, 244); }
        }

        public override Color PressedBackground
        {
            get { return Color.FromArgb(255, 232, 166); }
        }

        public override Color SelectedBorder
        {
            get { return Color.FromArgb(229, 195, 101); }
        }

        public override Color SelectedGradientBottom
        {
            get { return Color.FromArgb(255, 236, 181); }
        }

        public override Color SelectedGradientMiddle
        {
            get { return Color.FromArgb(255, 244, 208); }
        }

        public override Color SelectedGradientTop
        {
            get { return Color.FromArgb(255, 252, 242); }
        }

        public override Color TextColor
        {
            get { return Color.FromArgb(59, 41, 62); }
        }

        public override Color DropdownBorder
        {
            get { return Color.FromArgb(155, 167, 183); }
        }

        public override Color Arrow
        {
            get { return Color.Black; }
        }

        public override Color HeaderGradiantStart
        {
            get { return Color.FromArgb(77, 96, 130); }
        }

        public override Color HeaderGradiantEnd
        {
            get { return Color.FromArgb(61, 82, 119); }
        }

        public override Color HeaderTextColor
        {
            get { return Color.White; }
        }

        public override Color WindowBackground
        {
            get { return Color.FromArgb(44, 61, 90); }
        }
    }
}