namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmUpdateDCPassword
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateDCPassword));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.txtUsername = new Telerik.WinControls.UI.RadTextBox();
			this.radPictureBox1 = new Telerik.WinControls.UI.RadPictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtNewPassword = new Telerik.WinControls.UI.RadButtonTextBox();
			this.btnOK = new Telerik.WinControls.UI.RadButton();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUsername)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radPictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.txtUsername);
			this.radPanel1.Controls.Add(this.radPictureBox1);
			this.radPanel1.Controls.Add(this.label1);
			this.radPanel1.Controls.Add(this.txtNewPassword);
			this.radPanel1.Controls.Add(this.btnOK);
			this.radPanel1.Controls.Add(this.label3);
			this.radPanel1.Location = new System.Drawing.Point(12, 12);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(530, 155);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUsername.Location = new System.Drawing.Point(135, 69);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(227, 24);
			this.txtUsername.TabIndex = 18;
			this.txtUsername.ThemeName = "Crystal";
			// 
			// radPictureBox1
			// 
			this.radPictureBox1.ImageLayout = Telerik.WinControls.UI.RadImageLayout.Stretch;
			this.radPictureBox1.Location = new System.Drawing.Point(155, 12);
			this.radPictureBox1.Name = "radPictureBox1";
			this.radPictureBox1.Size = new System.Drawing.Size(175, 43);
			this.radPictureBox1.TabIndex = 17;
			this.radPictureBox1.ThemeName = "Crystal";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 71);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 20);
			this.label1.TabIndex = 15;
			this.label1.Text = "Username:";
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNewPassword.Location = new System.Drawing.Point(135, 107);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(227, 24);
			this.txtNewPassword.TabIndex = 14;
			this.txtNewPassword.ThemeName = "Crystal";
			this.txtNewPassword.TextChanged += new System.EventHandler(this.txtNewPassword_TextChanged);
			// 
			// btnOK
			// 
			this.btnOK.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(381, 69);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(133, 64);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "&Update Password";
			this.btnOK.ThemeName = "Crystal";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(24, 109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "New Password:";
			// 
			// frmUpdateDCPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(553, 179);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmUpdateDCPassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Update Password";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmResetPassword_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUsername)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radPictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.UI.RadPanel radPanel1;
		private System.Windows.Forms.Label label3;
		public Telerik.WinControls.UI.RadButton btnOK;
		private System.Windows.Forms.Label label1;
		private Telerik.WinControls.UI.RadPictureBox radPictureBox1;
		public Telerik.WinControls.UI.RadTextBox txtUsername;
		public Telerik.WinControls.UI.RadButtonTextBox txtNewPassword;
	}
}
