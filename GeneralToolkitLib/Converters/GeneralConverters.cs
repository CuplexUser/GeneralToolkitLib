using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GeneralToolkitLib.Converters
{
    public static class GeneralConverters
    {
        public static int GetSecondsFromDateTime(DateTime date)
        {
            return date.Hour * 3600 + date.Minute * 60 + date.Second;
        }

        public static string GetFileNameFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path can not be null or empty");

            int iPos = path.LastIndexOf('\\') + 1;
            if (iPos > 1 && iPos != path.Length)
                return path.Substring(iPos, path.Length - iPos);

            return path;
        }

        public static string GetDirectoryNameFromPath(string path, bool trailingSlash = true)
        {
            int lastBackSlash = path.LastIndexOf('\\');

            if (lastBackSlash > 0)
            {
                if (trailingSlash)
                    lastBackSlash++;
                return path.Substring(0, lastBackSlash);
            }
            return path;
        }

        public static string GetVolumeLabelFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            var pathSplitArr = path.Split('\\');
            if (pathSplitArr.Length > 0)
                return pathSplitArr[0] + "\\";

            return "";
        }

        public static string FormatFileSizeToString(long byteCount, int decimales = 1)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), decimales);
            return (Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture) + " " + suf[place];
        }

        public static byte[] StringToByteArray(string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString);
        }

        public static string ByteArrayToHexString(byte[] data)
        {
            var sb = new StringBuilder();
            foreach (byte b in data)
                sb.AppendFormat("{0:X2}", b);

            return sb.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string GetRandomHexValue(int length)
        {
            if (length < 2)
                throw new ArgumentException("Data length must be atleast 2");

            using (RandomNumberGenerator rndGen = RandomNumberGenerator.Create())
            {
                var buffer = new byte[length / 2];
                rndGen.GetBytes(buffer);
                return ByteArrayToHexString(buffer);
            }
        }

        public static string FileAttributesToString(FileAttributes fileAttributes)
        {
            string attributes = "";
            if (fileAttributes.HasFlag(FileAttributes.ReadOnly))
                attributes += "R";
            if (fileAttributes.HasFlag(FileAttributes.Hidden))
                attributes += "H";
            if (fileAttributes.HasFlag(FileAttributes.System))
                attributes += "S";
            if (fileAttributes.HasFlag(FileAttributes.Directory))
                attributes += "D";
            if (fileAttributes.HasFlag(FileAttributes.Archive))
                attributes += "A";
            if (fileAttributes.HasFlag(FileAttributes.Device))
                attributes += " DV ";
            if (fileAttributes.HasFlag(FileAttributes.Normal))
                attributes += "";
            if (fileAttributes.HasFlag(FileAttributes.Temporary))
                attributes += "TMP";
            if (fileAttributes.HasFlag(FileAttributes.Compressed))
                attributes += "C";
            if (fileAttributes.HasFlag(FileAttributes.Encrypted))
                attributes += " ENC ";

            return attributes.Trim();
        }

        public static string GeneratePasswordDerivedString(string verifiedPassword)
        {
            using (SHA512 sha512Implementation = SHA512.Create())
            {
                const string salt1 =
                    "MVtdiy4OhAMRMKDSKUojgAirwacYRuUnT9R84DgnwOaOl0QTppuv8m3poCaElfKVBlEClohoXusGzg6vOUEgHK7yHj0vzq8eedTX0sHkmrk1sDH1AXMJ1ELODvbia6R0but4npqsVzuT3q3GukH20pswOatqLVzMSuPZrigZKRUqlJMeG4NoNqkdJyh0QPKQDeznEshFB7VqwIiqeMMtDNQx6H4HXBibpMqBhV2Ptcbf3MdkKvg8stdjsS6cd7ds";
                const string salt2 =
                    "Nv7YkXUghdWRN2MJVF6fe8p3Llo4D5rsHckmtzPdJnsLRQisCT442Wh1nIdnmbgGxE4NuEBTjtthzM42mmFT74knRWiVhoXpBoQWdOc0njJGikZNJbSMQU3sZwtq5uhRNb3WKyRSfOM0RoRB6KqRO5ItxdhxXYVjSEvYq0NlMVllydrV7NjR65eaVdl6RIxHe6y42O3j79N0dL67aeoxTHTRP0YxnfxiOqLtIsdMB2Wb2xulXht9UZjTcTLLMe09";

                var hashInputArray = Encoding.UTF8.GetBytes(salt1 + verifiedPassword + salt2);
                var hashedBytes = sha512Implementation.ComputeHash(hashInputArray, 0, hashInputArray.Length);

                for (int i = 0; i < 10; i++)
                    hashedBytes = sha512Implementation.ComputeHash(hashedBytes, 0, hashedBytes.Length);

                return ByteArrayToHexString(hashedBytes);
            }
        }

        public static byte[] GetByteArrayFromString(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetStringFromByteArray(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }


        public static string ByteArrayToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private struct StaticLongFileSizes
        {
            public static readonly string[] FileSizeTypesStrings = { "b", "kB", "Mb", "Gb", "Tb", "Pbyte" };
        }

        public static class FileSizeToStringFormatter
        {
            public enum FileSizeSteps : long
            {
                Byte = 0x0,
                KiloByte = 0x400,
                MegaByte = 0x100000,
                GigaByte = 0x40000000,
                PetaByte = 0x10000000000
            }

            private static readonly string[] FileSizeTypesStrings = { " B", " KB", " MB", " GB", " TB", " PByte" };

            public static string ConvertFileSizeToString(long fileSize)
            {
                if (fileSize > (long)FileSizeSteps.KiloByte)
                {
                    int arrayPos = 0;
                    do
                    {
                        fileSize = fileSize >> 0xA;
                        arrayPos++;
                    } while (fileSize > 0x400);
                    return fileSize + FileSizeTypesStrings[arrayPos];
                }
                return fileSize + FileSizeTypesStrings[0];
            }

            public static string ConvertFileSizeToString(long fileSize, int decimals)
            {
                if (fileSize > (long)FileSizeSteps.KiloByte)
                {
                    if (decimals > 8 || decimals < 0)
                        decimals = 8;

                    double decData = fileSize;
                    int arrayPos = 0;
                    do
                    {
                        decData = decData / 1024d;
                        arrayPos++;
                    } while (decData > 0x400);


                    return Math.Round(decData, decimals) + FileSizeTypesStrings[arrayPos];
                }
                return fileSize + FileSizeTypesStrings[0];
            }

            public struct OffsetRange
            {
                public FileSizeSteps FileSizeStep;
                public long Max;
            }
        }
    }
}