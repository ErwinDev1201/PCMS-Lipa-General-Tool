namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmModifyNotes
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModifyNotes));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.cmbProviderList = new Telerik.WinControls.UI.RadDropDownList();
			this.txtNotes = new Telerik.WinControls.UI.RadTextBoxControl();
			this.btnDelete = new Telerik.WinControls.UI.RadButton();
			this.btnUpdateSave = new Telerik.WinControls.UI.RadButton();
			this.txtRemarks = new Telerik.WinControls.UI.RadTextBoxControl();
			this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
			this.txtPatientName = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
			this.txtChartNo = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.txtIntID = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNotes)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtChartNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.cmbProviderList);
			this.radPanel1.Controls.Add(this.txtNotes);
			this.radPanel1.Controls.Add(this.btnDelete);
			this.radPanel1.Controls.Add(this.btnUpdateSave);
			this.radPanel1.Controls.Add(this.txtRemarks);
			this.radPanel1.Controls.Add(this.radLabel6);
			this.radPanel1.Controls.Add(this.txtPatientName);
			this.radPanel1.Controls.Add(this.radLabel5);
			this.radPanel1.Controls.Add(this.txtChartNo);
			this.radPanel1.Controls.Add(this.radLabel4);
			this.radPanel1.Controls.Add(this.radLabel3);
			this.radPanel1.Controls.Add(this.txtIntID);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Location = new System.Drawing.Point(14, 18);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(643, 363);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// cmbProviderList
			// 
			this.cmbProviderList.DropDownAnimationEnabled = true;
			this.cmbProviderList.Location = new System.Drawing.Point(125, 49);
			this.cmbProviderList.Name = "cmbProviderList";
			this.cmbProviderList.Size = new System.Drawing.Size(227, 24);
			this.cmbProviderList.TabIndex = 10;
			this.cmbProviderList.Text = "Select the Provider";
			this.cmbProviderList.ThemeName = "Crystal";
			// 
			// txtNotes
			// 
			this.txtNotes.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNotes.Location = new System.Drawing.Point(125, 137);
			this.txtNotes.Multiline = true;
			this.txtNotes.Name = "txtNotes";
			this.txtNotes.Size = new System.Drawing.Size(506, 110);
			this.txtNotes.TabIndex = 9;
			this.txtNotes.ThemeName = "Crystal";
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(505, 80);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(126, 51);
			this.btnDelete.TabIndex = 8;
			this.btnDelete.Text = "Delete";
			this.btnDelete.ThemeName = "Crystal";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnUpdateSave
			// 
			this.btnUpdateSave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpdateSave.Location = new System.Drawing.Point(505, 22);
			this.btnUpdateSave.Name = "btnUpdateSave";
			this.btnUpdateSave.Size = new System.Drawing.Size(126, 51);
			this.btnUpdateSave.TabIndex = 7;
			this.btnUpdateSave.Text = "Update/Save";
			this.btnUpdateSave.ThemeName = "Crystal";
			this.btnUpdateSave.Click += new System.EventHandler(this.btnUpdateSave_Click);
			// 
			// txtRemarks
			// 
			this.txtRemarks.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(125, 253);
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(506, 91);
			this.txtRemarks.TabIndex = 6;
			this.txtRemarks.ThemeName = "Crystal";
			// 
			// radLabel6
			// 
			this.radLabel6.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel6.Location = new System.Drawing.Point(20, 264);
			this.radLabel6.Name = "radLabel6";
			this.radLabel6.Size = new System.Drawing.Size(66, 19);
			this.radLabel6.TabIndex = 3;
			this.radLabel6.Text = "Remarks: ";
			this.radLabel6.ThemeName = "Crystal";
			// 
			// txtPatientName
			// 
			this.txtPatientName.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPatientName.Location = new System.Drawing.Point(125, 108);
			this.txtPatientName.Name = "txtPatientName";
			this.txtPatientName.Size = new System.Drawing.Size(237, 23);
			this.txtPatientName.TabIndex = 3;
			this.txtPatientName.ThemeName = "Crystal";
			// 
			// radLabel5
			// 
			this.radLabel5.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel5.Location = new System.Drawing.Point(20, 176);
			this.radLabel5.Name = "radLabel5";
			this.radLabel5.Size = new System.Drawing.Size(42, 19);
			this.radLabel5.TabIndex = 2;
			this.radLabel5.Text = "Notes";
			this.radLabel5.ThemeName = "Crystal";
			// 
			// txtChartNo
			// 
			this.txtChartNo.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtChartNo.Location = new System.Drawing.Point(125, 79);
			this.txtChartNo.Name = "txtChartNo";
			this.txtChartNo.Size = new System.Drawing.Size(237, 23);
			this.txtChartNo.TabIndex = 5;
			this.txtChartNo.ThemeName = "Crystal";
			this.txtChartNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChartNo_KeyPress);
			// 
			// radLabel4
			// 
			this.radLabel4.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel4.Location = new System.Drawing.Point(20, 109);
			this.radLabel4.Name = "radLabel4";
			this.radLabel4.Size = new System.Drawing.Size(95, 19);
			this.radLabel4.TabIndex = 2;
			this.radLabel4.Text = "Patient Name: ";
			this.radLabel4.ThemeName = "Crystal";
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(20, 80);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(66, 19);
			this.radLabel3.TabIndex = 4;
			this.radLabel3.Text = "Chart No: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// txtIntID
			// 
			this.txtIntID.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIntID.Location = new System.Drawing.Point(125, 20);
			this.txtIntID.Name = "txtIntID";
			this.txtIntID.Size = new System.Drawing.Size(60, 23);
			this.txtIntID.TabIndex = 1;
			this.txtIntID.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(20, 52);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(99, 19);
			this.radLabel2.TabIndex = 2;
			this.radLabel2.Text = "Provider Name:";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(20, 24);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(58, 19);
			this.radLabel1.TabIndex = 0;
			this.radLabel1.Text = "Note ID: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// frmModifyNotes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 397);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmModifyNotes";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Diagnosis Information";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModifyDiagnosis_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbProviderList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtNotes)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPatientName)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtChartNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Telerik.WinControls.UI.RadPanel radPanel1;
		private Telerik.WinControls.Themes.CrystalTheme crystalTheme1;
		private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
		public Telerik.WinControls.UI.RadTextBox txtIntID;
		public Telerik.WinControls.UI.RadLabel radLabel2;
		public Telerik.WinControls.UI.RadLabel radLabel1;
		public Telerik.WinControls.UI.RadTextBox txtPatientName;
		public Telerik.WinControls.UI.RadLabel radLabel5;
		public Telerik.WinControls.UI.RadTextBox txtChartNo;
		public Telerik.WinControls.UI.RadLabel radLabel4;
		public Telerik.WinControls.UI.RadLabel radLabel3;
		public Telerik.WinControls.UI.RadLabel radLabel6;
		public Telerik.WinControls.UI.RadTextBoxControl txtRemarks;
		public Telerik.WinControls.UI.RadButton btnDelete;
		public Telerik.WinControls.UI.RadButton btnUpdateSave;
		public Telerik.WinControls.UI.RadTextBoxControl txtNotes;
		public Telerik.WinControls.UI.RadDropDownList cmbProviderList;
	}
}
