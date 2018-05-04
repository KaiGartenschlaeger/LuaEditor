using LuaEditor.Exceptions;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml;

namespace LuaEditor.Helper
{
    public static class XmlDocumentHelper
    {
        private static readonly Regex ColorRegex = new Regex(@"^(\d{1,3}),(\d{1,3}),(\d{1,3})$", RegexOptions.Compiled);

        public static XmlNode GetNode(this XmlNode parentNode, string childNodeName, bool isRequired = true)
        {
            if (parentNode == null)
                throw new ArgumentNullException(nameof(parentNode));

            XmlNode childNode = parentNode[childNodeName];

            if (childNode == null && isRequired)
                throw new NodeNotFoundException(parentNode.Name + "." + childNodeName);

            return childNode;
        }

        public static XmlAttribute GetAttribute(this XmlNode parentNode, string attributeName, bool isRequired = true)
        {
            if (parentNode == null)
                throw new ArgumentNullException(nameof(parentNode));

            XmlAttribute attribute = parentNode.Attributes[attributeName];

            if (attribute == null && isRequired)
                throw new AttributeNotFoundException(parentNode.Name + "." + attributeName);

            return attribute;
        }

        public static string GetAttributeText(this XmlNode parentNode, string attributeName, string fallbackValue = default(string), bool isRequired = true)
        {
            if (parentNode == null)
                throw new ArgumentNullException(nameof(parentNode));

            XmlAttribute attribute = parentNode.GetAttribute(attributeName, isRequired);

            if (attribute == null)
                return fallbackValue;

            if (isRequired && string.IsNullOrEmpty(attribute.InnerText))
                throw new Exception($"Das Element \"{parentNode.Name}.{attributeName}\" muss einen Wert enthalten.");

            return attribute.InnerText;
        }

        public static int GetAttributeInt(this XmlNode parentNode, string attributeName, int fallbackValue = default(int), bool isRequired = true)
        {
            string text = parentNode.GetAttributeText(attributeName, null, isRequired);

            int result;
            if (!int.TryParse(text, out result))
            {
                throw new Exception($"Der Wert des Elementes \"{parentNode.Name}.{attributeName}\" muss eine Ganzzahl beinhalten.");
            }

            return result;
        }

        public static Color GetAttributeColor(this XmlNode parentNode, string attributeName, Color fallbackValue = default(Color), bool isRequired = true)
        {
            string text = parentNode.GetAttributeText(attributeName, null, isRequired);

            if (text == null)
            {
                if (isRequired)
                    throw new Exception($"Das Element {parentNode.Name}[{attributeName}] darf nicht leer sein.");

                return fallbackValue;
            }

            Match match = ColorRegex.Match(text);
            if (!match.Success)
            {
                throw new Exception($"Der Wert des Attributes \"{parentNode.Name}[{attributeName}]\" enthält einen ungültigen Wert.");
            }

            return Color.FromArgb(
                Convert.ToInt32(match.Groups[1].Value.Trim()),
                Convert.ToInt32(match.Groups[2].Value.Trim()),
                Convert.ToInt32(match.Groups[3].Value.Trim()));
        }
    }
}