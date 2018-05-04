using PureSoft.Controls.VisualStudio.Colors;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio
{
    public class Vs2010ToolPaneHeader : Control
    {
        private Vs2010CommonColorTable _colorTable;

        public Vs2010ToolPaneHeader()
            : this(new Vs2010DefaultCommonColorTable())
        {
        }

        public Vs2010ToolPaneHeader(Vs2010CommonColorTable colorTable)
        {
            if (colorTable == null)
                throw new ArgumentNullException(nameof(colorTable));

            _colorTable = colorTable;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            LinearGradientBrush backgroundBrush = new LinearGradientBrush(
                point1: new Point(0, 0),
                point2: new Point(0, Height - 1),
                color1: _colorTable.HeaderGradiantStart,
                color2: _colorTable.HeaderGradiantEnd);

            try
            {
                e.Graphics.FillRectangle(
                    backgroundBrush,
                    new Rectangle(0, 0, Width, Height));
            }
            finally
            {
                backgroundBrush.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                Brush textBrush = new SolidBrush(
                    _colorTable.HeaderTextColor);

                try
                {
                    SizeF fontSize = e.Graphics.MeasureString(
                        Text,
                        Font);

                    e.Graphics.DrawString(Text, Font,
                        new SolidBrush(_colorTable.HeaderTextColor),
                        5,
                        Height / 2 - fontSize.Height / 2);
                }
                finally
                {
                    textBrush.Dispose();
                }
            }
        }
    }
}