namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmDiagnosis
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiagnosis));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			this.txtBodyParts = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.txtDiagnosis = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.dgBillDiagnosis = new Telerik.WinControls.UI.RadGridView();
			this.btnNew = new Telerik.WinControls.UI.RadButton();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtBodyParts)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDiagnosis)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillDiagnosis)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillDiagnosis.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.radPanel1.Controls.Add(this.lblSearchCount);
			this.radPanel1.Controls.Add(this.txtBodyParts);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.txtDiagnosis);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Controls.Add(this.dgBillDiagnosis);
			this.radPanel1.Controls.Add(this.btnNew);
			this.radPanel1.Location = new System.Drawing.Point(13, 24);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(874, 558);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSearchCount.Location = new System.Drawing.Point(21, 536);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(66, 19);
			this.lblSearchCount.TabIndex = 10;
			this.lblSearchCount.Text = "radLabel2";
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// txtBodyParts
			// 
			this.txtBodyParts.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBodyParts.Location = new System.Drawing.Point(113, 43);
			this.txtBodyParts.Name = "txtBodyParts";
			this.txtBodyParts.ShowClearButton = true;
			this.txtBodyParts.Size = new System.Drawing.Size(219, 23);
			this.txtBodyParts.TabIndex = 13;
			this.txtBodyParts.ThemeName = "Crystal";
			this.txtBodyParts.TextChanged += new System.EventHandler(this.txtBodyParts_TextChanged);
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(21, 46);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(75, 19);
			this.radLabel2.TabIndex = 12;
			this.radLabel2.Text = "Body Parts:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// txtDiagnosis
			// 
			this.txtDiagnosis.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDiagnosis.Location = new System.Drawing.Point(113, 14);
			this.txtDiagnosis.Name = "txtDiagnosis";
			this.txtDiagnosis.ShowClearButton = true;
			this.txtDiagnosis.Size = new System.Drawing.Size(219, 23);
			this.txtDiagnosis.TabIndex = 11;
			this.txtDiagnosis.ThemeName = "Crystal";
			this.txtDiagnosis.TextChanged += new System.EventHandler(this.txtDiagnosis_TextChanged);
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(21, 17);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(69, 19);
			this.radLabel1.TabIndex = 10;
			this.radLabel1.Text = "Diagnosis:";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// dgBillDiagnosis
			// 
			this.dgBillDiagnosis.AllowDrop = true;
			this.dgBillDiagnosis.AllowShowFocusCues = true;
			this.dgBillDiagnosis.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgBillDiagnosis.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgBillDiagnosis.Location = new System.Drawing.Point(19, 72);
			// 
			// 
			// 
			this.dgBillDiagnosis.MasterTemplate.AllowAddNewRow = false;
			this.dgBillDiagnosis.MasterTemplate.AllowCellContextMenu = false;
			this.dgBillDiagnosis.MasterTemplate.AllowColumnChooser = false;
			this.dgBillDiagnosis.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgBillDiagnosis.MasterTemplate.AllowColumnReorder = false;
			this.dgBillDiagnosis.MasterTemplate.AllowColumnResize = false;
			this.dgBillDiagnosis.MasterTemplate.AllowDeleteRow = false;
			this.dgBillDiagnosis.MasterTemplate.AllowDragToGroup = false;
			this.dgBillDiagnosis.MasterTemplate.AllowEditRow = false;
			this.dgBillDiagnosis.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgBillDiagnosis.MasterTemplate.AllowRowResize = false;
			this.dgBillDiagnosis.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgBillDiagnosis.Name = "dgBillDiagnosis";
			this.dgBillDiagnosis.Size = new System.Drawing.Size(835, 454);
			this.dgBillDiagnosis.TabIndex = 3;
			this.dgBillDiagnosis.ThemeName = "Crystal";
			this.dgBillDiagnosis.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgBillDiagnosis_KeyDown);
			this.dgBillDiagnosis.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgBillDiagnosis_MouseDoubleClick);
			// 
			// btnNew
			// 
			this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNew.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnNew.Image = global::PCMS_Lipa_General_Tool.Properties.Resources._new;
			this.btnNew.Location = new System.Drawing.Point(692, 14);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(130, 38);
			this.btnNew.TabIndex = 2;
			this.btnNew.Text = "&New";
			this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnNew.ThemeName = "Crystal";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// frmDiagnosis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 594);
			this.Controls.Add(this.radPanel1);
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDiagnosis";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Diagnosis";
			this.ThemeName = "Crystal";
			this.Load += new System.EventHandler(this.frmBillDiagnosis_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBillDiagnosis_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtBodyParts)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDiagnosis)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillDiagnosis.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgBillDiagnosis)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadGridView dgBillDiagnosis;
		public Telerik.WinControls.UI.RadButton btnNew;
		private Telerik.WinControls.UI.RadTextBox txtDiagnosis;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadTextBox txtBodyParts;
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
	}
}
