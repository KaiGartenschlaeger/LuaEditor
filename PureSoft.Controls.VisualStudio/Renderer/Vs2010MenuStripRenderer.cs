using PureSoft.Controls.VisualStudio.Colors;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio.Renderer
{
    public class Vs2010MenuStripRenderer : ToolStripProfessionalRenderer
    {
        private Vs2010MenuStripColorTable _colorTable;

        public Vs2010MenuStripRenderer()
            : this(new Vs2010DefaultMenuStripColorTable())
        {
        }

        public Vs2010MenuStripRenderer(Vs2010MenuStripColorTable colorTable)
        {
            ColorTable = colorTable;
        }

        public new Vs2010MenuStripColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new Vs2010DefaultMenuStripColorTable();
                }

                return _colorTable;
            }

            set { _colorTable = value; }
        }

        #region " Backgrounds and borders "

        protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            // The main background gradient
            using (LinearGradientBrush b = new LinearGradientBrush(e.AffectedBounds, this.ColorTable.BackgroundGradientTop, this.ColorTable.BackgroundGradientBottom, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderToolStripBorder(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            //MyBase.OnRenderToolStripBorder(e)

            // This check is to make sure only the dropdown borders are painted, and not a border around the entire ToolStrip

            if (e.ToolStrip.Parent == null)
            {
                // Border around dropdown
                Rectangle borderRect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);

                using (Pen p = new Pen(this.ColorTable.CommonColorTable.DropdownBorder))
                {
                    e.Graphics.DrawRectangle(p, borderRect);
                }

                // Fill in the 'connected area' (gap between dropdown and owner item)
                using (SolidBrush b = new SolidBrush(this.ColorTable.DroppedDownItemBackground))
                {
                    e.Graphics.FillRectangle(b, e.ConnectedArea);
                }

            }
            else
            {
                // Border underneath main toolstrip
                using (Pen p = new Pen(this.ColorTable.BottomBorder))
                {
                    e.Graphics.DrawLine(p, e.AffectedBounds.X, e.AffectedBounds.Bottom - 1, e.AffectedBounds.Right, e.AffectedBounds.Bottom - 1);
                }
            }
        }

        protected override void OnRenderMenuItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            // Only draw something if the item is enabled. If not, don't even draw the real selection (which is a blue border)

            if (e.Item.Enabled)
            {
                // MyBase.OnRenderMenuItemBackground(e)


                if (e.Item.Selected)
                {
                    if (!e.Item.IsOnDropDown)
                    {
                        // Item is menu header and selected: yellow selection gradient
                        Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                        CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, rect, true);
                    }
                    else
                    {
                        // Item is NOT menuheader (but subitem) and selected: slightly smaller yellow selection gradient
                        Rectangle rect = new Rectangle(2, 0, e.Item.Width - 4, e.Item.Height - 1);
                        CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, rect, true);
                    }

                }

                if (((ToolStripMenuItem)e.Item).DropDown.Visible && !e.Item.IsOnDropDown)
                {
                    // Item is menu header and dropdown is showing: solid gray background

                    Rectangle borderRect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height);

                    // Background
                    Rectangle bgRect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height + 2);
                    using (SolidBrush b = new SolidBrush(this.ColorTable.DroppedDownItemBackground))
                    {
                        e.Graphics.FillRectangle(b, bgRect);
                    }

                    // Border
                    using (Pen p = new Pen(this.ColorTable.CommonColorTable.DropdownBorder))
                    {
                        CommonDrawing.DrawRoundedRectangle(e.Graphics, p, borderRect.X, borderRect.Y, borderRect.Width, borderRect.Height, 2);
                    }
                }

                e.Item.ForeColor = this.ColorTable.CommonColorTable.TextColor;
            }

        }

        #endregion

        #region " Items "

        protected override void OnRenderItemText(System.Windows.Forms.ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = this.ColorTable.CommonColorTable.TextColor;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderItemCheck(System.Windows.Forms.ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);

            Rectangle rect = new Rectangle(3, 1, e.Item.Height - 3, e.Item.Height - 3);

            Color c = default(Color);
            if (e.Item.Selected)
            {
                c = this.ColorTable.CommonColorTable.CheckedSelectedBackground;
            }
            else
            {
                c = this.ColorTable.CommonColorTable.CheckedBackground;
            }

            using (SolidBrush b = new SolidBrush(c))
            {
                e.Graphics.FillRectangle(b, rect);
            }
            using (Pen p = new Pen(this.ColorTable.CommonColorTable.SelectedBorder))
            {
                e.Graphics.DrawRectangle(p, rect);
            }

            //TODO: draw vs2010 checkmark
            e.Graphics.DrawImage(e.Image, new Point(5, 3));
        }

        protected override void OnRenderImageMargin(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            base.OnRenderImageMargin(e);

            // Dropdown background gradient
            Rectangle bgRect = new Rectangle(0, -1, e.ToolStrip.Width, e.ToolStrip.Height + 1);
            using (LinearGradientBrush b = new LinearGradientBrush(bgRect, this.ColorTable.DropdownGradientTop, this.ColorTable.DropdownGradientBottom, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, bgRect);
            }

            // Image margin
            using (SolidBrush b = new SolidBrush(this.ColorTable.ImageMargin))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
        }

        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            base.OnRenderSeparator(e);
            int x1 = 28;
            int x2 = e.Item.Width;
            int y = 3;
            using (Pen p = new Pen(this.ColorTable.Separator))
            {
                e.Graphics.DrawLine(p, x1, y, x2, y);
            }
        }

        #endregion

        #region " Misc "

        protected override void OnRenderArrow(System.Windows.Forms.ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = this.ColorTable.CommonColorTable.Arrow;
            base.OnRenderArrow(e);
        }

        #endregion
    }
}