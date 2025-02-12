namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmAttorneyInformation
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
			Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttorneyInformation));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.lblDefSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.cmbAttorneyOption = new Telerik.WinControls.UI.RadDropDownList();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.dgDefAtty = new Telerik.WinControls.UI.RadGridView();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblDefSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAttorneyOption)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgDefAtty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgDefAtty.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.lblDefSearchCount);
			this.radPanel1.Controls.Add(this.cmbAttorneyOption);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Controls.Add(this.dgDefAtty);
			this.radPanel1.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radPanel1.Location = new System.Drawing.Point(13, 13);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(874, 569);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// lblDefSearchCount
			// 
			this.lblDefSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDefSearchCount.Location = new System.Drawing.Point(20, 547);
			this.lblDefSearchCount.Name = "lblDefSearchCount";
			this.lblDefSearchCount.Size = new System.Drawing.Size(66, 19);
			this.lblDefSearchCount.TabIndex = 18;
			this.lblDefSearchCount.Text = "radLabel5";
			this.lblDefSearchCount.ThemeName = "Crystal";
			// 
			// cmbAttorneyOption
			// 
			this.cmbAttorneyOption.DropDownAnimationEnabled = true;
			this.cmbAttorneyOption.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			radListDataItem1.Text = "All";
			radListDataItem2.Text = "Applicant";
			radListDataItem3.Text = "Defense";
			this.cmbAttorneyOption.Items.Add(radListDataItem1);
			this.cmbAttorneyOption.Items.Add(radListDataItem2);
			this.cmbAttorneyOption.Items.Add(radListDataItem3);
			this.cmbAttorneyOption.Location = new System.Drawing.Point(107, 43);
			this.cmbAttorneyOption.Name = "cmbAttorneyOption";
			this.cmbAttorneyOption.Size = new System.Drawing.Size(238, 24);
			this.cmbAttorneyOption.TabIndex = 17;
			this.cmbAttorneyOption.ThemeName = "Crystal";
			this.cmbAttorneyOption.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbAttorneyOption_SelectedIndexChanged);
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(15, 47);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(67, 19);
			this.radLabel2.TabIndex = 16;
			this.radLabel2.Text = "Category: ";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(107, 14);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(238, 23);
			this.txtSearch.TabIndex = 15;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(15, 17);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 14;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(744, 28);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(110, 39);
			this.btnNew.TabIndex = 13;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// dgDefAtty
			// 
			this.dgDefAtty.AllowShowFocusCues = true;
			this.dgDefAtty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgDefAtty.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgDefAtty.Location = new System.Drawing.Point(20, 73);
			// 
			// 
			// 
			this.dgDefAtty.MasterTemplate.AllowAddNewRow = false;
			this.dgDefAtty.MasterTemplate.AllowCellContextMenu = false;
			this.dgDefAtty.MasterTemplate.AllowColumnChooser = false;
			this.dgDefAtty.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgDefAtty.MasterTemplate.AllowColumnReorder = false;
			this.dgDefAtty.MasterTemplate.AllowColumnResize = false;
			this.dgDefAtty.MasterTemplate.AllowDeleteRow = false;
			this.dgDefAtty.MasterTemplate.AllowDragToGroup = false;
			this.dgDefAtty.MasterTemplate.AllowEditRow = false;
			this.dgDefAtty.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgDefAtty.MasterTemplate.AllowRowResize = false;
			this.dgDefAtty.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgDefAtty.Name = "dgDefAtty";
			this.dgDefAtty.Size = new System.Drawing.Size(834, 468);
			this.dgDefAtty.TabIndex = 0;
			this.dgDefAtty.ThemeName = "Crystal";
			this.dgDefAtty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgDefAtty_MouseDoubleClick);
			// 
			// frmAttorneyInformation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAttorneyInformation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = ".";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmAttorneyInformation_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAttorneyInformation_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblDefSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAttorneyOption)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgDefAtty.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgDefAtty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgDefAtty;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadDropDownList cmbAttorneyOption;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel lblDefSearchCount;
	}
}
