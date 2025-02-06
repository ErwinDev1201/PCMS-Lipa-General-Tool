namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmHearingRep
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHearingRep));
			this.v = new Telerik.WinControls.UI.RadPanel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.dgHearingRep = new Telerik.WinControls.UI.RadGridView();
			((System.ComponentModel.ISupportInitialize)(this.v)).BeginInit();
			this.v.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgHearingRep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgHearingRep.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// v
			// 
			this.v.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.v.Controls.Add(this.dgHearingRep);
			this.v.Controls.Add(this.radLabel2);
			this.v.Controls.Add(this.txtSearch);
			this.v.Controls.Add(this.radLabel1);
			this.v.Controls.Add(this.lblSearchCount);
			this.v.Controls.Add(this.btnNew);
			this.v.Location = new System.Drawing.Point(13, 24);
			this.v.Name = "v";
			this.v.Size = new System.Drawing.Size(874, 558);
			this.v.TabIndex = 0;
			this.v.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(110, 38);
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
			this.radLabel1.Location = new System.Drawing.Point(18, 41);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(84, 19);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(19, 600);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(66, 19);
			this.lblSearchCount.TabIndex = 4;
			this.lblSearchCount.Text = "radLabel2";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// btnNew
			// 
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Image = global::PCMS_Lipa_General_Tool.Properties.Resources._new;
			this.btnNew.Location = new System.Drawing.Point(692, 13);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(162, 48);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// radLabel2
			// 
			this.radLabel2.Location = new System.Drawing.Point(19, 535);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(68, 20);
			this.radLabel2.TabIndex = 10;
			this.radLabel2.Text = "radLabel2";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// dgHearingRep
			// 
			this.dgHearingRep.AllowShowFocusCues = true;
			this.dgHearingRep.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgHearingRep.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgHearingRep.Location = new System.Drawing.Point(19, 67);
			// 
			// 
			// 
			this.dgHearingRep.MasterTemplate.AllowAddNewRow = false;
			this.dgHearingRep.MasterTemplate.AllowCellContextMenu = false;
			this.dgHearingRep.MasterTemplate.AllowColumnChooser = false;
			this.dgHearingRep.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgHearingRep.MasterTemplate.AllowColumnReorder = false;
			this.dgHearingRep.MasterTemplate.AllowColumnResize = false;
			this.dgHearingRep.MasterTemplate.AllowDeleteRow = false;
			this.dgHearingRep.MasterTemplate.AllowDragToGroup = false;
			this.dgHearingRep.MasterTemplate.AllowEditRow = false;
			this.dgHearingRep.MasterTemplate.AllowRowResize = false;
			this.dgHearingRep.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgHearingRep.Name = "dgHearingRep";
			this.dgHearingRep.Size = new System.Drawing.Size(835, 462);
			this.dgHearingRep.TabIndex = 11;
			this.dgHearingRep.ThemeName = "Crystal";
			// 
			// frmHearingRep
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.v);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmHearingRep";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAdjusterinformation";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmHearingRep_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHearingRep_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.v)).EndInit();
			this.v.ResumeLayout(false);
			this.v.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgHearingRep.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgHearingRep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel v;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		public Telerik.WinControls.UI.RadGridView dgHearingRep;
	}
}
