using System;

namespace LuaEditor.Helper
{
    public static class GuidHelper
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString()
                .Replace("-", string.Empty);
        }
    }
}