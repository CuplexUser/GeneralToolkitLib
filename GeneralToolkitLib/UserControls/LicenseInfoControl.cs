using System;
using System.Text;
using System.Windows.Forms;
using GeneralToolkitLib.Encryption.License;
using GeneralToolkitLib.Encryption.License.DataModels;

namespace GeneralToolkitLib.UserControls
{
    public partial class LicenseInfoControl : UserControl
    {
        private LicenseDataModel _licenseData;
        public Action CreateRequest;
        public Action EnterLicense;

        public string NotRegisteredInfoText { get; set; }

        public bool ValidLicense =>LicenseService.Instance.ValidLicense;

        public LicenseInfoControl()
        {
            InitializeComponent();
        }

        public void InitLicenseData(LicenseDataModel licenseData)
        {
            _licenseData = licenseData;
            InitControlState();
        }

        private void InitControlState()
        {
            btnCreateRequest.Enabled = !ValidLicense;
            btnEnterLicense.Enabled = !ValidLicense;

            if (_licenseData?.RegistrationData == null)
                return;

            RegistrationDataModel registrationData = _licenseData.RegistrationData;
            txtLicenseInfo.Text = this.ValidLicense ? this.GetRegistrationDetails(registrationData) : this.NotRegisteredInfoText;
            txtComputerId.Text = registrationData.ComputerId.ComputerId;
        }

        private string GetRegistrationDetails(RegistrationDataModel registrationData)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(registrationData.FirstName + " " + registrationData.LastName);
            sb.AppendLine("Company: " + registrationData.Company);
            sb.AppendLine("Valid to: " + registrationData.ValidTo.ToString("Y"));

            return sb.ToString();
        }

        private void LicenseInfoControl_Load(object sender, EventArgs e)
        {
            InitControlState();
        }

        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            CreateRequest?.Invoke();
        }

        private void btnEnterLicense_Click(object sender, EventArgs e)
        {
            EnterLicense?.Invoke();
        }
    }
}