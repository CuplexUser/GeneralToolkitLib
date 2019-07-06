using System.IO;
using GeneralToolkitLib.Converters;

namespace GeneralToolkitLib.Hashing
{
    public class SHA512 : IHashTransform
    {
        public byte[] ComputeHash(Stream inputStream)
        {
            System.Security.Cryptography.SHA512 sha512Implementation = System.Security.Cryptography.SHA512.Create();
            return sha512Implementation.ComputeHash(inputStream);
        }

        public int HashSize => 512;

        public static string GetSHA512HashAsHexString(string inputString)
        {
            return GetSHA512HashAsHexString(GeneralConverters.GetByteArrayFromString(inputString));
        }

        public static string GetSHA512HashAsHexString(byte[] data)
        {
            System.Security.Cryptography.SHA256 sha512Implementation = System.Security.Cryptography.SHA256.Create();
            var hashData = sha512Implementation.ComputeHash(data);
            return GeneralConverters.ByteArrayToHexString(hashData);
        }

        public static byte[] GetSHA512HashAsByteArray(byte[] data)
        {
            System.Security.Cryptography.SHA256 sha512Implementation = System.Security.Cryptography.SHA256.Create();
            return sha512Implementation.ComputeHash(data);
        }
    }
}