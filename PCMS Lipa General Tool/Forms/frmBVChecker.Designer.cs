namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmBVChecker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBVChecker));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.btnCheckBV = new Telerik.WinControls.UI.RadButton();
			this.dgAvailability = new Telerik.WinControls.UI.RadGridView();
			this.lblSearchCount = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.btnCheckBV)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAvailability)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAvailability.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCheckBV
			// 
			this.btnCheckBV.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCheckBV.Location = new System.Drawing.Point(12, 12);
			this.btnCheckBV.Name = "btnCheckBV";
			this.btnCheckBV.Size = new System.Drawing.Size(268, 41);
			this.btnCheckBV.TabIndex = 0;
			this.btnCheckBV.Text = "Check Broadvoice Available";
			this.btnCheckBV.ThemeName = "Crystal";
			this.btnCheckBV.Click += new System.EventHandler(this.btnCheckBV_Click);
			// 
			// dgAvailability
			// 
			this.dgAvailability.AllowDrop = true;
			this.dgAvailability.Location = new System.Drawing.Point(13, 71);
			// 
			// 
			// 
			this.dgAvailability.MasterTemplate.AllowAddNewRow = false;
			this.dgAvailability.MasterTemplate.AllowCellContextMenu = false;
			this.dgAvailability.MasterTemplate.AllowColumnChooser = false;
			this.dgAvailability.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgAvailability.MasterTemplate.AllowColumnReorder = false;
			this.dgAvailability.MasterTemplate.AllowDeleteRow = false;
			this.dgAvailability.MasterTemplate.AllowDragToGroup = false;
			this.dgAvailability.MasterTemplate.AllowEditRow = false;
			this.dgAvailability.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgAvailability.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgAvailability.Name = "dgAvailability";
			this.dgAvailability.Size = new System.Drawing.Size(718, 383);
			this.dgAvailability.TabIndex = 1;
			this.dgAvailability.ThemeName = "Crystal";
			// 
			// lblSearchCount
			// 
			this.lblSearchCount.Location = new System.Drawing.Point(13, 460);
			this.lblSearchCount.Name = "lblSearchCount";
			this.lblSearchCount.Size = new System.Drawing.Size(2, 2);
			this.lblSearchCount.TabIndex = 2;
			this.lblSearchCount.ThemeName = "Crystal";
			// 
			// frmBVChecker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 62);
			this.Controls.Add(this.lblSearchCount);
			this.Controls.Add(this.dgAvailability);
			this.Controls.Add(this.btnCheckBV);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmBVChecker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmDBUtility";
			this.ThemeName = "Crystal";
			((System.ComponentModel.ISupportInitialize)(this.btnCheckBV)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAvailability.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAvailability)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblSearchCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.UI.RadButton btnCheckBV;
		private Telerik.WinControls.UI.RadGridView dgAvailability;
		private Telerik.WinControls.UI.RadLabel lblSearchCount;
	}
}
