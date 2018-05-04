using PureSoft.Controls.VisualStudio.Colors;
using System.Drawing;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio.Renderer
{
    public class Vs2010StatusStripRenderer : ToolStripProfessionalRenderer
    {
        private Vs2010StatusStripColorTable _colorTable;
        private Rectangle[] _baseSizeGripRectangles;

        public Vs2010StatusStripRenderer()
            : this(new Vs2010DefaultStatusStripColorTable())
        {
            _baseSizeGripRectangles = new Rectangle[] {
                new Rectangle(8, 0, 2, 2),
                new Rectangle(8, 4, 2, 2),
                new Rectangle(8, 8, 2, 2),
                new Rectangle(4, 4, 2, 2),
                new Rectangle(4, 8, 2, 2),
                new Rectangle(0, 8, 2, 2) };
        }

        public Vs2010StatusStripRenderer(Vs2010StatusStripColorTable colorTable)
        {
            _colorTable = colorTable;
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            Rectangle rect = new Rectangle(0, 0, e.AffectedBounds.Width, e.AffectedBounds.Height);
            using (SolidBrush b = new SolidBrush(_colorTable.Common.WindowBackground))
            {
                e.Graphics.FillRectangle(b, rect);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // base.OnRenderToolStripBorder(e);
        }

        private bool IsZeroWidthOrHeight(Rectangle rectangle)
        {
            if (rectangle.Width == 0)
            {
                return true;
            }

            return rectangle.Height == 0;
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = _colorTable.LabelTextColor;

            base.OnRenderItemText(e);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.GripStyle != ToolStripGripStyle.Visible)
                return;

            StatusStrip toolStrip = e.ToolStrip as StatusStrip;
            if (toolStrip != null)
            {
                Rectangle sizeGripBounds = toolStrip.SizeGripBounds;
                if (!IsZeroWidthOrHeight(sizeGripBounds))
                {
                    Rectangle[] rectangleArray = new Rectangle[_baseSizeGripRectangles.Length];
                    //Rectangle[] rectangleArray1 = new Rectangle[_baseSizeGripRectangles.Length];

                    for (int i = 0; i < _baseSizeGripRectangles.Length; i++)
                    {
                        Rectangle width = _baseSizeGripRectangles[i];
                        if (toolStrip.RightToLeft == RightToLeft.Yes)
                        {
                            width.X = sizeGripBounds.Width - width.X - width.Width;
                        }
                        width.Offset(sizeGripBounds.X, sizeGripBounds.Bottom - 12);
                        rectangleArray[i] = width;

                        if (toolStrip.RightToLeft != RightToLeft.Yes)
                        {
                            width.Offset(-1, -1);
                        }
                        else
                        {
                            width.Offset(1, -1);
                        }

                        //rectangleArray1[i] = width;
                    }

                    //e.Graphics.FillRectangles(new SolidBrush(_colorTable.Border), rectangleArray);
                    e.Graphics.FillRectangles(new SolidBrush(_colorTable.Grip), rectangleArray);
                }
            }
        }
    }
}