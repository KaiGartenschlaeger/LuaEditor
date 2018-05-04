using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    /// <summary>
    /// Holds the colors for elements common to all strips, such as selection colors.
    /// </summary>
    /// <remarks>Inherit this class, override all properties and return your desired colors.</remarks>
    public abstract class Vs2010CommonColorTable
    {
        /// <summary>
        /// Gets the color of the text on all items.
        /// </summary>
        /// <returns>The color of the text on all items.</returns>
        public abstract Color TextColor { get; }

        /// <summary>
        /// Gets the color of the border of a selected item.
        /// </summary>
        /// <returns>The color of the border of a selected item.</returns>
        public abstract Color SelectedBorder { get; }

        /// <summary>
        /// Gets the top color of the gradient making up a selection rectangle.
        /// </summary>
        /// <returns>The top color of the gradient making up a selection rectangle.</returns>
        public abstract Color SelectedGradientTop { get; }

        /// <summary>
        /// Gets the bottom color of the gradient making up a selection rectangle.
        /// </summary>
        /// <returns>The bottom color of the gradient making up a selection rectangle.</returns>
        public abstract Color SelectedGradientMiddle { get; }

        /// <summary>
        /// Gets the color of the solid bottom making up a selection rectangle.
        /// </summary>
        /// <returns>The color of the solid bottom making up a selection rectangle.</returns>
        public abstract Color SelectedGradientBottom { get; }

        /// <summary>
        /// Gets the background color of a pressed item.
        /// </summary>
        /// <returns>The background color of a pressed item.</returns>
        public abstract Color PressedBackground { get; }

        /// <summary>
        /// Gets the background color of a checked item.
        /// </summary>
        /// <returns>The background color of a checked item.</returns>
        public abstract Color CheckedBackground { get; }

        /// <summary>
        /// Gets the background color of a checked and selected item.
        /// </summary>
        /// <returns>The background color of a checked and selected item.</returns>
        public abstract Color CheckedSelectedBackground { get; }

        /// <summary>
        /// Gets the border color of a dropdown menu.
        /// </summary>
        /// <returns>The border color of a dropdown menu.</returns>
        public abstract Color DropdownBorder { get; }

        /// <summary>
        /// Gets the color of the arrow for both menus and dropdown buttons.
        /// </summary>
        /// <returns>The color of the arrow for both menus and dropdown buttons.</returns>
        public abstract Color Arrow { get; }

        /// <summary>
        /// Gets the color for the window background.
        /// </summary>
        public abstract Color WindowBackground { get; }

        /// <summary>
        /// Gets the text color for the tool pane header.
        /// </summary>
        public abstract Color HeaderTextColor { get; }

        /// <summary>
        /// Gets the start color for the tool pane header 
        /// </summary>
        public abstract Color HeaderGradiantStart { get; }

        /// <summary>
        /// Gets the end color for the tool pane header
        /// </summary>
        public abstract Color HeaderGradiantEnd { get; }
    }
}