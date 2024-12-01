namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmDeveloperAccess
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeveloperAccess));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.btnUpdateDevPassword = new Telerik.WinControls.UI.RadButton();
			this.txtNewPassword = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.txtcurrPassword = new Telerik.WinControls.UI.RadTextBox();
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateDevPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtcurrPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// btnUpdateDevPassword
			// 
			this.btnUpdateDevPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpdateDevPassword.Location = new System.Drawing.Point(15, 78);
			this.btnUpdateDevPassword.Name = "btnUpdateDevPassword";
			this.btnUpdateDevPassword.Size = new System.Drawing.Size(315, 34);
			this.btnUpdateDevPassword.TabIndex = 2;
			this.btnUpdateDevPassword.Text = "Update Password";
			this.btnUpdateDevPassword.ThemeName = "Crystal";
			this.btnUpdateDevPassword.Click += new System.EventHandler(this.btnUpdateDevPassword_Click);
			// 
			// txtNewPassword
			// 
			this.txtNewPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNewPassword.Location = new System.Drawing.Point(137, 48);
			this.txtNewPassword.Name = "txtNewPassword";
			this.txtNewPassword.Size = new System.Drawing.Size(193, 24);
			this.txtNewPassword.TabIndex = 3;
			this.txtNewPassword.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(15, 52);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(119, 20);
			this.radLabel2.TabIndex = 4;
			this.radLabel2.Text = "Developer Access";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(15, 22);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(120, 20);
			this.radLabel1.TabIndex = 6;
			this.radLabel1.Text = "Current Password";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// txtcurrPassword
			// 
			this.txtcurrPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtcurrPassword.Location = new System.Drawing.Point(137, 18);
			this.txtcurrPassword.Name = "txtcurrPassword";
			this.txtcurrPassword.Size = new System.Drawing.Size(193, 24);
			this.txtcurrPassword.TabIndex = 5;
			this.txtcurrPassword.ThemeName = "Crystal";
			// 
			// frmDeveloperAccess
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(342, 130);
			this.Controls.Add(this.radLabel1);
			this.Controls.Add(this.txtcurrPassword);
			this.Controls.Add(this.radLabel2);
			this.Controls.Add(this.txtNewPassword);
			this.Controls.Add(this.btnUpdateDevPassword);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDeveloperAccess";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmDBUtility";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateDevPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNewPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtcurrPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadButton btnUpdateDevPassword;
		private Telerik.WinControls.UI.RadTextBox txtNewPassword;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadTextBox txtcurrPassword;
	}
}
