﻿namespace PCMS_Lipa_General_Tool.Forms
{
    partial class frmEasyPrint
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEasyPrint));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.lblCountResult = new Telerik.WinControls.UI.RadLabel();
			this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.dgEasyPrint = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblCountResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgEasyPrint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgEasyPrint.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.lblCountResult);
			this.radPanel1.Controls.Add(this.txtSearch);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.dgEasyPrint);
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(874, 558);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// lblCountResult
			// 
			this.lblCountResult.Location = new System.Drawing.Point(19, 535);
			this.lblCountResult.Name = "lblCountResult";
			this.lblCountResult.Size = new System.Drawing.Size(68, 20);
			this.lblCountResult.TabIndex = 10;
			this.lblCountResult.Text = "radLabel2";
			this.lblCountResult.ThemeName = "Crystal";
			// 
			// txtSearch
			// 
			this.txtSearch.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSearch.Location = new System.Drawing.Point(114, 30);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.ShowClearButton = true;
			this.txtSearch.Size = new System.Drawing.Size(283, 23);
			this.txtSearch.TabIndex = 9;
			this.txtSearch.ThemeName = "Crystal";
			this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Location = new System.Drawing.Point(22, 33);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(86, 20);
			this.radLabel1.TabIndex = 8;
			this.radLabel1.Text = "Search here: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// dgEasyPrint
			// 
			this.dgEasyPrint.AllowShowFocusCues = true;
			this.dgEasyPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgEasyPrint.Location = new System.Drawing.Point(19, 59);
			// 
			// 
			// 
			this.dgEasyPrint.MasterTemplate.AllowAddNewRow = false;
			this.dgEasyPrint.MasterTemplate.AllowCellContextMenu = false;
			this.dgEasyPrint.MasterTemplate.AllowColumnChooser = false;
			this.dgEasyPrint.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgEasyPrint.MasterTemplate.AllowColumnReorder = false;
			this.dgEasyPrint.MasterTemplate.AllowColumnResize = false;
			this.dgEasyPrint.MasterTemplate.AllowDeleteRow = false;
			this.dgEasyPrint.MasterTemplate.AllowDragToGroup = false;
			this.dgEasyPrint.MasterTemplate.AllowEditRow = false;
			this.dgEasyPrint.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgEasyPrint.MasterTemplate.AllowRowResize = false;
			this.dgEasyPrint.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgEasyPrint.Name = "dgEasyPrint";
			this.dgEasyPrint.Size = new System.Drawing.Size(835, 470);
			this.dgEasyPrint.TabIndex = 3;
			this.dgEasyPrint.ThemeName = "Crystal";
			this.dgEasyPrint.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgEasyPrint_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(742, 14);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(110, 39);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// frmEasyPrint
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmEasyPrint";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Easy Print Denial";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmEasyPrint_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEasyPrint_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblCountResult)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgEasyPrint.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgEasyPrint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgEasyPrint;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadTextBox txtSearch;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel lblCountResult;
	}
}
