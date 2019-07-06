using System;
using System.Security.Cryptography;

namespace CompressionTest.Misc
{
    public static class Utils
    {
        public static  SerializiableTestClass  GetSerializiableTestClass(int dataSize)
        {
            SerializiableTestClass testClass = new SerializiableTestClass { Id = 1, Name = "Test class nr 1", Guid = Guid.NewGuid().ToString(), DataBytes = GetRandomBytes(dataSize, 0.5) };

            return testClass;
        }

        public static byte[] GetRandomBytes(int length, double randomRatio)
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
    }
}
