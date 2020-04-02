using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GeneralToolkitLib.Converters;

namespace GeneralToolkitLib.Utility.RandomGenerator
{
    /// <summary>
    /// Secure Random generator
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class SecureRandomGenerator : IDisposable
    {
        /// <summary>
        /// The alpha numeric string
        /// </summary>
        private const string AlphaNumericStr = "0123456789abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ";
        /// <summary>
        /// The letter string
        /// </summary>
        private const string LetterStr = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ";
        /// <summary>
        /// The special character string
        /// </summary>
        private const string SpecialCharStr = "!+-=#_[]<>?$@" + AlphaNumericStr;
        /// <summary>
        /// The numeric string
        /// </summary>
        private const string NumericStr = "0123456789";
        /// <summary>
        /// The random number generator
        /// </summary>
        private readonly RandomNumberGenerator _randomNumberGenerator;


        /// <summary>
        /// Initializes a new instance of the <see cref="SecureRandomGenerator"/> class.
        /// </summary>
        public SecureRandomGenerator()
        {
            _randomNumberGenerator = new RNGCryptoServiceProvider();
        }

        /// <summary>
        /// Gets the random int.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">minValue</exception>
        public int GetRandomInt(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));

            if (minValue == maxValue) return minValue;
            long diff = maxValue - minValue;
            byte[] buffer = new byte[4];
            while (true)
            {
                _randomNumberGenerator.GetBytes(buffer);
                uint rand = BitConverter.ToUInt32(buffer, 0);

                long max = (1 + (long)uint.MaxValue);
                long remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (int)(minValue + rand % diff);
                }
            }
        }

        /// <summary>
        /// Gets the random data.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public byte[] GetRandomData(int length)
        {
            byte[] data = new byte[length];
            _randomNumberGenerator.GetBytes(data);
            return data;
        }

        /// <summary>
        /// Gets the alpha numeric string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public string GetAlphaNumericString(int length)
        {
            var buffer = new byte[length * 16];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (int i = 0; i < buffer.Length; i += 16)
            {
                ushort rndVal = BitConverter.ToUInt16(buffer, i);
                sb.Append(AlphaNumericStr[rndVal % AlphaNumericStr.Length]);
            }


            return sb.ToString();
        }


        /// <summary>
        /// Gets the numeric string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public string GetNumericString(int length)
        {
            var buffer = new byte[length * 16];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (int i = 0; i < buffer.Length; i += 16)
            {
                ushort rndVal = BitConverter.ToUInt16(buffer, i);
                sb.Append(NumericStr[rndVal % NumericStr.Length]);
            }

            if (sb.Length > length)
                sb.Remove(length, sb.Length - length);

            return sb.ToString();
        }


        /// <summary>
        /// Gets the password string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public string GetPasswordString(int length)
        {
            var buffer = new byte[(length * 16)];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (int i = 0; i < buffer.Length; i += 16)
            {
                ushort rndVal = BitConverter.ToUInt16(buffer, i);
                sb.Append(SpecialCharStr[rndVal % SpecialCharStr.Length]);
            }

            if (sb.Length > length)
                sb.Remove(length, sb.Length - length);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the hexadecimal string.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public string GetHexString(int length)
        {
            var buffer = new byte[length * 4];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (var i = 0; i < buffer.Length; i += 32)
            {
                uint rndInt = BitConverter.ToUInt32(buffer, i);
                sb.Append(rndInt.ToString("X"));
            }

            if (sb.Length > length)
                sb.Remove(length, sb.Length - length);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the base64 string.
        /// </summary>
        /// <param name="nrBytes">The nr bytes.</param>
        /// <returns></returns>
        public string GetBase64String(int nrBytes)
        {
            var buffer = new byte[nrBytes];
            _randomNumberGenerator.GetBytes(buffer);
            string b64Str = Convert.ToBase64String(buffer, Base64FormattingOptions.None);

            //if (b64Str.Length > length)
            //    b64Str = b64Str.Remove(length, b64Str.Length - length);

            return b64Str;
        }

        /// <summary>
        /// Gets the base32 string.
        /// </summary>
        /// <param name="nrBytes">The nr bytes.</param>
        /// <returns></returns>
        public string GetBase32String(int nrBytes)
        {
            var buffer = new byte[nrBytes];
            _randomNumberGenerator.GetBytes(buffer);
            string base32Str = Base32.ToBase32String(buffer);

            return base32Str;
        }

        /// <summary>
        /// Gets the random type of the string from password.
        /// </summary>
        /// <param name="passwordType">Type of the password.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public async Task<string> GetRandomStringFromPasswordType(PasswordTypes passwordType, int length)
        {
            return await Task.Run(delegate
            {
                switch (passwordType)
                {
                    case PasswordTypes.StandardMixedChars:
                        return GetPasswordString(length);

                    case PasswordTypes.AlphaNumeric:
                        return GetAlphaNumericString(length);
                    case PasswordTypes.Numeric:
                        return GetNumericString(length);
                    case PasswordTypes.Base64:
                        return GetBase64String(length);
                    case PasswordTypes.Hex:
                        return GetHexString(length);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(passwordType), passwordType, null);
                }
            });
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _randomNumberGenerator?.Dispose();
        }
    }
}