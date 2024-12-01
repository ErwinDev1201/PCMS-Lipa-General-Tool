
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModMPN : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
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

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "MPNInfoSeq", txtIntID, null, "MPN-");
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Save")
			{
				mpn.MPNDBRequest(
					"Create",
					txtIntID,
					txtInsuranceName,
					txtMPNName,
					txtMPNUserName,
					txtPassword,
					txtWebLink,
					txtRemarks,
					empName
				);
				ClearData();
			}
			else if (RadMessageBox.Show(
				"Are you sure you want to update this record?",
				"Confirmation",
				MessageBoxButtons.YesNo,
				RadMessageIcon.Question
			) == DialogResult.Yes)
			{
				mpn.MPNDBRequest(
					"Update",
					txtIntID,
					txtInsuranceName,
					txtMPNName,
					txtMPNUserName,
					txtPassword,
					txtWebLink,
					txtRemarks,
					empName
				);
				ClearData();
			}

			Close();

		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				mpn.MPNDBRequest("Delete", txtIntID, txtInsuranceName, txtMPNName, txtMPNUserName, txtPassword, txtWebLink, txtRemarks, empName);
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
