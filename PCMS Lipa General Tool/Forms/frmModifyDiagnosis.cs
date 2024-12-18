
using PCMS_Lipa_General_Tool.Class;
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
			string nextSequence = task.GetSequenceNo("DiagSeq", "DX-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				task.LogError("GetDBID", empName, "frmDiagnosis", "N/A", ex);
			}
			/////task.GetSequenceNo("textbox", "DiagSeq", txtIntID.Text, null, "DX-");
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
			GetDBID();
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
					dx.DiagnosisDBRequest("Patch", txtIntID.Text, txtICD10.Text, txtICD9.Text, txtDiagnosis.Text, txtBodyPart.Text, txtRemarks.Text, empName);
				}
			}
			else
			{
				dx.DiagnosisDBRequest("Create", txtIntID.Text, txtICD10.Text, txtICD9.Text, txtDiagnosis.Text, txtBodyPart.Text, txtRemarks.Text, empName);
			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableINput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				dx.DiagnosisDBRequest("Delete", txtIntID.Text, txtICD10.Text, txtICD9.Text, txtDiagnosis.Text, txtBodyPart.Text, txtRemarks.Text, empName);
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
