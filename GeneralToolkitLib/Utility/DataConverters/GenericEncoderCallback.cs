using System;
using System.Text;

namespace GeneralToolkitLib.Utility.DataConverters
{
    public class GenericEncoderCallback
    {

        public static readonly Func<Encoding, string, byte[]> EncodeTextToBinary = delegate(Encoding encoder,  string textToEncode)
        {
            var result = encoder.GetBytes(textToEncode);
            return result;
        };

    }
}