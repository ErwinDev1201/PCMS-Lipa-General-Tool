namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmAssignProvider
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssignProvider));
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.btnDelete = new Telerik.WinControls.UI.RadButton();
			this.cmbProviderName = new Telerik.WinControls.UI.RadDropDownList();
			this.cmbEmployeeName = new Telerik.WinControls.UI.RadDropDownList();
			this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
			this.txtRemarks = new Telerik.WinControls.UI.RadTextBoxControl();
			this.btnSave = new Telerik.WinControls.UI.RadButton();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.txtIntID = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.dgAssignedProvider = new Telerik.WinControls.UI.RadGridView();
			this.lblCountResult = new Telerik.WinControls.UI.RadLabel();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAssignedProvider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAssignedProvider.MasterTemplate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCountResult)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.btnDelete);
			this.radPanel1.Controls.Add(this.cmbProviderName);
			this.radPanel1.Controls.Add(this.cmbEmployeeName);
			this.radPanel1.Controls.Add(this.radLabel5);
			this.radPanel1.Controls.Add(this.txtRemarks);
			this.radPanel1.Controls.Add(this.btnSave);
			this.radPanel1.Controls.Add(this.radLabel3);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.txtIntID);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Location = new System.Drawing.Point(14, 18);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(378, 514);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(210, 440);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(140, 45);
			this.btnDelete.TabIndex = 22;
			this.btnDelete.Text = "&Delete";
			this.btnDelete.ThemeName = "Crystal";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// cmbProviderName
			// 
			this.cmbProviderName.DropDownAnimationEnabled = true;
			this.cmbProviderName.Location = new System.Drawing.Point(130, 99);
			this.cmbProviderName.Name = "cmbProviderName";
			this.cmbProviderName.Size = new System.Drawing.Size(220, 24);
			this.cmbProviderName.TabIndex = 21;
			this.cmbProviderName.Text = "Select Provider ";
			this.cmbProviderName.ThemeName = "Crystal";
			// 
			// cmbEmployeeName
			// 
			this.cmbEmployeeName.DropDownAnimationEnabled = true;
			this.cmbEmployeeName.Location = new System.Drawing.Point(130, 60);
			this.cmbEmployeeName.Name = "cmbEmployeeName";
			this.cmbEmployeeName.Size = new System.Drawing.Size(220, 24);
			this.cmbEmployeeName.TabIndex = 20;
			this.cmbEmployeeName.Text = "Select Employee";
			this.cmbEmployeeName.ThemeName = "Crystal";
			// 
			// radLabel5
			// 
			this.radLabel5.Location = new System.Drawing.Point(20, 272);
			this.radLabel5.Name = "radLabel5";
			this.radLabel5.Size = new System.Drawing.Size(61, 20);
			this.radLabel5.TabIndex = 19;
			this.radLabel5.Text = "Remarks";
			this.radLabel5.ThemeName = "Crystal";
			// 
			// txtRemarks
			// 
			this.txtRemarks.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(92, 146);
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(258, 274);
			this.txtRemarks.TabIndex = 18;
			// 
			// btnSave
			// 
			this.btnSave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.Location = new System.Drawing.Point(20, 440);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(140, 45);
			this.btnSave.TabIndex = 17;
			this.btnSave.Text = "Save";
			this.btnSave.ThemeName = "Crystal";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(20, 102);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(99, 19);
			this.radLabel3.TabIndex = 15;
			this.radLabel3.Text = "Provider Name:";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(20, 65);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(107, 19);
			this.radLabel2.TabIndex = 13;
			this.radLabel2.Text = "Employee Name:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// txtIntID
			// 
			this.txtIntID.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIntID.Location = new System.Drawing.Point(130, 28);
			this.txtIntID.Name = "txtIntID";
			this.txtIntID.Size = new System.Drawing.Size(100, 23);
			this.txtIntID.TabIndex = 12;
			this.txtIntID.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(20, 28);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(70, 19);
			this.radLabel1.TabIndex = 11;
			this.radLabel1.Text = "Assign ID: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// dgAssignedProvider
			// 
			this.dgAssignedProvider.AllowDrop = true;
			this.dgAssignedProvider.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgAssignedProvider.Location = new System.Drawing.Point(398, 59);
			// 
			// 
			// 
			this.dgAssignedProvider.MasterTemplate.AllowAddNewRow = false;
			this.dgAssignedProvider.MasterTemplate.AllowCellContextMenu = false;
			this.dgAssignedProvider.MasterTemplate.AllowColumnChooser = false;
			this.dgAssignedProvider.MasterTemplate.AllowColumnHeaderContextMenu = false;
			this.dgAssignedProvider.MasterTemplate.AllowColumnReorder = false;
			this.dgAssignedProvider.MasterTemplate.AllowColumnResize = false;
			this.dgAssignedProvider.MasterTemplate.AllowDeleteRow = false;
			this.dgAssignedProvider.MasterTemplate.AllowDragToGroup = false;
			this.dgAssignedProvider.MasterTemplate.AllowEditRow = false;
			this.dgAssignedProvider.MasterTemplate.AllowRowHeaderContextMenu = false;
			this.dgAssignedProvider.MasterTemplate.ViewDefinition = tableViewDefinition1;
			this.dgAssignedProvider.Name = "dgAssignedProvider";
			this.dgAssignedProvider.Size = new System.Drawing.Size(462, 448);
			this.dgAssignedProvider.TabIndex = 1;
			this.dgAssignedProvider.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgAssignedProvider_MouseDoubleClick);
			// 
			// lblCountResult
			// 
			this.lblCountResult.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCountResult.Location = new System.Drawing.Point(398, 513);
			this.lblCountResult.Name = "lblCountResult";
			this.lblCountResult.Size = new System.Drawing.Size(66, 19);
			this.lblCountResult.TabIndex = 2;
			this.lblCountResult.Text = "radLabel4";
			// 
			// frmAssignProvider
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(872, 550);
			this.Controls.Add(this.lblCountResult);
			this.Controls.Add(this.dgAssignedProvider);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAssignProvider";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Email Information";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModifyEmailformat_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cmbEmployeeName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAssignedProvider.MasterTemplate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgAssignedProvider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lblCountResult)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		private Telerik.WinControls.UI.RadPanel radPanel1;
		public Telerik.WinControls.UI.RadLabel radLabel5;
		public Telerik.WinControls.UI.RadTextBoxControl txtRemarks;
		public Telerik.WinControls.UI.RadButton btnSave;
		public Telerik.WinControls.UI.RadLabel radLabel3;
		public Telerik.WinControls.UI.RadLabel radLabel2;
		public Telerik.WinControls.UI.RadTextBox txtIntID;
		public Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadDropDownList cmbProviderName;
		private Telerik.WinControls.UI.RadDropDownList cmbEmployeeName;
		public Telerik.WinControls.UI.RadButton btnDelete;
		private Telerik.WinControls.UI.RadGridView dgAssignedProvider;
		private Telerik.WinControls.UI.RadLabel lblCountResult;
	}
}
