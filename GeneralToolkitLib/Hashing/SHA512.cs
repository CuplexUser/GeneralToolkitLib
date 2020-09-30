using System.IO;
using GeneralToolkitLib.Converters;

namespace GeneralToolkitLib.Hashing
{
    /// <summary>Secure Hash Algorithm 512, transform functions implemented </summary>
    public class SHA512 : IHashTransform
    {
        /// <summary>
        /// Computes the hash.
        /// </summary>
        /// <param name="inputStream">The input stream.</param>
        /// <returns></returns>
        public byte[] ComputeHash(Stream inputStream)
        {
            System.Security.Cryptography.SHA512 sha512Implementation = System.Security.Cryptography.SHA512.Create();
            return sha512Implementation.ComputeHash(inputStream);
        }

        /// <summary>
        /// Gets the size of the hash.
        /// </summary>
        /// <value>
        /// The size of the hash.
        /// </value>
        public int HashSize => 512;

        /// <summary>
        /// Gets the sh a512 hash as hexadecimal string.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns></returns>
        public static string GetSHA512HashAsHexString(string inputString)
        {
            return GetSHA512HashAsHexString(GeneralConverters.GetByteArrayFromString(inputString));
        }

        /// <summary>
        /// Gets the sh a512 hash as hexadecimal string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string GetSHA512HashAsHexString(byte[] data)
        {
            System.Security.Cryptography.SHA512 sha512Implementation = System.Security.Cryptography.SHA512.Create();
            var hashData = sha512Implementation.ComputeHash(data);
            return GeneralConverters.ByteArrayToHexString(hashData);
        }

        /// <summary>
        /// Gets the sh a512 hash as byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static byte[] GetSHA512HashAsByteArray(byte[] data)
        {
            System.Security.Cryptography.SHA512 sha512Implementation = System.Security.Cryptography.SHA512.Create();
            return sha512Implementation.ComputeHash(data);
        }
    }
}