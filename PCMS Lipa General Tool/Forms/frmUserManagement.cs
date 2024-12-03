using DiscordMessenger;
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmUserManagement : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
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
		}

		private void PullDataFromTabletoTextBox()
		{
			DoubleClickEnable();
			btnResetPassword.Text = "Reset Password";
			user.FillUpUserTxtBox(dgEmployeeInfo, txtIntID, txtEmployeeName, txtUsername, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, cmbOffice, txtWorkEmail, txtBVNo, empName);
		}

		private void btnMoreInfo_Click(object sender, EventArgs e)
		{
			if (btnMoreInfo.Text == "Add More Information")
			{
				if (txtUsername.Text != "" && txtEmployeeName.Text != "" && txtPassword.Text != "" && cmbUserDept.Text != "" && cmbUserAccess.Text != "" && cmbPosition.Text != "" && cmbUserStatus.Text != "" && cmbOffice.Text != "")
				{
					btnMoreInfo.Enabled = false;
					user.EmployeeDatabase("Create", txtIntID, txtEmployeeName, txtUsername, txtPassword, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, txtWorkEmail, txtBVNo, cmbOffice, "Yes", "Crystal", empName);
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
				user.FillAdminUserProfile(moreEmpInfo.txtEmpID, moreEmpInfo.txtEmpName, moreEmpInfo.txtRDWebUsername, moreEmpInfo.txtRDWebPassword, moreEmpInfo.txtLytecUsername, moreEmpInfo.txtLytecPassword, moreEmpInfo.txtWorkEmail, moreEmpInfo.txtWorkEmailPass, moreEmpInfo.txtBVNo, moreEmpInfo.txtBVUsername, moreEmpInfo.txtBVPassword, moreEmpInfo.txtPCName, moreEmpInfo.txtPCUsername, moreEmpInfo.txtPCPassword, moreEmpInfo.txtRemarks, moreEmpInfo.txtDateofBirth, moreEmpInfo.cmbManagement, moreEmpInfo.cmbFirstTime, moreEmpInfo.txtDCUsernaem, moreEmpInfo.txtDCPassword, moreEmpInfo.cmbEmploymentStatus, empName, txtIntID);
				moreEmpInfo.Text = "View and Update Employee Detailed Information";
				moreEmpInfo.txtRDWebUsername.Focus();
				moreEmpInfo.EmpName = empName;
				moreEmpInfo.btnUpdate.Text = "Save";
				moreEmpInfo.ShowDialog();
			}
			else
			{
				user.FillAdminUserProfile(moreEmpInfo.txtEmpID, moreEmpInfo.txtEmpName, moreEmpInfo.txtRDWebUsername, moreEmpInfo.txtRDWebPassword, moreEmpInfo.txtLytecUsername, moreEmpInfo.txtLytecPassword, moreEmpInfo.txtWorkEmail, moreEmpInfo.txtWorkEmailPass, moreEmpInfo.txtBVNo, moreEmpInfo.txtBVUsername, moreEmpInfo.txtBVPassword, moreEmpInfo.txtPCName, moreEmpInfo.txtPCUsername, moreEmpInfo.txtPCPassword, moreEmpInfo.txtRemarks, moreEmpInfo.txtDateofBirth, moreEmpInfo.cmbManagement, moreEmpInfo.cmbFirstTime, moreEmpInfo.txtDCUsernaem, moreEmpInfo.txtDCPassword, moreEmpInfo.cmbEmploymentStatus, empName, txtIntID);
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
			GetDBID();
		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "UserInfoSeq", txtIntID, null, "PCMS-0");
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				DisableInput();
				user.EmployeeDatabase("Delete", txtIntID, txtEmployeeName, txtUsername, txtPassword, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, txtWorkEmail, txtBVNo, cmbOffice, null, null, empName);
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
					user.EmployeeDatabase("Update", txtIntID, txtEmployeeName, txtUsername, txtPassword, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, txtWorkEmail, txtBVNo, cmbOffice, null, null, empName);
					radPanel1.Enabled = true;
					DefaultItem();
					Clear();
				}
			}
			else
			{
				if (txtUsername.Text != "" && txtEmployeeName.Text != "" && txtPassword.Text != "" && cmbUserDept.Text != "" && cmbUserAccess.Text != "" && cmbPosition.Text != "" && cmbUserStatus.Text != "" && cmbOffice.Text != "")
				{
					//var query = "INSERT INTO [User Information] ([Employee ID], [EMPLOYEE NAME], USERNAME, PASSWORD, [USER ACCESS], POSITION, [DEPARTMENT], [STATUS], [EMAIL ADDRESS], OFFICE, [FIRST TIME LOGIN], THEME ) VALUES ('" + txtIntID.Text + "','" + txtEmployeeName.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cmbUserAccess.Text + "','" + cmbPosition.Text + "','" + cmbUserDept.Text + "','" + cmbUserStatus.Text + "','" + txtWorkEmail.Text + "','" + cmbOffice.Text + "', 'YES', 'Crystal')";
					user.EmployeeDatabase("Create", txtIntID, txtEmployeeName, txtUsername, txtPassword, cmbUserAccess, cmbPosition, cmbUserDept, cmbUserStatus, txtWorkEmail, txtBVNo, cmbOffice, "Yes", "Crystal", empName);
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
			user.UpdateUserPassword(txtUsername.Text, txtWorkEmail.Text, "Password has been reset and sent to user", "Pcms@123", "Password has been reset by Admin", empName);
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
						task.CheckIfExistinDB("[User Information]", "[Username]", txtUsername, "UserMgmt", "Create", null);
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

			if (cmbEmployeeStat.Text == "Both")
			{
				//var query = $"SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE [EMPLOYEE NAME] LIKE '%{txtSearch.Text}%'";
				//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
				task.SearchTwoColumnOneFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Broadvoice No.]", txtSearch, lblSearchCount, empName);
			}
			else
			{
				//var query = $"SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE [EMPLOYEE NAME] LIKE '%{txtSearch.Text}%' AND STATUS = '{cmbEmployeeStat.Text}'";
				//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
				task.EmpSearchTwoColumnTwoFieldCombo(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Status]", txtSearch, cmbEmployeeStat, lblSearchCount, empName);
			}
		}

		private void cmbEmployeeStat_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			if (cmbEmployeeStat.Text == "Both")
			{
				//var bothquery = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information]";
				//mainProcess.SearchDatagrid(dgEmployeeInfo, bothquery);
				task.SearchEmpTwoColumnOneFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Employee Name]", txtSearch, lblSearchCount, empName);
			}
			else
			{
				//var query = "SELECT [Employee ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS] FROM [User Information] WHERE STATUS = '" + cmbEmployeeStat.Text + "'";
				//mainProcess.SearchDatagrid(dgEmployeeInfo, query);
				task.SearchEmpTwoColumnTwoFieldText(dgEmployeeInfo, "[User Information]", "[Employee Name]", "[Status]", txtSearch, cmbEmployeeStat, lblSearchCount, empName);
			}
			
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