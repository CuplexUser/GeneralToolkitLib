namespace GeneralToolkitLib.UserControls
{
    partial class LicenceInfoControl
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
            this.grpLicence = new System.Windows.Forms.GroupBox();
            this.btnCreateRequest = new System.Windows.Forms.Button();
            this.btnEnterLicence = new System.Windows.Forms.Button();
            this.txtLicenceInfo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComputerId = new System.Windows.Forms.TextBox();
            this.grpLicence.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLicence
            // 
            this.grpLicence.Controls.Add(this.btnCreateRequest);
            this.grpLicence.Controls.Add(this.btnEnterLicence);
            this.grpLicence.Controls.Add(this.txtLicenceInfo);
            this.grpLicence.Controls.Add(this.label2);
            this.grpLicence.Controls.Add(this.label1);
            this.grpLicence.Controls.Add(this.txtComputerId);
            this.grpLicence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLicence.Location = new System.Drawing.Point(0, 0);
            this.grpLicence.Name = "grpLicence";
            this.grpLicence.Size = new System.Drawing.Size(305, 240);
            this.grpLicence.TabIndex = 0;
            this.grpLicence.TabStop = false;
            this.grpLicence.Text = "Licence Information";
            // 
            // btnCreateRequest
            // 
            this.btnCreateRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateRequest.Location = new System.Drawing.Point(23, 201);
            this.btnCreateRequest.Name = "btnCreateRequest";
            this.btnCreateRequest.Size = new System.Drawing.Size(110, 25);
            this.btnCreateRequest.TabIndex = 9;
            this.btnCreateRequest.Text = "Request Licence ";
            this.btnCreateRequest.UseVisualStyleBackColor = true;
            this.btnCreateRequest.Click += new System.EventHandler(this.btnCreateRequest_Click);
            // 
            // btnEnterLicence
            // 
            this.btnEnterLicence.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnterLicence.Location = new System.Drawing.Point(171, 201);
            this.btnEnterLicence.Name = "btnEnterLicence";
            this.btnEnterLicence.Size = new System.Drawing.Size(110, 25);
            this.btnEnterLicence.TabIndex = 8;
            this.btnEnterLicence.Text = "Enter Licence";
            this.btnEnterLicence.UseVisualStyleBackColor = true;
            this.btnEnterLicence.Click += new System.EventHandler(this.btnEnterLicence_Click);
            // 
            // txtLicenceInfo
            // 
            this.txtLicenceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicenceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLicenceInfo.Location = new System.Drawing.Point(23, 41);
            this.txtLicenceInfo.Multiline = true;
            this.txtLicenceInfo.Name = "txtLicenceInfo";
            this.txtLicenceInfo.ReadOnly = true;
            this.txtLicenceInfo.Size = new System.Drawing.Size(258, 100);
            this.txtLicenceInfo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Registered to:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Computer Id:";
            // 
            // txtComputerId
            // 
            this.txtComputerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComputerId.Location = new System.Drawing.Point(23, 166);
            this.txtComputerId.Name = "txtComputerId";
            this.txtComputerId.ReadOnly = true;
            this.txtComputerId.Size = new System.Drawing.Size(258, 20);
            this.txtComputerId.TabIndex = 4;
            // 
            // LicenceInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpLicence);
            this.Name = "LicenceInfoControl";
            this.Size = new System.Drawing.Size(305, 240);
            this.Load += new System.EventHandler(this.LicenceInfoControl_Load);
            this.grpLicence.ResumeLayout(false);
            this.grpLicence.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLicence;
        private System.Windows.Forms.TextBox txtLicenceInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComputerId;
        private System.Windows.Forms.Button btnCreateRequest;
        private System.Windows.Forms.Button btnEnterLicence;
    }
}
