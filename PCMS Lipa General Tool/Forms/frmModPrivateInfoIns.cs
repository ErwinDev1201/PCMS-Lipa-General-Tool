using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModPrivateInfoIns : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
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
			string nextSequence = db.GetSequenceNo("InsuranceInfoSeq", "INS-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBID", empName, "frmModPrivaateIns", "N/A", ex);
			}
			///db.GetSequenceNo("textbox", "InsuranceInfoSeq", txtIntID.Text, null, "INS-");
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
					bool isSuccess = insurance.InsuranceInfoDBRequest(
						"Update",
						txtIntID.Text,
						txtInsuranceName.Text,
						txtInsCode.Text,
						txtInsuranceAddress.Text,
						txtPayerID.Text,
						txtRemarks.Text,
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
				};
			}
			else
			{
				bool isSuccess = insurance.InsuranceInfoDBRequest(
						"Create",
						txtIntID.Text,
						txtInsuranceName.Text,
						txtInsCode.Text,
						txtInsuranceAddress.Text,
						txtPayerID.Text,
						txtRemarks.Text,
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

			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = insurance.InsuranceInfoDBRequest(
						"Delete",
						txtIntID.Text,
						txtInsuranceName.Text,
						txtInsCode.Text,
						txtInsuranceAddress.Text,
						txtPayerID.Text,
						txtRemarks.Text,
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