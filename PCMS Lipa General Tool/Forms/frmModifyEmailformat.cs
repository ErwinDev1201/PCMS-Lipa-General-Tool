using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyEmailformat : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly EmailFormatDB emailDB = new();
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;


		public frmModifyEmailformat()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}


		public void GetDBID()
		{
			string nextSequence = task.GetSequenceNo("EmailFormatSeq", "EF-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				task.LogError("GetDBID", empName, "frmModifyEmailFormat", "N/A", ex);
			}
			//task.GetSequenceNo("textbox", "EmailFormatSeq", txtIntID.Text, null, "EF-");
		}

		private void ClearData()
		{
			txtIntID.Clear();
			txtInsuranceName.Clear();
			txtEmailFormat.Clear();
			txtRemarks.Clear();

			txtInsuranceName.Enabled = true;
			txtEmailFormat.Enabled = true;
			txtRemarks.Enabled = true;
			btnSave.Enabled = true;
			GetDBID();
		}


		private void DsisableInput()
		{
			txtInsuranceName.Enabled = false;
			txtEmailFormat.Enabled = false;
			txtRemarks.Enabled = false;
			btnSave.Enabled = false;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

			if (txtEmailFormat.Text == "" || txtInsuranceName.Text == "")
			{
				RadMessageBox.Show("Cannot save with out value in Email Textbox and Insurance Textbox", "Warning", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			else
			{
				emailDB.EmailFormatDBRequest("Create", txtIntID.Text, txtInsuranceName.Text, txtEmailFormat.Text, txtRemarks.Text, empName);
				ClearData();
			}
			Close();
		}

		private void frmModifyEmailformat_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
