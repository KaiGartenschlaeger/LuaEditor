using PureSoft.Controls.VisualStudio.Colors;
using PureSoft.Controls.VisualStudio.Renderer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio
{
    public class Vs2010TabControl : TabControl
    {
        #region Win API

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, Int32 msg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region Constants

        //These are required for calculating the tab size properly
        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        private const int TAB_HEIGHT = 26;

        #endregion

        #region Fields

        private int _hotTrackIndex = -1;
        private readonly Vs2010TabControlColorTable _colors = new Vs2010DefaultTabControlColorTable();

        private Vs2010TabControlRenderer _Renderer;
        private StringAlignment _TabTextAlignment;
        private bool _Active;

        #endregion

        #region Constructor

        public Vs2010TabControl()
        {
            DrawMode = TabDrawMode.OwnerDrawFixed;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            TabTextAlignment = StringAlignment.Near;
            BackColor = Renderer.ColorTable.Background;
            Active = true;
            ItemSize = new Size(ItemSize.Width, TAB_HEIGHT);
            HotTrack = true;
        }

        #endregion

        #region Helper

        private Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState GetTabHeaderState(int tabIndex)
        {
            TabPage tab = TabPages[tabIndex];
            Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState state =
                default(Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState);

            if (object.ReferenceEquals(SelectedTab, tab))
            {
                if (Active)
                {
                    state = Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState.Active;
                }
                else
                {
                    state = Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState.Inactive;
                }
            }
            else if (tabIndex == _hotTrackIndex)
            {
                state = Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState.HotTracking;
            }
            else
            {
                state = Vs2010TabControlRenderer.PaintHeaderEventArgs.TabHeaderState.Normal;
            }

            return state;
        }

        #endregion

        #region Control methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            OnFontChanged(EventArgs.Empty);
        }

        // This takes care of tab text measurement
        // if you are just drawing using GDI+ the tab size is off (too small for small text, too large for long text)
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            IntPtr hFont = Font.ToHfont();
            SendMessage(Handle, WM_SETFONT, hFont, new IntPtr(-1));
            SendMessage(Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);

            UpdateStyles();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //Invalidating the tabs
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i <= TabCount - 1; i++)
                {
                    Invalidate(GetTabRect(i));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            //Invalidating the tabs
            for (int i = 0; i <= TabCount - 1; i++)
            {
                Invalidate(GetTabRect(i));
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (HotTrack)
            {
                //Invalidating the tabs
                for (int i = 0; i <= TabCount - 1; i++)
                {
                    Rectangle rect = GetTabRect(i);
                    if (rect.Contains(e.Location))
                    {
                        if (_hotTrackIndex != -1)
                        {
                            Invalidate(); // GetTabRect(_hotTrackIndex));
                        }

                        _hotTrackIndex = i;

                        Invalidate(rect);

                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            //Invalidating the tabs
            if (HotTrack && _hotTrackIndex != -1 && _hotTrackIndex < TabCount)
            {
                Invalidate(GetTabRect(_hotTrackIndex));
                _hotTrackIndex = -1;
            }
        }

        #endregion

        #region Paint methods

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            //I think this was for testing purposes, dunno but you can probably ignore/remove it
            Rectangle rect = e.Bounds;

            rect.Inflate(5, 5);
            e.Graphics.DrawRectangle(Pens.Red, rect);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Rectangle bgRect = new Rectangle(0, 0, Width, Height);
            using (SolidBrush brush = new SolidBrush(_colors.BackgroundDotsDark))
            {
                e.Graphics.FillRectangle(brush, bgRect);
            }

            //PaintEventArgs bgEventArgs = new PaintEventArgs(pevent.Graphics, bgRect);
            //Renderer.OnPaintTabHeadersBackground(bgEventArgs);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Tab headers
            for (int i = 0; i <= TabPages.Count - 1; i++)
            {
                TabPage tab = TabPages[i];

                //This is just trial and error (+1, +3, -5, etc)
                Rectangle tabRect = GetTabRect(i);
                tabRect = new Rectangle(tabRect.X + 1, tabRect.Y + 3, tabRect.Width, tabRect.Height - 5);

                var tabHeaderState = GetTabHeaderState(i);
                Vs2010TabControlRenderer.PaintHeaderEventArgs headerEventArgs =
                    new Vs2010TabControlRenderer.PaintHeaderEventArgs(e.Graphics, tabRect, tab, tabHeaderState, TabTextAlignment);

                //The actual drawing is all done in the Renderer for modularity
                Renderer.OnPaintTabHeader(headerEventArgs);
            }

            // Borders
            Vs2010TabControlRenderer.PaintBordersEventArgs borderEventArgs;

            if (TabPages.Count > 0)
            {
                Rectangle topBorderRect = new Rectangle(4, ItemSize.Height, Width - 8, 4);
                borderEventArgs =
                    new Vs2010TabControlRenderer.PaintBordersEventArgs(
                        e.Graphics,
                        topBorderRect,
                        Vs2010TabControlRenderer.PaintBordersEventArgs.BorderSide.Top,
                        Active);

                Renderer.OnPaintBorders(borderEventArgs);

                Rectangle bottomBorderRect = new Rectangle(4, Height - 4, Width - 8, 4);
                borderEventArgs = new Vs2010TabControlRenderer.PaintBordersEventArgs(e.Graphics,
                    bottomBorderRect,
                    Vs2010TabControlRenderer.PaintBordersEventArgs.BorderSide.Bottom,
                    Active);

                Renderer.OnPaintBorders(borderEventArgs);
            }
        }

        #endregion

        #region Properties

        //I made this for modularity, so the tab control can easily use different renderers
        //You can give it a different Renderers.Vs2010TabControlRenderer if you wanted to
        //or you can just ignore this
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Vs2010TabControlRenderer Renderer
        {
            get
            {
                if (_Renderer == null)
                {
                    _Renderer = new Vs2010TabControlRenderer();
                }
                return _Renderer;
            }

            set { _Renderer = value; }
        }

        [Browsable(true)]
        public StringAlignment TabTextAlignment
        {
            get { return _TabTextAlignment; }
            set { _TabTextAlignment = value; }
        }

        // This indicates whether it is the 'active' tab control. 
        // In VS for example, if you have multiple tabs side-by-side (in tab groups),
        // the tab control with the focused tab (this is what i call 'active') looks different (other colors) 
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                Invalidate();
            }
        }

        #endregion
    }
}