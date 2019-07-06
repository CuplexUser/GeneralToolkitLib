using System.IO;

namespace GeneralToolkitLib.Utility
{
    public static class FileSystem
    {
        public static bool IsValidDirectory(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                    return Directory.Exists(path);
            }
            catch
            {
                // ignored
            }

            return false;
        }
    }
}