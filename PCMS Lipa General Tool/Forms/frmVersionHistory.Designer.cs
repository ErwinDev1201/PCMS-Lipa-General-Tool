namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmVersion_History
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVersion_History));
			this.txtVersionHistory = new Telerik.WinControls.UI.RadTextBoxControl();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			((System.ComponentModel.ISupportInitialize)(this.txtVersionHistory)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// txtVersionHistory
			// 
			this.txtVersionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtVersionHistory.Location = new System.Drawing.Point(12, 36);
			this.txtVersionHistory.Multiline = true;
			this.txtVersionHistory.Name = "txtVersionHistory";
			this.txtVersionHistory.Size = new System.Drawing.Size(761, 506);
			this.txtVersionHistory.TabIndex = 0;
			this.txtVersionHistory.ThemeName = "Crystal";
			// 
			// frmVersion_History
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(785, 563);
			this.Controls.Add(this.txtVersionHistory);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmVersion_History";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmVersion_History";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.txtVersionHistory)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadTextBoxControl txtVersionHistory;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
	}
}
