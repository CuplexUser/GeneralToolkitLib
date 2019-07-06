using System;
using System.Text;
using System.Windows.Forms;
using GeneralToolkitLib.Encryption.Licence;
using GeneralToolkitLib.Encryption.Licence.DataModels;

namespace GeneralToolkitLib.UserControls
{
    public partial class LicenceInfoControl : UserControl
    {
        private LicenceDataModel _licenceData;
        public Action CreateRequest;
        public Action EnterLicence;

        public string NotRegisteredInfoText { get; set; }

        public bool ValidLicence => LicenceService.Instance.ValidLicence;

        public LicenceInfoControl()
        {
            InitializeComponent();
        }

        public void InitLicenceData(LicenceDataModel licenceData)
        {
            _licenceData = licenceData;
            InitControlState();
        }

        private void InitControlState()
        {
            btnCreateRequest.Enabled = !ValidLicence;
            btnEnterLicence.Enabled = !ValidLicence;

            if (_licenceData?.RegistrationData == null)
                return;

            RegistrationDataModel registrationData = _licenceData.RegistrationData;
            this.txtLicenceInfo.Text = this.ValidLicence ? this.GetRegistrationDetails(registrationData) : this.NotRegisteredInfoText;
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

        private void LicenceInfoControl_Load(object sender, EventArgs e)
        {
            InitControlState();
        }

        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            CreateRequest?.Invoke();
        }

        private void btnEnterLicence_Click(object sender, EventArgs e)
        {
            EnterLicence?.Invoke();
        }
    }
}