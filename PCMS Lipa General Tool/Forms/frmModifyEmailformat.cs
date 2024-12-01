using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyEmailformat : Telerik.WinControls.UI.RadForm
	{
		private const string Sql = @"SELECT MAX(NO+1) FROM [ADJUSTERS EMAIL FORMAT]";
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
			task.GetSequenceNo("textbox", "EmailFormatSeq", txtIntID, null, "EF-");
		}

		private void ClearData()
		{
			txtIntID.Clear();
			txtInsuranceName.Clear();
			txtEmailFormat.Clear();
			txtRemarks.Clear();
		}
		private void btnSave_Click(object sender, EventArgs e)
		{

			if (txtEmailFormat.Text == "" || txtInsuranceName.Text == "")
			{
				RadMessageBox.Show("Cannot save with out value in Email Textbox and Insurance Textbox", "Warning", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			else
			{
				emailDB.EmailFormatDBRequest("Create", txtIntID, txtInsuranceName, txtEmailFormat, txtRemarks, empName);
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
