namespace FolderAnalysis
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
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btnGo = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOpen = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.chkDelete = new System.Windows.Forms.CheckBox();
			this.lblMetric = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// fbd
			// 
			this.fbd.ShowNewFolderButton = false;
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(618, 50);
			this.textBox1.TabIndex = 0;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(12, 183);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(375, 23);
			this.progressBar1.TabIndex = 1;
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(474, 183);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 2;
			this.btnGo.Text = "&Go";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(555, 183);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(393, 183);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(75, 23);
			this.btnOpen.TabIndex = 4;
			this.btnOpen.Text = "&Open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(12, 102);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(618, 23);
			this.lblStatus.TabIndex = 5;
			this.lblStatus.Text = "lblStatus";
			// 
			// chkDelete
			// 
			this.chkDelete.AutoSize = true;
			this.chkDelete.Location = new System.Drawing.Point(15, 69);
			this.chkDelete.Name = "chkDelete";
			this.chkDelete.Size = new System.Drawing.Size(112, 17);
			this.chkDelete.TabIndex = 6;
			this.chkDelete.Text = "Clear existing data";
			this.chkDelete.UseVisualStyleBackColor = true;
			// 
			// lblMetric
			// 
			this.lblMetric.Location = new System.Drawing.Point(12, 138);
			this.lblMetric.Name = "lblMetric";
			this.lblMetric.Size = new System.Drawing.Size(618, 23);
			this.lblMetric.TabIndex = 7;
			this.lblMetric.Text = "lblMetric";
			// 
			// frmMain
			// 
			this.AcceptButton = this.btnGo;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(642, 212);
			this.Controls.Add(this.lblMetric);
			this.Controls.Add(this.chkDelete);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.textBox1);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Iterate Folder";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOpen;
				private System.Windows.Forms.Label lblStatus;
				private System.Windows.Forms.CheckBox chkDelete;
				private System.Windows.Forms.Label lblMetric;
    }
}

