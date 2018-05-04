using System;
using System.Diagnostics;
using System.IO;

namespace LuaEditor.Helper
{
    public static class FileHelper
    {

        public static bool IsValidFilename(string name)
        {
            foreach (char ch in Path.GetInvalidFileNameChars())
            {
                if (name.IndexOf(ch) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsValidFoldername(string name)
        {
            foreach (char ch in Path.GetInvalidPathChars())
            {
                if (name.IndexOf(ch) != -1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool OpenInExplorer(string path, bool isFile)
        {
            try
            {
                if (isFile)
                {
                    Process.Start("explorer", "/select,\"" + path + "\"");
                }
                else
                {
                    Process.Start("explorer", "/root,\"" + path + "\"");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}