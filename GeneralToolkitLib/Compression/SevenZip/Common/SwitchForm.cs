namespace GeneralToolkitLib.Compression.SevenZip.Common
{
    public class SwitchForm
    {
        public string IDString;
        public int MaxLen;
        public int MinLen;
        public bool Multi;
        public string PostCharSet;
        public SwitchType Type;

        public SwitchForm(string idString, SwitchType type, bool multi, int minLen, int maxLen, string postCharSet)
        {
            IDString = idString;
            Type = type;
            Multi = multi;
            MinLen = minLen;
            MaxLen = maxLen;
            PostCharSet = postCharSet;
        }

        public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
            : this(idString, type, multi, minLen, 0, "")
        {
        }

        public SwitchForm(string idString, SwitchType type, bool multi)
            : this(idString, type, multi, 0)
        {
        }
    }
}