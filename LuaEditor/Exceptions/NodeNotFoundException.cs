using System;

namespace LuaEditor.Exceptions
{
    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException(string nodeName)
            : base($"Das Element \"{nodeName}\" konnte nicht gefunden werden.")
        {
        }
    }
}