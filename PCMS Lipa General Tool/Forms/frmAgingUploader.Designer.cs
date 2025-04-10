namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmAgingUploader
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgingUploader));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.btnBrowse = new Telerik.WinControls.UI.RadButton();
			this.txtFilePath = new Telerik.WinControls.UI.RadTextBox();
			this.btnLoadPreview = new Telerik.WinControls.UI.RadButton();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnBrowse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnLoadPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(13, 27);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(85, 19);
			this.radLabel1.TabIndex = 0;
			this.radLabel1.Text = "Select Aging:";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnBrowse.Location = new System.Drawing.Point(315, 22);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(134, 28);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.ThemeName = "Crystal";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(104, 22);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(205, 24);
			this.txtFilePath.TabIndex = 7;
			this.txtFilePath.ThemeName = "Crystal";
			// 
			// btnLoadPreview
			// 
			this.btnLoadPreview.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLoadPreview.Location = new System.Drawing.Point(209, 56);
			this.btnLoadPreview.Name = "btnLoadPreview";
			this.btnLoadPreview.Size = new System.Drawing.Size(240, 28);
			this.btnLoadPreview.TabIndex = 9;
			this.btnLoadPreview.Text = "Load Preview and Import to System";
			this.btnLoadPreview.ThemeName = "Crystal";
			this.btnLoadPreview.Click += new System.EventHandler(this.btnLoadPreview_Click);
			// 
			// frmAgingUploader
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(461, 99);
			this.Controls.Add(this.btnLoadPreview);
			this.Controls.Add(this.txtFilePath);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.radLabel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAgingUploader";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmDBUtility";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnBrowse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnLoadPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadButton btnBrowse;
		private Telerik.WinControls.UI.RadTextBox txtFilePath;
		private Telerik.WinControls.UI.RadButton btnLoadPreview;
	}
}
