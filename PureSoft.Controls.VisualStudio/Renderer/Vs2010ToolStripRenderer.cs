using PureSoft.Controls.VisualStudio.Colors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PureSoft.Controls.VisualStudio.Renderer
{
    public class Vs2010ToolStripRenderer : Vs2010MenuStripRenderer
    {
        private Vs2010ToolStripColorTable _colorTable;

        public Vs2010ToolStripRenderer()
            : this(new Vs2010DefaultToolStripColorTable())
        {
        }

        public Vs2010ToolStripRenderer(Vs2010ToolStripColorTable colorTable)
        {
            this.ColorTable = colorTable;
        }

        public new Vs2010ToolStripColorTable ColorTable
        {
            get
            {
                if (_colorTable == null)
                {
                    _colorTable = new Vs2010DefaultToolStripColorTable();
                }

                return _colorTable;
            }

            set { _colorTable = value; }
        }

        #region " Backgrounds and borders "

        protected override void OnRenderToolStripBackground(System.Windows.Forms.ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            Rectangle roundedRect = new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y + 1, e.AffectedBounds.Width, e.AffectedBounds.Height - 3);
            using (SolidBrush b = new SolidBrush(this.ColorTable.Background))
            {
                e.Graphics.FillRectangle(b, roundedRect);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.Parent != null)
            {
                Rectangle rect = new Rectangle(
                    e.AffectedBounds.X,
                    e.AffectedBounds.Y,
                    e.AffectedBounds.Width,
                    e.AffectedBounds.Height - 2);

                using (Pen p = new Pen(ColorTable.Border))
                {
                    CommonDrawing.DrawRoundedRectangle(e.Graphics, p, rect.X, rect.Y, rect.Width, rect.Height, 2);
                }
            }
            else
            {
                // Call base (menu strip renderer) OnRenderToolStripBorder method
                base.OnRenderToolStripBorder(e);

                // But now fill in the top border completely
                using (Pen p = new Pen(this.ColorTable.CommonColorTable.DropdownBorder))
                {
                    e.Graphics.DrawLine(p, e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Right, e.AffectedBounds.Y);
                }
            }
        }

        #endregion

        #region " Items "

        protected override void OnRenderItemText(System.Windows.Forms.ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = this.ColorTable.CommonColorTable.TextColor;
            base.OnRenderItemText(e);
        }

        #region " Buttons "

        protected override void OnRenderButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            bool @checked = ((ToolStripButton)e.Item).Checked;
            bool drawBorder = false;

            if (@checked)
            {
                drawBorder = true;

                if (e.Item.Selected && !e.Item.Pressed)
                {
                    using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.CheckedSelectedBackground))
                    {
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
                else
                {
                    using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.CheckedBackground))
                    {
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
            }
            else
            {
                if (e.Item.Pressed)
                {
                    drawBorder = true;
                    using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.PressedBackground))
                    {
                        e.Graphics.FillRectangle(b, rect);
                    }
                }
                else if (e.Item.Selected)
                {
                    drawBorder = true;
                    CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, rect, false);
                }

            }

            if (drawBorder)
            {
                using (Pen p = new Pen(this.ColorTable.CommonColorTable.SelectedBorder))
                {
                    e.Graphics.DrawRectangle(p, rect);
                }
            }
        }

        protected override void OnRenderDropDownButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
            bool drawBorder = false;

            if (e.Item.Pressed)
            {
                drawBorder = true;
                using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.PressedBackground))
                {
                    e.Graphics.FillRectangle(b, rect);
                }
            }
            else if (e.Item.Selected)
            {
                drawBorder = true;
                CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, rect, false);
            }

            if (drawBorder)
            {
                using (Pen p = new Pen(this.ColorTable.CommonColorTable.SelectedBorder))
                {
                    e.Graphics.DrawRectangle(p, rect);
                }
            }
        }

        protected override void OnRenderSplitButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            base.OnRenderSplitButtonBackground(e);

            bool drawBorder = false;
            bool drawSeparator = true;

            dynamic item = (ToolStripSplitButton)e.Item;

            Rectangle btnRect = new Rectangle(0, 0, item.ButtonBounds.Width - 1, item.ButtonBounds.Height - 1);
            Rectangle borderRect = new Rectangle(0, 0, item.Bounds.Width - 1, item.Bounds.Height - 1);

            if (item.DropDownButtonPressed)
            {
                drawBorder = true;
                drawSeparator = false;
                using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.PressedBackground))
                {
                    e.Graphics.FillRectangle(b, borderRect);
                }
            }
            else if (item.DropDownButtonSelected)
            {
                drawBorder = true;
                CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, borderRect, false);
            }

            if (item.ButtonPressed)
            {
                using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.PressedBackground))
                {
                    e.Graphics.FillRectangle(b, btnRect);
                }
            }

            if (drawBorder)
            {
                using (Pen p = new Pen(this.ColorTable.CommonColorTable.SelectedBorder))
                {
                    e.Graphics.DrawRectangle(p, borderRect);
                    if (drawSeparator)
                        e.Graphics.DrawRectangle(p, btnRect);
                }

                this.DrawCustomArrow(e.Graphics, item);
            }
        }

        private void DrawCustomArrow(Graphics g, ToolStripSplitButton item)
        {
            int dropWidth = item.DropDownButtonBounds.Width - 1;
            int dropHeight = item.DropDownButtonBounds.Height - 1;
            float triangleWidth = dropWidth / 2f + 1;
            float triangleLeft = item.DropDownButtonBounds.Left + (dropWidth - triangleWidth) / 2f;
            float triangleHeight = triangleWidth / 2f;
            float triangleTop = item.DropDownButtonBounds.Top + (dropHeight - triangleHeight) / 2f + 1;
            RectangleF arrowRect = new RectangleF(triangleLeft, triangleTop, triangleWidth, triangleHeight);

            this.DrawCustomArrow(g, item, Rectangle.Round(arrowRect));
        }

        private void DrawCustomArrow(Graphics g, ToolStripItem item, Rectangle rect)
        {
            ToolStripArrowRenderEventArgs arrowEventArgs = new ToolStripArrowRenderEventArgs(g, item, rect, this.ColorTable.CommonColorTable.Arrow, ArrowDirection.Down);
            base.OnRenderArrow(arrowEventArgs);
        }

        protected override void OnRenderOverflowButtonBackground(System.Windows.Forms.ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = default(Rectangle);
            Rectangle rectEnd = default(Rectangle);
            rect = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 2);
            rectEnd = new Rectangle(rect.X - 5, rect.Y, rect.Width - 5, rect.Height);

            if (e.Item.Pressed)
            {
                using (SolidBrush b = new SolidBrush(this.ColorTable.CommonColorTable.PressedBackground))
                {
                    e.Graphics.FillRectangle(b, rect);
                }
            }
            else if (e.Item.Selected)
            {
                CommonDrawing.DrawSelection(e.Graphics, this.ColorTable.CommonColorTable, rect, false);
            }
            else
            {
                using (SolidBrush b = new SolidBrush(this.ColorTable.OverflowBackground))
                {
                    e.Graphics.FillRectangle(b, rect);
                }
            }

            using (SolidBrush b = new SolidBrush(this.ColorTable.Background))
            {
                CommonDrawing.FillRoundedRectangle(e.Graphics, b, rectEnd.X, rectEnd.Y, rectEnd.Width, rectEnd.Height, 3);
            }

            // Icon
            int w = rect.Width - 1;
            int h = rect.Height - 1;
            float triangleWidth = w / 2f + 1;
            float triangleLeft = rect.Left + (w - triangleWidth) / 2f + 3;
            float triangleHeight = triangleWidth / 2f;
            float triangleTop = rect.Top + (h - triangleHeight) / 2f + 7;
            RectangleF arrowRect = new RectangleF(triangleLeft, triangleTop, triangleWidth, triangleHeight);
            this.DrawCustomArrow(e.Graphics, e.Item, Rectangle.Round(arrowRect));

            using (Pen p = new Pen(this.ColorTable.CommonColorTable.Arrow))
            {
                e.Graphics.DrawLine(p, triangleLeft + 2, triangleTop - 2, triangleLeft + triangleWidth - 2, triangleTop - 2);
            }
        }

        #endregion

        #endregion

        #region Misc

        protected override void OnRenderGrip(System.Windows.Forms.ToolStripGripRenderEventArgs e)
        {
            if (e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical)
            {
                int h = e.GripBounds.Height - 3;
                float ratio = 14f / 22f;

                int totalDotsHeight = Convert.ToInt32(h * ratio);
                int singleDotHeight = Convert.ToInt32(totalDotsHeight / 7f);
                int dotsStart = Convert.ToInt32((h - totalDotsHeight) / 2f) + 1;

                using (SolidBrush b = new SolidBrush(this.ColorTable.Grip))
                {

                    Rectangle bounds = new Rectangle(e.GripBounds.X, dotsStart, singleDotHeight, singleDotHeight);
                    for (int i = 0; i <= 3; i++)
                    {
                        e.Graphics.FillRectangle(b, bounds);
                        bounds.Offset(0, 2 * singleDotHeight);
                    }

                }
            }
        }

        protected override void OnRenderSeparator(System.Windows.Forms.ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(3, 3, 1, e.Item.Height - 6);
            using (SolidBrush b = new SolidBrush(this.ColorTable.Separator))
            {
                e.Graphics.FillRectangle(b, rect);
            }
        }

        #endregion
    }
}