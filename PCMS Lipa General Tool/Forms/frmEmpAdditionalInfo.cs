using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmEmpAdditionalInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly User user = new();
		public string txtNoProv;
		public string EmpName;
		public string accessLevel;


		public frmEmpAdditionalInfo()
		{
			InitializeComponent();
			FillManagementName();
			//mainProcess.CreateDbId(txtEmpID, Sql, @"PCMS-");
			txtEmpID.Enabled = false;
			//cmbThemeSelection.Enabled = false;

			// show the eye in password field
			this.txtRDWebPassword.UseSystemPasswordChar = true;
			this.txtLytecPassword.UseSystemPasswordChar = true;
			this.txtPCPassword.UseSystemPasswordChar = true;
			this.txtBVPassword.UseSystemPasswordChar = true;
			this.txtWorkEmailPass.UseSystemPasswordChar = true;
			RadButtonElement btnPCPass = new();
			RadButtonElement btnRDPass = new();
			RadButtonElement btnLyPass = new();
			RadButtonElement btnBVPass = new();
			RadButtonElement btnEMPass = new();
			btnPCPass.Click += BtnPCPass_Click;
			btnEMPass.Click += BtnEMPass_Click;
			btnBVPass.Click += BtnBVPass_Click;
			btnRDPass.Click += BtnRDPass_Click;
			btnLyPass.Click += BtnLyPass_Click;
			this.txtPCPassword.RightButtonItems.Add(btnPCPass);
			this.txtRDWebPassword.RightButtonItems.Add(btnRDPass);
			this.txtWorkEmailPass.RightButtonItems.Add(btnEMPass);
			this.txtLytecPassword.RightButtonItems.Add(btnLyPass);
			this.txtBVPassword.RightButtonItems.Add(btnBVPass);
			var font1 = ThemeResolutionService.GetCustomFont("Font Awesome 5 Free Regular");


			btnPCPass.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btnPCPass.BorderElement.Visibility = ElementVisibility.Collapsed;
			btnPCPass.CustomFont = font1.Name;
			btnPCPass.CustomFontSize = 5;
			btnPCPass.Text = "\ue052";

			btnRDPass.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btnRDPass.BorderElement.Visibility = ElementVisibility.Collapsed;
			btnRDPass.CustomFont = font1.Name;
			btnRDPass.CustomFontSize = 5;
			btnRDPass.Text = "\ue052";


			btnEMPass.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btnEMPass.BorderElement.Visibility = ElementVisibility.Collapsed;
			btnEMPass.CustomFont = font1.Name;
			btnEMPass.CustomFontSize = 5;
			btnEMPass.Text = "\ue052";


			btnLyPass.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btnLyPass.BorderElement.Visibility = ElementVisibility.Collapsed;
			btnLyPass.CustomFont = font1.Name;
			btnLyPass.CustomFontSize = 5;
			btnLyPass.Text = "\ue052";

			btnBVPass.ButtonFillElement.Visibility = ElementVisibility.Collapsed;
			btnBVPass.BorderElement.Visibility = ElementVisibility.Collapsed;
			btnBVPass.CustomFont = font1.Name;
			btnBVPass.CustomFontSize = 5;
			btnBVPass.Text = "\ue052";
		}

		private void BtnLyPass_Click(object sender, EventArgs e)
		{
			this.txtLytecPassword.UseSystemPasswordChar = !this.txtLytecPassword.UseSystemPasswordChar;
		}

		private void BtnRDPass_Click(object sender, EventArgs e)
		{
			this.txtRDWebPassword.UseSystemPasswordChar = !this.txtRDWebPassword.UseSystemPasswordChar;
		}

		private void BtnBVPass_Click(object sender, EventArgs e)
		{
			txtBVPassword.UseSystemPasswordChar = !txtBVPassword.UseSystemPasswordChar;
		}


		private void BtnEMPass_Click(object sender, EventArgs e)
		{
			txtWorkEmailPass.UseSystemPasswordChar = !txtWorkEmailPass.UseSystemPasswordChar;
		}

		private void BtnPCPass_Click(object sender, EventArgs e)
		{
			txtPCPassword.UseSystemPasswordChar = !txtPCPassword.UseSystemPasswordChar;
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			user.MoreEmployeeDatabase(txtEmpID, txtEmpName, txtRDWebUsername, txtRDWebPassword, txtLytecUsername, txtLytecPassword, txtWorkEmail, txtWorkEmailPass, txtDateofBirth, txtBVNo, txtBVUsername, txtBVPassword, txtPCName, txtPCUsername, txtPCPassword, cmbManagement, txtRemarks, cmbFirstTime, txtDCUsernaem, txtDCPassword, EmpName, cmbEmploymentStatus);
			//task.AddActivityLog("Updated User Information for\nEmployee No: " + txtEmpID.Text + "\nEmployee Name: " + txtEmpName, EmpName, "Updated User Information for Employee #: " + txtEmpID.Text, "UPDATED USER INFORMATION",  EmpName);
			//RadMessageBox.Show("Record Succesfully Updated", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			user.EmployeeDatabase("Delete", txtEmpID, txtEmpName, null, null, null, null, null, null, null, null, null, null, null, EmpName);
			Close();
		}

		private void frmEmpAdditionalInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void FillManagementName()
		{
			cmbManagement.Items.Clear();
			user.FillComboMgmt(cmbManagement, EmpName);
			
		}

		private void cmbManagement_PopupOpened(object sender, EventArgs e)
		{
			FillManagementName();
		}
	}
}
