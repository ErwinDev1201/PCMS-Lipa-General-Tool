namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmBillReviewDirectory
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBillReviewDirectory));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.dgBillReview = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillReview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillReview.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.lblSearchCount);
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.dgBillReview);
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
			this.txtSearch.Location = new System.Drawing.Point(109, 43);
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
			this.radLabel1.Location = new System.Drawing.Point(19, 47);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// dgBillReview
			// 
			this.dgBillReview.AllowShowFocusCues = true;
			this.dgBillReview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgBillReview.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgBillReview.Location = new System.Drawing.Point(19, 73);
			// 
			// 
			// 
			this.dgBillReview.MasterTemplate.AllowAddNewRow = false;
			this.dgBillReview.MasterTemplate.AllowCellContextMenu = false;
			this.dgBillReview.MasterTemplate.AllowColumnChooser = false;
			this.dgBillReview.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgBillReview.MasterTemplate.AllowColumnReorder = false;
			this.dgBillReview.MasterTemplate.AllowColumnResize = false;
			this.dgBillReview.MasterTemplate.AllowDeleteRow = false;
			this.dgBillReview.MasterTemplate.AllowDragToGroup = false;
			this.dgBillReview.MasterTemplate.AllowEditRow = false;
			this.dgBillReview.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgBillReview.MasterTemplate.AllowRowResize = false;
			this.dgBillReview.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgBillReview.Name = "dgBillReview";
			this.dgBillReview.Size = new System.Drawing.Size(835, 450);
			this.dgBillReview.TabIndex = 3;
			this.dgBillReview.ThemeName = "Crystal";
			this.dgBillReview.Click += new System.EventHandler(this.dgBillReview_Click);
			this.dgBillReview.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgBillReview_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Image = global::PCMS_Lipa_General_Tool.Properties.Resources._new;
			this.btnNew.Location = new System.Drawing.Point(692, 19);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(162, 48);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(20, 536);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(66, 19);
			this.lblSearchCount.TabIndex = 5;
			this.lblSearchCount.Text = "radLabel2";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// frmBillReviewDirectory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBillReviewDirectory";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAdjusterinformation";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmInsBillReviewDirectory_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInsBillReviewDirectory_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillReview.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillReview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgBillReview;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
	}
}
