﻿using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	

	public partial class frmUserInformation : RadForm
	{
		private readonly User user = new();
		private static readonly FEWinForm fe = new();
		private static readonly Notification error = new();
		public string txtNoProv;
		public string EmpName;
		public string accessLevel;

		private Color _originalBackColor;

		public frmUserInformation()
		{
			InitializeComponent();
			//FillManagementName();
			_originalBackColor = GetThemeBackColor(txtWorkEmail);
			//mainProcess.CreateDbId(txtEmpID, Sql, @"PCMS-");
			txtEmpID.Enabled = false;
			UIInitializattion();
			//cmbThemeSelection.Enabled = false;


		}

		private void UIInitializattion()
		{
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

			// show null helper
			txtUsername.NullText = "Add Username here";
			txtEmpName.NullText = "Add Employee Name here";
			txtWorkEmailMain.NullText = "Add Work Email here";
		}

		private void EnableInput()
		{
			txtEmpID.Enabled = true;
			txtUsername.Enabled = true;
			txtEmpName.Enabled = true;
			txtWorkEmailMain.Enabled = true;
			txtRDWebUsername.Enabled = true;
			txtRDWebPassword.Enabled = true;
			txtLytecUsername.Enabled = true;
			txtLytecPassword.Enabled = true;
			txtBVNo.Enabled = true;
			txtBVUsername.Enabled = true;
			txtBVPassword.Enabled = true;
			txtPCName.Enabled = true;
			txtPCUsername.Enabled = true;
			txtPCPassword.Enabled = true;
			txtWorkEmail.Enabled = true;
			txtWorkEmailPass.Enabled = true;
			txtDCUsernaem.Enabled = true;
			txtDCPassword.Enabled = true;
			txtRemarks.Enabled = true;
			cmbUserAccess.Enabled = true;
			cmbUserDept.Enabled = true;
			cmbPosition.Enabled = true;
			cmbUserStatus.Enabled = true;
			cmbOffice.Enabled = true;
			cmbManagement.Enabled = true;
			cmbEmploymentStatus.Enabled = true;
			cmbFirstTime.Enabled = true;
			dtpDateofBirth.Enabled = true;
			btnUpdate.Visible = true;
			btnDelete.Visible = true;
		}

		private void DisableInput()
		{
			txtEmpID.Enabled = false;
			txtUsername.Enabled = false;
			txtEmpName.Enabled = false;
			txtWorkEmailMain.Enabled = false;
			txtRDWebUsername.Enabled = false;
			txtRDWebPassword.Enabled = false;
			txtLytecUsername.Enabled = false;
			txtLytecPassword.Enabled = false;
			txtBVNo.Enabled = false;
			txtBVUsername.Enabled = false;
			txtBVPassword.Enabled = false;
			txtPCName.Enabled = false;
			txtPCUsername.Enabled = false;
			txtPCPassword.Enabled = false;
			txtWorkEmail.Enabled = false;
			txtWorkEmailPass.Enabled = false;
			txtDCUsernaem.Enabled = false;
			txtDCPassword.Enabled = false;
			txtRemarks.Enabled = false;
			cmbUserAccess.Enabled = false;
			cmbUserDept.Enabled = false;
			cmbPosition.Enabled = false;
			cmbUserStatus.Enabled = false;
			cmbOffice.Enabled = false;
			cmbManagement.Enabled = false;
			cmbEmploymentStatus.Enabled = false;
			cmbFirstTime.Enabled = false;
			dtpDateofBirth.Enabled = false;
			btnUpdate.Visible = false;
			btnDelete.Visible = false;
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
			if (txtUsername.Text != ""
				&& txtEmpName.Text != ""
				&& cmbUserDept.Text != ""
				&& cmbUserAccess.Text != ""
				&& cmbPosition.Text != ""
				&& cmbUserStatus.Text != ""
				&& cmbOffice.Text != "")
			{
				if (btnUpdate.Text == "Update")
				{
					DialogResult result = RadMessageBox.Show(
					"Are you sure you want to update the user information?",
					"Confirm User Update",
					MessageBoxButtons.YesNo,
					RadMessageIcon.Question);
					if (result != DialogResult.Yes)
						return; // Exit if the user cancels
				}
				string operation = btnUpdate.Text == "Save" ? "Create" : "Update";

				if (!DateTime.TryParse(dtpDateofBirth.Text, out DateTime dateOfBirth))
				{
					MessageBox.Show("Invalid date format. Please enter a valid date.");
					return;
				}

				DisableInput();

				bool isSuccess = user.EmployeeDatabaseAllInfo(
					operation,
					txtEmpID.Text, txtEmpName.Text, txtUsername.Text, cmbUserAccess.Text,
					cmbPosition.Text, cmbUserDept.Text, cmbUserStatus.Text, txtWorkEmail.Text,
					txtBVNo.Text, cmbOffice.Text, cmbFirstTime.Text, "Crystal",
					txtRDWebUsername.Text, txtRDWebPassword.Text, txtLytecUsername.Text,
					txtLytecPassword.Text, txtWorkEmailPass.Text, dateOfBirth,
					txtBVUsername.Text, txtBVPassword.Text, txtPCName.Text, txtPCUsername.Text,
					txtPCPassword.Text, cmbManagement.Text, txtRemarks.Text, txtDCUsernaem.Text,
					txtDCPassword.Text, cmbEmploymentStatus.Text, EmpName, out string message
				);
				EnableInput();

				fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");
				Close();
			}
			else
			{
				RadMessageBox.Show("Below fields are required to save Information \n" +
						"Employee Name \n" +
						"Username \n" +
						"User Department \n" +
						"User Access \n" +
						"User Position \n" +
						"User Status \n" +
						"Office", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
			}

			
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DialogResult result = RadMessageBox.Show(
				"Are you sure you want to delete the user information?",
				"Confirm User Delete",
				MessageBoxButtons.YesNo,
				RadMessageIcon.Question);

			if (result != DialogResult.Yes)
				return;
			DisableInput();
			bool isSuccess = user.EmployeeDatabaseAllInfo(
				"Delete",
				txtEmpID.Text,
				txtEmpName.Text,
				null, null, null, null, null, txtWorkEmail.Text, null, null, null, null, null, null,
				null, null, null, DateTime.MinValue, null, null, null, null, null, null,
				null, null, null, null, EmpName, out string message);
			fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");
			EnableInput();
			Close();
		}

		private void frmEmpAdditionalInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		public void SetFieldsReadOnly()
		{
			// First Group
			txtEmpID.ReadOnly = true;
			txtUsername.ReadOnly = true;
			txtEmpName.ReadOnly = true;
			txtWorkEmailMain.ReadOnly = true;

			// Second Group
			txtRDWebUsername.ReadOnly = true;
			txtRDWebPassword.ReadOnly = true;
			txtLytecUsername.ReadOnly = true;
			txtLytecPassword.ReadOnly = true;

			// Third Group
			txtBVNo.ReadOnly = true;
			txtBVUsername.ReadOnly = true;
			txtBVPassword.ReadOnly = true;
			txtPCName.ReadOnly = true;
			txtPCUsername.ReadOnly = true;
			txtPCPassword.ReadOnly = true;
			txtWorkEmail.ReadOnly = true;
			txtWorkEmailPass.ReadOnly = true;
			txtDCUsernaem.ReadOnly = true;
			txtDCPassword.ReadOnly = true;
			txtRemarks.IsReadOnly = true;

			// Set ComboBoxes as disabled (read-only alternative)
			cmbUserAccess.Enabled = false;
			cmbUserDept.Enabled = false;
			cmbPosition.Enabled = false;
			cmbUserStatus.Enabled = false;
			cmbOffice.Enabled = false;
			cmbManagement.Enabled = false;
			cmbEmploymentStatus.Enabled = false;
			cmbFirstTime.Enabled = false;

			// Set DateTimePicker as disabled (read-only alternative)
			dtpDateofBirth.Enabled = false;
			//btnUpdate.Visible = false;
			//btnDelete.Visible = false;
		}

		public void DisableItem()
		{
			// First Group
			txtEmpID.Enabled = false;
			txtUsername.Enabled = false;
			txtEmpName.Enabled = false;
			txtWorkEmailMain.Enabled = false;

			// Second Group
			txtRDWebUsername.Enabled = false;
			txtRDWebPassword.Enabled = false;
			txtLytecUsername.Enabled = false;
			txtLytecPassword.Enabled = false;

			// Third Group
			txtBVNo.Enabled = false;
			txtBVUsername.Enabled = false;
			txtBVPassword.Enabled = false;
			txtPCName.Enabled = false;
			txtPCUsername.Enabled = false;
			txtPCPassword.Enabled = false;
			txtWorkEmail.Enabled = false;
			txtWorkEmailPass.Enabled = false;
			txtDCUsernaem.Enabled = false;
			txtDCPassword.Enabled = false;
			txtRemarks.Enabled = false;

			// Set ComboBoxes as disabled (read-only alternative)
			cmbUserAccess.Enabled = false;
			cmbUserDept.Enabled = false;
			cmbPosition.Enabled = false;
			cmbUserStatus.Enabled = false;
			cmbOffice.Enabled = false;
			cmbManagement.Enabled = false;
			cmbEmploymentStatus.Enabled = false;
			cmbFirstTime.Enabled = false;

			// Set DateTimePicker as disabled (read-only alternative)
			dtpDateofBirth.Enabled = false;
			//btnUpdate.Visible = false;
			//btnDelete.Visible = false;
		}

		public void EnableItem()
		{
			// First Group
			txtEmpID.Enabled = true;
			txtUsername.Enabled = true;
			txtEmpName.Enabled = true;
			txtWorkEmailMain.Enabled = true;

			// Second Group
			txtRDWebUsername.Enabled = true;
			txtRDWebPassword.Enabled = true;
			txtLytecUsername.Enabled = true;
			txtLytecPassword.Enabled = true;

			// Third Group
			txtBVNo.Enabled = true;
			txtBVUsername.Enabled = true;
			txtBVPassword.Enabled = true;
			txtPCName.Enabled = true;
			txtPCUsername.Enabled = true;
			txtPCPassword.Enabled = true;
			txtWorkEmail.Enabled = true;
			txtWorkEmailPass.Enabled = true;
			txtDCUsernaem.Enabled = true;
			txtDCPassword.Enabled = true;
			txtRemarks.Enabled = true;

			// Set ComboBoxes as enable (read-only alternative)
			cmbUserAccess.Enabled = true;
			cmbUserDept.Enabled = true;
			cmbPosition.Enabled = true;
			cmbUserStatus.Enabled = true;
			cmbOffice.Enabled = true;
			cmbManagement.Enabled = true;
			cmbEmploymentStatus.Enabled = true;
			cmbFirstTime.Enabled = true;

			// Set DateTimePicker as enable (read-only alternative)
			dtpDateofBirth.Enabled = true;
			//btnUpdate.Visible = false;
			//btnDelete.Visible = false;
		}

		private void FillManagementName()
		{
			cmbManagement.Items.Clear();
			List<string> items = user.GetManagementList(EmpName);
			cmbManagement.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbManagement.Items.Add(item);
			}

		}

		private void cmbManagement_PopupOpened(object sender, EventArgs e)
		{
			FillManagementName();
		}

		public void DefaultItem(string action)
		{
			if (action == "Create")
			{
				btnUpdate.Text = "Save";
				txtRDWebUsername.Text = @"hsn-pcms\";
				cmbUserStatus.Text = "Active";
				cmbUserAccess.Items.Add("Administrator");
				cmbUserAccess.Items.Add("Management");
				cmbUserAccess.Items.Add("Power User");
				cmbUserAccess.Items.Add("User");
				cmbUserAccess.Items.Add("Programmer");
				cmbUserAccess.SelectedIndex = 3;
				lblResult.Visible = false;
			}
			else
			{
				btnUpdate.Text = "Update";
			}
			
		}
		private void cmbUserAccess_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			LoadDropdownValues();
		}

		private void LoadDropdownValues()
		{
			try
			{
				if (string.IsNullOrWhiteSpace(cmbUserAccess.Text))
				{
					RadMessageBox.Show("Please select user access first.", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
					cmbUserDept.Text = "";
					cmbPosition.Text = "";
					return;
				}

				// Preserve current selections before clearing
				string selectedDepartment = cmbUserDept.SelectedItem?.Text ?? cmbUserDept.Text;
				string selectedPosition = cmbPosition.SelectedItem?.Text ?? cmbPosition.Text;

				string userAccess = cmbUserAccess.SelectedItem != null ? cmbUserAccess.SelectedItem.Text : cmbUserAccess.Text;

				cmbUserDept.Items.Clear();
				cmbPosition.Items.Clear();

				switch (userAccess)
				{
					case "Administrator":
						cmbUserDept.Items.AddRange(["All Department", "IT"]);
						cmbPosition.Items.AddRange(["IT Manager", "Operations Manager", "Supervisor"]);
						break;

					case "Management":
						cmbUserDept.Items.Add("All Department");
						cmbPosition.Items.AddRange(["Operations Manager", "Supervisor"]);
						break;

					case "Power User":
					case "User":
						cmbUserDept.Items.AddRange(["Workers Comp", "Private"]);
						cmbPosition.Items.AddRange(["Collector", "Back Office"]);
						break;

					case "Programmer":
						cmbUserDept.Items.AddRange(["All Department", "IT"]);
						cmbPosition.Items.Add("Programmer");
						break;

					default:
						RadMessageBox.Show("Invalid selection. Please choose a valid user access.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
						return;
				}

				// Restore previous selections if they still exist
				var matchedDepartment = cmbUserDept.Items.FirstOrDefault(item => item.Text == selectedDepartment);
				if (matchedDepartment != null)
				{
					cmbUserDept.SelectedItem = matchedDepartment;
				}
				else
				{
					cmbUserDept.SelectedIndex = 0; // Select the first available option
				}

				var matchedPosition = cmbPosition.Items.FirstOrDefault(item => item.Text == selectedPosition);
				if (matchedPosition != null)
				{
					cmbPosition.SelectedItem = matchedPosition;
				}
				else
				{
					cmbPosition.SelectedIndex = 0; // Select the first available option
				}
			}
			catch (Exception ex)
			{
				error.LogError("LoadDropdownValues", EmpName, "frmUserInformation", null, ex);
			}
		}



		private void txtUsername_TextChanged(object sender, EventArgs e)
		{
			if (txtUsername.Text.Length > 2)
			{
				//lblalert.Text = "";
				if (txtUsername.Text.Contains(";") || txtUsername.Text.Contains("--") || txtUsername.Text.Contains("/*") || txtUsername.Text.Contains("xp_"))
				{
					RadMessageBox.Show("Invalid Input", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				else
				{
					string resultMessage = user.CheckIfExistinDB(txtUsername.Text, "UserMgmt", "Create");
					if (!string.IsNullOrEmpty(resultMessage))
					{
						lblResult.Text = resultMessage;
						lblResult.Visible = true;
					}
					else
					{
						lblResult.Visible = false;
					}

				}
			}
		}

		private void btnResetPassword_Click(object sender, EventArgs e)
		{
			DialogResult result = RadMessageBox.Show(
				"Are you sure you want to reset the password?",
				"Confirm Password Reset",
				MessageBoxButtons.YesNo,
				RadMessageIcon.Question);

			if (result != DialogResult.Yes)
				return; // Exit if the user cancels
			var password = user.GenerateRandomPassword();
			DisableItem();
			bool isSuccess = user.UpdateUserPassword(
				txtUsername.Text,
				txtWorkEmail.Text,
				password,
				"adminreset",
				EmpName,
				out string message,
				out string status);
			EnableItem();
			if (status == "Success")
			{
				RadMessageBox.Show("A temporary password has been sent to user's email", "Password Reset", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
		
			
		}

		private void txtWorkEmailMain_TextChanged(object sender, EventArgs e)
		{
			if (sender is RadTextBox txtWorkEmailMain)
			{
				// Email Validation Regex Pattern
				string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

				// Apply background color based on validation result
				txtWorkEmailMain.BackColor = Regex.IsMatch(txtWorkEmailMain.Text, emailPattern) ? _originalBackColor : Color.IndianRed;

				// Ensure txtWorkEmailMain is set correctly if email is valid
				if (txtWorkEmailMain.BackColor == _originalBackColor)
					txtWorkEmail.Text = txtWorkEmailMain.Text;
			}
		}

		private Color GetThemeBackColor(RadTextBox txtBox)
		{
			// Get the FillPrimitive which controls the background of the RadTextBox
			var fillPrimitive = txtBox.TextBoxElement.FindDescendant<FillPrimitive>();
			return fillPrimitive?.BackColor ?? SystemColors.Window; // Fallback to default system textbox color
		}

		private void cmbPosition_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			// Set the default management name based on the selected position
			if (cmbPosition.Text == "Collector")
			{
				cmbManagement.Text = "Angeline Uy";
			}
			else
			{
				cmbManagement.Text = "Edimson Escalona";
			}
		}



		private void cmbUserAccess_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			cmbUserAccess.Items.Add("Administrator");
			cmbUserAccess.Items.Add("Management");
			cmbUserAccess.Items.Add("Power User");
			cmbUserAccess.Items.Add("User");
			cmbUserAccess.Items.Add("Programmer");
		}

		private void cmbManagement_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			cmbManagement.Items.Add("Angeline Uy");
			cmbManagement.Items.Add("Edimson Escalona");
		}

		private void LoadDepartmentValues()
		{
			try
			{
				string userAccess = cmbUserAccess.SelectedItem != null ? cmbUserAccess.SelectedItem.Text : cmbUserAccess.Text;
				if (string.IsNullOrEmpty(userAccess)) return;

				string selectedDepartment = cmbUserDept.SelectedItem?.Text ?? cmbUserDept.Text;
				cmbUserDept.Items.Clear();

				switch (userAccess)
				{
					case "Administrator":
						cmbUserDept.Items.AddRange(["All Department", "IT"]);
						break;
					case "Management":
						cmbUserDept.Items.Add("All Department");
						break;
					case "Power User":
					case "User":
						cmbUserDept.Items.AddRange(["Workers Comp", "Private"]);
						break;
					case "Programmer":
						cmbUserDept.Items.AddRange(["All Department", "IT"]);
						break;
				}

				// Restore selection
				var matchedItem = cmbUserDept.Items.FirstOrDefault(item => item.Text == selectedDepartment);
				if (matchedItem != null) cmbUserDept.SelectedItem = matchedItem;
			}
			catch (Exception ex)
			{
				error.LogError("LoadDepartmentValues", EmpName, "frmUserInformation", null, ex);
			}
		}

		private void LoadPositionValues()
		{
			try
			{
				string userAccess = cmbUserAccess.SelectedItem != null ? cmbUserAccess.SelectedItem.Text : cmbUserAccess.Text;
				if (string.IsNullOrEmpty(userAccess)) return;

				string selectedPosition = cmbPosition.SelectedItem?.Text ?? cmbPosition.Text;
				cmbPosition.Items.Clear();

				switch (userAccess)
				{
					case "Administrator":
						cmbPosition.Items.AddRange(["IT Manager", "Operations Manager", "Supervisor"]);
						break;
					case "Management":
						cmbPosition.Items.AddRange(["Operations Manager", "Supervisor"]);
						break;
					case "Power User":
					case "User":
						cmbPosition.Items.AddRange(["Collector", "Back Office"]);
						break;
					case "Programmer":
						cmbPosition.Items.Add("Programmer");
						break;
				}

				// Restore selection
				var matchedItem = cmbPosition.Items.FirstOrDefault(item => item.Text == selectedPosition);
				if (matchedItem != null) cmbPosition.SelectedItem = matchedItem;
			}
			catch (Exception ex)
			{
				error.LogError("LoadPositionValues", EmpName, "frmUserInformation", null, ex);
			}
		}


		private void cmbUserDept_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{

			try
			{
				if (string.IsNullOrWhiteSpace(cmbUserAccess.Text))
				{
					RadMessageBox.Show("Please select user access first.", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
					e.Cancel = true; // Prevents opening the dropdown
					return;
				}

				LoadDepartmentValues();
			}
			catch (Exception ex)
			{
				error.LogError("cmbUserDept_PopupOpening", EmpName, "frmUserInformation", null, ex);
			}
		}

		private void cmbPosition_PopupOpening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(cmbUserAccess.Text))
				{
					RadMessageBox.Show("Please select user access first.", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
					e.Cancel = true; // Prevents opening the dropdown
					return;
				}

				LoadPositionValues();
			}
			catch (Exception ex)
			{
				error.LogError("cmbPosition_PopupOpening", EmpName, "frmUserInformation", null, ex);
			}
		}
	}
}
