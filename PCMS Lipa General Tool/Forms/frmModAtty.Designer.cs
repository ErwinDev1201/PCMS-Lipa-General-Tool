namespace PCMS_Lipa_General_Tool.Forms
{
	partial class frmModAtty
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModAtty));
			this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
			this.cmbAttyType = new Telerik.WinControls.UI.RadDropDownList();
			this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
			this.btnDelete = new Telerik.WinControls.UI.RadButton();
			this.btnUpdateSave = new Telerik.WinControls.UI.RadButton();
			this.txtRemarks = new Telerik.WinControls.UI.RadTextBoxControl();
			this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
			this.txtFaxNo = new Telerik.WinControls.UI.RadTextBox();
			this.txtEmailAdd = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
			this.txtPhoneNo = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
			this.txtAttyName = new Telerik.WinControls.UI.RadTextBox();
			this.txtIntID = new Telerik.WinControls.UI.RadTextBox();
			this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
			this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
			this.crystalTheme1 = new Telerik.WinControls.Themes.CrystalTheme();
			this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
			this.radPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbAttyType)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateSave)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFaxNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEmailAdd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAttyName)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtIntID)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// radPanel1
			// 
			this.radPanel1.Controls.Add(this.cmbAttyType);
			this.radPanel1.Controls.Add(this.radLabel7);
			this.radPanel1.Controls.Add(this.btnDelete);
			this.radPanel1.Controls.Add(this.btnUpdateSave);
			this.radPanel1.Controls.Add(this.txtRemarks);
			this.radPanel1.Controls.Add(this.radLabel6);
			this.radPanel1.Controls.Add(this.txtFaxNo);
			this.radPanel1.Controls.Add(this.txtEmailAdd);
			this.radPanel1.Controls.Add(this.radLabel5);
			this.radPanel1.Controls.Add(this.txtPhoneNo);
			this.radPanel1.Controls.Add(this.radLabel4);
			this.radPanel1.Controls.Add(this.radLabel3);
			this.radPanel1.Controls.Add(this.txtAttyName);
			this.radPanel1.Controls.Add(this.txtIntID);
			this.radPanel1.Controls.Add(this.radLabel2);
			this.radPanel1.Controls.Add(this.radLabel1);
			this.radPanel1.Location = new System.Drawing.Point(13, 18);
			this.radPanel1.Name = "radPanel1";
			this.radPanel1.Size = new System.Drawing.Size(500, 334);
			this.radPanel1.TabIndex = 0;
			this.radPanel1.ThemeName = "Crystal";
			// 
			// cmbAttyType
			// 
			this.cmbAttyType.DropDownAnimationEnabled = true;
			this.cmbAttyType.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			radListDataItem1.Text = "Applicant";
			radListDataItem2.Text = "Defense";
			this.cmbAttyType.Items.Add(radListDataItem1);
			this.cmbAttyType.Items.Add(radListDataItem2);
			this.cmbAttyType.Location = new System.Drawing.Point(127, 52);
			this.cmbAttyType.Name = "cmbAttyType";
			this.cmbAttyType.Size = new System.Drawing.Size(224, 24);
			this.cmbAttyType.TabIndex = 10;
			this.cmbAttyType.ThemeName = "Crystal";
			// 
			// radLabel7
			// 
			this.radLabel7.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel7.Location = new System.Drawing.Point(19, 52);
			this.radLabel7.Name = "radLabel7";
			this.radLabel7.Size = new System.Drawing.Size(73, 19);
			this.radLabel7.TabIndex = 9;
			this.radLabel7.Text = "Atty Type:  ";
			this.radLabel7.ThemeName = "Crystal";
			// 
			// btnDelete
			// 
			this.btnDelete.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDelete.Location = new System.Drawing.Point(372, 96);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(110, 38);
			this.btnDelete.TabIndex = 8;
			this.btnDelete.Text = "Delete";
			this.btnDelete.ThemeName = "Crystal";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnUpdateSave
			// 
			this.btnUpdateSave.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpdateSave.Location = new System.Drawing.Point(372, 52);
			this.btnUpdateSave.Name = "btnUpdateSave";
			this.btnUpdateSave.Size = new System.Drawing.Size(110, 38);
			this.btnUpdateSave.TabIndex = 7;
			this.btnUpdateSave.Text = "Update/Save";
			this.btnUpdateSave.ThemeName = "Crystal";
			this.btnUpdateSave.Click += new System.EventHandler(this.btnUpdateSave_Click);
			// 
			// txtRemarks
			// 
			this.txtRemarks.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(127, 198);
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(224, 111);
			this.txtRemarks.TabIndex = 6;
			this.txtRemarks.ThemeName = "Crystal";
			// 
			// radLabel6
			// 
			this.radLabel6.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel6.Location = new System.Drawing.Point(19, 233);
			this.radLabel6.Name = "radLabel6";
			this.radLabel6.Size = new System.Drawing.Size(66, 19);
			this.radLabel6.TabIndex = 3;
			this.radLabel6.Text = "Remarks: ";
			this.radLabel6.ThemeName = "Crystal";
			// 
			// txtFaxNo
			// 
			this.txtFaxNo.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFaxNo.Location = new System.Drawing.Point(127, 169);
			this.txtFaxNo.Name = "txtFaxNo";
			this.txtFaxNo.Size = new System.Drawing.Size(224, 23);
			this.txtFaxNo.TabIndex = 3;
			this.txtFaxNo.ThemeName = "Crystal";
			// 
			// txtEmailAdd
			// 
			this.txtEmailAdd.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtEmailAdd.Location = new System.Drawing.Point(127, 140);
			this.txtEmailAdd.Name = "txtEmailAdd";
			this.txtEmailAdd.Size = new System.Drawing.Size(224, 23);
			this.txtEmailAdd.TabIndex = 3;
			this.txtEmailAdd.ThemeName = "Crystal";
			// 
			// radLabel5
			// 
			this.radLabel5.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel5.Location = new System.Drawing.Point(19, 173);
			this.radLabel5.Name = "radLabel5";
			this.radLabel5.Size = new System.Drawing.Size(55, 19);
			this.radLabel5.TabIndex = 2;
			this.radLabel5.Text = "Fax No: ";
			this.radLabel5.ThemeName = "Crystal";
			// 
			// txtPhoneNo
			// 
			this.txtPhoneNo.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPhoneNo.Location = new System.Drawing.Point(127, 111);
			this.txtPhoneNo.Name = "txtPhoneNo";
			this.txtPhoneNo.Size = new System.Drawing.Size(224, 23);
			this.txtPhoneNo.TabIndex = 5;
			this.txtPhoneNo.ThemeName = "Crystal";
			// 
			// radLabel4
			// 
			this.radLabel4.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel4.Location = new System.Drawing.Point(19, 144);
			this.radLabel4.Name = "radLabel4";
			this.radLabel4.Size = new System.Drawing.Size(99, 19);
			this.radLabel4.TabIndex = 2;
			this.radLabel4.Text = "Email Address: ";
			this.radLabel4.ThemeName = "Crystal";
			// 
			// radLabel3
			// 
			this.radLabel3.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel3.Location = new System.Drawing.Point(19, 115);
			this.radLabel3.Name = "radLabel3";
			this.radLabel3.Size = new System.Drawing.Size(72, 19);
			this.radLabel3.TabIndex = 4;
			this.radLabel3.Text = "Phone No: ";
			this.radLabel3.ThemeName = "Crystal";
			// 
			// txtAttyName
			// 
			this.txtAttyName.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAttyName.Location = new System.Drawing.Point(127, 82);
			this.txtAttyName.Name = "txtAttyName";
			this.txtAttyName.Size = new System.Drawing.Size(224, 23);
			this.txtAttyName.TabIndex = 3;
			this.txtAttyName.ThemeName = "Crystal";
			// 
			// txtIntID
			// 
			this.txtIntID.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtIntID.Location = new System.Drawing.Point(127, 23);
			this.txtIntID.Name = "txtIntID";
			this.txtIntID.Size = new System.Drawing.Size(60, 23);
			this.txtIntID.TabIndex = 1;
			this.txtIntID.ThemeName = "Crystal";
			// 
			// radLabel2
			// 
			this.radLabel2.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel2.Location = new System.Drawing.Point(19, 82);
			this.radLabel2.Name = "radLabel2";
			this.radLabel2.Size = new System.Drawing.Size(79, 19);
			this.radLabel2.TabIndex = 2;
			this.radLabel2.Text = "Atty Name:  ";
			this.radLabel2.ThemeName = "Crystal";
			// 
			// radLabel1
			// 
			this.radLabel1.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radLabel1.Location = new System.Drawing.Point(19, 27);
			this.radLabel1.Name = "radLabel1";
			this.radLabel1.Size = new System.Drawing.Size(74, 19);
			this.radLabel1.TabIndex = 0;
			this.radLabel1.Text = "Internal ID: ";
			this.radLabel1.ThemeName = "Crystal";
			// 
			// frmModAtty
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(525, 367);
			this.Controls.Add(this.radPanel1);
			this.Font = new System.Drawing.Font("Roboto", 9.749998F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmModAtty";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmDiagnosisinfo";
			this.ThemeName = "Crystal";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModifyAtty_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
			this.radPanel1.ResumeLayout(false);
			this.radPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.cmbAttyType)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.btnUpdateSave)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtRemarks)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtFaxNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtEmailAdd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtPhoneNo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtAttyName)).EndInit();
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
		private Telerik.WinControls.UI.RadLabel radLabel2;
		private Telerik.WinControls.UI.RadLabel radLabel1;
		private Telerik.WinControls.UI.RadLabel radLabel5;
		private Telerik.WinControls.UI.RadLabel radLabel4;
		private Telerik.WinControls.UI.RadLabel radLabel3;
		private Telerik.WinControls.UI.RadLabel radLabel6;
		public Telerik.WinControls.UI.RadTextBox txtAttyName;
		public Telerik.WinControls.UI.RadTextBox txtIntID;
		public Telerik.WinControls.UI.RadTextBox txtFaxNo;
		public Telerik.WinControls.UI.RadTextBox txtEmailAdd;
		public Telerik.WinControls.UI.RadTextBox txtPhoneNo;
		public Telerik.WinControls.UI.RadTextBoxControl txtRemarks;
		public Telerik.WinControls.UI.RadButton btnDelete;
		public Telerik.WinControls.UI.RadButton btnUpdateSave;
		private Telerik.WinControls.UI.RadLabel radLabel7;
		public Telerik.WinControls.UI.RadDropDownList cmbAttyType;
	}
}
