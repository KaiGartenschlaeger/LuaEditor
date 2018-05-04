using PureSoft.Controls.VisualStudio.Colors;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio.Renderer
{
    public class Vs2010TabControlRenderer
    {
        #region Fields

        private Vs2010TabControlColorTable _colorTable;

        #endregion

        #region Constructor

        public Vs2010TabControlRenderer()
            : this(new Vs2010DefaultTabControlColorTable())
        {
        }

        public Vs2010TabControlRenderer(Vs2010TabControlColorTable colorTable)
        {
            ColorTable = colorTable;
        }

        #endregion

        #region Helper

        private void DrawSelectedTabHeader(Graphics g, RectangleF rect)
        {
            float roundRadius = 2f;

            using (LinearGradientBrush brush = new LinearGradientBrush(rect,
                ColorTable.SelectedHeaderGradientTop,
                ColorTable.SelectedHeaderGradientMiddle,
                LinearGradientMode.Vertical))
            {
                dynamic b = new ColorBlend(4);
                b.Colors = new Color[] {
                    ColorTable.SelectedHeaderGradientTop,
                    ColorTable.SelectedHeaderGradientMiddle,
                    ColorTable.SelectedHeaderGradientBottom,
                    ColorTable.SelectedHeaderGradientBottom
                };

                b.Positions = new float[] {
                    0f,
                    0.5f,
                    0.5f,
                    1f
                };

                brush.InterpolationColors = b;

                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(rect.X + roundRadius, rect.Y, rect.Right - 2 * roundRadius, rect.Y);
                    gp.AddArc(rect.Right - 2 * roundRadius, rect.Y, 2 * roundRadius, 2 * roundRadius, 270, 90);
                    gp.AddLine(rect.Right, rect.Y + roundRadius, rect.Right, rect.Bottom);
                    gp.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                    gp.AddLine(rect.X, rect.Bottom, rect.X, rect.Y);
                    gp.AddArc(rect.X, rect.Y, roundRadius * 2, roundRadius * 2, 180, 90);
                    gp.CloseFigure();

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillPath(brush, gp);
                    g.SmoothingMode = SmoothingMode.Default;
                }
            }
        }

        private void DrawInactiveTabHeader(Graphics g, RectangleF rect)
        {
            float roundRadius = 2f;
            using (SolidBrush brush = new SolidBrush(ColorTable.InactiveColor))
            {
                using (GraphicsPath gp = new GraphicsPath())
                {
                    gp.AddLine(rect.X + roundRadius, rect.Y, rect.Right - 2 * roundRadius, rect.Y);
                    gp.AddArc(rect.Right - 2 * roundRadius, rect.Y, 2 * roundRadius, 2 * roundRadius, 270, 90);
                    gp.AddLine(rect.Right, rect.Y + roundRadius, rect.Right, rect.Bottom);
                    gp.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                    gp.AddLine(rect.X, rect.Bottom, rect.X, rect.Y);
                    gp.AddArc(rect.X, rect.Y, roundRadius * 2, roundRadius * 2, 180, 90);
                    gp.CloseFigure();

                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.FillPath(brush, gp);
                    g.SmoothingMode = SmoothingMode.Default;
                }
            }
        }

        private void DrawHoveringTabHeader(Graphics g, Rectangle rect)
        {
            rect = new Rectangle(rect.X, rect.Y + 1, rect.Width - 2, rect.Height + 1);
            Rectangle innerRect = new Rectangle(rect.X + 1, rect.Y, rect.Width - 1, rect.Height - 3);

            using (LinearGradientBrush b = new LinearGradientBrush(innerRect,
                ColorTable.HoveringHeaderGradientTop,
                ColorTable.HoveringHeaderGradientBottom,
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(b, innerRect);
            }
            using (Pen p = new Pen(ColorTable.HoveringHeaderBorder))
            {
                CommonDrawing.DrawRoundedRectangle(g, p, rect.X, rect.Y, rect.Width, rect.Height, 2);
            }
        }

        private void DrawCloseButtonBorder(Graphics g, Rectangle rect)
        {
            using (Pen p = new Pen(ColorTable.CloseButtonBorder))
            {
                g.DrawRectangle(p, rect);
            }
        }

        #endregion

        #region Draw overrides

        #endregion

        #region Methods

        public virtual void OnPaintTabHeader(PaintHeaderEventArgs e)
        {
            Color textColor = default(Color);

            switch (e.State)
            {
                case PaintHeaderEventArgs.TabHeaderState.Active:
                    DrawSelectedTabHeader(e.Graphics, e.ClipRectangle);
                    textColor = Color.Black;
                    break;

                case PaintHeaderEventArgs.TabHeaderState.HotTracking:
                    DrawHoveringTabHeader(e.Graphics, e.ClipRectangle);
                    textColor = Color.White;
                    break;

                case PaintHeaderEventArgs.TabHeaderState.Inactive:
                    DrawInactiveTabHeader(e.Graphics, e.ClipRectangle);
                    textColor = Color.White;
                    break;

                default:
                    textColor = Color.White;
                    break;
            }

            Rectangle textRect = e.ClipRectangle;

            //textRect.Inflate(-2, 0)
            //textRect.Width -= e.TabPage.GetCloseButtonRectangle().Width + 2
            //textRect.Offset(1, 0)

            TextRenderer.DrawText(e.Graphics, e.TabPage.Text, e.TabPage.Font, textRect, textColor,
                TextFormatFlags.VerticalCenter);
        }

        public virtual void OnPaintTabHeadersBackground(PaintEventArgs e)
        {
            using (HatchBrush h1 = new HatchBrush(HatchStyle.Percent20,
                ColorTable.BackgroundDotsLight, ColorTable.Background))
            {
                e.Graphics.FillRectangle(h1, e.ClipRectangle);
            }

            using (HatchBrush h2 = new HatchBrush(HatchStyle.Percent20,
                ColorTable.BackgroundDotsDark, Color.Transparent))
            {
                e.Graphics.RenderingOrigin = new Point(0, -1);
                e.Graphics.FillRectangle(h2, e.ClipRectangle);
                e.Graphics.RenderingOrigin = Point.Empty;
            }
        }

        public virtual void OnPaintBorders(PaintBordersEventArgs e)
        {
            Color c = default(Color);
            if (e.Active)
            {
                c = ColorTable.Border;
            }
            else
            {
                c = ColorTable.InactiveColor;
            }

            using (SolidBrush b = new SolidBrush(c))
            {
                e.Graphics.FillRectangle(b, e.ClipRectangle);
            }
        }

        public void OnPaintCloseButton(PaintCloseButtonEventArgs e)
        {
            if (e.CloseButtonState == PaintCloseButtonEventArgs.CloseButtonStates.None)
                return;

            switch (e.CloseButtonState)
            {
                case PaintCloseButtonEventArgs.CloseButtonStates.Clicked:
                    using (SolidBrush b = new SolidBrush(this.ColorTable.CloseButtonClickedBackColor))
                    {
                        e.Graphics.FillRectangle(b, e.ClipRectangle);
                    }

                    DrawCloseButtonBorder(e.Graphics, e.ClipRectangle);
                    break;

                case PaintCloseButtonEventArgs.CloseButtonStates.Hovering:
                    using (SolidBrush b = new SolidBrush(this.ColorTable.CloseButtonHoverBackColor))
                    {
                        e.Graphics.FillRectangle(b, e.ClipRectangle);
                    }

                    DrawCloseButtonBorder(e.Graphics, e.ClipRectangle);
                    break;
            }

            if (e.Image != null)
            {
                Rectangle rect = e.ClipRectangle;
                rect.Inflate(-2, -2);
                rect.Height -= 2;
                rect.Offset(1, 1);

                e.Graphics.DrawImage(e.Image, rect);
            }
        }

        #endregion

        #region Properties

        public Vs2010TabControlColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new Vs2010DefaultTabControlColorTable();
                }

                return _colorTable;
            }

            set { _colorTable = value; }
        }

        #endregion

        #region Classes

        public class PaintCloseButtonEventArgs : PaintEventArgs
        {
            public enum CloseButtonStates
            {
                None,
                Transparent,
                Hovering,
                Clicked
            }

            public PaintCloseButtonEventArgs(Graphics g, Rectangle clipRect, CloseButtonStates state, Image img)
                : base(g, clipRect)
            {
                _CloseButtonState = state;
                _Image = img;
            }

            private CloseButtonStates _CloseButtonState;
            public CloseButtonStates CloseButtonState
            {
                get { return _CloseButtonState; }
            }

            private Image _Image;
            public Image Image
            {
                get { return _Image; }
            }
        }

        public class PaintHeaderEventArgs : PaintEventArgs
        {
            public enum TabHeaderState
            {
                /// <summary>
                /// Represents a normal tab header.
                /// </summary>
                Normal,

                /// <summary>
                /// Represents the tab header the mouse is hovering over.
                /// </summary>
                HotTracking,

                /// <summary>
                /// Represents an active (selected) tab header.
                /// </summary>
                /// <remarks></remarks>
                Active,

                /// <summary>
                /// Represents an inactive (selected but without focus) tab header.
                /// </summary>
                /// <remarks></remarks>
                Inactive
            }

            public PaintHeaderEventArgs(Graphics g, Rectangle clipRect, TabPage tabPage, TabHeaderState state, StringAlignment alignment)
                : base(g, clipRect)
            {
                _TabPage = tabPage;
                _State = state;
                _TextAlignment = alignment;
            }

            private TabHeaderState _State;
            public TabHeaderState State
            {
                get { return _State; }
            }

            private TabPage _TabPage;
            public TabPage TabPage
            {
                get { return _TabPage; }
            }

            private StringAlignment _TextAlignment;
            public StringAlignment TextAlignment
            {
                get { return _TextAlignment; }
            }
        }

        public class PaintBordersEventArgs : PaintEventArgs
        {
            public enum BorderSide
            {
                Top,
                Bottom
            }

            public PaintBordersEventArgs(Graphics g, Rectangle clipRect, BorderSide side, bool active)
                : base(g, clipRect)
            {
                _Side = side;
                _Active = active;
            }

            private BorderSide _Side;
            public BorderSide Side
            {
                get { return _Side; }
            }

            private bool _Active;
            public bool Active
            {
                get { return _Active; }
            }
        }

        #endregion
    }
}