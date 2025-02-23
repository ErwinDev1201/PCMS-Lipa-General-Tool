using PCMS_Lipa_General_Tool.Class;
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
		private static readonly Notification notif = new();
		private readonly User user = new();
		//private readonly MailSender mailSender = new MailSender();		

		public string empName;
		public string accessLevel;

		public frmUserManagement()
		{
			InitializeComponent();
			//ShowAllUserAccess()
			//DefaultItem();
		}

		public void ShowAllUserAccess()
		{
			dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
					Text = "Modify User",
					EmpName = empName,
					accessLevel = accessLevel
				};

				// Populate text fields safely
				modUser.txtEmpID.Text = selectedRow.Cells["Employee ID"].Value?.ToString() ?? string.Empty;
				modUser.txtUsername.Text = selectedRow.Cells["Username"].Value?.ToString() ?? string.Empty;
				modUser.txtEmpName.Text = selectedRow.Cells["Employee Name"].Value?.ToString() ?? string.Empty;
				modUser.txtWorkEmailMain.Text = selectedRow.Cells["Email Address"].Value?.ToString() ?? string.Empty;
				modUser.cmbUserAccess.Text = selectedRow.Cells["User Access"].Value?.ToString() ?? string.Empty;
				modUser.cmbUserDept.Text = selectedRow.Cells["Department"].Value?.ToString() ?? string.Empty;
				modUser.cmbPosition.Text = selectedRow.Cells["Position"].Value?.ToString() ?? string.Empty;
				modUser.cmbUserStatus.Text = selectedRow.Cells["Status"].Value?.ToString() ?? string.Empty;
				modUser.cmbOffice.Text = selectedRow.Cells["Office"].Value?.ToString() ?? string.Empty;

				// Second group
				modUser.txtRDWebUsername.Text = selectedRow.Cells["RDWeb Username"].Value?.ToString() ?? string.Empty;
				modUser.txtRDWebPassword.Text = selectedRow.Cells["RDWeb Password"].Value?.ToString() ?? string.Empty;
				modUser.txtLytecUsername.Text = selectedRow.Cells["Lytec Username"].Value?.ToString() ?? string.Empty;
				modUser.txtLytecPassword.Text = selectedRow.Cells["Lytec Password"].Value?.ToString() ?? string.Empty;

				// Third group
				modUser.txtBVNo.Text = selectedRow.Cells["Broadvoice No."].Value?.ToString() ?? string.Empty;
				modUser.txtBVUsername.Text = selectedRow.Cells["Broadvoice Username"].Value?.ToString() ?? string.Empty;
				modUser.txtBVPassword.Text = selectedRow.Cells["Broadvoice Password"].Value?.ToString() ?? string.Empty;
				modUser.txtPCName.Text = selectedRow.Cells["PC Assigned"].Value?.ToString() ?? string.Empty;
				modUser.txtPCUsername.Text = selectedRow.Cells["PC Username"].Value?.ToString() ?? string.Empty;
				modUser.txtPCPassword.Text = selectedRow.Cells["PC Password"].Value?.ToString() ?? string.Empty;
				modUser.txtWorkEmail.Text = selectedRow.Cells["Email Address"].Value?.ToString() ?? string.Empty;
				modUser.txtWorkEmailPass.Text = selectedRow.Cells["Email Password"].Value?.ToString() ?? string.Empty;
				modUser.txtDCUsernaem.Text = selectedRow.Cells["Discord Username"].Value?.ToString() ?? string.Empty;
				modUser.txtDCPassword.Text = selectedRow.Cells["Discord Password"].Value?.ToString() ?? string.Empty;

				// Handle date field safely
				if (selectedRow.Cells["Date of Birth"].Value != null && DateTime.TryParse(selectedRow.Cells["Date of Birth"].Value.ToString(), out DateTime dob))
				{
					modUser.dtpDateofBirth.Value = dob;
				}
				else
				{
					modUser.dtpDateofBirth.Value = DateTime.Now; // Default value
				}

				modUser.cmbManagement.Text = selectedRow.Cells["Team"].Value?.ToString() ?? string.Empty;
				modUser.cmbEmploymentStatus.Text = selectedRow.Cells["Employment Status"].Value?.ToString() ?? string.Empty;
				modUser.cmbFirstTime.Text = selectedRow.Cells["First Time Login"].Value?.ToString() ?? string.Empty;
				modUser.txtRemarks.Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty;

				// Hide error label and set update mode
				modUser.lblResult.Visible = false;
				modUser.DefaultItem("Update");

				// Set button visibility based on access level
				modUser.btnDelete.Visible = accessLevel == "Administrator";

				// Show the form
				modUser.ShowDialog();
				ShowAllUserAccess();
			}
			catch (Exception ex)
			{
				notif.LogError("frmUserInformation", empName, "frmDiagnosis", null, ex);
				RadMessageBox.Show($@"
Oops!
We're having a little trouble retrieving the information right now. Please try again later, or feel free to reach out to the Software Developer if you need help.",
				"Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void frmUserManagement_Load(object sender, EventArgs e)
		{
			//this.dgEmployeeInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
				dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
				dgEmployeeInfo.DataSource = null;
				dgEmployeeInfo.DataSource = resultTable;
				dgEmployeeInfo.Refresh();
				// Ensure the search count message updates properly
				lblSearchCount.Text = searchCountMessage;
				dgEmployeeInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
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

		}

		private void btnNewUser_Click(object sender, EventArgs e)
		{
			var userUpdate = new frmUserInformation
			{
				Text = "Add New User"
			};
			user.GetDBID(out string ID, empName);
			userUpdate.txtEmpID.Text = ID;
			//userUpdate.lblResult.Visible = false;
			userUpdate.EmpName = empName;
			userUpdate.cmbFirstTime.Text = "Yes";
			userUpdate.DefaultItem("Create");
			userUpdate.btnResetPassword.Enabled = false;
			userUpdate.ShowDialog();
			ShowAllUserAccess();

		}
	}
}