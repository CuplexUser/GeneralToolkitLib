using System;
using System.Security.Cryptography;
using GeneralToolkitLib.Converters;
using GeneralToolkitLib.Utility.RandomGenerator;

namespace GeneralToolkitLib.OTP
{
    public abstract class Authenticator
    {
        //private readonly RNGCryptoServiceProvider Random = new RNGCryptoServiceProvider(); // Is Thread-Safe
        //private int KeyLength = 16;
        //private const string AvailableKeyChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

        public enum SecretKeyLength
        {
            n16Bytes = 16,
            n24Bytes = 24,
            n32bytes = 32,
        }

        
        public static string GenerateKey(SecretKeyLength skLength)
        {
            int keyLength = (int)skLength;
            string generatedKey;

            using (var secureRng = new SecureRandomGenerator())
            {
                generatedKey = secureRng.GetBase32String(keyLength);
            }

            return generatedKey;
        }

        protected string GetCodeInternal(string secret, ulong challengeValue)
        {
            ulong chlg = challengeValue;
            byte[] challenge = new byte[8];
            for (int j = 7; j >= 0; j--)
            {
                challenge[j] = (byte)((int)chlg & 0xff);
                chlg >>= 8;
            }

            var key = Base32Encoding.ToBytes(secret);
            for (int i = secret.Length; i < key.Length; i++)
            {
                key[i] = 0;
            }

            HMACSHA1 mac = new HMACSHA1(key);
            var hash = mac.ComputeHash(challenge);

            int offset = hash[hash.Length - 1] & 0xf;

            int truncatedHash = 0;
            for (int j = 0; j < 4; j++)
            {
                truncatedHash <<= 8;
                truncatedHash |= hash[offset + j];
            }

            truncatedHash &= 0x7FFFFFFF;
            truncatedHash %= 1000000;

            string code = truncatedHash.ToString();
            return code.PadLeft(6, '0');
        }

        protected bool ConstantTimeEquals(string a, string b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;

            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)a[i] ^ b[i];
            }

            return diff == 0;
        }
    }
}