namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmMPN
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMPN));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.txtLink = new Telerik.WinControls.UI.RadTextBox();
			this.dgMPN = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLink)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMPN)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMPN.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.lblSearchCount);
			this.radPanel1.Controls.Add(this.txtLink);
			this.radPanel1.Controls.Add(this.dgMPN);
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(937, 634);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(114, 43);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(283, 24);
			this.txtSearch.TabIndex = 9;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(22, 46);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(90, 20);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(19, 600);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(69, 20);
			this.lblSearchCount.TabIndex = 5;
			this.lblSearchCount.Text = "radLabel2";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// txtLink
			// 
			this.txtLink.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLink.Location = new System.Drawing.Point(601, 43);
			this.txtLink.Name = "txtLink";
			this.txtLink.Size = new System.Drawing.Size(100, 24);
			this.txtLink.TabIndex = 4;
			this.txtLink.ThemeName = "Crystal";
			// 
			// dgMPN
			// 
			this.dgMPN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgMPN.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgMPN.Location = new System.Drawing.Point(19, 73);
			// 
			// 
			// 
			this.dgMPN.MasterTemplate.AllowAddNewRow = false;
			this.dgMPN.MasterTemplate.AllowCellContextMenu = false;
			this.dgMPN.MasterTemplate.AllowColumnChooser = false;
			this.dgMPN.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgMPN.MasterTemplate.AllowColumnReorder = false;
			this.dgMPN.MasterTemplate.AllowColumnResize = false;
			this.dgMPN.MasterTemplate.AllowDeleteRow = false;
			this.dgMPN.MasterTemplate.AllowDragToGroup = false;
			this.dgMPN.MasterTemplate.AllowEditRow = false;
			this.dgMPN.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgMPN.MasterTemplate.AllowRowResize = false;
			this.dgMPN.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgMPN.Name = "dgMPN";
			this.dgMPN.Size = new System.Drawing.Size(898, 520);
			this.dgMPN.TabIndex = 3;
			this.dgMPN.ThemeName = "Crystal";
			this.dgMPN.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgMPN_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Location = new System.Drawing.Point(789, 19);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(128, 48);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// frmMPN
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(962, 670);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMPN";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAdjusterinformation";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmMPN_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMPN_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtLink)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMPN.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgMPN)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgMPN;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
		private Telerik.WinControls.UI.RadTextBox txtLink;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
	}
}
