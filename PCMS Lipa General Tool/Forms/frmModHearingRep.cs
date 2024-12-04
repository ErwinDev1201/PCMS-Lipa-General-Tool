using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModHearingRep : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly HearingRep hearing = new();
		//private const string Sql = @"SELECT MAX(NO+1) FROM [HEARING REP LIST]";		
		public string empName;

		public frmModHearingRep()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{
			txtIntID.Clear();
			txtBoard.Clear();
			txtRemarks.Clear();
			txtEmailAdd.Clear();
			txtPhoneNo.Clear();
			txtRemarks.Clear();
			txtHearingRep.Clear();

			txtIntID.Enabled = true;
			txtBoard.ReadOnly = true;
			txtBoard.Enabled = true;
			txtRemarks.Enabled = true;
			txtEmailAdd.Enabled = true;
			txtPhoneNo.Enabled = true;
			txtRemarks.Enabled = true;
			txtHearingRep.Enabled = true;
			btnUpdateSave.Enabled = true;
			btnDelete.Enabled = true;
		}

		private void DisableInput()
		{
			txtIntID.Enabled = false;
			txtBoard.Enabled = false;
			txtRemarks.Enabled = false;
			txtEmailAdd.Enabled = false;
			txtPhoneNo.Enabled = false;
			txtRemarks.Enabled = false;
			txtHearingRep.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnDelete.Enabled = false;
		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "HearingRepSeq", txtIntID, null, "EP-"); ;
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				hearing.HRRepDBRequest("Delete", txtIntID.Text, txtBoard.Text, txtHearingRep.Text, txtEmailAdd.Text, txtPhoneNo.Text, txtRemarks.Text, empName);
			}
			ClearData();
			Close();
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					hearing.HRRepDBRequest("Update", txtIntID.Text, txtBoard.Text, txtHearingRep.Text, txtEmailAdd.Text, txtPhoneNo.Text, txtRemarks.Text, empName);
				}
			}
			else
			{
				hearing.HRRepDBRequest("Create", txtIntID.Text, txtBoard.Text, txtHearingRep.Text, txtEmailAdd.Text, txtPhoneNo.Text, txtRemarks.Text, empName);
				
			}
			ClearData();
			Close(); ;
		}

		private void frmModHearingRep_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}

}
