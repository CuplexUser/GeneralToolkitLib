using System;
using System.Runtime.InteropServices;

namespace GeneralToolkitLib.WindowsApi
{
    public static class ScreenSaver
    {
        private static bool _screenSaverEnabled;
        public static bool ScreenSaverEnabled
        { get { return _screenSaverEnabled; } }

        [DllImport("kernel32.dll")]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        public static void Disable()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
            _screenSaverEnabled = false;
        }

        public static void Enable()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
            _screenSaverEnabled = true;
        }
    }
}
