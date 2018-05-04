using System;
using System.IO;

namespace LuaEditor.Helper
{
    public static class PathHelper
    {
        public static string ToRelativePath(string filePath, string rootPath)
        {
            bool isRooted = Path.IsPathRooted(filePath);
            if (!isRooted)
            {
                return filePath;
            }

            Uri filePathUri = new Uri(filePath, UriKind.RelativeOrAbsolute);

            // Folders must end in a slash
            if (!rootPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                rootPath += Path.DirectorySeparatorChar;
            }

            Uri rootPathUri = new Uri(rootPath);
            Uri relativePath = rootPathUri.MakeRelativeUri(filePathUri);

            string result = relativePath.ToString();

            result = Uri.UnescapeDataString(result);
            result = result.Replace('/', Path.DirectorySeparatorChar);

            return result;
        }

        public static string ToAbsolutePath(string filePath, string rootPath)
        {
            string r = Path.Combine(rootPath, filePath);
            return Path.GetFullPath(r);
        }
    }
}