namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmAgingExcelPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAgingExcelPreview));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.cmbProviderList = new Telerik.WinControls.UI.RadDropDownList();
			this.btnImportDb = new Telerik.WinControls.UI.RadButton();
			this.dgExcelPreview = new Telerik.WinControls.UI.RadGridView();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.ProgBarStatus = new Telerik.WinControls.UI.RadProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnImportDb)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExcelPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExcelPreview.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ProgBarStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.ProgBarStatus);
			this.radPanel1.Controls.Add(this.cmbProviderList);
			this.radPanel1.Controls.Add(this.btnImportDb);
			this.radPanel1.Controls.Add(this.dgExcelPreview);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(874, 558);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// cmbProviderList
			// 
			this.cmbProviderList.Location = new System.Drawing.Point(19, 524);
			this.cmbProviderList.Name = "cmbProviderList";
			this.cmbProviderList.Size = new System.Drawing.Size(204, 24);
			this.cmbProviderList.TabIndex = 14;
			this.cmbProviderList.Text = "Select Provider";
			this.cmbProviderList.ThemeName = "Crystal";
			this.cmbProviderList.PopupOpened += new System.EventHandler(this.cmbProviderList_PopupOpened);
			// 
			// btnImportDb
			// 
			this.btnImportDb.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnImportDb.Location = new System.Drawing.Point(229, 524);
			this.btnImportDb.Name = "btnImportDb";
			this.btnImportDb.Size = new System.Drawing.Size(134, 28);
			this.btnImportDb.TabIndex = 12;
			this.btnImportDb.Text = "Import into System";
			this.btnImportDb.ThemeName = "Crystal";
			this.btnImportDb.Click += new System.EventHandler(this.btnImportDb_Click);
			// 
			// dgExcelPreview
			// 
			this.dgExcelPreview.AllowShowFocusCues = true;
			this.dgExcelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgExcelPreview.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgExcelPreview.Location = new System.Drawing.Point(19, 13);
			// 
			// 
			// 
			this.dgExcelPreview.MasterTemplate.AllowAddNewRow = false;
			this.dgExcelPreview.MasterTemplate.AllowCellContextMenu = false;
			this.dgExcelPreview.MasterTemplate.AllowColumnChooser = false;
			this.dgExcelPreview.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgExcelPreview.MasterTemplate.AllowColumnReorder = false;
			this.dgExcelPreview.MasterTemplate.AllowColumnResize = false;
			this.dgExcelPreview.MasterTemplate.AllowDeleteRow = false;
			this.dgExcelPreview.MasterTemplate.AllowDragToGroup = false;
			this.dgExcelPreview.MasterTemplate.AllowEditRow = false;
			this.dgExcelPreview.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgExcelPreview.MasterTemplate.AllowRowResize = false;
			this.dgExcelPreview.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgExcelPreview.Name = "dgExcelPreview";
			this.dgExcelPreview.Size = new System.Drawing.Size(835, 505);
			this.dgExcelPreview.TabIndex = 3;
			this.dgExcelPreview.ThemeName = "Crystal";
			// 
			// ProgBarStatus
			// 
			this.ProgBarStatus.Location = new System.Drawing.Point(393, 524);
			this.ProgBarStatus.Name = "ProgBarStatus";
			this.ProgBarStatus.Size = new System.Drawing.Size(461, 24);
			this.ProgBarStatus.TabIndex = 15;
			this.ProgBarStatus.Text = "Ready";
			this.ProgBarStatus.ThemeName = "Crystal";
			// 
			// frmAgingExcelPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAgingExcelPreview";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ung ";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnImportDb)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExcelPreview.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgExcelPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ProgBarStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgExcelPreview;
		private Telerik.WinControls.UI.RadDropDownList cmbProviderList;
		public Telerik.WinControls.UI.RadButton btnImportDb;
		private Telerik.WinControls.UI.RadProgressBar ProgBarStatus;
	}
}
