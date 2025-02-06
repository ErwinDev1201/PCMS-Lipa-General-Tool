namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmBundlecodes
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
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBundlecodes));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.dgBundleCode = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.lblbundeCode = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBundleCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBundleCode.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblbundeCode)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.lblbundeCode);
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.dgBundleCode);
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(874, 558);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(112, 24);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(283, 23);
			this.txtSearch.TabIndex = 9;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(22, 27);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(19, 69);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(796, 19);
			this.radLabel2.TabIndex = 4;
			this.radLabel2.Text = "NOTE:  USE OF MODIFIER 59 IS ALLOWED WHEN THE BUNDLED CPT CODES ARE PERFORMED IN " +
    "A DIFFERENT REGION THAN CMT.";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// dgBundleCode
			// 
			this.dgBundleCode.AllowShowFocusCues = true;
			this.dgBundleCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgBundleCode.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgBundleCode.Location = new System.Drawing.Point(19, 94);
			// 
			// 
			// 
			this.dgBundleCode.MasterTemplate.AllowAddNewRow = false;
			this.dgBundleCode.MasterTemplate.AllowCellContextMenu = false;
			this.dgBundleCode.MasterTemplate.AllowColumnChooser = false;
			this.dgBundleCode.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgBundleCode.MasterTemplate.AllowColumnReorder = false;
			this.dgBundleCode.MasterTemplate.AllowColumnResize = false;
			this.dgBundleCode.MasterTemplate.AllowDeleteRow = false;
			this.dgBundleCode.MasterTemplate.AllowDragToGroup = false;
			this.dgBundleCode.MasterTemplate.AllowEditRow = false;
			this.dgBundleCode.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgBundleCode.MasterTemplate.AllowRowResize = false;
			this.dgBundleCode.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgBundleCode.Name = "dgBundleCode";
			this.dgBundleCode.Size = new System.Drawing.Size(835, 429);
			this.dgBundleCode.TabIndex = 3;
			this.dgBundleCode.ThemeName = "Crystal";
			this.dgBundleCode.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgBundleCode_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Image = global::PCMS_Lipa_General_Tool.Properties.Resources._new;
			this.btnNew.Location = new System.Drawing.Point(692, 15);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(162, 48);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// lblbundeCode
			// 
			this.lblbundeCode.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblbundeCode.Location = new System.Drawing.Point(21, 536);
			this.lblbundeCode.Name = "lblbundeCode";
			this.lblbundeCode.Size = new System.Drawing.Size(66, 19);
			this.lblbundeCode.TabIndex = 10;
			this.lblbundeCode.Text = "radLabel3";
			this.lblbundeCode.ThemeName = "Crystal";
			// 
			// frmBundlecodes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBundlecodes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PT Bundle Codes";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmBundlecodes_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBundlecodes_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBundleCode.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBundleCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblbundeCode)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadButton btnNew;
		public Telerik.WinControls.UI.RadLabel radLabel2;
		public Telerik.WinControls.UI.RadGridView dgBundleCode;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel lblbundeCode;
	}
}
