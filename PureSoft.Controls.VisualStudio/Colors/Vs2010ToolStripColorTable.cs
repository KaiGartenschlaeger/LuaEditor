using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public abstract class Vs2010ToolStripColorTable
    {
        /// <summary>
        /// Gets the <see cref="Vs2010CommonColorTable" /> used to draw the common elements.
        /// </summary>
        /// <returns>The <see cref="Vs2010CommonColorTable" /> used to draw the common elements.</returns>
        /// <remarks>Override this property and return an instance of your desired <see cref="Vs2010CommonColorTable" />.</remarks>
        public abstract Vs2010CommonColorTable CommonColorTable { get; }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        /// <returns>The background top gradient color.</returns>
        public abstract Color Background { get; }

        /// <summary>
        /// Gets the border color.
        /// </summary>
        /// <returns>The border color.</returns>
        public abstract Color Border { get; }

        /// <summary>
        /// Gets the color of a parent <see cref="ToolStripPanel" />.
        /// </summary>
        /// <returns>The color of a parent <see cref="ToolStripPanel" />.</returns>
        /// <remarks>When parented by a <see cref="ToolStripPanel" />, the background of this <see cref="ToolStripPanel" /> will be set to this color.</remarks>
        public abstract Color ParentBackground { get; }

        public abstract Color WindowBackground { get; }

        /// <summary>
        /// Gets the color of the grip handle.
        /// </summary>
        /// <returns>The color of the grip handle.</returns>
        public abstract Color Grip { get; }

        /// <summary>
        /// Gets the color of a separator item.
        /// </summary>
        /// <returns>The color of a separator item.</returns>
        public abstract Color Separator { get; }

        /// <summary>
        /// Gets the background color of the overflow indicator.
        /// </summary>
        /// <returns>The background color of the overflow indicator.</returns>
        public abstract Color OverflowBackground { get; }
    }
}