using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public abstract class Vs2010ControlsColorTable
    {
        /// <summary>
        /// Gets the <see cref="Vs2010CommonColorTable" /> used to draw the common elements.
        /// </summary>
        /// <returns>The <see cref="Vs2010CommonColorTable" /> used to draw the common elements.</returns>
        /// <remarks>Override this property and return an instance of your desired <see cref="Vs2010CommonColorTable" />.</remarks>
        public abstract Vs2010CommonColorTable CommonColorTable { get; }

        public abstract Color ComboboxBackground { get; }
        public abstract Color ComboboxBorder { get; }
        public abstract Color DropdownGradientTop { get; }
        public abstract Color DropdownGradientBottom { get; }
        public abstract Color ButtonHoverTop { get; }
        public abstract Color DarkBackground { get; }
        public abstract Color ButtonNormalGradientTop { get; }
        public abstract Color ButtonNormalGradientMiddle { get; }
        public abstract Color ButtonNormalGradientBottom { get; }
        public abstract Color ButtonNormalBorder { get; }
        public abstract Color StatusStripBackColor { get; }
    }
}