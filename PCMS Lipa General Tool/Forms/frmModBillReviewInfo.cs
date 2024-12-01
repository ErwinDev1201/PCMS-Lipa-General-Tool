using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModBillReviewInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly BillReview bill = new();
		public string empName;

		public frmModBillReviewInfo()
		{
			InitializeComponent();
			//mainProcess.CreateDbId(txtIntID, _sql, @"WC-");
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{
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


		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "BillReviewSeq", txtIntID, null, "BR-");
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bill.BillReviewDBRequest("Delete", txtIntID, txtInsuranceName, txtPhoneNo, txtFax, txtURPhone, txtURFaxNo, txtBRPhoneNo, txtBRFaxNo, txtOnlineEmail, txtRemarks, empName);
			}
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bill.BillReviewDBRequest("Update", txtIntID, txtInsuranceName, txtPhoneNo, txtFax, txtURPhone, txtURFaxNo, txtBRPhoneNo, txtBRFaxNo, txtOnlineEmail, txtRemarks, empName);
				}
				ClearData();
			}
			else
			{
				bill.BillReviewDBRequest("Create", txtIntID, txtInsuranceName, txtPhoneNo, txtFax, txtURPhone, txtURFaxNo, txtBRPhoneNo, txtBRFaxNo, txtOnlineEmail, txtRemarks, empName);
				ClearData();
			}
			Close();
		}

		private void frmModBillReviewInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
