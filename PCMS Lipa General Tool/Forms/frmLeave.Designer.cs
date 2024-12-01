namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmLeave
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
			Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeave));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
			this.btnRefresh = new Telerik.WinControls.UI.RadButton();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.cmbFilterStatus = new Telerik.WinControls.UI.RadDropDownList();
			this.cmbFilterName = new Telerik.WinControls.UI.RadDropDownList();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.lblCountSearch = new Telerik.WinControls.UI.RadLabel();
			this.dgLeave = new Telerik.WinControls.UI.RadGridView();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
			this.radGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFilterStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFilterName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCountSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLeave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLeave.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Controls.Add(this.radGroupBox1);
			this.radPanel1.Controls.Add(this.lblCountSearch);
			this.radPanel1.Controls.Add(this.dgLeave);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(937, 634);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(760, 19);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(157, 48);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&File a Leave";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// radGroupBox1
			// 
			this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
			this.radGroupBox1.Controls.Add(this.btnRefresh);
			this.radGroupBox1.Controls.Add(this.radLabel3);
			this.radGroupBox1.Controls.Add(this.cmbFilterStatus);
			this.radGroupBox1.Controls.Add(this.cmbFilterName);
			this.radGroupBox1.Controls.Add(this.radLabel2);
			this.radGroupBox1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(1);
			this.radGroupBox1.HeaderText = "Filter by";
			this.radGroupBox1.Location = new System.Drawing.Point(19, 62);
			this.radGroupBox1.Name = "radGroupBox1";
			this.radGroupBox1.Size = new System.Drawing.Size(898, 80);
			this.radGroupBox1.TabIndex = 5;
			this.radGroupBox1.Text = "Filter by";
			this.radGroupBox1.ThemeName = "Crystal";
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRefresh.Location = new System.Drawing.Point(853, 39);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(30, 30);
			this.btnRefresh.TabIndex = 6;
			this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnRefresh.ThemeName = "Crystal";
			this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(368, 43);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(109, 20);
			this.radLabel3.TabIndex = 3;
			this.radLabel3.Text = "Filter by Status: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// cmbFilterStatus
			// 
			this.cmbFilterStatus.DropDownAnimationEnabled = true;
			this.cmbFilterStatus.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			radListDataItem1.Text = "FOR APPROVAL";
			radListDataItem2.Text = "APPROVED";
			radListDataItem3.Text = "NOT APPROVED";
			radListDataItem4.Text = "NEED SUP. DOC";
			this.cmbFilterStatus.Items.Add(radListDataItem1);
			this.cmbFilterStatus.Items.Add(radListDataItem2);
			this.cmbFilterStatus.Items.Add(radListDataItem3);
			this.cmbFilterStatus.Items.Add(radListDataItem4);
			this.cmbFilterStatus.Location = new System.Drawing.Point(479, 41);
			this.cmbFilterStatus.Name = "cmbFilterStatus";
			this.cmbFilterStatus.Size = new System.Drawing.Size(174, 24);
			this.cmbFilterStatus.TabIndex = 1;
			this.cmbFilterStatus.ThemeName = "Crystal";
			this.cmbFilterStatus.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbFilterStatus_SelectedIndexChanged);
			// 
			// cmbFilterName
			// 
			this.cmbFilterName.DropDownAnimationEnabled = true;
			this.cmbFilterName.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbFilterName.Location = new System.Drawing.Point(114, 39);
			this.cmbFilterName.Name = "cmbFilterName";
			this.cmbFilterName.Size = new System.Drawing.Size(174, 24);
			this.cmbFilterName.TabIndex = 0;
			this.cmbFilterName.ThemeName = "Crystal";
			this.cmbFilterName.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbFilterName_SelectedIndexChanged);
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(6, 41);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(105, 20);
			this.radLabel2.TabIndex = 2;
			this.radLabel2.Text = "Filter by Name: ";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// lblCountSearch
			// 
			this.lblCountSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCountSearch.Location = new System.Drawing.Point(19, 600);
			this.lblCountSearch.Name = "lblCountSearch";
			this.lblCountSearch.Size = new System.Drawing.Size(69, 20);
			this.lblCountSearch.TabIndex = 4;
			this.lblCountSearch.Text = "radLabel2";
			this.lblCountSearch.ThemeName = "Crystal";
			// 
			// dgLeave
			// 
			this.dgLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgLeave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgLeave.Location = new System.Drawing.Point(19, 148);
			// 
			// 
			// 
			this.dgLeave.MasterTemplate.AllowAddNewRow = false;
			this.dgLeave.MasterTemplate.AllowCellContextMenu = false;
			this.dgLeave.MasterTemplate.AllowColumnChooser = false;
			this.dgLeave.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgLeave.MasterTemplate.AllowColumnReorder = false;
			this.dgLeave.MasterTemplate.AllowColumnResize = false;
			this.dgLeave.MasterTemplate.AllowDeleteRow = false;
			this.dgLeave.MasterTemplate.AllowDragToGroup = false;
			this.dgLeave.MasterTemplate.AllowEditRow = false;
			this.dgLeave.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgLeave.MasterTemplate.AllowRowResize = false;
			this.dgLeave.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgLeave.Name = "dgLeave";
			this.dgLeave.Size = new System.Drawing.Size(898, 445);
			this.dgLeave.TabIndex = 3;
			this.dgLeave.ThemeName = "Crystal";
			this.dgLeave.DoubleClick += new System.EventHandler(this.dgLeave_DoubleClick);
			// 
			// frmLeave
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(962, 670);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLeave";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Adjuster Information";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmLeave_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLeave_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
			this.radGroupBox1.ResumeLayout(false);
			this.radGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFilterStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbFilterName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCountSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLeave.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgLeave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgLeave;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadLabel lblCountSearch;
		private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		public Telerik.WinControls.UI.RadDropDownList cmbFilterStatus;
		public Telerik.WinControls.UI.RadDropDownList cmbFilterName;
		public Telerik.WinControls.UI.RadLabel radLabel3;
		public Telerik.WinControls.UI.RadButton btnRefresh;
	}
}
