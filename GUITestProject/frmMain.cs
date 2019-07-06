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
            FormCreateSQL formCreateSql= new FormCreateSQL();
            formCreateSql.Show(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGenerateSharedSecret_Click(object sender, EventArgs e)
        {
            if(txtOTPLabel.Text.Length == 0)
            {
                MessageBox.Show("Label is missing!");
                return;
            }

            createAndVerifyOTP1.Init(txtOTPLabel.Text);
        }
    }
}
