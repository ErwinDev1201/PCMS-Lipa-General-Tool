namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmAIAssistant
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
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.txtUserInput = new Telerik.WinControls.UI.RadTextBoxControl();
			this.txtResponse = new Telerik.WinControls.UI.RadTextBoxControl();
			this.btnAskAI = new Telerik.WinControls.UI.RadButton();
			((System.ComponentModel.ISupportInitialize)(this.txtUserInput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtResponse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnAskAI)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// txtUserInput
			// 
			this.txtUserInput.Location = new System.Drawing.Point(12, 55);
			this.txtUserInput.Name = "txtUserInput";
			this.txtUserInput.Size = new System.Drawing.Size(604, 100);
			this.txtUserInput.TabIndex = 0;
			this.txtUserInput.ThemeName = "Crystal";
			// 
			// txtResponse
			// 
			this.txtResponse.Location = new System.Drawing.Point(12, 192);
			this.txtResponse.Name = "txtResponse";
			this.txtResponse.Size = new System.Drawing.Size(757, 290);
			this.txtResponse.TabIndex = 1;
			this.txtResponse.ThemeName = "Crystal";
			// 
			// btnAskAI
			// 
			this.btnAskAI.Location = new System.Drawing.Point(636, 55);
			this.btnAskAI.Name = "btnAskAI";
			this.btnAskAI.Size = new System.Drawing.Size(127, 100);
			this.btnAskAI.TabIndex = 2;
			this.btnAskAI.Text = "Ask AI";
			this.btnAskAI.ThemeName = "Crystal";
			this.btnAskAI.Click += new System.EventHandler(this.btnAskAI_Click);
			// 
			// frmAIAssistant
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(781, 527);
			this.Controls.Add(this.btnAskAI);
			this.Controls.Add(this.txtResponse);
			this.Controls.Add(this.txtUserInput);
			this.Name = "frmAIAssistant";
			this.Text = "frmAIAssistant";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.txtUserInput)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtResponse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnAskAI)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadTextBoxControl txtUserInput;
		private Telerik.WinControls.UI.RadTextBoxControl txtResponse;
		private Telerik.WinControls.UI.RadButton btnAskAI;
	}
}
