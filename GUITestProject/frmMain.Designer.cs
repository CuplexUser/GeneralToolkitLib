namespace GUITestProject
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCreateSQLFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageTestOTP = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rb32 = new System.Windows.Forms.RadioButton();
            this.rb24 = new System.Windows.Forms.RadioButton();
            this.rb16 = new System.Windows.Forms.RadioButton();
            this.btnGenerateSharedSecret = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOTPLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.createAndVerifyOTP1 = new GeneralToolkitLib.UserControls.CreateAndVerifyOTP();
            this.txtImportSecret = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnVerifyEncodingAndImport = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabPageTestOTP.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(688, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCreateSQLFormToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openCreateSQLFormToolStripMenuItem
            // 
            this.openCreateSQLFormToolStripMenuItem.Name = "openCreateSQLFormToolStripMenuItem";
            this.openCreateSQLFormToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openCreateSQLFormToolStripMenuItem.Text = "Open Create SQL Form";
            this.openCreateSQLFormToolStripMenuItem.Click += new System.EventHandler(this.openCreateSQLFormToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabPageTestOTP
            // 
            this.tabPageTestOTP.Controls.Add(this.panel2);
            this.tabPageTestOTP.Controls.Add(this.panel1);
            this.tabPageTestOTP.Location = new System.Drawing.Point(4, 22);
            this.tabPageTestOTP.Name = "tabPageTestOTP";
            this.tabPageTestOTP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTestOTP.Size = new System.Drawing.Size(680, 370);
            this.tabPageTestOTP.TabIndex = 0;
            this.tabPageTestOTP.Text = "Generate and verify OTP";
            this.tabPageTestOTP.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnVerifyEncodingAndImport);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtImportSecret);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.btnGenerateSharedSecret);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtOTPLabel);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(364, 6);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(300, 350);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.rb32);
            this.panel3.Controls.Add(this.rb24);
            this.panel3.Controls.Add(this.rb16);
            this.panel3.Location = new System.Drawing.Point(107, 38);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(185, 81);
            this.panel3.TabIndex = 4;
            // 
            // rb32
            // 
            this.rb32.AutoSize = true;
            this.rb32.Location = new System.Drawing.Point(3, 49);
            this.rb32.Name = "rb32";
            this.rb32.Size = new System.Drawing.Size(37, 17);
            this.rb32.TabIndex = 5;
            this.rb32.Text = "32";
            this.rb32.UseVisualStyleBackColor = true;
            // 
            // rb24
            // 
            this.rb24.AutoSize = true;
            this.rb24.Location = new System.Drawing.Point(3, 26);
            this.rb24.Name = "rb24";
            this.rb24.Size = new System.Drawing.Size(37, 17);
            this.rb24.TabIndex = 4;
            this.rb24.Text = "24";
            this.rb24.UseVisualStyleBackColor = true;
            // 
            // rb16
            // 
            this.rb16.AutoSize = true;
            this.rb16.Checked = true;
            this.rb16.Location = new System.Drawing.Point(3, 3);
            this.rb16.Name = "rb16";
            this.rb16.Size = new System.Drawing.Size(37, 17);
            this.rb16.TabIndex = 3;
            this.rb16.TabStop = true;
            this.rb16.Text = "16";
            this.rb16.UseVisualStyleBackColor = true;
            // 
            // btnGenerateSharedSecret
            // 
            this.btnGenerateSharedSecret.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateSharedSecret.Location = new System.Drawing.Point(157, 315);
            this.btnGenerateSharedSecret.Name = "btnGenerateSharedSecret";
            this.btnGenerateSharedSecret.Size = new System.Drawing.Size(135, 27);
            this.btnGenerateSharedSecret.TabIndex = 1;
            this.btnGenerateSharedSecret.Text = "Generate random secret";
            this.btnGenerateSharedSecret.UseVisualStyleBackColor = true;
            this.btnGenerateSharedSecret.Click += new System.EventHandler(this.btnGenerateSharedSecret_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Secret key length:";
            // 
            // txtOTPLabel
            // 
            this.txtOTPLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOTPLabel.Location = new System.Drawing.Point(75, 12);
            this.txtOTPLabel.MaxLength = 50;
            this.txtOTPLabel.Name = "txtOTPLabel";
            this.txtOTPLabel.Size = new System.Drawing.Size(217, 20);
            this.txtOTPLabel.TabIndex = 1;
            this.txtOTPLabel.Text = "Secure Login System";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "OTP Label:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.createAndVerifyOTP1);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 350);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageTestOTP);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(688, 396);
            this.tabControl1.TabIndex = 0;
            // 
            // createAndVerifyOTP1
            // 
            this.createAndVerifyOTP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createAndVerifyOTP1.Location = new System.Drawing.Point(0, 0);
            this.createAndVerifyOTP1.Name = "createAndVerifyOTP1";
            this.createAndVerifyOTP1.Size = new System.Drawing.Size(350, 350);
            this.createAndVerifyOTP1.TabIndex = 0;
            // 
            // txtImportSecret
            // 
            this.txtImportSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImportSecret.Location = new System.Drawing.Point(8, 160);
            this.txtImportSecret.MaxLength = 55;
            this.txtImportSecret.Name = "txtImportSecret";
            this.txtImportSecret.Size = new System.Drawing.Size(284, 20);
            this.txtImportSecret.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 141);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Use previously generated key:";
            // 
            // btnVerifyEncodingAndImport
            // 
            this.btnVerifyEncodingAndImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerifyEncodingAndImport.Location = new System.Drawing.Point(182, 186);
            this.btnVerifyEncodingAndImport.Name = "btnVerifyEncodingAndImport";
            this.btnVerifyEncodingAndImport.Size = new System.Drawing.Size(110, 27);
            this.btnVerifyEncodingAndImport.TabIndex = 7;
            this.btnVerifyEncodingAndImport.Text = "Import Secret Key";
            this.btnVerifyEncodingAndImport.UseVisualStyleBackColor = true;
            this.btnVerifyEncodingAndImport.Click += new System.EventHandler(this.btnVerifyEncodingAndImport_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 420);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(350, 400);
            this.Name = "frmMain";
            this.Text = "Test Application for toolkit forms";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPageTestOTP.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCreateSQLFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageTestOTP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rb32;
        private System.Windows.Forms.RadioButton rb24;
        private System.Windows.Forms.RadioButton rb16;
        private System.Windows.Forms.Button btnGenerateSharedSecret;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOTPLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private GeneralToolkitLib.UserControls.CreateAndVerifyOTP createAndVerifyOTP1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnVerifyEncodingAndImport;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtImportSecret;
    }
}

