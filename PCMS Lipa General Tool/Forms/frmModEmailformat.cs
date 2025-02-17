using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModEmailformat : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly EmailFormatDB emailDB = new();
		//private readonly MailSender mailSender = new MailSender();
		public string txtID;
		public string empName;


		public frmModEmailformat()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
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
			emailDB.GetDBID(out string ID, empName);
			txtIntID.Text = ID;
		}


		private void DsisableInput()
		{
			txtInsuranceName.Enabled = false;
			txtEmailFormat.Enabled = false;
			txtRemarks.Enabled = false;
			btnSave.Enabled = false;
		}

		private void EnableInput()
		{
			txtInsuranceName.Enabled = true;
			txtEmailFormat.Enabled = true;
			txtRemarks.Enabled = true;
			btnSave.Enabled = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

			if (txtEmailFormat.Text == "" || txtInsuranceName.Text == "")
			{
				RadMessageBox.Show("Cannot save with out value in Email Textbox and Insurance Textbox", "Warning", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			else
			{
				DsisableInput();
				bool isSuccess = emailDB.EmailFormatDBRequest(
					"Create", 
					txtIntID.Text, 
					txtInsuranceName.Text, 
					txtEmailFormat.Text, 
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
				EnableInput();
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
