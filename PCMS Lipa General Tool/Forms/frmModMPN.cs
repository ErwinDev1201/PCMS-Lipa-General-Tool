
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModMPN : Telerik.WinControls.UI.RadForm
	{
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly MPN mpn = new();
		public string txtID;
		public string empName;

		public frmModMPN()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		private void ClearData()
		{
			//txtID.Clear();
			txtInsuranceName.Clear();
			txtRemarks.Clear();
			txtMPNUserName.Clear();
			txtPassword.Clear();
			txtMPNName.Clear();
			txtWebLink.Clear();
			txtRemarks.Clear();

		}

		

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			//string operation = btnUpdateSave.Text == "Update" ? "Update" : "Create";
			if (btnUpdateSave.Text == "Save")
			{
				bool isSuccess = mpn.MPNDBRequest(
					"Create",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtMPNName.Text,
					txtMPNUserName.Text,
					txtPassword.Text,
					txtWebLink.Text,
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
			else if (RadMessageBox.Show(
				"Are you sure you want to update this record?",
				"Confirmation",
				MessageBoxButtons.YesNo,
				RadMessageIcon.Question
			) == DialogResult.Yes)
			{

				bool isSuccess = mpn.MPNDBRequest(
					"Update",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtMPNName.Text,
					txtMPNUserName.Text,
					txtPassword.Text,
					txtWebLink.Text,
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

				bool isSuccess = mpn.MPNDBRequest(
					"Delete",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtMPNName.Text,
					txtMPNUserName.Text,
					txtPassword.Text,
					txtWebLink.Text,
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

		private void frmModMPN_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
