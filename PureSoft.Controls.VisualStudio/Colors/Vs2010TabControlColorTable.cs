using System.Drawing;

namespace PureSoft.Controls.VisualStudio.Colors
{
    public abstract class Vs2010TabControlColorTable
    {
        public abstract Vs2010CommonColorTable CommonColorTable { get; }

        public abstract Color Background { get; }
        public abstract Color BackgroundDotsLight { get; }
        public abstract Color BackgroundDotsDark { get; }
        public abstract Color SelectedHeaderGradientTop { get; }
        public abstract Color SelectedHeaderGradientBottom { get; }
        public abstract Color SelectedHeaderGradientMiddle { get; }
        public abstract Color HoveringHeaderGradientTop { get; }
        public abstract Color HoveringHeaderGradientBottom { get; }
        public abstract Color HoveringHeaderBorder { get; }
        public abstract Color InactiveColor { get; }
        public abstract Color Border { get; }
        public abstract Color CloseButtonBorder { get; }
        public abstract Color CloseButtonHoverBackColor { get; }
        public abstract Color CloseButtonClickedBackColor { get; }
    }
}