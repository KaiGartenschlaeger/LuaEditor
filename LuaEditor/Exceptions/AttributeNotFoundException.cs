using System;

namespace LuaEditor.Exceptions
{
    public class AttributeNotFoundException : Exception
    {
        public AttributeNotFoundException(string attributeName)
            : base($"Das Attribute \"{attributeName}\" konnte nicht gefunden werden.")
        {
        }
    }
}