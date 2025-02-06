using System.Drawing;

namespace PCMS_Lipa_General_Tool.Forms
{
    partial class FrmLogin
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblDeveloper = new Telerik.WinControls.UI.RadLabel();
			this.lblProgVersion = new Telerik.WinControls.UI.RadLabel();
			this.lblProgName = new Telerik.WinControls.UI.RadLabel();
			this.loginPanel = new Telerik.WinControls.UI.RadPanel();
			this.btnLogin = new Telerik.WinControls.UI.RadButton();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.txtPassword = new Telerik.WinControls.UI.RadButtonTextBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtUsername = new Telerik.WinControls.UI.RadTextBox();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.lblalert = new Telerik.WinControls.UI.RadLabel();
			this.frmLoginTimer = new System.Windows.Forms.Timer(this.components);
			this.fluentTheme1 = new Telerik.WinControls.Themes.FluentTheme();
			this.materialTheme1 = new Telerik.WinControls.Themes.MaterialTheme();
			this.materialPinkTheme1 = new Telerik.WinControls.Themes.MaterialPinkTheme();
			this.materialBlueGreyTheme1 = new Telerik.WinControls.Themes.MaterialBlueGreyTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblDeveloper)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblProgVersion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblProgName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.loginPanel)).BeginInit();
			this.loginPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnLogin)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPassword)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUsername)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblalert)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.pictureBox1);
			this.radPanel1.Controls.Add(this.lblDeveloper);
			this.radPanel1.Controls.Add(this.lblProgVersion);
			this.radPanel1.Controls.Add(this.lblProgName);
			this.radPanel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radPanel1.Location = new System.Drawing.Point(13, 13);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(439, 101);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// pictureBox1
			// 
			this.pictureBox1.ErrorImage = null;
			this.pictureBox1.Image = global::PCMS_Lipa_General_Tool.Properties.Resources.online_test;
			this.pictureBox1.Location = new System.Drawing.Point(13, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(87, 76);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// lblDeveloper
			// 
			this.lblDeveloper.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDeveloper.Location = new System.Drawing.Point(117, 62);
			this.lblDeveloper.Name = "lblDeveloper";
			this.lblDeveloper.Size = new System.Drawing.Size(66, 19);
			this.lblDeveloper.TabIndex = 2;
			this.lblDeveloper.Text = "radLabel5";
			this.lblDeveloper.ThemeName = "Crystal";
			// 
			// lblProgVersion
			// 
			this.lblProgVersion.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProgVersion.Location = new System.Drawing.Point(117, 37);
			this.lblProgVersion.Name = "lblProgVersion";
			this.lblProgVersion.Size = new System.Drawing.Size(66, 19);
			this.lblProgVersion.TabIndex = 1;
			this.lblProgVersion.Text = "radLabel4";
			this.lblProgVersion.ThemeName = "Crystal";
			// 
			// lblProgName
			// 
			this.lblProgName.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProgName.Location = new System.Drawing.Point(117, 12);
			this.lblProgName.Name = "lblProgName";
			this.lblProgName.Size = new System.Drawing.Size(66, 19);
			this.lblProgName.TabIndex = 0;
			this.lblProgName.Text = "radLabel3";
			this.lblProgName.ThemeName = "Crystal";
			// 
			// loginPanel
			// 
			this.loginPanel.Controls.Add(this.btnLogin);
			this.loginPanel.Controls.Add(this.radLabel2);
			this.loginPanel.Controls.Add(this.radLabel1);
			this.loginPanel.Controls.Add(this.txtPassword);
			this.loginPanel.Controls.Add(this.linkLabel1);
			this.loginPanel.Controls.Add(this.txtUsername);
			this.loginPanel.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.loginPanel.ForeColor = System.Drawing.Color.BlanchedAlmond;
			this.loginPanel.Location = new System.Drawing.Point(12, 120);
			this.loginPanel.Name = "loginPanel";
			this.loginPanel.Size = new System.Drawing.Size(440, 108);
			this.loginPanel.TabIndex = 1;
			this.loginPanel.ThemeName = "Crystal";
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLogin.Image = global::PCMS_Lipa_General_Tool.Properties.Resources.login_;
			this.btnLogin.Location = new System.Drawing.Point(307, 17);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(118, 52);
			this.btnLogin.TabIndex = 3;
			this.btnLogin.Text = "Login";
			this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnLogin.ThemeName = "Crystal";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(14, 50);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(69, 19);
			this.radLabel2.TabIndex = 8;
			this.radLabel2.Text = "Password:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(14, 21);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(71, 19);
			this.radLabel1.TabIndex = 7;
			this.radLabel1.Text = "Username:";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.Location = new System.Drawing.Point(91, 46);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(210, 23);
			this.txtPassword.TabIndex = 2;
			this.txtPassword.ThemeName = "Crystal";
			this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
			this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.linkLabel1.LinkColor = System.Drawing.Color.CornflowerBlue;
			this.linkLabel1.Location = new System.Drawing.Point(192, 72);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(109, 15);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Forgot Password?";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUsername.Location = new System.Drawing.Point(91, 17);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(210, 23);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.ThemeName = "Crystal";
			this.txtUsername.WordWrap = false;
			this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
			// 
			// lblalert
			// 
			this.lblalert.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblalert.ForeColor = System.Drawing.Color.Red;
			this.lblalert.Location = new System.Drawing.Point(13, 234);
			this.lblalert.Name = "lblalert";
			this.lblalert.Size = new System.Drawing.Size(66, 19);
			this.lblalert.TabIndex = 2;
			this.lblalert.Text = "radLabel3";
			this.lblalert.ThemeName = "Crystal";
			// 
			// FrmLogin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 258);
			this.Controls.Add(this.lblalert);
			this.Controls.Add(this.loginPanel);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmLogin";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmLogin_Load);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblDeveloper)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblProgVersion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblProgName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.loginPanel)).EndInit();
			this.loginPanel.ResumeLayout(false);
			this.loginPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnLogin)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPassword)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUsername)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblalert)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.UI.RadPanel loginPanel;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.UI.RadLabel lblProgVersion;
		private Telerik.WinControls.UI.RadLabel lblProgName;
		private Telerik.WinControls.UI.RadLabel lblDeveloper;
		private System.Windows.Forms.PictureBox pictureBox1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadLabel lblalert;
		public Telerik.WinControls.UI.RadTextBox txtUsername;
		public System.Windows.Forms.LinkLabel linkLabel1;
		public Telerik.WinControls.UI.RadButtonTextBox txtPassword;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadButton btnLogin;
		private System.Windows.Forms.Timer frmLoginTimer;
		private Telerik.WinControls.Themes.FluentTheme fluentTheme1;
		private Telerik.WinControls.Themes.MaterialTheme materialTheme1;
		private Telerik.WinControls.Themes.MaterialPinkTheme materialPinkTheme1;
		private Telerik.WinControls.Themes.MaterialBlueGreyTheme materialBlueGreyTheme1;
	}
}
