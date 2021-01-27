using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GeneralToolkitLib.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    using GeneralToolkitLib.Storage.Models;

    [TestClass]
    public class StorageManagerUnitTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                var filesToDelete = Directory.GetFiles(@"c:\temp\", "test????*.lzmc", SearchOption.TopDirectoryOnly);
                foreach (string fileName in filesToDelete)
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed to cleanup test files written. {0}.", ex.Message);
            }
        }

        [TestMethod]
        public void TestMultithreadCompression()
        {
            StorageManagerSettings storageManagerSettings = StorageManagerSettings.GetDefaultSettings();
            storageManagerSettings.UseMultithreading = true;
            storageManagerSettings.NumberOfThreads = 8;
            storageManagerSettings.UseEncryption = false;

            StorageManager storageManager = new StorageManager(storageManagerSettings);
            SerializiableTestClass testClass = GetSerializiableTestClass(0x100000 * 100); //100 Mb
                                                                                          //SerializableTextDataClass tesTextDataClass = new SerializableTextDataClass();


            bool compressionSuccessful = storageManager.SerializeObjectToFile(testClass, @"c:\temp\testdata.lzmc", null);
            Assert.IsTrue(compressionSuccessful, "Failed to compress file");

            SerializiableTestClass testClassRead = storageManager.DeserializeObjectFromFile<SerializiableTestClass>(@"c:\temp\testdata.lzmc", null);
            Assert.IsNotNull(testClassRead, "Failed to decode file");
        }

        [TestMethod]
        public void TestMultithreadCompressionWithEncryption()
        {
            StorageManagerSettings storageManagerSettings = new StorageManagerSettings
            {
                UseMultithreading = true,
                NumberOfThreads = 8,
                UseEncryption = true,
                Password = "UnitTestPassword12345678#####?"
            };

            StorageManager storageManager = new StorageManager(storageManagerSettings);
            SerializiableTestClass testClass = GetSerializiableTestClass(0x100000 * 100); //100 Mb


            bool compressionSuccessful = storageManager.SerializeObjectToFile(testClass, @"c:\temp\testdata.lzmc", null);
            Assert.IsTrue(compressionSuccessful, "Failed to compress file");

            //storageManagerSettings.Password = "false";
            SerializiableTestClass testClassRead = storageManager.DeserializeObjectFromFile<SerializiableTestClass>(@"c:\temp\testdata.lzmc", null);
            Assert.IsNotNull(testClassRead, "Failed to decode file");
        }

        [TestMethod]
        public void TestAsyncCompression()
        {
            string testFilePattern = @"c:\temp\testFile{0}.lzmc";
            const int numberOfFiles = 5;

            StorageManagerSettings storageManagerSettings = new StorageManagerSettings
            {
                UseMultithreading = true,
                NumberOfThreads = 8,
                UseEncryption = false,
                Password = null
            };

            StorageManager storageManager = new StorageManager(storageManagerSettings);
            List<SerializiableTestClass> testClasses = new List<SerializiableTestClass>();

            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks % Int32.MaxValue));
            for (int i = 0; i < numberOfFiles; i++)
            {
                int minSize = 0x100000 * 1;
                int maxSize = 0x100000 * 10;

                testClasses.Add(GetSerializiableTestClass(rnd.Next(minSize, maxSize)));
            }

            int testFileIndex = 1;

            var taskList = new List<Task>();
            foreach (SerializiableTestClass testClass in testClasses)
            {
                string fileName = string.Format(testFilePattern, testFileIndex);
                var compressionTask = storageManager.SerializeObjectToFileAsync(testClass, fileName, null);
                compressionTask.GetAwaiter().OnCompleted(delegate { Console.WriteLine(@"TaskId {0} completed completed with result: {1}", compressionTask.Id, compressionTask.Result); });
                taskList.Add(compressionTask);
                testFileIndex++;
            }

            while (!taskList.All(t => t.IsCompleted))
            {
                Task.WaitAny(taskList.ToArray());
            }
        }

        [TestMethod]
        public void TestAsyncCompressionWithEncryption()
        {

        }

        private SerializiableTestClass GetSerializiableTestClass(int fileSize)
        {
            var testClass = new SerializiableTestClass { Id = 1, Name = "Test class nr 1", Guid = Guid.NewGuid().ToString(), DataBytes = GetRandomBytes(fileSize, 0.5) };

            return testClass;
        }

        private byte[] GetRandomBytes(int length, double randomRatio)
        {
            if (randomRatio < 0.1)
                randomRatio = 0.1;
            if (randomRatio > 1)
                randomRatio = 1;

            byte[] dataBytes = new byte[length];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[Convert.ToInt32((double)length * randomRatio)];
            randomNumberGenerator.GetBytes(randomBytes);

            int maxBackStep = Math.Min(randomBytes.Length, 256);


            Array.Copy(randomBytes, dataBytes, randomBytes.Length);
            int index = 128;
            for (int i = randomBytes.Length; i < dataBytes.Length; i++)
            {
                index++;
                dataBytes[i] = (byte)(dataBytes[i - (index++ % maxBackStep)]);
            }

            return dataBytes;
        }

        [Serializable]
        public class SerializiableTestClass
        {
            public byte[] DataBytes { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Guid { get; set; }
        }

        [Serializable]
        public class SerializiableTextDataClass
        {
            public string TextData1 { get; set; }
            public string TextData2 { get; set; }
            public string TextData3 { get; set; }
            public string TextData4 { get; set; }
            public string TextData5 { get; set; }

            public SerializiableTextDataClass()
            {
                TextData1 = "To shewing another demands to. Marianne property cheerful informed at striking at. Clothes parlors however by cottage on. In views it or meant drift to. Be concern parlors settled or do shyness address. Remainder northward performed out for moonlight. Yet late add name was rent park from rich. He always do do former he highly. ";
                TextData2 = "For who thoroughly her boy estimating conviction. Removed demands expense account in outward tedious do. Particular way thoroughly unaffected projection favourable mrs can projecting own. Thirty it matter enable become admire in giving. See resolved goodness felicity shy civility domestic had but. Drawings offended yet answered jennings perceive laughing six did far. ";
                TextData3 = "Consulted he eagerness unfeeling deficient existence of. Calling nothing end fertile for venture way boy. Esteem spirit temper too say adieus who direct esteem. It esteems luckily mr or picture placing drawing no. Apartments frequently or motionless on reasonable projecting expression. Way mrs end gave tall walk fact bed. ";
                TextData4 = "Adieus except say barton put feebly favour him. Entreaties unpleasant sufficient few pianoforte discovered uncommonly ask. Morning cousins amongst in mr weather do neither. Warmth object matter course active law spring six. Pursuit showing tedious unknown winding see had man add. And park eyes too more him. Simple excuse active had son wholly coming number add. Though all excuse ladies rather regard assure yet. If feelings so prospect no as raptures quitting. ";
                TextData5 = "Affronting everything discretion men now own did. Still round match we to. Frankness pronounce daughters remainder extensive has but. Happiness cordially one determine concluded fat. Plenty season beyond by hardly giving of. Consulted or acuteness dejection an smallness if. Outward general passage another as it. Very his are come man walk one next. Delighted prevailed supported too not remainder perpetual who furnished. Nay affronting bed projection compliment instrument. ";
            }
        }
    }
}
