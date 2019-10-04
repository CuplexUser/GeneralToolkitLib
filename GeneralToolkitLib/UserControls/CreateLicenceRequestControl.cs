using System;
using System.Windows.Forms;
using GeneralToolkitLib.Encryption.License;
using GeneralToolkitLib.Encryption.License.DataModels;

namespace GeneralToolkitLib.UserControls
{
    public partial class CreateLicenseRequestControl : UserControl
    {
        private RegistrationDataModel _registrationData;
        private RegistrationDataManager _registrationDataManager;

        public RegistrationDataModel RegistrationData
        {
            get { return _registrationData; }
        }

        public CreateLicenseRequestControl()
        {
            InitializeComponent();
            lblInfo.Text = "";
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text.Length < 2)
                lblInfo.Text = "Missing firstname";
            if (txtLastName.Text.Length < 2)
                lblInfo.Text = "Missing lastname";
            if (txtCompany.Text.Length < 2)
                lblInfo.Text = "Missing company";

            _registrationData = new RegistrationDataModel
            {
                FirstName = this.txtFirstName.Text,
                LastName = this.txtLastName.Text,
                Company = this.txtCompany.Text,
                ComputerId = SysInfoManager.GetComputerId()
            };

            _registrationDataManager = RegistrationDataManager.Create(_registrationData);
            txtLicenseRequest.Text = _registrationDataManager.SerializeToString();
        }
    }
}