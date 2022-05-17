using GeneralToolkitLib.Converters;
using GeneralToolkitLib.OTP;
using System;
using System.Windows.Forms;

namespace GUITestProject
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //createAndVerifyOTP1.Init("Test Label123");
        }

        private void openCreateSQLFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreateSQL formCreateSql = new FormCreateSQL();
            formCreateSql.Show(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGenerateSharedSecret_Click(object sender, EventArgs e)
        {
            if (txtOTPLabel.Text.Length == 0)
            {
                MessageBox.Show("Label is missing!");
                return;
            }

            Authenticator.SecretKeyLength keyLengthEnumVal = Authenticator.SecretKeyLength.n16Bytes;

            if (rb24.Checked)
            {
                keyLengthEnumVal = Authenticator.SecretKeyLength.n24Bytes;
            }
            else if (rb32.Checked)
            {
                keyLengthEnumVal = Authenticator.SecretKeyLength.n32bytes;
            }

            createAndVerifyOTP1.Init(txtOTPLabel.Text, keyLengthEnumVal);
        }

        private void btnVerifyEncodingAndImport_Click(object sender, EventArgs e)
        {
            string importKey = txtImportSecret.Text;
            byte[] keyBytes;
            try
            {
                keyBytes = Base32Encoding.ToBytes(importKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid key format!", "Unable to import key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
                txtImportSecret.SelectAll();
                txtImportSecret.Select();
                return;
            }

            createAndVerifyOTP1.InitWithExistingBase32Key(txtOTPLabel.Text, Base32.ToBase32String(keyBytes));
            txtImportSecret.Text = "";
        }
    }
}
