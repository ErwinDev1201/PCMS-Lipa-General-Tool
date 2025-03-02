namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmInsuranceInfo
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsuranceInfo));
			this.v = new Telerik.WinControls.UI.RadPanel();
			this.lblcountSearchResult = new Telerik.WinControls.UI.RadLabel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.dgInsuranceInfo = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.v)).BeginInit();
			this.v.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblcountSearchResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgInsuranceInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgInsuranceInfo.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// v
			// 
			this.v.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.v.Controls.Add(this.lblcountSearchResult);
			this.v.Controls.Add(this.txtSearch);
			this.v.Controls.Add(this.radLabel1);
			this.v.Controls.Add(this.dgInsuranceInfo);
			this.v.Controls.Add(this.btnNew);
			this.v.Location = new System.Drawing.Point(13, 24);
			this.v.Name = "v";
			this.v.Size = new System.Drawing.Size(874, 558);
			this.v.TabIndex = 0;
			this.v.ThemeName = "Crystal";
			// 
			// lblcountSearchResult
			// 
			this.lblcountSearchResult.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblcountSearchResult.Location = new System.Drawing.Point(19, 536);
			this.lblcountSearchResult.Name = "lblcountSearchResult";
			this.lblcountSearchResult.Size = new System.Drawing.Size(66, 19);
			this.lblcountSearchResult.TabIndex = 10;
			this.lblcountSearchResult.Text = "radLabel2";
			this.lblcountSearchResult.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(109, 32);
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
			this.radLabel1.Location = new System.Drawing.Point(17, 35);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// dgInsuranceInfo
			// 
			this.dgInsuranceInfo.AllowDrop = true;
			this.dgInsuranceInfo.AllowShowFocusCues = true;
			this.dgInsuranceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgInsuranceInfo.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgInsuranceInfo.Location = new System.Drawing.Point(19, 61);
			// 
			// 
			// 
			this.dgInsuranceInfo.MasterTemplate.AllowAddNewRow = false;
			this.dgInsuranceInfo.MasterTemplate.AllowCellContextMenu = false;
			this.dgInsuranceInfo.MasterTemplate.AllowColumnChooser = false;
			this.dgInsuranceInfo.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgInsuranceInfo.MasterTemplate.AllowColumnReorder = false;
			this.dgInsuranceInfo.MasterTemplate.AllowColumnResize = false;
			this.dgInsuranceInfo.MasterTemplate.AllowDeleteRow = false;
			this.dgInsuranceInfo.MasterTemplate.AllowDragToGroup = false;
			this.dgInsuranceInfo.MasterTemplate.AllowEditRow = false;
			this.dgInsuranceInfo.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgInsuranceInfo.MasterTemplate.AllowRowResize = false;
			this.dgInsuranceInfo.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgInsuranceInfo.Name = "dgInsuranceInfo";
			this.dgInsuranceInfo.Size = new System.Drawing.Size(835, 469);
			this.dgInsuranceInfo.TabIndex = 3;
			this.dgInsuranceInfo.ThemeName = "Crystal";
			this.dgInsuranceInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgInsuranceInfo_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(744, 16);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(110, 39);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// frmInsuranceInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.v);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmInsuranceInfo";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAdjusterinformation";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmAdjusterinformation_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInsuranceInfo_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.v)).EndInit();
			this.v.ResumeLayout(false);
			this.v.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblcountSearchResult)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgInsuranceInfo.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgInsuranceInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel v;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgInsuranceInfo;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel lblcountSearchResult;
	}
}
