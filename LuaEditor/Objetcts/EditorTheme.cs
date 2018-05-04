using LuaEditor.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;

namespace LuaEditor.Objetcts
{
    public class EditorTheme
    {
        #region Fields

        private readonly Dictionary<EditorThemeCategory, EditorThemeColor> _colors;

        private readonly static EditorTheme DefaultTheme = new EditorTheme()
        {
            Name = "Default",

            FontName = "Courier New",
            FontSize = 12,

            DefaultBackgroundColor = Color.White,
            DefaultTextColor = Color.Black
        };

        #endregion

        #region Constructor

        public EditorTheme()
        {
            _colors = new Dictionary<EditorThemeCategory, EditorThemeColor>();
        }

        #endregion

        #region Static methods

        public static EditorTheme FromFile(string path)
        {
            EditorTheme theme = new EditorTheme();

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            var themeNode = doc.GetNode("Theme");

            // name
            theme.Name = themeNode.GetAttributeText("name");

            // font
            var fontNode = themeNode.GetNode("Font");
            theme.FontName = fontNode.GetAttributeText("name");
            theme.FontSize = fontNode.GetAttributeInt("size");

            // colors
            XmlNode colorsNode = themeNode.GetNode("Colors");

            theme.DefaultBackgroundColor = colorsNode.GetAttributeColor("background", Color.White, true);
            theme.DefaultTextColor = colorsNode.GetAttributeColor("text", Color.Black, true);

            foreach (XmlNode colorNode in themeNode.SelectNodes(".//Color"))
            {
                EditorThemeColor color = new EditorThemeColor();

                EditorThemeCategory category = (EditorThemeCategory)Enum.Parse(
                    typeof(EditorThemeCategory), colorNode.GetAttributeText("category"));

                color.Background = colorNode.GetAttributeColor("background", theme.DefaultBackgroundColor, false);
                color.Text = colorNode.GetAttributeColor("text", theme.DefaultTextColor, false);

                theme.Colors.Add(category, color);
            }

            return theme;
        }

        public static string NameFromFile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNode themeNode = doc.GetNode("Theme", false);
            if (themeNode == null)
                return null;

            string result = themeNode.GetAttributeText("name", null, false);
            if (result == null)
                return null;

            return result;
        }

        #endregion

        #region Methods

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public Color GetTextColor(EditorThemeCategory category)
        {
            if (_colors.ContainsKey(category))
                return _colors[category].Text;

            return DefaultTextColor;
        }

        public Color GetBackgroundColor(EditorThemeCategory category)
        {
            if (_colors.ContainsKey(category))
                return _colors[category].Background;

            return DefaultBackgroundColor;
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string FontName { get; set; }
        public int FontSize { get; set; }

        public Color DefaultBackgroundColor { get; set; }
        public Color DefaultTextColor { get; set; }

        public Dictionary<EditorThemeCategory, EditorThemeColor> Colors
        {
            get { return _colors; }
        }

        public static EditorTheme Default
        {
            get { return DefaultTheme; }
        }

        #endregion
    }
}