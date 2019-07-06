using System.IO;
using GeneralToolkitLib.Converters;

namespace GeneralToolkitLib.Hashing
{
    public class SHA256 : IHashTransform
    {
        public byte[] ComputeHash(Stream inputStream)
        {
            System.Security.Cryptography.SHA256 sha256Implementation = System.Security.Cryptography.SHA256.Create();
            return sha256Implementation.ComputeHash(inputStream);
        }

        public int HashSize => 256;

        public static string GetSHA256HashAsHexString(byte[] data)
        {
            System.Security.Cryptography.SHA256 sha256Implementation = System.Security.Cryptography.SHA256.Create();
            var hashData = sha256Implementation.ComputeHash(data);
            return GeneralConverters.ByteArrayToHexString(hashData);
        }

        public static string GetSHA256HashAsHexString(string inputString)
        {
            return GetSHA256HashAsHexString(GeneralConverters.GetByteArrayFromString(inputString));
        }

        public static byte[] GetSHA256HashAsByteArray(byte[] data)
        {
            System.Security.Cryptography.SHA256 sha256Implementation = System.Security.Cryptography.SHA256.Create();
            return sha256Implementation.ComputeHash(data);
        }
    }
}