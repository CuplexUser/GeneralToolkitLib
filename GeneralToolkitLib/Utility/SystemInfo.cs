using System;
using System.Linq;
using System.Management;
using System.Text;
using Serilog;


namespace GeneralToolkitLib.Utility
{
    internal static class SystemInfo
    {
        #region -> Private Variables

        public static bool UseProcessorID;
        public static bool UseBaseBoardProduct;
        public static bool UseBaseBoardManufacturer;
        public static bool UseDiskDriveSignature;
        public static bool UseVideoControllerCaption;
        public static bool UsePhysicalMediaSerialNumber;
        public static bool UseBiosVersion;
        public static bool UseBiosManufacturer;
        public static bool UseWindowsSerialNumber;

        #endregion

        public static string GetSystemInfo(string softwareName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(softwareName);

            if (UseProcessorID)
                sb.AppendLine(RunQuery("Processor", "ProcessorId"));

            if (UseBaseBoardProduct)
                sb.AppendLine(RunQuery("BaseBoard", "Product"));

            if (UseBaseBoardManufacturer)
                sb.AppendLine(RunQuery("BaseBoard", "Manufacturer"));

            if (UseDiskDriveSignature)
                sb.AppendLine(RunQuery("DiskDrive", "__RELPATH"));

            if (UseVideoControllerCaption)
                sb.AppendLine(RunQuery("VideoController", "Caption"));

            if (UsePhysicalMediaSerialNumber)
                sb.AppendLine(RunQuery("PhysicalMedia", "SerialNumber"));

            if (UseBiosVersion)
                sb.AppendLine(RunQuery("BIOS", "Version"));

            if (UseBiosManufacturer)
                sb.AppendLine(RunQuery("BIOS", "Manufacturer"));

            if (UseWindowsSerialNumber)
                sb.AppendLine(RunQuery("OperatingSystem", "SerialNumber"));

            return sb.ToString();
        }

        private static string RunQuery(string tableName, string methodName)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_" + tableName);
            foreach (var mo in mos.Get().Cast<ManagementObject>())
            {
                try
                {
                    return mo.GetPropertyValue(methodName).ToString();
                }
                catch (Exception e)
                {
                    Log.Error(e,"SystemInfo.RunQuery()");
                }
            }
            return "";
        }
    }
}