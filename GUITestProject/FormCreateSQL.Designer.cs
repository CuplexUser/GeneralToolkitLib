namespace GUITestProject
{
    partial class FormCreateSQL
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
            this.txtInputRows = new System.Windows.Forms.RichTextBox();
            this.txtSQLRows = new System.Windows.Forms.RichTextBox();
            this.btnGenerateSQL = new System.Windows.Forms.Button();
            this.splitContainerForTextboxes = new System.Windows.Forms.SplitContainer();
            this.groupBoxForTextboxes = new System.Windows.Forms.GroupBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForTextboxes)).BeginInit();
            this.splitContainerForTextboxes.Panel1.SuspendLayout();
            this.splitContainerForTextboxes.Panel2.SuspendLayout();
            this.splitContainerForTextboxes.SuspendLayout();
            this.groupBoxForTextboxes.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInputRows
            // 
            this.txtInputRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputRows.Location = new System.Drawing.Point(0, 0);
            this.txtInputRows.Name = "txtInputRows";
            this.txtInputRows.Size = new System.Drawing.Size(600, 173);
            this.txtInputRows.TabIndex = 0;
            this.txtInputRows.Text = "";
            // 
            // txtSQLRows
            // 
            this.txtSQLRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLRows.Location = new System.Drawing.Point(0, 0);
            this.txtSQLRows.Name = "txtSQLRows";
            this.txtSQLRows.Size = new System.Drawing.Size(600, 165);
            this.txtSQLRows.TabIndex = 1;
            this.txtSQLRows.Text = "";
            // 
            // btnGenerateSQL
            // 
            this.btnGenerateSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateSQL.Location = new System.Drawing.Point(462, 425);
            this.btnGenerateSQL.Name = "btnGenerateSQL";
            this.btnGenerateSQL.Size = new System.Drawing.Size(160, 25);
            this.btnGenerateSQL.TabIndex = 2;
            this.btnGenerateSQL.Text = "Generate Insert Queries";
            this.btnGenerateSQL.UseVisualStyleBackColor = true;
            // 
            // splitContainerForTextboxes
            // 
            this.splitContainerForTextboxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerForTextboxes.Location = new System.Drawing.Point(5, 18);
            this.splitContainerForTextboxes.Name = "splitContainerForTextboxes";
            this.splitContainerForTextboxes.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerForTextboxes.Panel1
            // 
            this.splitContainerForTextboxes.Panel1.Controls.Add(this.txtInputRows);
            // 
            // splitContainerForTextboxes.Panel2
            // 
            this.splitContainerForTextboxes.Panel2.Controls.Add(this.txtSQLRows);
            this.splitContainerForTextboxes.Size = new System.Drawing.Size(600, 342);
            this.splitContainerForTextboxes.SplitterDistance = 173;
            this.splitContainerForTextboxes.TabIndex = 3;
            // 
            // groupBoxForTextboxes
            // 
            this.groupBoxForTextboxes.Controls.Add(this.splitContainerForTextboxes);
            this.groupBoxForTextboxes.Location = new System.Drawing.Point(12, 12);
            this.groupBoxForTextboxes.Name = "groupBoxForTextboxes";
            this.groupBoxForTextboxes.Padding = new System.Windows.Forms.Padding(5);
            this.groupBoxForTextboxes.Size = new System.Drawing.Size(610, 365);
            this.groupBoxForTextboxes.TabIndex = 4;
            this.groupBoxForTextboxes.TabStop = false;
            this.groupBoxForTextboxes.Text = "Input and output fields";
            // 
            // lblTemplate
            // 
            this.lblTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTemplate.AutoSize = true;
            this.lblTemplate.Location = new System.Drawing.Point(14, 383);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(54, 13);
            this.lblTemplate.TabIndex = 5;
            this.lblTemplate.Text = "Template:";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTemplate.Location = new System.Drawing.Point(17, 399);
            this.txtTemplate.MaxLength = 250;
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(605, 20);
            this.txtTemplate.TabIndex = 6;
            this.txtTemplate.Text = "INSERT INTO WordDictionary(Word, LanguageId, [Lowercase],[WordLength]) VALUES({0}" +
    ",1,1,0)";
            // 
            // FormCreateSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 462);
            this.Controls.Add(this.txtTemplate);
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.groupBoxForTextboxes);
            this.Controls.Add(this.btnGenerateSQL);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormCreateSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create SQL Insert Queries";
            this.splitContainerForTextboxes.Panel1.ResumeLayout(false);
            this.splitContainerForTextboxes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForTextboxes)).EndInit();
            this.splitContainerForTextboxes.ResumeLayout(false);
            this.groupBoxForTextboxes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtInputRows;
        private System.Windows.Forms.RichTextBox txtSQLRows;
        private System.Windows.Forms.Button btnGenerateSQL;
        private System.Windows.Forms.SplitContainer splitContainerForTextboxes;
        private System.Windows.Forms.GroupBox groupBoxForTextboxes;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.TextBox txtTemplate;
    }
}