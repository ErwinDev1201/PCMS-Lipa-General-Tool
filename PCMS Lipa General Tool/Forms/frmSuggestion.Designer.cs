namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmSuggestion
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSuggestion));
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.btnSendSuggestion = new Telerik.WinControls.UI.RadButton();
			this.txtSuggestion = new Telerik.WinControls.UI.RadTextBoxControl();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnSendSuggestion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSuggestion)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.btnSendSuggestion);
			this.radPanel1.Controls.Add(this.txtSuggestion);
			this.radPanel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radPanel1.Location = new System.Drawing.Point(13, 13);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(440, 201);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// btnSendSuggestion
			// 
			this.btnSendSuggestion.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSendSuggestion.Location = new System.Drawing.Point(15, 137);
			this.btnSendSuggestion.Name = "btnSendSuggestion";
			this.btnSendSuggestion.Size = new System.Drawing.Size(409, 46);
			this.btnSendSuggestion.TabIndex = 1;
			this.btnSendSuggestion.Text = "Send Suggestion";
			this.btnSendSuggestion.ThemeName = "Crystal";
			this.btnSendSuggestion.Click += new System.EventHandler(this.btnSendSuggestion_Click);
			// 
			// txtSuggestion
			// 
			this.txtSuggestion.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSuggestion.Location = new System.Drawing.Point(15, 16);
			this.txtSuggestion.Name = "txtSuggestion";
			this.txtSuggestion.Size = new System.Drawing.Size(409, 115);
			this.txtSuggestion.TabIndex = 0;
			this.txtSuggestion.ThemeName = "Crystal";
			// 
			// frmSuggestion
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(465, 226);
			this.Controls.Add(this.radPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSuggestion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmSuggestion";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSuggestion_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.btnSendSuggestion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSuggestion)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.UI.RadTextBoxControl txtSuggestion;
		private Telerik.WinControls.UI.RadButton btnSendSuggestion;
	}
}
