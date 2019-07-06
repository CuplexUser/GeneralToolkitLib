namespace GeneralToolkitLib.UserControls
{
    partial class CreateAndVerifyOTP
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
            this.pnlGenerateOTP = new System.Windows.Forms.Panel();
            this.grpShowQR = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShowVerificationPanel = new System.Windows.Forms.Button();
            this.txtOTPSecret = new System.Windows.Forms.TextBox();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlVerifyOTP = new System.Windows.Forms.Panel();
            this.grpVerifyOTP = new System.Windows.Forms.GroupBox();
            this.txtVerifyResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOTPCodeToVerify = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnVerifyCode = new System.Windows.Forms.Button();
            this.pnlGenerateOTP.SuspendLayout();
            this.grpShowQR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.pnlVerifyOTP.SuspendLayout();
            this.grpVerifyOTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlGenerateOTP
            // 
            this.pnlGenerateOTP.Controls.Add(this.grpShowQR);
            this.pnlGenerateOTP.Location = new System.Drawing.Point(3, 3);
            this.pnlGenerateOTP.Name = "pnlGenerateOTP";
            this.pnlGenerateOTP.Size = new System.Drawing.Size(300, 320);
            this.pnlGenerateOTP.TabIndex = 0;
            // 
            // grpShowQR
            // 
            this.grpShowQR.Controls.Add(this.btnCancel);
            this.grpShowQR.Controls.Add(this.btnShowVerificationPanel);
            this.grpShowQR.Controls.Add(this.txtOTPSecret);
            this.grpShowQR.Controls.Add(this.pictureBoxQRCode);
            this.grpShowQR.Controls.Add(this.label1);
            this.grpShowQR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpShowQR.Location = new System.Drawing.Point(0, 0);
            this.grpShowQR.MinimumSize = new System.Drawing.Size(225, 275);
            this.grpShowQR.Name = "grpShowQR";
            this.grpShowQR.Size = new System.Drawing.Size(300, 320);
            this.grpShowQR.TabIndex = 0;
            this.grpShowQR.TabStop = false;
            this.grpShowQR.Text = "Authentication Code";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(25, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnShowVerificationPanel
            // 
            this.btnShowVerificationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowVerificationPanel.Location = new System.Drawing.Point(200, 281);
            this.btnShowVerificationPanel.Name = "btnShowVerificationPanel";
            this.btnShowVerificationPanel.Size = new System.Drawing.Size(75, 25);
            this.btnShowVerificationPanel.TabIndex = 1;
            this.btnShowVerificationPanel.Text = "Next";
            this.btnShowVerificationPanel.UseVisualStyleBackColor = true;
            this.btnShowVerificationPanel.Click += new System.EventHandler(this.btnShowVerificationPanel_Click);
            // 
            // txtOTPSecret
            // 
            this.txtOTPSecret.Location = new System.Drawing.Point(67, 27);
            this.txtOTPSecret.Name = "txtOTPSecret";
            this.txtOTPSecret.ReadOnly = true;
            this.txtOTPSecret.Size = new System.Drawing.Size(208, 20);
            this.txtOTPSecret.TabIndex = 2;
            this.txtOTPSecret.Text = "ABCDEFGHIJKLMNOP";
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxQRCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxQRCode.Location = new System.Drawing.Point(25, 55);
            this.pictureBoxQRCode.MinimumSize = new System.Drawing.Size(175, 175);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(250, 220);
            this.pictureBoxQRCode.TabIndex = 0;
            this.pictureBoxQRCode.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Secret:";
            // 
            // pnlVerifyOTP
            // 
            this.pnlVerifyOTP.Controls.Add(this.grpVerifyOTP);
            this.pnlVerifyOTP.Location = new System.Drawing.Point(309, 3);
            this.pnlVerifyOTP.Name = "pnlVerifyOTP";
            this.pnlVerifyOTP.Size = new System.Drawing.Size(300, 320);
            this.pnlVerifyOTP.TabIndex = 1;
            // 
            // grpVerifyOTP
            // 
            this.grpVerifyOTP.Controls.Add(this.txtVerifyResult);
            this.grpVerifyOTP.Controls.Add(this.label3);
            this.grpVerifyOTP.Controls.Add(this.txtOTPCodeToVerify);
            this.grpVerifyOTP.Controls.Add(this.label2);
            this.grpVerifyOTP.Controls.Add(this.btnPrevious);
            this.grpVerifyOTP.Controls.Add(this.btnVerifyCode);
            this.grpVerifyOTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpVerifyOTP.Location = new System.Drawing.Point(0, 0);
            this.grpVerifyOTP.MinimumSize = new System.Drawing.Size(225, 275);
            this.grpVerifyOTP.Name = "grpVerifyOTP";
            this.grpVerifyOTP.Size = new System.Drawing.Size(300, 320);
            this.grpVerifyOTP.TabIndex = 0;
            this.grpVerifyOTP.TabStop = false;
            this.grpVerifyOTP.Text = "Verify Authentication Code";
            // 
            // txtVerifyResult
            // 
            this.txtVerifyResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVerifyResult.Location = new System.Drawing.Point(66, 55);
            this.txtVerifyResult.MaxLength = 8;
            this.txtVerifyResult.Multiline = true;
            this.txtVerifyResult.Name = "txtVerifyResult";
            this.txtVerifyResult.ReadOnly = true;
            this.txtVerifyResult.Size = new System.Drawing.Size(209, 219);
            this.txtVerifyResult.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Result:";
            // 
            // txtOTPCodeToVerify
            // 
            this.txtOTPCodeToVerify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOTPCodeToVerify.Location = new System.Drawing.Point(66, 27);
            this.txtOTPCodeToVerify.MaxLength = 8;
            this.txtOTPCodeToVerify.Name = "txtOTPCodeToVerify";
            this.txtOTPCodeToVerify.Size = new System.Drawing.Size(209, 20);
            this.txtOTPCodeToVerify.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Code:";
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Location = new System.Drawing.Point(25, 280);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 25);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnVerifyCode
            // 
            this.btnVerifyCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerifyCode.Location = new System.Drawing.Point(200, 280);
            this.btnVerifyCode.Name = "btnVerifyCode";
            this.btnVerifyCode.Size = new System.Drawing.Size(75, 25);
            this.btnVerifyCode.TabIndex = 2;
            this.btnVerifyCode.Text = "Verify";
            this.btnVerifyCode.UseVisualStyleBackColor = true;
            this.btnVerifyCode.Click += new System.EventHandler(this.btnVerifyCode_Click);
            // 
            // CreateAndVerifyOTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlVerifyOTP);
            this.Controls.Add(this.pnlGenerateOTP);
            this.Name = "CreateAndVerifyOTP";
            this.Size = new System.Drawing.Size(630, 350);
            this.Load += new System.EventHandler(this.CreateAndVerifyOTP_Load);
            this.pnlGenerateOTP.ResumeLayout(false);
            this.grpShowQR.ResumeLayout(false);
            this.grpShowQR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.pnlVerifyOTP.ResumeLayout(false);
            this.grpVerifyOTP.ResumeLayout(false);
            this.grpVerifyOTP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlGenerateOTP;
        private System.Windows.Forms.GroupBox grpShowQR;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Panel pnlVerifyOTP;
        private System.Windows.Forms.Button btnShowVerificationPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOTPSecret;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpVerifyOTP;
        private System.Windows.Forms.TextBox txtOTPCodeToVerify;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnVerifyCode;
        private System.Windows.Forms.TextBox txtVerifyResult;
        private System.Windows.Forms.Label label3;
    }
}
