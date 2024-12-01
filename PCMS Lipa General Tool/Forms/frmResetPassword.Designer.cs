namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmResetPassword
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResetPassword));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.txtNewPassword = new Telerik.WinControls.UI.RadButtonTextBox();
			this.lblIDValidation = new Telerik.WinControls.UI.RadLabel();
			this.lblPasswordValidation = new Telerik.WinControls.UI.RadLabel();
			this.lblValidation = new Telerik.WinControls.UI.RadLabel();
			this.btnCancel = new Telerik.WinControls.UI.RadButton();
			this.btnOK = new Telerik.WinControls.UI.RadButton();
			this.txtWorkEmail = new Telerik.WinControls.UI.RadTextBox();
			this.txtEmpID = new Telerik.WinControls.UI.RadTextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblTemp = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblIDValidation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblPasswordValidation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblValidation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWorkEmail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEmpID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.txtNewPassword);
			this.radPanel1.Controls.Add(this.lblIDValidation);
			this.radPanel1.Controls.Add(this.lblPasswordValidation);
			this.radPanel1.Controls.Add(this.lblValidation);
			this.radPanel1.Controls.Add(this.btnCancel);
			this.radPanel1.Controls.Add(this.btnOK);
			this.radPanel1.Controls.Add(this.txtWorkEmail);
			this.radPanel1.Controls.Add(this.txtEmpID);
			this.radPanel1.Controls.Add(this.label3);
			this.radPanel1.Controls.Add(this.label2);
			this.radPanel1.Controls.Add(this.lblTemp);
			this.radPanel1.Location = new System.Drawing.Point(12, 12);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(447, 229);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNewPassword.Location = new System.Drawing.Point(128, 123);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(293, 24);
			this.txtNewPassword.TabIndex = 14;
			this.txtNewPassword.ThemeName = "Crystal";
			this.txtNewPassword.TextChanged += new System.EventHandler(this.txtNewPassword_TextChanged);
			// 
			// lblIDValidation
			// 
			this.lblIDValidation.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIDValidation.Location = new System.Drawing.Point(127, 52);
			this.lblIDValidation.Name = "lblIDValidation";
			this.lblIDValidation.Size = new System.Drawing.Size(2, 2);
			this.lblIDValidation.TabIndex = 13;
			this.lblIDValidation.ThemeName = "Crystal";
			// 
			// lblPasswordValidation
			// 
			this.lblPasswordValidation.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPasswordValidation.Location = new System.Drawing.Point(128, 148);
			this.lblPasswordValidation.Name = "lblPasswordValidation";
			this.lblPasswordValidation.Size = new System.Drawing.Size(2, 2);
			this.lblPasswordValidation.TabIndex = 12;
			this.lblPasswordValidation.ThemeName = "Crystal";
			// 
			// lblValidation
			// 
			this.lblValidation.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblValidation.Location = new System.Drawing.Point(127, 98);
			this.lblValidation.Name = "lblValidation";
			this.lblValidation.Size = new System.Drawing.Size(2, 2);
			this.lblValidation.TabIndex = 11;
			this.lblValidation.ThemeName = "Crystal";
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(308, 172);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(110, 40);
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.ThemeName = "Crystal";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(128, 172);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(166, 40);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "&Ok";
			this.btnOK.ThemeName = "Crystal";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtWorkEmail
			// 
			this.txtWorkEmail.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtWorkEmail.Location = new System.Drawing.Point(128, 73);
			this.txtWorkEmail.Name = "txtWorkEmail";
			this.txtWorkEmail.Size = new System.Drawing.Size(293, 24);
			this.txtWorkEmail.TabIndex = 7;
			this.txtWorkEmail.ThemeName = "Crystal";
			this.txtWorkEmail.TextChanged += new System.EventHandler(this.txtWorkEmail_TextChanged);
			// 
			// txtEmpID
			// 
			this.txtEmpID.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmpID.Location = new System.Drawing.Point(128, 22);
			this.txtEmpID.Name = "txtEmpID";
			this.txtEmpID.Size = new System.Drawing.Size(76, 24);
			this.txtEmpID.TabIndex = 6;
			this.txtEmpID.ThemeName = "Crystal";
			this.txtEmpID.TextChanged += new System.EventHandler(this.txtEmpID_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(20, 127);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "New Password:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(20, 75);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 20);
			this.label2.TabIndex = 4;
			this.label2.Text = "Email: ";
			// 
			// lblTemp
			// 
			this.lblTemp.AutoSize = true;
			this.lblTemp.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTemp.Location = new System.Drawing.Point(20, 24);
			this.lblTemp.Name = "lblTemp";
			this.lblTemp.Size = new System.Drawing.Size(77, 20);
			this.lblTemp.TabIndex = 3;
			this.lblTemp.Text = "Username: ";
			// 
			// frmResetPassword
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 253);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmResetPassword";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reset Password";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmResetPassword_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblIDValidation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblPasswordValidation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblValidation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtWorkEmail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEmpID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.UI.RadPanel radPanel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		public Telerik.WinControls.UI.RadButton btnOK;
		public Telerik.WinControls.UI.RadTextBox txtWorkEmail;
		public Telerik.WinControls.UI.RadTextBox txtEmpID;
		public Telerik.WinControls.UI.RadButton btnCancel;
		public System.Windows.Forms.Label lblTemp;
		private Telerik.WinControls.UI.RadLabel lblValidation;
		private Telerik.WinControls.UI.RadLabel lblIDValidation;
		private Telerik.WinControls.UI.RadLabel lblPasswordValidation;
		private Telerik.WinControls.UI.RadButtonTextBox txtNewPassword;
	}
}
