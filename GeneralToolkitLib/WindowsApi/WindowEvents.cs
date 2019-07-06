namespace GeneralToolkitLib.WindowsApi
{
    public static class WindowEvents
    {
        public const int WM_SYSCOMMAND = 0x0112;

        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;

        ///<summary>
        /// Sent when form maximizes
        ///</summary>
        public const int SC_MAXIMIZE = 0xF030;

        ///<summary>
        /// Sent when form maximizes because of doubcle click on caption
        /// JTE: Don't use this constant. As per the documentation; you
        ///      must mask off the last 4 bits of wParam by AND'ing it
        ///      with 0xFFF0. You can't assume the last 4 bits. 
        ///</summary>
        public const int SC_MAXIMIZE2 = 0xF032;

        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;

        ///<summary>
        /// Closes the form
        ///</summary>
        public const int SC_CLOSE = 0xF060;

        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;

        ///<summary>
        /// Sent when form is maximized from the taskbar
        ///</summary>
        public const int SC_RESTORE = 0xF120;

        ///<summary>
        /// Sent when form maximizes because of doubcle click on caption
        /// JTE: Don't use this constant. As per the documentation; you
        ///      must mask off the last 4 bits of wParam by AND'ing it
        ///      with 0xFFF0. You can't assume the last 4 bits. 
        ///</summary>
        public const int SC_RESTORE2 = 0xF122;

        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;

        // Keyboard
        public const int WM_KEYDOWN = 0x100;
        public const int WM_SYSKEYDOWN = 0x104;


        // Windows form override function
        /*
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            protected override void WndProc(ref Message m)
            {
            }
        */
    }
}