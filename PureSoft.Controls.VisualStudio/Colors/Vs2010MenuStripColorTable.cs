using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    /// <summary>
    /// Holds the colors used by a <see cref="Vs2010Renderers.Renderers.Vs2010MenuStripRenderer" />.
    /// </summary>
    /// <remarks>Inherit this class, override all properties and return your desired colors.</remarks>
    public abstract class Vs2010MenuStripColorTable
    {
        /// <summary>
        /// Gets the <see cref="Vs2010CommonColorTable" /> used to draw the common elements.
        /// </summary>
        /// <returns>The <see cref="Vs2010CommonColorTable" /> used to draw the common elements.</returns>
        /// <remarks>Override this property and return an instance of your desired <see cref="Vs2010CommonColorTable" />.</remarks>
        public abstract Vs2010CommonColorTable CommonColorTable { get; }

        /// <summary>
        /// Gets the color of the border underneath the <see cref="Vs2010Renderers.Controls.Vs2010MenuStrip" />.
        /// </summary>
        /// <returns>The color of the border underneath the <see cref="Vs2010Renderers.Controls.Vs2010MenuStrip" />.</returns>
        public abstract Color BottomBorder { get; }

        /// <summary>
        /// Gets the background top gradient color.
        /// </summary>
        /// <returns>The background top gradient color.</returns>
        public abstract Color BackgroundGradientTop { get; }

        /// <summary>
        /// Gets the background bottom gradient color.
        /// </summary>
        /// <returns>The background bottom gradient color.</returns>
        public abstract Color BackgroundGradientBottom { get; }

        /// <summary>
        /// Gets the background color of a menu header item that is currently dropped down.
        /// </summary>
        /// <returns>The background color of a menu header item that is currently dropped down.</returns>
        public abstract Color DroppedDownItemBackground { get; }

        /// <summary>
        /// Gets the background top gradient color of a dropdown menu.
        /// </summary>
        /// <returns>The background top gradient color of a dropdown menu.</returns>
        public abstract Color DropdownGradientTop { get; }

        /// <summary>
        /// Gets the background bottom gradient color of a dropdown menu.
        /// </summary>
        /// <returns>The background bottom gradient color of a dropdown menu.</returns>
        public abstract Color DropdownGradientBottom { get; }

        /// <summary>
        /// Gets the color of the image margin of a dropdown menu.
        /// </summary>
        /// <returns>The color of the image margin of a dropdown menu.</returns>
        public abstract Color ImageMargin { get; }

        /// <summary>
        /// Gets the color of a separator item.
        /// </summary>
        /// <returns>The color of a separator item.</returns>
        public abstract Color Separator { get; }
    }
}