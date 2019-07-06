using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GeneralToolkitLib.Utility.RandomGenerator
{
    public class SecureRandomGenerator
    {
        private const string AlphaNumericStr= "0123456789abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ";
        private const string LetterStr = "abcdefghijklmnopqrstuvxyzABCDEFGHIJKLMNOPQRSTUVXYZ";
        private const string SpecialCharStr = "!+-=#_[]<>?$@"+ AlphaNumericStr;
        private const string NumericStr = "0123456789";
        private readonly RandomNumberGenerator _randomNumberGenerator;
        

        public SecureRandomGenerator()
        {
            _randomNumberGenerator =new RNGCryptoServiceProvider($"r8vuGDGbXnOFa4Av9wZsz9bJYATmYtmD-Ticks:{DateTime.Now.Ticks}- nsVK3IAsQy2ccH3KyRHTLo9uxDpEplNO - Date:{DateTime.Now:hh:mm:ss t z}");
        }


        public int GetRandomInt(int minValue, int maxValue)
        {
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue));
            if (minValue == maxValue) return minValue;
            Int64 diff = maxValue - minValue;
            byte[] buffer = new byte[4];
            while (true)
            {
                _randomNumberGenerator.GetBytes(buffer);
                UInt32 rand = BitConverter.ToUInt32(buffer, 0);

                Int64 max = (1 + (Int64)UInt32.MaxValue);
                Int64 remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (Int32)(minValue + (rand % diff));
                }
            }
        }

        public byte[] GetRandomData(int length)
        {
            byte[] data= new byte[length];
            _randomNumberGenerator.GetBytes(data);
            return data;
        }

        public string GetAlphaNumericString(int length)
        {
            var buffer = new byte[length*16];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (int i = 0; i < buffer.Length; i+=16)
            {
                ushort rndVal = BitConverter.ToUInt16(buffer, i);
                sb.Append(AlphaNumericStr[rndVal % AlphaNumericStr.Length]);
            }


            return sb.ToString();
        }


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

        public string GetHexString(int length)
        {
            var buffer = new byte[length*4];
            var sb = new StringBuilder();
            _randomNumberGenerator.GetBytes(buffer);

            for (var i = 0; i < buffer.Length; i+=32)
            {
                uint rndInt = BitConverter.ToUInt32(buffer,i);
                sb.Append(rndInt.ToString("X"));
            }

            if (sb.Length > length)
                sb.Remove(length, sb.Length - length);

            return sb.ToString();
        }

        public string GetBase64String(int length)
        {
            var buffer = new byte[length];
            _randomNumberGenerator.GetBytes(buffer);
            string b64Str= Convert.ToBase64String(buffer, Base64FormattingOptions.None);
            if (b64Str.Length > length)
                b64Str=b64Str.Remove(length, b64Str.Length - length);
            return b64Str;
        }

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
    }
}