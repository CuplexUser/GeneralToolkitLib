using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GeneralToolkitLib.UserControls
{
    public partial class ProgressWaitControl : UserControl
    {
        private const int NumberOfCircles = 3;
        private const int MaxStateValue = 1000;
        private const int MinHeight = 20;
        private const int MinWidth = MinHeight*3;
        private bool _active;
        private bool _repaintBackground = true;
        private int _stateValue;

        public ProgressWaitControl()
        {
            InitializeComponent();
        }

        [DefaultValue("Apperence")]
        [Browsable(true)]
        public override string Text { get; set; }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Behavior")]
        public bool Active
        {
            get { return _active; }
            set
            {
                timerPBar.Enabled = value;
                SetWaitStateStatus(_active != value);
                _active = value;
            }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Category("Design")]
        public bool ShowText { get; set; }


        private void ProgressWaitControl_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                BackColor = Color.GhostWhite;
            }
        }

        private void SetWaitStateStatus(bool reset)
        {
            if (reset)
            {
                _stateValue = 1;
                _repaintBackground = true;
            }
        }

        private void RenderWaitState()
        {
            if (_stateValue > MaxStateValue)
            {
                _stateValue = 0;
                _repaintBackground = true;
            }


            _stateValue += 10;
            Refresh();
        }


        private void timerPBar_Tick(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                RenderWaitState();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            if (Height < MinHeight)
                Height = MinHeight;

            if (Width < MinWidth)
                Width = MinWidth;

            if (Height*NumberOfCircles > Width)
                Height = Width/NumberOfCircles;

            base.OnResize(e);
            _repaintBackground = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (_repaintBackground||!_active)
            {
                using (Graphics g = e.Graphics)
                {
                    Color c = BackColor;
                    if (Parent != null)
                        c = Parent.BackColor;

                    Brush b = new SolidBrush(c);
                    g.FillRectangle(b, g.ClipBounds);
                }
                _repaintBackground = false;
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            if (_active)
            {
                using (Graphics g = e.Graphics)
                {
                    Brush b = new SolidBrush(Color.Black);
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;

                    if (_stateValue < 1)
                        _stateValue = 1;

                    float percentCompleted = _stateValue/(float) MaxStateValue;
                    if (percentCompleted < 0.1)
                        percentCompleted = 0.1f;

                    if (percentCompleted > 1)
                        percentCompleted = 1;

                    Rectangle circleRectangle = new Rectangle(0, 0, Width/NumberOfCircles, Height);
                    Rectangle originalRect = new Rectangle(circleRectangle.Location, circleRectangle.Size);
                    g.ResetTransform();

                    int index = 0;
                    while (index < NumberOfCircles)
                    {

                        circleRectangle.Inflate((int) (originalRect.Width*percentCompleted), (int)(originalRect.Width * percentCompleted));


                        g.FillEllipse(b, circleRectangle);
                        g.TranslateTransform(originalRect.Width, 0);

                        index++;
                    }
                }
            }


            base.OnPaint(e);
        }

        private void ProgressWaitControl_Resize(object sender, EventArgs e)
        {
            _repaintBackground = true;
        }

        private void ProgressWaitControl_Leave(object sender, EventArgs e)
        {
            _repaintBackground = true;
        }
    }
}