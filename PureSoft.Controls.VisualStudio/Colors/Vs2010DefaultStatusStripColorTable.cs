using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public class Vs2010DefaultStatusStripColorTable : Vs2010StatusStripColorTable
    {
        public override Color LabelTextColor
        {
            get { return Color.White; }
        }

        public override Color Grip
        {
            get { return Color.FromArgb(98, 115, 141); }
        }
    }
}