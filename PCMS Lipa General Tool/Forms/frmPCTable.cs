using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmPCTable : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private readonly User user = new();
		//private readonly MailSender mailSender = new MailSender();		

		public string empName;
		public string accessLevel;

		public frmPCTable()
		{
			InitializeComponent();
			//ShowAllUserAccess()
			//DefaultItem();
		}

		public void ShowAllUserAccess()
		{
			dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedDataCells);
			var dataTable = user.ViewEmployeeInformationUser(empName, out string lblCount, "frmUserMgmt");
			dgEmployeeInfo.DataSource = dataTable;
			lblSearchCount.Text = lblCount;
			HideColumns();        
		}

		private void HideColumns()
		{
			//[EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS], [Broadvoice No.]
			dgEmployeeInfo.Columns["Password"].IsVisible = false;
			dgEmployeeInfo.Columns["User Access"].IsVisible = false;
			dgEmployeeInfo.Columns["RDWeb Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Lytec Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Email Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Broadvoice Username"].IsVisible = false;
			dgEmployeeInfo.Columns["Broadvoice Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Broadvoice Status"].IsVisible = false;
			dgEmployeeInfo.Columns["PC Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Discord Password"].IsVisible = false;
			dgEmployeeInfo.Columns["Theme"].IsVisible = false;
			dgEmployeeInfo.Columns["DeveloperAccess"].IsVisible = false;
			dgEmployeeInfo.Columns["First Time Login"].IsVisible = false;

		}

		private void PullDataFromTabletoTextBox()
		{

			try
			{
				
				if (dgEmployeeInfo.SelectedRows.Count == 0)
					return;

				var selectedRow = dgEmployeeInfo.SelectedRows[0];
				var modUser = new frmUserInformation
				{
					txtEmpID = { Text = selectedRow.Cells["Employee ID"].Value?.ToString() ?? string.Empty },
					txtUsername = { Text = selectedRow.Cells["Username"].Value?.ToString() ?? string.Empty },
					txtEmpName = { Text = selectedRow.Cells["Employee Name"].Value?.ToString() ?? string.Empty },
					txtWorkEmailMain = { Text = selectedRow.Cells["Email Address"].Value?.ToString() ?? string.Empty },
					cmbUserAccess = { Text = selectedRow.Cells["User Access"].Value?.ToString() ?? string.Empty },
					cmbUserDept = { Text = selectedRow.Cells["Department"].Value?.ToString() ?? string.Empty },
					cmbPosition = { Text = selectedRow.Cells["Position"].Value?.ToString() ?? string.Empty },
					cmbUserStatus = { Text = selectedRow.Cells["Status"].Value?.ToString() ?? string.Empty },
					cmbOffice = { Text = selectedRow.Cells["Office"].Value?.ToString() ?? string.Empty },

					/// second group
					txtRDWebUsername = { Text = selectedRow.Cells["RDWeb Username"].Value?.ToString() ?? string.Empty },
					txtRDWebPassword = { Text = selectedRow.Cells["RDWeb Password"].Value?.ToString() ?? string.Empty },
					txtLytecUsername = { Text = selectedRow.Cells["Lytec Username"].Value?.ToString() ?? string.Empty },
					txtLytecPassword = { Text = selectedRow.Cells["Lytec Password"].Value?.ToString() ?? string.Empty },

					/// third group
					txtBVNo = { Text = selectedRow.Cells["Broadvoice No."].Value?.ToString() ?? string.Empty },
					txtBVUsername = { Text = selectedRow.Cells["Broadvoice Username"].Value?.ToString() ?? string.Empty },
					txtBVPassword = { Text = selectedRow.Cells["Broadvoice Password"].Value?.ToString() ?? string.Empty },
					txtPCName = { Text = selectedRow.Cells["PC Assigned"].Value?.ToString() ?? string.Empty },
					txtPCUsername = { Text = selectedRow.Cells["PC Username"].Value?.ToString() ?? string.Empty },
					txtPCPassword = { Text = selectedRow.Cells["PC Password"].Value?.ToString() ?? string.Empty },
					txtWorkEmail = { Text = selectedRow.Cells["Email Address"].Value?.ToString() ?? string.Empty },
					txtWorkEmailPass = { Text = selectedRow.Cells["Email Password"].Value?.ToString() ?? string.Empty },
					txtDCUsernaem = { Text = selectedRow.Cells["Discord Username"].Value?.ToString() ?? string.Empty },
					txtDCPassword = { Text = selectedRow.Cells["Discord Password"].Value?.ToString() ?? string.Empty },
					dtpDateofBirth = { Text = selectedRow.Cells["Date of Birth"].Value?.ToString() ?? string.Empty },
					cmbManagement = { Text = selectedRow.Cells["Team"].Value?.ToString() ?? string.Empty },
					cmbEmploymentStatus = { Text = selectedRow.Cells["Employment Status"].Value?.ToString() ?? string.Empty },
					cmbFirstTime = { Text = selectedRow.Cells["First Time Login"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty },

					//
					lblResult = { Visible = false },
					EmpName = empName,
					Text = "Modify User",
					//btnUpdate = { Text = "Update" },
					accessLevel = accessLevel,
					
				};
				modUser.DefaultItem("Update");
				modUser.btnDelete.Visible = accessLevel != "Administrator";
				modUser.ShowDialog();
				ShowAllUserAccess();
			}
			catch (Exception ex)
			{
				notif.LogError("frmUserInformation", empName, "frmDiagnosis", null, ex);
				RadMessageBox.Show($@"
Oops!
We're having a little trouble retrieving the information right now. Please try again later, or feel free to reach out to the Software Developer if you need help.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}

		}
		private void frmUserManagement_Load(object sender, EventArgs e)
		{
			//this.dgEmployeeInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			cmbEmployeeStat.Text = "Active";
			dgEmployeeInfo.ReadOnly = true;
			ShowAllUserAccess();
		}

		private void dgEmployeeInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PullDataFromTabletoTextBox();
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			
			LoadSearchandFilter();
		}

		private void LoadSearchandFilter()
		{
			
			try
			{
				// Debug output to check dropdown selection
				Console.WriteLine($"Selected status: {cmbEmployeeStat.Text}");
				Console.WriteLine($"Dropdown Selected Value: {cmbEmployeeStat.Text}");

				// Determine search text (null if empty)
				string searchText = string.IsNullOrEmpty(txtSearch.Text) ? null : txtSearch.Text;

				// Perform search with or without text
				DataTable resultTable = user.GetSearch(
					searchText,                 // Pass null if empty
					cmbEmployeeStat.Text,        // Use the selected filter
					out string searchCountMessage,
					empName,
					"Admin"
				);
				Console.WriteLine($"Rows returned: {resultTable.Rows.Count}");

				// Ensure the data grid gets updated in both cases
				dgEmployeeInfo.DataSource = null;
				dgEmployeeInfo.DataSource = resultTable;
				dgEmployeeInfo.Refresh();
				// Ensure the search count message updates properly
				lblSearchCount.Text = searchCountMessage;
				//if (!string.IsNullOrEmpty(txtSearch.Text))
				//{
				//	// check what the dropdown is firing
				//	Console.WriteLine($"Selected status: {cmbEmployeeStat.Text}");
				//	// Input values for the search
				//	// Call the back-end method to perform the search
				//	DataTable resultTable = user.GetSearch(
				//		txtSearch.Text,
				//		cmbEmployeeStat.Text,
				//		out string searchCountMessage, empName, "Admin");
				//
				//	// Bind the result to the RadGridView
				//	dgEmployeeInfo.DataSource = resultTable;
				//
				//	// Display the search count in a label or message box
				//	lblSearchCount.Text = searchCountMessage; // Ensure lblSearchCount is a valid label in your form
				//}
				//else
				//{
				//	// Call the back-end method to perform the search using filter status only
				//	DataTable resultTable = user.GetSearch(
				//		null,
				//		cmbEmployeeStat.Text,
				//		out string searchCountMessage, empName, "Admin");
				//	dgEmployeeInfo.DataSource = resultTable;
				//
				//	// Display the search count in a label or message box
				//	lblSearchCount.Text = searchCountMessage;
				//
				//}
				dgEmployeeInfo.BestFitColumns(BestFitColumnMode.AllCells);
				HideColumns();
			}
			catch (Exception ex)
			{
				notif.LogError("LoadSearchFilter", empName, "frmUserManagement", null, ex);
			}
		}

		private void cmbEmployeeStat_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			Console.WriteLine($"SelectedIndexChanged triggered: {cmbEmployeeStat.Text}");
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

		private void btnNewUser_Click(object sender, EventArgs e)
		{
			var userUpdate = new frmUserInformation
			{
				Text = "Add New User"
			};
			user.GetDBID(out string ID, empName);
			userUpdate.txtEmpID.Text = ID;
			userUpdate.EmpName = empName;
			userUpdate.cmbFirstTime.Text = "Yes";
			userUpdate.DefaultItem("New");
			userUpdate.btnResetPassword.Enabled = false;
			userUpdate.ShowDialog();
			ShowAllUserAccess();

		}
	}
}