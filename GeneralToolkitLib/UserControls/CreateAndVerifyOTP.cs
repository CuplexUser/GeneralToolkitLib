using System;
using System.Drawing;
using System.Windows.Forms;
using GeneralToolkitLib.Barcode;
using GeneralToolkitLib.OTP;
using Serilog;

namespace GeneralToolkitLib.UserControls
{
    public partial class CreateAndVerifyOTP : UserControl
    {
        private const bool ShowFullDatetimeUnderVerification = false;
        private readonly QRCodeGenerator _qrCodeGenerator;
        private readonly TimeAuthenticator _timeAuthenticator;
        private GoogleAuthenticator _googleAuthenticator;
        private Image _qrCodeImage;
        private bool _initialized;
        public event EventHandler CancelClicked;
        public event EventHandler CodeVerified;
        public event EventHandler CodeVerificationFailed;

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

        public void InitWithExistingBase32Key(string label, string secretKey)
        {
            try
            {
                _initialized = true;
                _googleAuthenticator = new GoogleAuthenticator(label, secretKey);
                txtOTPSecret.Text = _googleAuthenticator.Secret;
                GenerateQRCode();
                
            }
            catch (Exception exception)
            {
                _initialized = false;
                txtOTPSecret.Text = "";
                pictureBoxQRCode.Image = pictureBoxQRCode.ErrorImage;
                Log.Error(exception, "InitWithExistingBase32Key failed! using {label} and {secretKey}", label, secretKey);
            }
        }


        public void Init(string label, Authenticator.SecretKeyLength keyLength)
        {
            _initialized = true;
            _googleAuthenticator = new GoogleAuthenticator(label, Authenticator.GenerateKey(keyLength));
            txtOTPSecret.Text = _googleAuthenticator.Secret;
            GenerateQRCode();
        }

        private void GenerateQRCode()
        {
            if (!_initialized) return;
            var qrCode = _qrCodeGenerator.CreateQrCode(_googleAuthenticator.KeyUri, QRCodeGenerator.ECCLevel.M);
            _qrCodeImage = qrCode.GetGraphic(50);
            pictureBoxQRCode.BackgroundImage = _qrCodeImage;
            pictureBoxQRCode.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!_initialized) return;
            CancelClicked?.Invoke(this, new EventArgs());
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
                CodeVerified?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                txtVerifyResult.Text = "The code is incorrect";
                CodeVerificationFailed?.Invoke(this, EventArgs.Empty);
            }

#if _SHOW_FULL_DATE_TIME_
            txtVerifyResult.Text += $"\r\n{DateTime.Now:yyyy-MM-dd / HH:mm:ss}";
#else
            txtVerifyResult.Text += $"\r\n{DateTime.Now:HH:mm:ss}";
#endif
        }

    }
}