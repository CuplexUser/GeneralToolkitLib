using System.Windows.Forms;

namespace GeneralToolkitLib.UserControls
{
    partial class LicenseInfoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateRequest = new System.Windows.Forms.Button();
            this.btnEnterLicense = new System.Windows.Forms.Button();
            this.grpLicense = new System.Windows.Forms.GroupBox();
            this.txtLicenseInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtComputerId = new System.Windows.Forms.TextBox();
            this.grpLicense.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateRequest
            // 
            this.btnCreateRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateRequest.Location = new System.Drawing.Point(268, 224);
            this.btnCreateRequest.Name = "btnCreateRequest";
            this.btnCreateRequest.Size = new System.Drawing.Size(109, 23);
            this.btnCreateRequest.TabIndex = 0;
            this.btnCreateRequest.Text = "Create Request";
            this.btnCreateRequest.Click += new System.EventHandler(this.btnCreateRequest_Click);
            // 
            // btnEnterLicense
            // 
            this.btnEnterLicense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnterLicense.Location = new System.Drawing.Point(383, 224);
            this.btnEnterLicense.Name = "btnEnterLicense";
            this.btnEnterLicense.Size = new System.Drawing.Size(109, 23);
            this.btnEnterLicense.TabIndex = 0;
            this.btnEnterLicense.Text = "Enter License";
            this.btnEnterLicense.Click += new System.EventHandler(this.btnEnterLicense_Click);
            // 
            // grpLicense
            // 
            this.grpLicense.Controls.Add(this.btnCreateRequest);
            this.grpLicense.Controls.Add(this.btnEnterLicense);
            this.grpLicense.Controls.Add(this.txtLicenseInfo);
            this.grpLicense.Controls.Add(this.label1);
            this.grpLicense.Controls.Add(this.label2);
            this.grpLicense.Controls.Add(this.txtComputerId);
            this.grpLicense.Location = new System.Drawing.Point(5, 5);
            this.grpLicense.Name = "grpLicense";
            this.grpLicense.Size = new System.Drawing.Size(500, 253);
            this.grpLicense.TabIndex = 0;
            this.grpLicense.TabStop = false;
            this.grpLicense.Text = "License";
            // 
            // txtLicenseInfo
            // 
            this.txtLicenseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenseInfo.Location = new System.Drawing.Point(90, 119);
            this.txtLicenseInfo.Margin = new System.Windows.Forms.Padding(5);
            this.txtLicenseInfo.Multiline = true;
            this.txtLicenseInfo.Name = "txtLicenseInfo";
            this.txtLicenseInfo.ReadOnly = true;
            this.txtLicenseInfo.Size = new System.Drawing.Size(402, 89);
            this.txtLicenseInfo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computer id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "License Info:";
            // 
            // txtComputerId
            // 
            this.txtComputerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComputerId.Location = new System.Drawing.Point(88, 26);
            this.txtComputerId.Margin = new System.Windows.Forms.Padding(5);
            this.txtComputerId.Multiline = true;
            this.txtComputerId.Name = "txtComputerId";
            this.txtComputerId.ReadOnly = true;
            this.txtComputerId.Size = new System.Drawing.Size(404, 83);
            this.txtComputerId.TabIndex = 0;
            // 
            // LicenseInfoControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.grpLicense);
            this.Name = "LicenseInfoControl";
            this.Size = new System.Drawing.Size(515, 265);
            this.Load += new System.EventHandler(this.LicenseInfoControl_Load);
            this.grpLicense.ResumeLayout(false);
            this.grpLicense.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLicense;
        private System.Windows.Forms.TextBox txtLicenseInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComputerId;
        private System.Windows.Forms.Button btnCreateRequest;
        private System.Windows.Forms.Button btnEnterLicense;
    }
}
