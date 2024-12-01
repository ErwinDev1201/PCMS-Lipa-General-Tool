
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyDiagnosis : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Diagnosis dx = new();
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;


		public frmModifyDiagnosis()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "DiagSeq", txtIntID, null, "DX-");
		}

		private void ClearData()
		{
			txtIntID.Clear();
			txtRemarks.Clear();
			txtICD10.Clear();
			txtICD9.Clear();
			txtDiagnosis.Clear();
			txtBodyPart.Clear();

		}
		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					dx.DiagnosisDBRequest("Patch", txtIntID, txtICD10, txtICD9, txtDiagnosis, txtBodyPart, txtRemarks, empName);
				}
			}
			else
			{
				dx.DiagnosisDBRequest("Create", txtIntID, txtICD10, txtICD9, txtDiagnosis, txtBodyPart, txtRemarks, empName);
			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				dx.DiagnosisDBRequest("Delete", txtIntID, txtICD10, txtICD9, txtDiagnosis, txtBodyPart, txtRemarks, empName);
				ClearData();
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
