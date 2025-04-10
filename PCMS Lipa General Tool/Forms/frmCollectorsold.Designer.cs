namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmCollectorsold
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCollectorsold));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.radStatusStrip1 = new Telerik.WinControls.UI.RadStatusStrip();
			this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
			this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
			this.radMenuItem2 = new Telerik.WinControls.UI.RadMenuItem();
			this.radMenuItem3 = new Telerik.WinControls.UI.RadMenuItem();
			this.radMenuItem4 = new Telerik.WinControls.UI.RadMenuItem();
			this.radMenuItem5 = new Telerik.WinControls.UI.RadMenuItem();
			this.radMenuItem6 = new Telerik.WinControls.UI.RadMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radStatusStrip1
			// 
			this.radStatusStrip1.Location = new System.Drawing.Point(0, 661);
			this.radStatusStrip1.Name = "radStatusStrip1";
			this.radStatusStrip1.Size = new System.Drawing.Size(992, 26);
			this.radStatusStrip1.TabIndex = 8;
			this.radStatusStrip1.ThemeName = "Crystal";
			// 
			// radMenu1
			// 
			this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1,
            this.radMenuItem2,
            this.radMenuItem3,
            this.radMenuItem4,
            this.radMenuItem5,
            this.radMenuItem6});
			this.radMenu1.Location = new System.Drawing.Point(0, 0);
			this.radMenu1.Name = "radMenu1";
			this.radMenu1.Size = new System.Drawing.Size(992, 34);
			this.radMenu1.TabIndex = 7;
			this.radMenu1.ThemeName = "Crystal";
			// 
			// radMenuItem1
			// 
			this.radMenuItem1.Name = "radMenuItem1";
			this.radMenuItem1.Text = "File";
			// 
			// radMenuItem2
			// 
			this.radMenuItem2.Name = "radMenuItem2";
			this.radMenuItem2.Text = "Tools";
			// 
			// radMenuItem3
			// 
			this.radMenuItem3.Name = "radMenuItem3";
			this.radMenuItem3.Text = "TM Pantry";
			// 
			// radMenuItem4
			// 
			this.radMenuItem4.Name = "radMenuItem4";
			this.radMenuItem4.Text = "Leave";
			// 
			// radMenuItem5
			// 
			this.radMenuItem5.Name = "radMenuItem5";
			this.radMenuItem5.Text = "Themes";
			// 
			// radMenuItem6
			// 
			this.radMenuItem6.Name = "radMenuItem6";
			this.radMenuItem6.Text = "Help";
			// 
			// frmCollectorsold
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 687);
			this.Controls.Add(this.radStatusStrip1);
			this.Controls.Add(this.radMenu1);
			this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmCollectorsold";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmCollectors";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.radStatusStrip1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadStatusStrip radStatusStrip1;
		private Telerik.WinControls.UI.RadMenu radMenu1;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem2;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem3;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem4;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem5;
		private Telerik.WinControls.UI.RadMenuItem radMenuItem6;
	}
}
