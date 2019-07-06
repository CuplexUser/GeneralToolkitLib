using System.IO;
using System.Security.Cryptography;
using System.Text;
using GeneralToolkitLib.Converters;

namespace GeneralToolkitLib.Hashing
{
    public class MD5 : IHashTransform
    {
        private readonly HashAlgorithm md5Implementation;

        public MD5()
        {
            md5Implementation = System.Security.Cryptography.MD5.Create();
        }

        public byte[] ComputeHash(Stream inputStream)
        {
            return md5Implementation.ComputeHash(inputStream);
        }

        public string ComputeHashOnString(string inputData)
        {
            byte[] strBytes = Encoding.UTF8.GetBytes(inputData);
            byte[] hashedBytes = md5Implementation.ComputeHash(strBytes);

            return GetMD5HashAsHexString(hashedBytes);
        }

        public static string GetMD5HashAsHexString(byte[] data)
        {
            var md5Implementation = System.Security.Cryptography.MD5.Create();
            byte[] hashData = md5Implementation.ComputeHash(data);
            return GeneralConverters.ByteArrayToHexString(hashData);
        }

        public int HashSize
        {
            get { return 128; }
        }

        public void Report(HashProgress value)
        {
            throw new System.NotImplementedException();
        }
    }
}