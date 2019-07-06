using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralToolkitLib.Converters
{
    public static class StringManipulation
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string ReverseXor(string s)
        {
            char[] charArray = s.ToCharArray();
            int len = s.Length - 1;

            for (int i = 0; i < len; i++, len--)
            {
                charArray[i] ^= charArray[len];
                charArray[len] ^= charArray[i];
                charArray[i] ^= charArray[len];
            }

            return new string(charArray);
        }

        public static string ReverseBySeparator(string s, string separator)
        {
            string[] strArr = s.Split(separator.ToCharArray());
            Array.Reverse(strArr);
            StringBuilder sb = new StringBuilder();

            foreach (string strSegment in strArr)
            {
                sb.Append(strSegment + separator);
            }

            return sb.ToString();
        }
    }
}