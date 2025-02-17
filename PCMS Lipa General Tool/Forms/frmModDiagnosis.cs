
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModDiagnosis : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly Diagnosis dx = new();
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;


		public frmModDiagnosis()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		

		private void ClearData()
		{
			txtIntID.Clear();
			txtRemarks.Clear();
			txtICD10.Clear();
			txtICD9.Clear();
			txtDiagnosis.Clear();
			txtBodyPart.Clear();

			txtIntID.Enabled = true;
			txtRemarks.Enabled = true;
			txtICD10.Enabled = true;
			txtICD9.Enabled = true;
			txtDiagnosis.Enabled = true;
			txtBodyPart.Enabled = true;
			btnUpdateSave.Enabled = true;
			btnDelete.Enabled = true;
			dx.GetDBID(out string ID, empName);
			txtIntID.Text = ID;
		}

		private void DisableINput()
		{
			txtIntID.Enabled = false;
			txtRemarks.Enabled = false;
			txtICD10.Enabled = false;
			txtICD9.Enabled = false;
			txtDiagnosis.Enabled = false;
			txtBodyPart.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnDelete.Enabled = false;

		}


		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableINput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bool isSuccess = dx.DiagnosisDBRequest(
						"Update", 
						txtIntID.Text, 
						txtICD10.Text, 
						txtICD9.Text, 
						txtDiagnosis.Text, 
						txtBodyPart.Text, 
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
			}
			else
			{
				bool isSuccess = dx.DiagnosisDBRequest(
						"Create",
						txtIntID.Text,
						txtICD10.Text,
						txtICD9.Text,
						txtDiagnosis.Text,
						txtBodyPart.Text,
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


		// **Extracted common logic to avoid duplication**
		private bool ProcessDiagnosisRequest(string operationType, out string message)
		{
			try
			{
				return dx.DiagnosisDBRequest(
					operationType,
					txtIntID.Text,
					txtICD10.Text,
					txtICD9.Text,
					txtDiagnosis.Text,
					txtBodyPart.Text,
					txtRemarks.Text,
					empName,
					out message);
			}
			catch (Exception ex)
			{
				message = $"Database error: {ex.Message}";
				return false;
			}
		}


		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableINput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = dx.DiagnosisDBRequest(
						"Delete",
						txtIntID.Text,
						txtICD10.Text,
						txtICD9.Text,
						txtDiagnosis.Text,
						txtBodyPart.Text,
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
				Close();
			}
		}

		private void frmModifyDiagnosis_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
