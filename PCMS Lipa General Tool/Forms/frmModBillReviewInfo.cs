using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModBillReviewInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly BillReview bill = new();
		private static readonly Error error = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		public string empName;

		public frmModBillReviewInfo()
		{
			InitializeComponent();
			//mainProcess.CreateDbId(txtIntID, _sql, @"WC-");
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{

			txtBRFaxNo.Enabled = true;
			txtPhoneNo.Enabled = true;
			txtFax.Enabled = true;
			txtInsuranceName.Enabled = true;
			txtPhoneNo.Enabled = true;
			txtOnlineEmail.Enabled = true;
			txtRemarks.Enabled = true;
			txtURPhone.Enabled = true;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;

			txtBRFaxNo.Clear();
			txtPhoneNo.Clear();
			txtFax.Clear();
			txtInsuranceName.Clear();
			txtPhoneNo.Clear();
			txtOnlineEmail.Clear();
			txtRemarks.Clear();
			txtURFaxNo.Clear();
			txtURPhone.Clear();
		}

		private void DisableInput()
		{
			txtBRFaxNo.Enabled = false;
			txtPhoneNo.Enabled = false;
			txtFax.Enabled = false;
			txtInsuranceName.Enabled = false;
			txtPhoneNo.Enabled = false;
			txtOnlineEmail.Enabled = false;
			txtRemarks.Enabled = false;
			txtURPhone.Enabled = false;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;
		}


		

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = bill.BillReviewDBRequest(
					"Delete",
					txtIntID.Text, 
					txtInsuranceName.Text,
					txtPhoneNo.Text, 
					txtFax.Text, 
					txtURPhone.Text, 
					txtURFaxNo.Text, 
					txtBRPhoneNo.Text, 
					txtBRFaxNo.Text, 
					txtOnlineEmail.Text, 
					txtRemarks.Text, 
					empName, out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
			}
			ClearData();
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bool isSuccess = bill.BillReviewDBRequest(
					"Update",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtPhoneNo.Text,
					txtFax.Text,
					txtURPhone.Text,
					txtURFaxNo.Text,
					txtBRPhoneNo.Text,
					txtBRFaxNo.Text,
					txtOnlineEmail.Text,
					txtRemarks.Text,
					empName, out string message);
					if (isSuccess)
					{
						fe.SendToastNotifDesktop(message, "Success");
					}
					else
					{
						fe.SendToastNotifDesktop(message, "Failed");
					}
				}
			}
			else
			{
				bool isSuccess = bill.BillReviewDBRequest(
					"Create",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtPhoneNo.Text,
					txtFax.Text,
					txtURPhone.Text,
					txtURFaxNo.Text,
					txtBRPhoneNo.Text,
					txtBRFaxNo.Text,
					txtOnlineEmail.Text,
					txtRemarks.Text,
					empName, out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
			}
			ClearData();
			Close();
		}

		private void frmModBillReviewInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void radPanel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
