﻿using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmUserManagement : Telerik.WinControls.UI.RadForm
	{
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly User user = new();
		//private readonly MailSender mailSender = new MailSender();		

		public string empName;
		public string accessLevel;

		public frmUserManagement()
		{
			InitializeComponent();
			ShowAllUserAccess();
			DefaultItem();
		}

		private void DoubleClickEnable()
		{
			txtUsername.Enabled = true;
			txtWorkEmail.Enabled = true;
			txtEmployeeName.Enabled = true;
			cmbUserAccess.Enabled = true;
			cmbPosition.Enabled = true;
			cmbUserDept.Enabled = true;
			cmbOffice.Enabled = true;
			btnCancel.Enabled = true;
			btnMoreInfo.Enabled = true;
			btnUpdateSave.Enabled = true;
			btnUpdateSave.Visible = true;
			btnUpdateSave.Text = "Update";
			txtBVNo.Enabled = true;
			if (accessLevel == "Management")
			{
				btnRemove.Visible = false;
				btnUpdateSave.Visible = false;
				btnCancel.Visible = false;
				btnNew.Visible = false;
				btnResetPassword.Enabled = false;

			}
			else if (accessLevel == "Administrator")
			{
				btnRemove.Visible = false;
			}
			else
			{
				btnRemove.Visible = true;
				btnRemove.Enabled = true;
			}
			btnCancel.Visible = true;
			btnResetPassword.Visible = true;
			btnNew.Enabled = false;
			btnNew.Visible = true;
			txtPassword.Visible = false;
			btnResetPassword.Visible = true;
			//chkshowpw.IsChecked = true;			
			cmbUserStatus.Enabled = true;
			btnMoreInfo.Visible = true;
			lblResult.Visible = false;
			btnMoreInfo.Text = "View More Information";
		}


		public void DefaultItem()
		{
			Clear();
			txtIntID.Enabled = false;
			txtUsername.Enabled = false;
			txtPassword.Enabled = false;
			btnResetPassword.Visible = false;
			txtEmployeeName.Enabled = false;
			cmbUserDept.Enabled = false;
			cmbOffice.Enabled = false;
			txtBVNo.Enabled = false;
			cmbUserAccess.Enabled = false;
			txtWorkEmail.Enabled = false;
			txtPassword.Visible = true;
			btnResetPassword.Visible = false;
			cmbPosition.Enabled = false;
			btnRemove.Visible = false;
			btnUpdateSave.Visible = false;
			btnCancel.Visible = false;
			btnNew.Visible = true;
			btnNew.Enabled = true;
			txtSearch.Clear();
			lblResult.Visible = false;
			cmbUserStatus.Enabled = false;
			cmbEmployeeStat.Text = "Active";
			cmbUserAccess.Items.Add("Administrator");
			cmbUserAccess.Items.Add("Management");
			cmbUserAccess.Items.Add("Power User");
			cmbUserAccess.Items.Add("User");
			cmbUserAccess.Items.Add("Programmer");
			cmbUserAccess.SelectedIndex = 3;
			//pwdMnageUser.Visibility = Visibility.Visible;			
			btnMoreInfo.Text = "Add More Information";
			btnMoreInfo.Visible = false;
		}

		private void EnableTextandSave()
		{
			txtIntID.Enabled = true;
			txtIntID.ReadOnly = true;
			txtUsername.Focus();
			txtUsername.Enabled = true;
			txtPassword.Enabled = true;
			txtEmployeeName.Enabled = true;
			txtPassword.Visible = true;
			txtPassword.Text = "Pcms@123";
			//chkshowpw.Enabled = true;
			//pwdMnageUser.Enabled = true;
			cmbUserStatus.Enabled = true;
			cmbUserAccess.Enabled = true;
			//chkshowpw.IsChecked = true;
			cmbPosition.Enabled = true;
			cmbUserDept.Enabled = true;
			cmbOffice.Enabled = true;
			txtWorkEmail.Enabled = true;
			btnRemove.Visible = false;
			btnUpdateSave.Visible = true;
			btnUpdateSave.Text = "Save";
			txtBVNo.Enabled = true;
			btnMoreInfo.Enabled = true;
			btnCancel.Enabled = true;
			btnCancel.Visible = true;
			btnNew.Enabled = false;
			btnMoreInfo.Visible = true;
			cmbUserStatus.Text = "Active";
			cmbOffice.Text = "Lipa";
			btnUpdateSave.Enabled =                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
			//pwdMnageUser.Visibility = ;Visibility.;			
			dgEmployeeInfo.ReadOnly = true;
			//			mainProcess.CreateDbId(txtIntID, Sql, @"PCMS-0");
		}

		public void ShowAllUserAccess()
		{
			dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedDataCells);
			var dataTable = user.ViewEmployeeInformationUser(empName, out string lblCount, "frmUserMgmt");
			dgEmployeeInfo.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
		}

		private void Clear()
		{
			//txtIntID.Clear();
			txtPassword.Clear();
			txtUsername.Clear();
			cmbUserStatus.Text = "";
			txtEmployeeName.Clear();
			cmbUserAccess.Text = "";
			cmbOffice.Text = "";
			txtWorkEmail.Text = "";
			cmbUserDept.Text = "";
			cmbPosition.Text = "";
			txtBVNo.Text = "";
			//txtSearch.Text = "";
		}

		private void DisableInput()
		{
			txtIntID.Enabled = false;
			txtEmployeeName.Enabled = false;
			txtBVNo.Enabled = false;
			txtUsername.Enabled = false;
			txtWorkEmail.Enabled = false;
			txtPassword.Enabled = false;
			cmbEmployeeStat.Enabled = false;
			cmbOffice.Enabled = false;
			cmbPosition.Enabled = false;
			cmbUserDept.Enabled = false;
			btnCancel.Enabled = false;
			btnMoreInfo.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnRemove.Enabled = false;
			cmbUserDept.Enabled = false;
			btnResetPassword.Enabled = false;
			cmbUserDept.Enabled = false;
			lblResult.Visible = false;
		}

		private void PullDataFromTabletoTextBox()
		{

			try
			{
				if (dgEmployeeInfo.SelectedRows.Count > 0)
				{
					var selectedRow = dgEmployeeInfo.SelectedRows[0];

					// Safely extract each value and check for nulls
					txtIntID.Text = selectedRow.Cells["Employee ID"]?.Value?.ToString() ?? string.Empty;
					txtEmployeeName.Text = selectedRow.Cells["Employee Name"]?.Value?.ToString() ?? string.Empty;
					txtUsername.Text = selectedRow.Cells["Username"]?.Value?.ToString() ?? string.Empty;
					cmbUserAccess.Text = selectedRow.Cells["User Access"]?.Value?.ToString() ?? string.Empty;
					cmbPosition.Text = selectedRow.Cells["Position"]?.Value?.ToString() ?? string.Empty;
					cmbUserDept.Text = selectedRow.Cells["Department"]?.Value?.ToString() ?? string.Empty;
					cmbUserStatus.Text = selectedRow.Cells["Status"]?.Value?.ToString() ?? string.Empty;
					cmbOffice.Text = selectedRow.Cells["Office"]?.Value?.ToString() ?? string.Empty;
					txtWorkEmail.Text = selectedRow.Cells["Email Address"]?.Value?.ToString() ?? string.Empty;
					txtBVNo.Text = selectedRow.Cells["Broadvoice No."]?.Value?.ToString() ?? string.Empty;
					//txtIntID.Text = dgEmployeeInfo.SelectedRows[0].Cells["Employee ID"].Value + string.Empty;
					//txtEmployeeName.Text = dgEmployeeInfo.SelectedRows[0].Cells["Employee Name"].Value + string.Empty;
					//txtUsername.Text = dgEmployeeInfo.SelectedRows[0].Cells["Username"].Value + string.Empty;
					//cmbUserAccess.Text = dgEmployeeInfo.SelectedRows[0].Cells["User Access"].Value + string.Empty;
					//cmbPosition.Text = dgEmployeeInfo.SelectedRows[0].Cells["Position"].Value + string.Empty;
					//cmbUserDept.Text = dgEmployeeInfo.SelectedRows[0].Cells["Department"].Value + string.Empty;
					//cmbUserStatus.Text = dgEmployeeInfo.SelectedRows[0].Cells["Status"].Value + string.Empty;
					//cmbOffice.Text = dgEmployeeInfo.SelectedRows[0].Cells["Office"].Value + string.Empty;
					//txtWorkEmail.Text = dgEmployeeInfo.SelectedRows[0].Cells["Email Address"].Value + string.Empty;
					//txtBVNo.Text = dgEmployeeInfo.SelectedRows[0].Cells["Broadvoice No."].Value + string.Empty;
				}
				DoubleClickEnable();
				btnResetPassword.Text = "Reset Password";
			}
			catch (Exception ex)
			{

				error.LogError($"PullDataFromTabletoTextBox", empName, "frmUserManagement", txtIntID.Text, ex);
				RadMessageBox.Show($@"
Oops!
We're having a little trouble retrieving the information right now. Please try again later, or feel free to reach out to the Software Developer if you need help.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}

			//user.FillUpUserTxtBox(dgEmployeeInfo, txtIntID, txtEmployeeName, txtUsername, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, cmbOffice, txtWorkEmail, txtBVNo, empName);
		}

		private void btnMoreInfo_Click(object sender, EventArgs e)
		{
			if (btnMoreInfo.Text == "Add More Information")
			{
				if (txtUsername.Text != "" && txtEmployeeName.Text != "" && txtPassword.Text != "" && cmbUserDept.Text != "" && cmbUserAccess.Text != "" && cmbPosition.Text != "" && cmbUserStatus.Text != "" && cmbOffice.Text != "")
				{
					btnMoreInfo.Enabled = false;
					bool isSuccess = user.EmployeeDatabase(
						"Create",
						txtIntID.Text,
						txtEmployeeName.Text,
						txtUsername.Text,
						//txtPassword.Text,
						cmbUserAccess.Text,
						cmbPosition.Text,
						cmbUserDept.Text,
						cmbUserStatus.Text,
						txtWorkEmail.Text,
						txtBVNo.Text,
						cmbOffice.Text,
						"Yes",
						"Crystal",
						empName,
						out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					}
					UpdateMoreEmployeeInformation("Create");
					ShowAllUserAccess();
					DefaultItem();
				}
				else
				{
					RadMessageBox.Show("Below fields are required to save Information \n" +
						"Name \n" +
						"Username \n" +
						"Password \n" +
						"Group \n" +
						"User Type \n" +
						"Role \n" +
						"User Status \n" +
						"Office"
						, "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				}

			}
			else
			{
				 
				var moreEmpInfo = new frmEmpAdditionalInfo
				{
					Text = "View and Update Employee Detailed Information"
				};
				UpdateMoreEmployeeInformation("Update");
				ShowAllUserAccess();
				DefaultItem();
			}
		}

		private void UpdateMoreEmployeeInformation(string request)
		{
			var moreEmpInfo = new frmEmpAdditionalInfo();

			if (request == "Create")
			{
				moreEmpInfo.txtEmpID.Text = txtIntID.Text;
				moreEmpInfo.txtEmpName.Text = txtEmployeeName.Text;
				moreEmpInfo.txtWorkEmail.Text = txtWorkEmail.Text;
				moreEmpInfo.txtBVNo.Text = txtBVNo.Text;
				user.FillAdminUserProfile(
					txtIntID.Text,
					out string RDWebUsername,
			out string RDWebPassword,
			out string LytecUsername,
			out string LytecPassword,
			out string EmailPassword,
			out string BVUsername,
			out string BVPassword,
			out string PCName,
			out string PCUsername,
			out string PCPassword,
			out string Remarks,
			out string DateOfBirth,
			out string cmbDirectReport,
			out string firstTimeLogin,
			out string DCUsername,
			out string DCPassword,
			out string cmbEmploymentStatus,
			empName,
			"usermgmt");
				//user.FillAdminUserProfile
				//	(
				moreEmpInfo.txtRDWebUsername.Text = RDWebUsername;
				moreEmpInfo.txtRDWebPassword.Text = RDWebPassword;
				moreEmpInfo.txtLytecUsername.Text = LytecUsername;
				moreEmpInfo.txtLytecPassword.Text = LytecPassword;
				moreEmpInfo.txtWorkEmailPass.Text = EmailPassword;
				moreEmpInfo.txtBVUsername.Text = BVUsername;
				moreEmpInfo.txtBVPassword.Text = BVPassword;
				moreEmpInfo.txtPCName.Text = PCName;
				moreEmpInfo.txtPCUsername.Text = PCUsername;
				moreEmpInfo.txtPCPassword.Text = PCPassword;
				moreEmpInfo.txtRemarks.Text = Remarks;
				moreEmpInfo.txtDateofBirth.Text = DateOfBirth;
				moreEmpInfo.cmbFirstTime.Text = firstTimeLogin;
				moreEmpInfo.cmbManagement.Text = cmbDirectReport;
				moreEmpInfo.txtDCUsernaem.Text = DCUsername;
				moreEmpInfo.txtDCPassword.Text = DCPassword;
				moreEmpInfo.cmbEmploymentStatus.Text = cmbEmploymentStatus;
				//	empName);
				moreEmpInfo.Text = "View and Update Employee Detailed Information";
				moreEmpInfo.txtRDWebUsername.Focus();
				moreEmpInfo.EmpName = empName;
				moreEmpInfo.btnUpdate.Text = "Save";
				moreEmpInfo.ShowDialog();
			}
			else
			{
				moreEmpInfo.txtEmpID.Text = txtIntID.Text;
				moreEmpInfo.txtEmpName.Text = txtEmployeeName.Text;
				moreEmpInfo.txtWorkEmail.Text = txtWorkEmail.Text;
				moreEmpInfo.txtBVNo.Text = txtBVNo.Text;
				user.FillAdminUserProfile(
					txtIntID.Text,
					out string RDWebUsername,
			out string RDWebPassword,
			out string LytecUsername,
			out string LytecPassword,
			out string EmailPassword,
			out string BVUsername,
			out string BVPassword,
			out string PCName,
			out string PCUsername,
			out string PCPassword,
			out string Remarks,
			out string DateOfBirth,
			out string cmbDirectReport,
			out string firstTimeLogin,
			out string DCUsername,
			out string DCPassword,
			out string cmbEmploymentStatus,
			empName,
			"usermgmt");
				//user.FillAdminUserProfile
				//	(
				moreEmpInfo.txtRDWebUsername.Text = RDWebUsername;
				moreEmpInfo.txtRDWebPassword.Text = RDWebPassword;
				moreEmpInfo.txtLytecUsername.Text = LytecUsername;
				moreEmpInfo.txtLytecPassword.Text = LytecPassword;
				moreEmpInfo.txtWorkEmailPass.Text = EmailPassword;
				moreEmpInfo.txtBVUsername.Text = BVUsername;
				moreEmpInfo.txtBVPassword.Text = BVPassword;
				moreEmpInfo.txtPCName.Text = PCName;
				moreEmpInfo.txtPCUsername.Text = PCUsername;
				moreEmpInfo.txtPCPassword.Text = PCPassword;
				moreEmpInfo.txtRemarks.Text = Remarks;
				moreEmpInfo.txtDateofBirth.Text = DateOfBirth;
				moreEmpInfo.cmbFirstTime.Text = firstTimeLogin;
				moreEmpInfo.cmbManagement.Text = cmbDirectReport;
				moreEmpInfo.txtDCUsernaem.Text = DCUsername;
				moreEmpInfo.txtDCPassword.Text = DCPassword;
				moreEmpInfo.cmbEmploymentStatus.Text = cmbEmploymentStatus;
				if (accessLevel == "Management")
				{
					moreEmpInfo.btnUpdate.Visible = false;
					moreEmpInfo.btnDelete.Visible = false;
				}
				else if (accessLevel == "Administrator")
				{
					moreEmpInfo.btnDelete.Visible = false;
				}
				moreEmpInfo.txtRDWebUsername.Focus();
				moreEmpInfo.EmpName = empName;
				moreEmpInfo.accessLevel = accessLevel;
				moreEmpInfo.btnUpdate.Text = "Save";
				moreEmpInfo.ShowDialog();
			}
		}

		private void frmUserManagement_Load(object sender, EventArgs e)
		{
			//this.dgEmployeeInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEmployeeInfo.ReadOnly = true;
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			EnableTextandSave();
			user.GetDBID(out string ID, empName);
			txtIntID.Text = ID;
		}

		

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				DisableInput();
				bool isSuccess = user.EmployeeDatabase(
					"Delete",
					txtIntID.Text,
					txtEmployeeName.Text,
					txtUsername.Text,
					txtPassword.Text,
					cmbUserAccess.Text,
					cmbPosition.Text,
					cmbUserDept.Text,
					cmbUserStatus.Text,
					txtWorkEmail.Text,
					txtBVNo.Text,
					cmbOffice.Text,
					null,
					null,
					empName,
					out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
				Clear();
				ShowAllUserAccess();
				DefaultItem();
			}
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					DisableInput();
					//radPanel1.Enabled = false;
					bool isSuccess = user.EmployeeDatabase(
						"Update",
						txtIntID.Text,
						txtEmployeeName.Text,
						txtUsername.Text,
						//txtPassword.Text,
						cmbUserAccess.Text,
						cmbPosition.Text,
						cmbUserDept.Text,
						cmbUserStatus.Text,
						txtWorkEmail.Text,
						txtBVNo.Text,
						cmbOffice.Text,
						null,
						null,
						empName,
						out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					}
					radPanel1.Enabled = true;
					DefaultItem();
					Clear();
				}
			}
			else
			{
				if (txtUsername.Text != "" && txtEmployeeName.Text != "" && txtPassword.Text != "" && cmbUserDept.Text != "" && cmbUserAccess.Text != "" && cmbPosition.Text != "" && cmbUserStatus.Text != "" && cmbOffice.Text != "")
				{
					bool isSuccess = user.EmployeeDatabase(
						"Create",
						txtIntID.Text,
						txtEmployeeName.Text,
						txtUsername.Text,
						txtPassword.Text,
						cmbUserAccess.Text,
						cmbPosition.Text,
						cmbUserDept.Text,
						cmbUserStatus.Text,
						txtWorkEmail.Text,
						txtBVNo.Text,
						cmbOffice.Text, 
						"Yes", 
						"Crystal", 
						empName,
						out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					}
					Clear();
					DefaultItem();
					ShowAllUserAccess();
					txtUsername.Focus();
				}
				else
				{
					RadMessageBox.Show("Below fields are required to save Information \n" +
						"Name \n" +
						"Username \n" +
						"Password \n" +
						"Group \n" +
						"User Type \n" +
						"Role \n" +
						"User Status \n" +
						"Office", "Notification", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
			}
		}

		private void dgEmployeeInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PullDataFromTabletoTextBox();
		}

		private void dgEmployeeInfo_KeyUp(object sender, KeyEventArgs e)
		{
			PullDataFromTabletoTextBox();
		}

		private void btnResetPassword_Click(object sender, EventArgs e)
		{
			bool isSuccess = user.UpdateUserPassword(
				txtUsername.Text,
				txtWorkEmail.Text,
				"Pcms@123",
				"Password has been reset by Admin",
				empName,
				out string message,
				out string status);
			btnResetPassword.Text = "Password has been reset and sent to user";
			DefaultItem();
		}


		//private void cmbEmployeeStat_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		//{
		//	
		//
		//}





		private void btnCancel_Click(object sender, EventArgs e)
		{
			DefaultItem();
		}

		private void txtUsername_TextChanged(object sender, EventArgs e)
		{
			if (btnMoreInfo.Text == "Add More Information")
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
		}

		private void cmbPosition_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			//if (cmbPosition.Text == "Management")
			//{
			//	cmbUserAccess.Text = "Management";
			//}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			LoadSearchandFilter();
			//if (cmbEmployeeStat.Text == "Both")
			//{
			//	//var query = $"SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE [EMPLOYEE NAME] LIKE '%{txtSearch.Text}%'";
			//	//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
			//	task.SearchTwoColumnOneFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Broadvoice No.]", txtSearch, lblSearchCount, empName);
			//}
			//else
			//{
			//	//var query = $"SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE [EMPLOYEE NAME] LIKE '%{txtSearch.Text}%' AND STATUS = '{cmbEmployeeStat.Text}'";
			//	//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
			//	task.EmpSearchTwoColumnTwoFieldCombo(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Status]", txtSearch, cmbEmployeeStat, lblSearchCount, empName);
			//}
		}

		private void LoadSearchandFilter()
		{
			try
			{
				// check what the dropdown is firing
				Console.WriteLine($"Selected status: {cmbEmployeeStat.Text}");
				// Input values for the search
				// Call the back-end method to perform the search
				DataTable resultTable = user.GetSearch(
					txtSearch.Text,
					cmbEmployeeStat.Text,
					out string searchCountMessage, empName);

				// Bind the result to the RadGridView
				dgEmployeeInfo.DataSource = resultTable;

				// Display the search count in a label or message box
				lblSearchCount.Text = searchCountMessage; // Ensure lblSearchCount is a valid label in your form
			}
			catch (Exception ex)
			{
				error.LogError("LoadSearchFilter", empName, "frmUserManagement", null, ex);
			}
		}

		private void cmbEmployeeStat_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			LoadSearchandFilter();
			
			//if (cmbEmployeeStat.Text == "All")
			//{
			//	//var bothquery = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information]";
			//	//mainProcess.SearchDatagrid(dgEmployeeInfo, bothquery);
			//	//task.SearchEmpTwoColumnOneFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Employee Name]", txtSearch, lblSearchCount, empName);
			//}
			//else
			//{
			//	//var query = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE STATUS = '" + cmbEmployeeStat.Text + "'";
			//	//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
			//	task.SearchEmpTwoColumnTwoFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Status]", txtSearch, cmbEmployeeStat, lblSearchCount, empName);
			//}

		}

		private void cmbUserAccess_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			//cmbUserDept.Items.Clear();
			//cmbPosition.Items.Clear();

			if (cmbUserAccess.SelectedItem == null)
			{
				cmbUserDept.Text = "";
				cmbPosition.Text = "";
				//RadMessageBox.Show("Department, Position and User Access are mandatory, Please don't leave them empty");
			}
			else
			{
				string userAccess = cmbUserAccess.SelectedItem.Text;
				cmbUserDept.Items.Clear();
				cmbPosition.Items.Clear();

				switch (userAccess)
				{
					case "Administrator":
						cmbUserDept.Items.Add("All Department");
						cmbPosition.Items.Add("Operations Manager");
						cmbPosition.Items.Add("Supervisor");
						break;

					case "Management":
						cmbUserDept.Items.Add("All Department");
						cmbPosition.Items.Add("Operations Manager");
						cmbPosition.Items.Add("Supervisor");
						break;

					case "Power User":
						cmbUserDept.Items.Add("Workers Comp");
						cmbUserDept.Items.Add("Private");
						cmbPosition.Items.Add("Collector");
						cmbPosition.Items.Add("Back Office");
						break;

					case "User":
						cmbUserDept.Items.Add("Workers Comp");
						cmbUserDept.Items.Add("Private");
						cmbPosition.Items.Add("Collector");
						cmbPosition.Items.Add("Back Office");
						break;

					case "Programmer":
						cmbUserDept.Items.Add("All Department");
						cmbPosition.Items.Add("Programmer");
						break;
				}

				// Optionally, set a default selection for radDropDownList2
				if (cmbUserDept.Items.Count > 0)
					cmbUserDept.SelectedIndex = 0;
				if (cmbPosition.Items.Count > 0)
					cmbPosition.SelectedIndex = 0;
			}

		}
	}
}