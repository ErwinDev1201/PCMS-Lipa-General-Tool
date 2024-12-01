using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmLeave : Telerik.WinControls.UI.RadForm
	{
		private readonly Leave leave = new();
		private readonly User user = new();
		private readonly CommonTask task = new();
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
			user.FillComboEmp(cmbFilterName, EmpName);
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
				accessLevel = accessLevel,
			};
			modleave.btnDelete.Visible = false;
			modleave.btnCancel.Location = new System.Drawing.Point(880, 223);
			modleave.dtpEndDate.Text = DateTime.Now.AddDays(1).ToString();
			modleave.dtpStartdate.Text = DateTime.Now.ToString();
			modleave.txtEmpID.Text = empID;
			modleave.GetDBListID();
			leave.FillUpSupportLeaveForm(modleave.txtEmpID, modleave.txtEmployeeName, modleave.txtPosition, modleave.txtEmploymentStatus, EmpName);
			modleave.ShowDialog();
			ShowLeaveList();
			//ShowAdjInfo();

		}
		//Bug: The createID() method is not being called.

		private void frmLeave_Load(object sender, EventArgs e)
		{
			dgLeave.BestFitColumns(BestFitColumnMode.AllCells);
			//this.dgAdjusterInfo.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
			dgLeave.ReadOnly = true;
		}

		private void frmLeave_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void dgLeave_DoubleClick(object sender, EventArgs e)
		{
			var modLeave = new frmModLeave
			{
				txtEmpID = { Text = empID }
			};

			leave.FillUpLeaveFields(
				dgLeave,
				modLeave.lblLeaveID,
				modLeave.txtEmpID,
				modLeave.txtEmployeeName,
				modLeave.dtpStartdate,
				modLeave.dtpEndDate,
				modLeave.txtReason,
				modLeave.rdoWPay,
				modLeave.rdoWOPay,
				modLeave.rdoSick,
				modLeave.rdoVacation,
				modLeave.rdoPaternity,
				modLeave.rdoMaternity,
				modLeave.rdoBirthday,
				modLeave.rdoBereavement,
				modLeave.cmbApproval,
				modLeave.txtRemarks,
				EmpName
			);
			leave.FillUpSupportLeaveForm(modLeave.txtEmpID, modLeave.txtEmployeeName, modLeave.txtPosition, modLeave.txtEmploymentStatus, EmpName);


			if (accessLevel is "User" or "Power User")
			{
				modLeave.ReadOnlyAccess();
			}
			else
			{
				if (accessLevel == "Management")
				{
					modLeave.ReadOnlyAccess();
					modLeave.btnSaveUpdate.Visible = true;
				}

				modLeave.grpApprovalStatus.Enabled = true;
				modLeave.btnSaveUpdate.Text = "Update";
			}

			modLeave.EmpName = EmpName;
			modLeave.ShowDialog();

			if (accessLevel is "User" or "Power User")
			{
				cmbFilterName.Text = EmpName;
			}

			ShowLeaveList();
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