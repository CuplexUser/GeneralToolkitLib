using System;
using System.Diagnostics;
using System.IO;
using JetBrains.Annotations;
using Serilog;

namespace GeneralToolkitLib.Utility
{
    public static class FileSystem
    {
        public static bool OpenImageInDefaultAplication([NotNull] string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new ArgumentException("File does not exist", nameof(fileName));

                ProcessStartInfo psi = new ProcessStartInfo(fileName)
                {
                    UseShellExecute = true
                };
                Process.Start(psi);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "OpenImageInDefaultAplication");
                return false;
            }
        }
    }
}