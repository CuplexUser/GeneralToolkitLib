using System;
using System.Drawing;
using System.Windows.Forms;
using GeneralToolkitLib.Barcode;
using GeneralToolkitLib.OTP;

namespace GeneralToolkitLib.UserControls
{
    public partial class CreateAndVerifyOTP : UserControl
    {
        private readonly QRCodeGenerator _qrCodeGenerator;
        private readonly TimeAuthenticator _timeAuthenticator;
        private GoogleAuthenticator _googleAuthenticator;
        private Image _qrCodeImage;
        private bool _initialized;
        public event EventHandler CancelClicked;
        public event EventHandler CodeVerified;

        public bool OTPVerificationCompleted { get; private set; }

        public GoogleAuthenticator GoogleAuthenticator
        {
            get { return _googleAuthenticator; }
        }

        public CreateAndVerifyOTP()
        {
            InitializeComponent();

            _qrCodeGenerator = new QRCodeGenerator();
            _timeAuthenticator = new TimeAuthenticator();
            txtOTPSecret.Text = "";
        }

        private void CreateAndVerifyOTP_Load(object sender, EventArgs e)
        {
            pnlGenerateOTP.Location = new Point(0, 0);
            pnlVerifyOTP.Location = new Point(0, 0);
            pnlVerifyOTP.Dock = DockStyle.Fill;
            pnlGenerateOTP.Dock = DockStyle.Fill;

            SetVisiblePanel(OTPPanel.CreateQR);
        }

        public void Init(string label, int keyLength)
        {
        }


        public void Init(string label)
        {
            _initialized = true;
            _googleAuthenticator = new GoogleAuthenticator(label, Authenticator.GenerateKey());
            txtOTPSecret.Text = _googleAuthenticator.Secret;
            GenerateQRCode();
        }

        private void GenerateQRCode()
        {
            if (!_initialized) return;
            var qrCode = _qrCodeGenerator.CreateQrCode(_googleAuthenticator.KeyUri, QRCodeGenerator.ECCLevel.M);
            _qrCodeImage = qrCode.GetGraphic(50);
            pictureBoxQRCode.BackgroundImage = _qrCodeImage;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            if (CancelClicked != null)
                CancelClicked.Invoke(this, new EventArgs());
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SetVisiblePanel(OTPPanel.CreateQR);
        }

        private void btnShowVerificationPanel_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            SetVisiblePanel(OTPPanel.VerifyOTP);
        }

        public void SetVisiblePanel(OTPPanel otpPanel)
        {
            if (otpPanel == OTPPanel.CreateQR)
            {
                pnlGenerateOTP.Visible = true;
                pnlVerifyOTP.Visible = false;
            }
            else
            {
                pnlGenerateOTP.Visible = false;
                pnlVerifyOTP.Visible = true;
            }
        }

        public enum OTPPanel
        {
            CreateQR = 1,
            VerifyOTP = 2,
        }

        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            OTPVerificationCompleted = _timeAuthenticator.CheckCode(_googleAuthenticator.Secret, txtOTPCodeToVerify.Text);
            if (OTPVerificationCompleted)
            {
                txtVerifyResult.Text = "Code verified successfully";
                if (CodeVerified != null)
                    CodeVerified.Invoke(this, new EventArgs());
            }
            else
                txtVerifyResult.Text = "The code is incorrect";
        }
    }
}