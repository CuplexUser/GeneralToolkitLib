using System;
using System.Diagnostics;
using CompressionTest.Misc;
using GeneralToolkitLib.Logging;
using GeneralToolkitLib.Storage;
using GeneralToolkitLib.Storage.Models;
using Serilog;

namespace CompressionTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            string testDataFilePath1 = Environment.CurrentDirectory + "\\orgImpl.7z";
            string testDataFilePath2 = Environment.CurrentDirectory + "\\managedLZMA.7z";

            const int dataLength = 0x19000000;
            Stopwatch stopwatch = new Stopwatch();
            SerializiableTestClass testClass = Utils.GetSerializiableTestClass(dataLength);
            WriteToConsoleAndLog(string.Format("Test begining with {0} data using current C# single thread implementation", GeneralToolkitLib.Converters.GeneralConverters.FileSizeToStringFormatter.ConvertFileSizeToString(dataLength)));

            StorageManagerSettings settings = new StorageManagerSettings(true, 8, false, null);
            StorageManager storageManager = new StorageManager(settings);
            stopwatch.Start();
            storageManager.SerializeObjectToFile(testClass, testDataFilePath1, null);
            stopwatch.Stop();
            WriteToConsoleAndLog("Test ended after: " + stopwatch.Elapsed);

            ManagedIncludeDllCompression sevenZipCompression= new ManagedIncludeDllCompression();


            stopwatch.Reset();
            stopwatch.Start();
            WriteToConsoleAndLog(string.Format("Test begining with {0} data using LZMA managed dll implementation", GeneralToolkitLib.Converters.GeneralConverters.FileSizeToStringFormatter.ConvertFileSizeToString(dataLength)));
            sevenZipCompression.TestManagedIncludeDllCompression(testDataFilePath2, testClass);
            WriteToConsoleAndLog("Test ended after: " + stopwatch.Elapsed);
            stopwatch.Stop();


            Console.ReadLine();
        }

        private static void WriteToConsoleAndLog(string lineData)
        {
            Console.WriteLine(lineData);
            Log.Information(lineData);
        }
    }
}