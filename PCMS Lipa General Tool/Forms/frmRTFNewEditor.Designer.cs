namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmRTFNewEditor
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
			this.radRichTextEditor1 = new Telerik.WinControls.UI.RadRichTextEditor();
			this.WinRTFEditor = new Telerik.WinControls.UI.RichTextEditorRibbonBar();
			this.radRibbonFormBehavior1 = new Telerik.WinControls.UI.RadRibbonFormBehavior();
			((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WinRTFEditor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radRichTextEditor1
			// 
			this.radRichTextEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radRichTextEditor1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
			this.radRichTextEditor1.Location = new System.Drawing.Point(13, 175);
			this.radRichTextEditor1.Name = "radRichTextEditor1";
			this.radRichTextEditor1.Padding = new System.Windows.Forms.Padding(20);
			this.radRichTextEditor1.SelectionFill = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(179)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
			this.radRichTextEditor1.SelectionStroke = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(236)))), ((int)(((byte)(248)))));
			this.radRichTextEditor1.Size = new System.Drawing.Size(946, 657);
			this.radRichTextEditor1.TabIndex = 0;
			// 
			// WinRTFEditor
			// 
			this.WinRTFEditor.ApplicationMenuStyle = Telerik.WinControls.UI.ApplicationMenuStyle.BackstageView;
			this.WinRTFEditor.AssociatedRichTextEditor = null;
			this.WinRTFEditor.BuiltInStylesVersion = Telerik.WinForms.Documents.Model.Styles.BuiltInStylesVersion.Office2013;
			this.WinRTFEditor.EnableKeyMap = false;
			this.WinRTFEditor.Location = new System.Drawing.Point(0, 0);
			this.WinRTFEditor.Name = "WinRTFEditor";
			this.WinRTFEditor.ShowLayoutModeButton = true;
			this.WinRTFEditor.Size = new System.Drawing.Size(971, 166);
			this.WinRTFEditor.TabIndex = 1;
			this.WinRTFEditor.TabStop = false;
			// 
			// radRibbonFormBehavior1
			// 
			this.radRibbonFormBehavior1.Form = this;
			// 
			// frmRTFNewEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(971, 844);
			this.Controls.Add(this.WinRTFEditor);
			this.Controls.Add(this.radRichTextEditor1);
			this.FormBehavior = this.radRibbonFormBehavior1;
			this.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None;
			this.Name = "frmRTFNewEditor";
			// 
			// 
			// 
			this.RootElement.ApplyShapeToControl = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "";
			((System.ComponentModel.ISupportInitialize)(this.radRichTextEditor1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WinRTFEditor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.UI.RadRichTextEditor radRichTextEditor1;
		private Telerik.WinControls.UI.RadRibbonFormBehavior radRibbonFormBehavior1;
		public Telerik.WinControls.UI.RichTextEditorRibbonBar WinRTFEditor;
	}
}
