using PureSoft.Controls.VisualStudio.Colors;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PureSoft.Controls.VisualStudio.Renderer
{
    public class CommonDrawing
    {
        /// <summary>
        /// Draws the rounded yellow selection rectangle.
        /// </summary>
        public static void DrawSelection(Graphics g, Vs2010CommonColorTable colorTable, Rectangle rect, bool rounded = true, bool drawBorder = true)
        {
            DrawSelection(g, colorTable.SelectedGradientTop, colorTable.SelectedGradientMiddle, colorTable.SelectedGradientBottom, colorTable.SelectedBorder, rect, rounded, drawBorder);
        }

        public static void DrawSelection(Graphics g, Color gradientTop, Color gradientBottom, Color bottom, Color border, Rectangle rect, bool rounded = true, bool drawBorder = true)
        {
            Rectangle topRect = default(Rectangle);
            Rectangle bottomRect = default(Rectangle);

            if (rounded)
            {
                Rectangle fillRect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 1, rect.Height - 1);
                topRect = fillRect;
                topRect.Height -= Convert.ToInt32(topRect.Height / 2);
                bottomRect = new Rectangle(topRect.X, topRect.Bottom, topRect.Width, fillRect.Height - topRect.Height);
            }
            else
            {
                topRect = rect;
                topRect.Height -= Convert.ToInt32(topRect.Height / 2);
                bottomRect = new Rectangle(topRect.X, topRect.Bottom, topRect.Width, rect.Height - topRect.Height);
            }

            // Top gradient
            using (LinearGradientBrush b = new LinearGradientBrush(topRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
            {
                g.FillRectangle(b, topRect);
            }

            // Bottom
            using (SolidBrush b = new SolidBrush(bottom))
            {
                g.FillRectangle(b, bottomRect);
            }

            // Border
            if (drawBorder)
            {
                using (Pen p = new Pen(border))
                {
                    if (rounded)
                    {
                        CommonDrawing.DrawRoundedRectangle(g, p, rect.X, rect.Y, rect.Width, rect.Height, 2);
                    }
                    else
                    {
                        g.DrawRectangle(p, rect);
                    }
                }
            }
        }



        public static void DrawRoundedRectangle(Graphics g, Pen p, float x, float y, float width, float height, float radius)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddLine(x + radius, y, x + width - (radius * 2), y);
                gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
                gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
                gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
                gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
                gp.AddLine(x, y + height - (radius * 2), x, y + radius);
                gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                gp.CloseFigure();

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawPath(p, gp);
                g.SmoothingMode = SmoothingMode.Default;
            }
        }


        public static void FillRoundedRectangle(Graphics g, Brush b, float x, float y, float width, float height, float radius)
        {
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddLine(x + radius, y, x + width - (radius * 2), y);
                gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
                gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
                gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
                gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
                gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
                gp.AddLine(x, y + height - (radius * 2), x, y + radius);
                gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
                gp.CloseFigure();

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(b, gp);
                g.SmoothingMode = SmoothingMode.Default;
            }
        }

    }
}