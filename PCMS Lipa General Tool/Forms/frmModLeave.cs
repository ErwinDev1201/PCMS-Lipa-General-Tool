using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModLeave : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Leave leave = new();
		public string accessLevel;
		public string EmpName;
		public string leaveID;

		public frmModLeave()
		{
			InitializeComponent();
			txtEmpID.ReadOnly = true;
			txtEmployeeName.ReadOnly = true;
			txtEmploymentStatus.ReadOnly = true;
			txtPosition.ReadOnly = true;
			grpApprovalStatus.Enabled = false;
			cmbApproval.Text = "FOR APPROVAL";
			//lblLeaveID.Text = "Ready";
			//ShowInsuranceInfo();
		}

		//private void FillupEmpInfo()
		//{
		//	//mainProcess.FillUpSupportLeaveForm(txtEmpID, txtEmployeeName, txtPosition, txtEmploymentStatus, EmpName);
		//}

		private void DefaulSet()
		{
			txtEmpID.Enabled = true;
			txtEmployeeName.Enabled = true;
			txtEmploymentStatus.Enabled = true;
			txtPosition.Enabled = true;
			txtReason.Enabled = true;
			txtRemarks.Enabled = true;
			dtpEndDate.Enabled = true;
			dtpStartdate.Enabled = true;
			rdoBereavement.Enabled = true;
			rdoBirthday.Enabled = true;
			rdoMaternity.Enabled = true;
			rdoPaternity.Enabled = true;
			rdoBirthday.Enabled = true;
			rdoSick.Enabled = true;
			rdoVacation.Enabled = true;
			rdoWOPay.Enabled = true;
			rdoWPay.Enabled = true;
			btnCancel.Enabled = true;
			btnDelete.Enabled = true;
			btnSaveUpdate.Enabled = true;
			lblLeaveID.Enabled = true;
		}


		private void DisableInput()
		{
			txtEmpID.Enabled = false;
			txtEmployeeName.Enabled = false;
			txtEmploymentStatus.Enabled = false;
			txtPosition.Enabled = false;
			txtReason.Enabled = false;
			txtRemarks.Enabled = false;
			dtpEndDate.Enabled = false;
			dtpStartdate.Enabled = false;
			rdoBereavement.Enabled = false;
			rdoBirthday.Enabled = false;
			rdoMaternity.Enabled = false;
			rdoPaternity.Enabled = false;
			rdoBirthday.Enabled = false;
			rdoSick.Enabled = false;
			rdoVacation.Enabled = false;
			rdoWOPay.Enabled = false;
			rdoWPay.Enabled = false;
			btnCancel.Enabled = false;
			btnDelete.Enabled = false;
			btnSaveUpdate.Enabled = false;
			lblLeaveID.Enabled = false;
		}

		private void btnSaveUpdate_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (!ValidateInputs())
			{
				return;
			}

			string paymentOption = rdoWOPay.IsChecked ? "Without Pay" : "With Pay";
			string typeOfLeave = DetermineLeaveType();
			string action = btnSaveUpdate.Text == "Update" ? "update this leave" : "file this leave";
			string operation = btnSaveUpdate.Text == "Update" ? "Update" : "Create";

			if (ConfirmAction(action, txtReason.Text, dtpStartdate.Text, dtpEndDate.Text, paymentOption, typeOfLeave, cmbApproval.Text))
			{
				leave.LeaveFiling(operation, lblLeaveID, txtEmpID, txtEmployeeName, dtpStartdate, dtpEndDate, paymentOption, typeOfLeave, txtReason, cmbApproval, txtRemarks, EmpName, txtPosition);
			}
			DefaulSet();
			Close();

			//if (!rdoWPay.IsChecked && !rdoWOPay.IsChecked)
			//{
			//	RadMessageBox.Show("Payment Option is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
			//}
			//else
			//{
			//	if (!rdoBirthday.IsChecked && !rdoPaternity.IsChecked && !rdoBereavement.IsChecked && !rdoSick.IsChecked && !rdoVacation.IsChecked && !rdoMaternity.IsChecked)
			//	{
			//		RadMessageBox.Show("Type of Leave is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
			//	}
			//	else
			//	{
			//		if (txtReason.Text == "")
			//		{
			//			RadMessageBox.Show("Reason is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
			//		}
			//		else
			//		{
			//			string paymentoption;
			//			string typeofLeave;
			//			if (!rdoWOPay.IsChecked)
			//			{
			//				paymentoption = "With Pay";
			//			}
			//			else
			//			{
			//				paymentoption = "Without Pay";
			//			}
			//			if (rdoBereavement.IsChecked)
			//			{
			//				typeofLeave = "Bereavement";
			//			}
			//			else if (rdoPaternity.IsChecked)
			//			{
			//				typeofLeave = "Paternity";
			//			}
			//			else if (rdoBirthday.IsChecked)
			//			{
			//				typeofLeave = "Birthday";
			//			}
			//			else if (rdoMaternity.IsChecked)
			//			{
			//				typeofLeave = "Maternity";
			//			}
			//			else if (rdoSick.IsChecked)
			//			{
			//				typeofLeave = "Sick";
			//			}
			//			else
			//			{
			//				typeofLeave = "Vacation";
			//			}
			//			if (btnSaveUpdate.Text == "Update")
			//			{
			//				if (DialogResult.Yes == RadMessageBox.Show("Are you sure want to update this leave? \n\n" +
			//					"Reason: " + txtReason.Text + "\n" +
			//					"Start Date: " + dtpStartdate.Text + "\n" +
			//					"End Date: " + dtpEndDate.Text + "\n" +
			//					"Payment Option: " + paymentoption + "\n" +
			//					"Type of Leave: " + typeofLeave + "\n" +
			//					"Approval Status: " + cmbApproval.Text, "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			//				{
			//					leave.LeaveFiling("Patch", lblLeaveID, txtEmployeeName, dtpStartdate, dtpEndDate, paymentoption, typeofLeave, txtReason, cmbApproval, txtRemarks, txtPosition, EmpName);
			//				}
			//			}
			//			else
			//			{
			//				if (DialogResult.Yes == RadMessageBox.Show("Are you sure want to file this leave? \n\n" +
			//				"Reason: " + txtReason.Text + "\n" +
			//				"Start Date: " + dtpStartdate.Text + "\n" +
			//				"End Date: " + dtpEndDate.Text + "\n" +
			//				"Payment Option: " + paymentoption + "\n" +
			//				"Type of Leave: " + typeofLeave + "\n\n" +
			//				"Once you file it you cannot delete or update it. Only Administrator can update or delete it.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			//				{
			//					leave.LeaveFiling("Create", lblLeaveID, txtEmployeeName, dtpStartdate, dtpEndDate, paymentoption, typeofLeave, txtReason, cmbApproval, txtRemarks, txtPosition, EmpName);
			//				}
			//
			//			}
			//			Close();
			//			//ShowInsuranceInfo();
			//
			//		}
			//	}
			//}
		}

		private bool ValidateInputs()
		{
			if (!rdoWPay.IsChecked && !rdoWOPay.IsChecked)
			{
				RadMessageBox.Show("Payment Option is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			if (!IsLeaveTypeSelected())
			{
				RadMessageBox.Show("Type of Leave is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			if (string.IsNullOrEmpty(txtReason.Text))
			{
				RadMessageBox.Show("Reason is required", "Warning", MessageBoxButtons.OK, RadMessageIcon.Info);
				return false;
			}

			return true;
		}

		private bool IsLeaveTypeSelected()
		{
			return rdoBirthday.IsChecked || rdoPaternity.IsChecked || rdoBereavement.IsChecked || rdoSick.IsChecked || rdoVacation.IsChecked || rdoMaternity.IsChecked;
		}

		private string DetermineLeaveType()
		{
			if (rdoBereavement.IsChecked) return "Bereavement";
			if (rdoPaternity.IsChecked) return "Paternity";
			if (rdoBirthday.IsChecked) return "Birthday";
			if (rdoMaternity.IsChecked) return "Maternity";
			if (rdoSick.IsChecked) return "Sick";
			return "Vacation";
		}

		private bool ConfirmAction(string action, string reason, string startDate, string endDate, string paymentOption, string typeOfLeave, string approvalStatus)
		{
			string confirmationMessage = $"Are you sure you want to {action}?\n\n" +
										 $"Reason: {reason}\n" +
										 $"Start Date: {startDate}\n" +
										 $"End Date: {endDate}\n" +
										 $"Payment Option: {paymentOption}\n" +
										 $"Type of Leave: {typeOfLeave}\n" +
										 $"Approval Status: {approvalStatus}\n\n" +
										 (action == "file this leave" ? "Once filed, only an Administrator can update or delete it." : string.Empty);

			return RadMessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question) == DialogResult.Yes;
		}

		public void ReadOnlyAccess()
		{
			txtEmpID.ReadOnly = true;
			txtEmployeeName.ReadOnly = true;
			txtEmploymentStatus.ReadOnly = true;
			txtPosition.ReadOnly = true;
			grpLeaveDate.Enabled = false;
			grpPaymentOption.Enabled = false;
			grpTypeofLeave.Enabled = false;
			txtReason.IsReadOnly = true;
			grpApprovalStatus.Enabled = false;
			btnCancel.Visible = false;
			btnDelete.Visible = false;
			btnSaveUpdate.Visible = false;
		}

		public void GetDBListID()
		{
			task.GetSequenceNo("label", "LeaveSeq", null, lblLeaveID, "LV-");
		}

		private void frmModLeave_Load(object sender, EventArgs e)
		{
			//FillupEmpInfo();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{

			if (DialogResult.Yes == RadMessageBox.Show("Are you sure want to delete this leave?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				leave.LeaveFiling("Delete", lblLeaveID, txtEmpID, txtEmployeeName, dtpStartdate, dtpEndDate, null, null, txtReason, cmbApproval, txtRemarks, EmpName, null);
			}
			Close();
		}

		private void dtpEndDate_ValueChanged(object sender, EventArgs e)
		{
			if (dtpEndDate.Value.Date < dtpStartdate.Value.Date)
			{
				RadMessageBox.Show("Start date must be a date before end date", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}

		private void dtpStartdate_ValueChanged(object sender, EventArgs e)
		{
			//if (dtpEndDate.Value.Date < dtpStartdate.Value.Date)
			//{
			//	RadMessageBox.Show("Start date must be a date before end date", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			//}
			//dtpEndDate.Text = dtDateTime.Now.AddDays(1).ToString();
			dtpEndDate.Text = dtpStartdate.Text;
		}
	}
}