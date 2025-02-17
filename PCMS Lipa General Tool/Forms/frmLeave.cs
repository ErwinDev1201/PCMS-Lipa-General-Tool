using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmLeave : Telerik.WinControls.UI.RadForm
	{
		private readonly Leave leave = new();
		private readonly User user = new();
		private static readonly Notification notif = new();
		//private readonly MailSender mailSender = new MailSender();						
		public string accessLevel;
		public string EmpName;
		public string empID;

		public frmLeave()
		{
			InitializeComponent();
			AutoCompleteCombo();
			this.btnRefresh.ButtonElement.ToolTipText = "Refresh";
			FillEmpCmb();
		}


		private void AutoCompleteCombo()
		{
			cmbFilterName.AutoCompleteMode = AutoCompleteMode.Suggest;
			cmbFilterName.DropDownListElement.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
		}

		public void FillEmpCmb()
		{
			cmbFilterName.Items.Clear();
			List<string> items = user.GetEmployeeList(EmpName);
			cmbFilterName.Items.Clear(); // Clear existing items, if any
			foreach (var item in items)
			{
				cmbFilterName.Items.Add(item);
			}
		}


		//public void ShowLeaveList()
		//{
		//	if (cmbFilterName.Text != null)
		//	{
		//		string query = "SELECT * FROM [Leave] Where [Employee Name] = '" + cmbFilterName.Text + "'";
		//		task.ViewDatagrid(dgLeave, query, lblCountSearch, EmpName);
		//	}
		//	else
		//	{
		//		string query = "SELECT * FROM [Leave]";
		//		task.ViewDatagrid(dgLeave, query, lblCountSearch, EmpName);
		//	}
		//}
		//
		public void ShowLeaveList()
		{
			bool isElevatedAccess = accessLevel == "Administrator" || accessLevel == "Programmer";

			string filterName = cmbFilterName.Text ?? string.Empty;
			string filterStatus = cmbFilterStatus.Text ?? string.Empty;

			string query = leave.GetLeaveQuery(filterName, filterStatus, isElevatedAccess);

			leave.ViewLeave(dgLeave, query, lblCountSearch, EmpName);
		}



		private void btnNew_Click(object sender, EventArgs e)
		{
			var modleave = new frmModLeave()
			{
				Text = "Leave",
				EmpName = EmpName,
				accessLevel = accessLevel
			};
			modleave.btnDelete.Visible = false;
			modleave.btnCancel.Location = new System.Drawing.Point(880, 223);
			modleave.dtpEndDate.Text = DateTime.Now.AddDays(1).ToString();
			modleave.dtpStartdate.Text = DateTime.Now.ToString();
			modleave.txtEmpID.Text = empID;
			leave.GetDBListID(out string ID, EmpName);
			modleave.lblLeaveID.Text = ID;
			//modleave.GetDBListID();
			string position = modleave.txtPosition.Text;
			string empStat = modleave.txtEmploymentStatus.Text;
			//string empName = EmpName;

			leave.FillUpSupportLeaveForm(empID, ref position, ref empStat, EmpName);

			modleave.txtEmployeeName.Text = EmpName;
			modleave.txtPosition.Text = position;
			modleave.txtEmploymentStatus.Text = empStat;
			modleave.dtpStartdate.Focus();
			///leave.FillUpSupportLe
			/////var modleave = new frmModLeave()
			/////{
			/////	Text = "Leave",
			/////	EmpName = EmpName,
			/////	accessLevel = accessLevel,
			/////};
			/////modleave.btnDelete.Visible = false;
			/////modleave.btnCancel.Location = new System.Drawing.Point(880, 223);
			/////modleave.dtpEndDate.Text = DateTime.Now.AddDays(1).ToString();
			/////modleave.dtpStartdate.Text = DateTime.Now.ToString();
			/////modleave.txtEmpID.Text = empID;
			/////leave.GetDBListID(out string ID, EmpName);
			/////modleave.lblLeaveID.Text = ID;
			///////modleave.GetDBListID();
			/////
			///////string empID = modLeave.txtEmpID.Text;
			///////string employeeName = modleave.txtEmployeeName.Text;
			/////string position = modleave.txtPosition.Text;
			/////string empStat = modleave.txtEmploymentStatus.Text;
			/////string empName = EmpName;
			/////
			/////leave.FillUpSupportLeaveForm(empID, ref position, ref empStat, empName);
			/////
			/////modleave.txtEmployeeName.Text = empName;
			/////modleave.txtPosition.Text = position;
			/////modleave.txtEmploymentStatus.Text = empStat;
			///////leave.FillUpSupportLeaveForm(modLeave.txtEmpI
			///////leave.FillUpSupportLeaveForm(modleave.txtEmpID.Text, modleave.txtEmployeeName.Text, modleave.txtPosition.Text, modleave.txtEmploymentStatus.Text, EmpName);
			modleave.ShowDialog();
			ShowLeaveList();

		}


		private void frmLeave_Load(object sender, EventArgs e)
		{
			dgLeave.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgAdjusterInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgLeave.ReadOnly = true;
			ShowLeaveList();
		}

		private void frmLeave_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		//private void PopulateLeaveDetails(RadGridView dgLeave)
		//{
		//	if (dgLeave.SelectedRows.Count == 0)
		//	{
		//		MessageBox.Show("Please select a leave entry from the grid.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		//		return;
		//	}
		//
		//	var modleave = new frmModLeave();
		//	// Extract selected Leave ID
		//	var selectedRow = dgLeave.SelectedRows[0];
		//	string selectedLeaveID = selectedRow.Cells["Leave ID"].Value.ToString();
		//
		//	// Output variables
		//
		//	try
		//	{
		//		// Call FillUpLeaveFields to retrieve data
		//		leave.FillUpLeaveFields(
		//			out string leaveID,
		//			out string employeeID,
		//			out string employeeName,
		//			out DateTime? startDate,
		//			out DateTime? endDate,
		//			out string reason,
		//			out bool isWithPay,
		//			out string leaveType,
		//			out string approvalStatus,
		//			out string remarks,
		//			selectedLeaveID);
		//
		//		// Populate Telerik controls
		//		modleave.lblLeaveID.Text = leaveID;
		//		modleave.txtEmpID.Text = employeeID;
		//		modleave.txtEmployeeName.Text = employeeName;
		//
		//		if (startDate.HasValue)
		//			modleave.dtpStartdate.Value = startDate.Value;
		//		else
		//			modleave.dtpStartdate.ResetText();
		//
		//		if (endDate.HasValue)
		//			modleave.dtpEndDate.Value = endDate.Value;
		//		else
		//			modleave.dtpEndDate.ResetText();
		//
		//		modleave.txtReason.Text = reason;
		//		modleave.cmbApproval.Text = approvalStatus;
		//		modleave.txtRemarks.Text = remarks;
		//
		//		// Set radio buttons
		//		modleave.rdoWPay.IsChecked = isWithPay;
		//		modleave.rdoWOPay.IsChecked = !isWithPay;
		//
		//		// Display leave type
		//		modleave.rdoSick.IsChecked = leaveType == "Sick";
		//		modleave.rdoVacation.IsChecked = leaveType == "Vacation";
		//		modleave.rdoPaternity.IsChecked = leaveType == "Paternity";
		//		modleave.rdoMaternity.IsChecked = leaveType == "Maternity";
		//		modleave.rdoBirthday.IsChecked = leaveType == "Birthday";
		//		modleave.rdoBereavement.IsChecked = leaveType == "Bereavement";
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show($"Error while populating leave details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}


		private void dgLeave_DoubleClick(object sender, EventArgs e)
		{
			
			if (dgLeave.SelectedRows.Count == 0)
			{
				MessageBox.Show("Please select a leave entry from the grid.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}


			var modleave = new frmModLeave();// Extract selected Leave ID
			var selectedRow = dgLeave.SelectedRows[0];
			string selectedLeaveID = selectedRow.Cells["Leave ID"].Value.ToString();

			// Output variables

			try
			{
				// Call FillUpLeaveFields to retrieve data
				leave.FillUpLeaveFields(
					//string leaveID,
					out string employeeID,
					out string employeeName,
					out DateTime? startDate,
					out DateTime? endDate,
					out string reason,
					out bool isWithPay,
					out string leaveType,
					out string approvalStatus,
					out string remarks,
					selectedLeaveID,
					EmpName);

				// Populate Telerik controls
				modleave.lblLeaveID.Text = selectedLeaveID;
				modleave.txtEmpID.Text = employeeID;
				modleave.txtEmployeeName.Text = employeeName;

				if (startDate.HasValue)
					modleave.dtpStartdate.Value = startDate.Value;
				else
					modleave.dtpStartdate.ResetText();

				if (endDate.HasValue)
					modleave.dtpEndDate.Value = endDate.Value;
				else
					modleave.dtpEndDate.ResetText();

				modleave.txtReason.Text = reason;
				modleave.cmbApproval.Text = approvalStatus;
				modleave.txtRemarks.Text = remarks;

				// Set radio buttons
				modleave.rdoWPay.IsChecked = isWithPay;
				modleave.rdoWOPay.IsChecked = !isWithPay;

				// Display leave type
				modleave.rdoSick.IsChecked = leaveType == "Sick";
				modleave.rdoVacation.IsChecked = leaveType == "Vacation";
				modleave.rdoPaternity.IsChecked = leaveType == "Paternity";
				modleave.rdoMaternity.IsChecked = leaveType == "Maternity";
				modleave.rdoBirthday.IsChecked = leaveType == "Birthday";
				modleave.rdoBereavement.IsChecked = leaveType == "Bereavement";

				if (accessLevel is "User" or "Power User")
				{
					modleave.ReadOnlyAccess();
				}
				else
				{
					if (accessLevel == "Management")
					{
						modleave.ReadOnlyAccess();
						modleave.btnSaveUpdate.Visible = true;
					}

					modleave.grpApprovalStatus.Enabled = true;
					modleave.btnSaveUpdate.Text = "Update";
				}

				string position = modleave.txtPosition.Text;
				string empStat = modleave.txtEmploymentStatus.Text;
				string empName = EmpName;
				Console.WriteLine(empStat);
				
				leave.FillUpSupportLeaveForm(employeeID, ref position, ref empStat, empName);

				//modLeave.txtEmployeeName.Text = employeeName;
				modleave.txtPosition.Text = position;
				modleave.txtEmploymentStatus.Text = empStat;

				modleave.EmpName = EmpName;
				modleave.ShowDialog();

				if (accessLevel is "User" or "Power User")
				{
					cmbFilterName.Text = EmpName;
				}

				ShowLeaveList();
			}
			catch (Exception ex)
			{
				notif.LogError("dgLeave_DoubleClick", EmpName, "frmLeave", selectedLeaveID, ex);
			}


			//var modLeave = new frmModLeave
			//{
			//	txtEmpID = { Text = empID }
			//};
			//
			//leave.FillUpLeaveFields(
			//	dgLeave,
			//	modLeave.lblLeaveID,
			//	modLeave.txtEmpID,
			//	modLeave.txtEmployeeName,
			//	modLeave.dtpStartdate,
			//	modLeave.dtpEndDate,
			//	modLeave.txtReason,
			//	modLeave.rdoWPay,
			//	modLeave.rdoWOPay,
			//	modLeave.rdoSick,
			//	modLeave.rdoVacation,
			//	modLeave.rdoPaternity,
			//	modLeave.rdoMaternity,
			//	modLeave.rdoBirthday,
			//	modLeave.rdoBereavement,
			//	modLeave.cmbApproval,
			//	modLeave.txtRemarks,
			//	EmpName
			//);
			//
			//Console.WriteLine(empID);
			//
			////string empID = modLeave.txtEmpID.Text;
			////string employeeName = modLeave.txtEmployeeName.Text;
			//string position = modLeave.txtPosition.Text;
			//string empStat = modLeave.txtEmploymentStatus.Text;
			//string empName = EmpName;
			//Console.WriteLine(empStat);
			//
			//leave.FillUpSupportLeaveForm(empID, ref position, ref empStat, empName);
			//
			////modLeave.txtEmployeeName.Text = employeeName;
			//modLeave.txtPosition.Text = position;
			//modLeave.txtEmploymentStatus.Text = empStat;
			////leave.FillUpSupportLeaveForm(modLeave.txtEmpID.Text, modLeave.txtEmployeeName.Text, modLeave.txtPosition.Text, modLeave.txtEmploymentStatus.Text, EmpName);
			//


			//if (accessLevel == "User" || accessLevel == "Power User")
			//{
			//	try
			//	{
			//		var modLeave = new frmModLeave();
			//		modLeave.txtEmpID.Text += empID;
			//		leave.FillUpSupportLeaveForm(empID, modLeave.txtEmployeeName, modLeave.txtPosition, modLeave.txtEmploymentStatus, EmpName);
			//		leave.FillUpLeaveFields(dgLeave, modLeave.lblLeaveID, modLeave.txtEmployeeName, modLeave.dtpStartdate, modLeave.dtpEndDate, modLeave.txtReason, modLeave.rdoWPay, modLeave.rdoWOPay, modLeave.rdoSick, modLeave.rdoVacation, modLeave.rdoPaternity, modLeave.rdoMaternity, modLeave.rdoBirthday, modLeave.rdoBereavement, modLeave.cmbApproval, modLeave.txtRemarks, EmpName);
			//		modLeave.ReadOnlyAccess();
			//		modLeave.ShowDialog();
			//		cmbFilterName.Text = EmpName;
			//		ShowLeaveList();
			//	}
			//	catch (Exception ex)
			//	{
			//		var ErrorMessage = ex.Message + "\n\n Name: " + EmpName + "\n Module: frmLeave \n Process: dgLeave_DoubleClick \n\n Detailed Error: " + ex.ToString();
			//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
			//	}
			//}
			//else
			//{
			//	try
			//	{
			//		var modLeave = new frmModLeave();
			//		modLeave.txtEmpID.Text = empID;
			//		leave.FillUpSupportLeaveForm(empID, modLeave.txtEmployeeName, modLeave.txtPosition, modLeave.txtEmploymentStatus, EmpName);
			//		leave.FillUpLeaveFields(dgLeave, modLeave.lblLeaveID, modLeave.txtEmployeeName, modLeave.dtpStartdate, modLeave.dtpEndDate, modLeave.txtReason, modLeave.rdoWPay, modLeave.rdoWOPay, modLeave.rdoSick, modLeave.rdoVacation, modLeave.rdoPaternity, modLeave.rdoMaternity, modLeave.rdoBirthday, modLeave.rdoBereavement, modLeave.cmbApproval, modLeave.txtRemarks, EmpName);
			//		if (accessLevel == "Management")
			//		{
			//			modLeave.ReadOnlyAccess();
			//			modLeave.btnSaveUpdate.Visible = true;
			//			///modLeave.btnDelete.Enabled = false;
			//			///modLeave.
			//		}
			//		modLeave.EmpName = EmpName;
			//		modLeave.grpApprovalStatus.Enabled = true;
			//		modLeave.btnSaveUpdate.Text = "Update";
			//		modLeave.ShowDialog();
			//		//cmbFilterName.Text = EmpName;
			//		ShowLeaveList();
			//	}
			//	catch (Exception ex)
			//	{
			//		var ErrorMessage = ex.Message + "\n\n Name: " + EmpName + "\n Module: frmLeave \n Process: dgLeave_DoubleClick \n\n Detailed Error: " + ex.ToString();
			//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
			//	}
			//
			//}
		}



		private void cmbFilterName_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			ShowLeaveList();
		}

		private void cmbFilterStatus_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
		{
			ShowLeaveList();
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			ShowLeaveList();
		}
	}
}