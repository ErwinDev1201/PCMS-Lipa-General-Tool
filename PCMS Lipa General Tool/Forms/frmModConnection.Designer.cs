namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmModConnection
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModConnection));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.fluentTheme1 = new Telerik.WinControls.Themes.FluentTheme();
			this.materialPinkTheme1 = new Telerik.WinControls.Themes.MaterialPinkTheme();
			this.materialBlueGreyTheme1 = new Telerik.WinControls.Themes.MaterialBlueGreyTheme();
			this.txtConfiguration = new Telerik.WinControls.UI.RadTextBoxControl();
			this.btnSave = new Telerik.WinControls.UI.RadButton();
			this.btnCancel = new Telerik.WinControls.UI.RadButton();
			((System.ComponentModel.ISupportInitialize)(this.txtConfiguration)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// txtConfiguration
			// 
			this.txtConfiguration.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtConfiguration.Location = new System.Drawing.Point(12, 24);
			this.txtConfiguration.Multiline = true;
			this.txtConfiguration.Name = "txtConfiguration";
			this.txtConfiguration.Size = new System.Drawing.Size(501, 287);
			this.txtConfiguration.TabIndex = 0;
			this.txtConfiguration.ThemeName = "Crystal";
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(288, 317);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(110, 38);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "&Save";
			this.btnSave.ThemeName = "Crystal";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(404, 317);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(110, 38);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.ThemeName = "Crystal";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancel_KeyDown);
			// 
			// frmModConnection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 367);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtConfiguration);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmModConnection";
			this.Text = "frmModConnection";
			this.ThemeName = "Crystal";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.txtConfiguration)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.Themes.FluentTheme fluentTheme1;
		private Telerik.WinControls.Themes.MaterialPinkTheme materialPinkTheme1;
		private Telerik.WinControls.Themes.MaterialBlueGreyTheme materialBlueGreyTheme1;
		private Telerik.WinControls.UI.RadTextBoxControl txtConfiguration;
		private Telerik.WinControls.UI.RadButton btnSave;
		private Telerik.WinControls.UI.RadButton btnCancel;
	}
}
