using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyAtty : Telerik.WinControls.UI.RadForm
	{

		private readonly Attorney atty = new();
		private readonly CommonTask task = new();
		private const string Def = @"SELECT MAX(NO+1) FROM [DEF ATTORNEY INFO]";
		private const string App = @"SELECT MAX(NO+1) FROM [APP ATTORNEY INFO]";
		public string whatAttorney;
		public string empName;

		public frmModifyAtty()
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
			GetDBID();

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
			GetDBID();
		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "AttyInfo", txtIntID, null, "ATTY-");
		}


		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					atty.AttorneyDBRequest("Update", txtIntID.Text, cmbAttyType.Text, txtAttyName.Text, txtPhoneNo.Text, txtFaxNo.Text, txtEmailAdd.Text, txtRemarks.Text, empName);
				}
			}
			else
			{
				atty.AttorneyDBRequest("Create", txtIntID.Text, cmbAttyType.Text, txtAttyName.Text, txtPhoneNo.Text, txtFaxNo.Text, txtEmailAdd.Text, txtRemarks.Text, empName);
			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				atty.AttorneyDBRequest("Delete", txtIntID.Text, cmbAttyType.Text, txtAttyName.Text, txtPhoneNo.Text, txtFaxNo.Text, txtEmailAdd.Text, txtRemarks.Text, empName);
				ClearData();
				Close();
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
