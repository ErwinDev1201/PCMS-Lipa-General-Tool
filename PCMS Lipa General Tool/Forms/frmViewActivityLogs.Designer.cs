namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmViewActivityLogs
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewActivityLogs));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
			this.cmbAction = new Telerik.WinControls.UI.RadDropDownList();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.dgActivityLogs = new Telerik.WinControls.UI.RadGridView();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.radLabel12)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAction)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgActivityLogs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgActivityLogs.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.radLabel12);
			this.radPanel1.Controls.Add(this.cmbAction);
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.lblSearchCount);
			this.radPanel1.Controls.Add(this.dgActivityLogs);
			this.radPanel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(1070, 719);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// radLabel12
			// 
			this.radLabel12.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel12.Location = new System.Drawing.Point(687, 28);
			this.radLabel12.Name = "radLabel12";
			this.radLabel12.Size = new System.Drawing.Size(91, 20);
			this.radLabel12.TabIndex = 13;
			this.radLabel12.Text = "User Status:  ";
			this.radLabel12.ThemeName = "Crystal";
			// 
			// cmbAction
			// 
			this.cmbAction.DropDownAnimationEnabled = true;
			this.cmbAction.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			radListDataItem1.Text = "Both";
			radListDataItem2.Text = "Active";
			radListDataItem3.Text = "InActive";
			this.cmbAction.Items.Add(radListDataItem1);
			this.cmbAction.Items.Add(radListDataItem2);
			this.cmbAction.Items.Add(radListDataItem3);
			this.cmbAction.Location = new System.Drawing.Point(781, 26);
			this.cmbAction.Name = "cmbAction";
			this.cmbAction.Size = new System.Drawing.Size(269, 24);
			this.cmbAction.TabIndex = 12;
			this.cmbAction.ThemeName = "Crystal";
			this.cmbAction.PopupOpening += new System.ComponentModel.CancelEventHandler(this.cmbAction_PopupOpening);
			this.cmbAction.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbAction_SelectedIndexChanged);
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(113, 28);
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
			this.radLabel1.Location = new System.Drawing.Point(21, 31);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(90, 20);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(19, 684);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(69, 20);
			this.lblSearchCount.TabIndex = 5;
			this.lblSearchCount.Text = "radLabel2";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// dgActivityLogs
			// 
			this.dgActivityLogs.AllowShowFocusCues = true;
			this.dgActivityLogs.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgActivityLogs.Location = new System.Drawing.Point(19, 58);
			// 
			// 
			// 
			this.dgActivityLogs.MasterTemplate.AllowAddNewRow = false;
			this.dgActivityLogs.MasterTemplate.AllowCellContextMenu = false;
			this.dgActivityLogs.MasterTemplate.AllowColumnChooser = false;
			this.dgActivityLogs.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgActivityLogs.MasterTemplate.AllowColumnReorder = false;
			this.dgActivityLogs.MasterTemplate.AllowColumnResize = false;
			this.dgActivityLogs.MasterTemplate.AllowDeleteRow = false;
			this.dgActivityLogs.MasterTemplate.AllowDragToGroup = false;
			this.dgActivityLogs.MasterTemplate.AllowEditRow = false;
			this.dgActivityLogs.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgActivityLogs.Name = "dgActivityLogs";
			this.dgActivityLogs.Size = new System.Drawing.Size(1031, 620);
			this.dgActivityLogs.TabIndex = 3;
			this.dgActivityLogs.ThemeName = "Crystal";
			this.dgActivityLogs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgActivityLogs_MouseDoubleClick);
			// 
			// frmViewActivityLogs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1095, 755);
			this.Controls.Add(this.radPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmViewActivityLogs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmAdjusterinformation";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmViewActivityLogs_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmViewActivityLogs_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.radLabel12)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbAction)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgActivityLogs.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgActivityLogs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.UI.RadGridView dgActivityLogs;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel radLabel12;
		private Telerik.WinControls.UI.RadDropDownList cmbAction;
	}
}
