using System.Drawing;

namespace LuaEditor.Objetcts
{
    public class EditorFormSettings
    {
        /// <summary>
        /// Gibt an, ob die Position innerhalb der Bounds Eigenschaft in absoluten Bildschirm Koordinaten angegeben ist.
        /// </summary>
        public bool IsAbsolutePos { get; set; }


        /// <summary>
        /// Enthält die Position und Größe des Fensters.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// Gibt an, ob das Fenster maximiert ist.
        /// </summary>
        public bool Maximized { get; set; }
    }
}