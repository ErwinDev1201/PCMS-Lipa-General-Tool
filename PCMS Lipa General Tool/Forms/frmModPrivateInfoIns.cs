using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModPrivateInfoIns : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Insurance insurance = new();
		public string empName;

		public frmModPrivateInfoIns()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{
			txtIntID.Clear();
			txtInsCode.Clear();
			txtRemarks.Clear();
			txtPayerID.Clear();
			txtInsuranceName.Clear();
			txtInsuranceAddress.Clear();
		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "InsuranceInfoSeq", txtIntID, null, "INS-");
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Update")
			{
				if (RadMessageBox.Show(
					"Are you sure you want to update this record?",
					"Confirmation",
					MessageBoxButtons.YesNo,
					RadMessageIcon.Question
				) == DialogResult.Yes)
				{
					insurance.InsuranceInfoDBRequest(
						"Update",
						txtIntID,
						txtInsuranceName,
						txtInsCode,
						txtInsuranceAddress,
						txtPayerID,
						txtRemarks,
						empName
					);
				}
			}
			else
			{
				insurance.InsuranceInfoDBRequest(
					"Create",
					txtIntID,
					txtInsuranceName,
					txtInsCode,
					txtInsuranceAddress,
					txtPayerID,
					txtRemarks,
					empName
				);
				
			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				insurance.InsuranceInfoDBRequest("Delete", txtIntID, txtInsuranceName, txtInsCode, txtInsuranceAddress, txtPayerID, txtRemarks, empName);
				ClearData();
				Close();
			}
		}

		private void frmModPrivateInfoIns_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}