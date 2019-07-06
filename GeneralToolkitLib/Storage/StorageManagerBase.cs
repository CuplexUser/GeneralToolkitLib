using System;
using System.IO;

namespace GeneralToolkitLib.Storage
{
    public abstract class StorageManagerBase
    {
        protected static void CreateFilePathIfItDoesNotExist(string fullPath)
        {
            string directoryPath = GetDirectoryFromFullPath(fullPath);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        protected static string GetDirectoryFromFullPath(string fullPath)
        {
            int lastIndex = fullPath.LastIndexOf("\\", StringComparison.Ordinal);
            if (lastIndex > 0)
                return fullPath.Substring(0, lastIndex);

            return fullPath;
        }
    }
}