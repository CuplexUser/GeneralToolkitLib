//using System.Drawing;
//using System.Windows.Forms;

//namespace GeneralToolkitLib.WindowsApi
//{
//    /// <span class="code-SummaryComment"><summary></span>
//    /// Class used to preserve / restore / maximize state of the form
//    /// <span class="code-SummaryComment"></summary></span>
//    public class FormState
//    {
//        private FormWindowState _formWindowState;
//        private FormBorderStyle _formBorderStyle;
//        private bool _topMost;
//        private Rectangle _screenBounds;
//        private readonly Form _form;
//        private bool _isFullscreen = false;
//        Rectangle screenRect = new Rectangle();
//        private const int SnapLength = 10;

//        public Rectangle FormBounds
//        {
//            get { return _bounds; }
//        }

//        public bool SnapToScreenEdges { get; set; }

//        public FormState(Form form)
//        {
//            _form = form;
//            BindEvents();
//        }

//        private void BindEvents()
//        {
//            _form.LocationChanged += this.LocationChanged;
//        }

//        private void LocationChanged(object sender, System.EventArgs e)
//        {

//            _form.RectangleToScreen(screenRect);



//        }

//        public bool Fullscreen
//        {
//            get
//            {
//                return _isFullscreen;
//            }
//        }


//        public void TogleFullscreen(bool enable)
//        {
//            if (_isFullscreen == enable)
//            {
//                return;
//            }


//            _isFullscreen = enable;
//            if (enable)
//            {
//                _form.FormBorderStyle = FormBorderStyle.None;
//                _form.TopMost = true;
//                WinApi.SetWinFullScreen(_form.Handle);
//            }
//            else
//            {

//            }






//        }

//        private void SaveFormState()
//        {
//            _formWindowState = _form.WindowState;
//            _formBorderStyle = _form.FormBorderStyle;
//            _topMost = _form.TopMost;
//            _screenBounds = _form.RectangleToScreen(_form.ClientRectangle);
//        }

//        public void RestoreForm()
//        {
//            _form.WindowState = _formWindowState;
//            _form.FormBorderStyle = _formBorderStyle;
//            _form.TopMost = _topMost;
//            _form.Bounds = _bounds;
//        }
//    }
//}
