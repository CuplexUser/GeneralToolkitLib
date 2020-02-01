using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GeneralToolkitLib.WindowsApi
{
    public static class LoadSystemIcon
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadImage(
            IntPtr hinst,
            string lpszName,
            uint uType,
            int cxDesired,
            int cyDesired,
            uint fuLoad);

        public static Bitmap GetShieldIcon()
        {
            var size = SystemInformation.SmallIconSize;
            var image = LoadImage(IntPtr.Zero, "#106", 1, size.Width, size.Height, 0);

            if (image == IntPtr.Zero)
            {
                return null;
            }

            using (var icon = Icon.FromHandle(image))
            {
                var bitmap = new Bitmap(size.Width, size.Height);

                using (var g = Graphics.FromImage(bitmap))
                {
                    g.DrawIcon(icon, new Rectangle(0, 0, size.Width, size.Height));
                }

                return bitmap;
            }
        }
    }
}
