namespace GeneralToolkitLib.Compression.SevenZip.Common
{
    public class Parser
    {
        private const char kSwitchID1 = '-';
        private const char kSwitchID2 = '/';
        private const char kSwitchMinus = '-';
        private readonly SwitchResult[] _switches;

        public SwitchResult this[int index]
        {
            get { return _switches[index]; }
        }

        public Parser(int numSwitches)
        {
            _switches = new SwitchResult[numSwitches];
            for (int i = 0; i < numSwitches; i++)
            {
                _switches[i] = new SwitchResult();
            }
        }

        private static bool IsItSwitchChar(char c)
        {
            return (c == kSwitchID1 || c == kSwitchID2);
        }
    }
}