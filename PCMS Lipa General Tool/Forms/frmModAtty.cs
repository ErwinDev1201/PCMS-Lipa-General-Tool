using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModAtty : Telerik.WinControls.UI.RadForm
	{

		private static readonly Notification notif = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly Attorney atty = new();
		public string whatAttorney;
		public string empName;

		public frmModAtty()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{
			txtAttyName.Clear();
			txtRemarks.Clear();
			txtEmailAdd.Clear();
			txtFaxNo.Clear();
			txtPhoneNo.Clear();
			txtRemarks.Clear();

			txtAttyName.Enabled = true;
			txtRemarks.Enabled = true;
			txtEmailAdd.Enabled = true;
			txtFaxNo.Enabled = true;
			txtPhoneNo.Enabled = true;
			txtRemarks.Enabled = true;
			btnDelete.Enabled = true;
			btnUpdateSave.Enabled = true;
			atty.GetDBID(out string ID, empName);
			txtIntID.Text = ID;

		}

		private void DisableInput()
		{
			txtAttyName.Enabled = false;
			txtRemarks.Enabled = false;
			txtEmailAdd.Enabled = false;
			txtFaxNo.Enabled = false;
			txtPhoneNo.Enabled = false;
			txtRemarks.Enabled = false;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;
		}

		


		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bool isSuccess = atty.AttorneyDBRequest(
						"Update", 
						txtIntID.Text, 
						cmbAttyType.Text, 
						txtAttyName.Text, 
						txtPhoneNo.Text, 
						txtFaxNo.Text, 
						txtEmailAdd.Text, 
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
				bool isSuccess = atty.AttorneyDBRequest(
						"Create",
						txtIntID.Text,
						cmbAttyType.Text,
						txtAttyName.Text,
						txtPhoneNo.Text,
						txtFaxNo.Text,
						txtEmailAdd.Text,
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
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				bool isSuccess = atty.AttorneyDBRequest(
						"Delete",
						txtIntID.Text,
						cmbAttyType.Text,
						txtAttyName.Text,
						txtPhoneNo.Text,
						txtFaxNo.Text,
						txtEmailAdd.Text,
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

		private void frmModifyAtty_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
