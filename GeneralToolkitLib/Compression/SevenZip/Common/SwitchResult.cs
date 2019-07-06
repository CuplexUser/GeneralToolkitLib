using System.Collections;

namespace GeneralToolkitLib.Compression.SevenZip.Common
{
    public class SwitchResult
    {
        public int PostCharIndex;
        public ArrayList PostStrings = new ArrayList();
        public bool ThereIs;
        public bool WithMinus;

        public SwitchResult()
        {
            ThereIs = false;
        }
    }
}