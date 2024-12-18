using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModifyAdjusterInfo : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly Adjuster adj = new();
		public string txtID;
		public string empName;

		public frmModifyAdjusterInfo()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;
		}

		public void ClearData()
		{
			txtInsuranceName.Clear();
			txtRemarks.Clear();
			txtEmailAdd.Clear();
			txtFax.Clear();
			txtphoneno.Clear();
			txtExtension.Clear();
			txtSupervisor.Clear();
			txtRemarks.Clear();
			txtAdjusterName.Clear();

			txtInsuranceName.Enabled = true;
			txtRemarks.Enabled = true;
			txtEmailAdd.Enabled = true;
			txtFax.Enabled = true;
			txtphoneno.Enabled = true;
			txtExtension.Enabled = true;
			txtSupervisor.Enabled = true;
			txtRemarks.Enabled = true;
			txtAdjusterName.Enabled = true;
			btnDelete.Enabled = true;
			btnUpdateSave.Enabled = true;
			GetDBID(); 
		}

		private void DisableInput()
		{
			txtInsuranceName.Enabled = false;
			txtRemarks.Enabled = false;
			txtEmailAdd.Enabled = false;
			txtFax.Enabled = false;
			txtphoneno.Enabled = false;
			txtExtension.Enabled = false;
			txtSupervisor.Enabled = false;
			txtRemarks.Enabled = false;
			txtAdjusterName.Enabled = false;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;
		}

		public void GetDBID()
		{
			string nextSequence = task.GetSequenceNo("AdjusterInfo", "ADJ-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					txtIntID.Text = nextSequence;
				}
			}
			catch (Exception ex)
			{
				task.LogError("GetDBID", empName, "frmModifyAdjusterInfo", "N/A", ex);
			}
			////task.GetSequenceNo("textbox", "AdjusterInfo", txtIntID.Text, null, "ADJ-");
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				{
					adj.AdjusterDBRequest(
					"Delete",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtAdjusterName.Text,
					txtphoneno.Text,
					txtExtension.Text,
					txtFax.Text,
					txtEmailAdd.Text,
					txtSupervisor.Text,
					txtRemarks.Text,
					empName
					);

				}
				ClearData();
				Close();
			}
		}  //

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record??", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					adj.AdjusterDBRequest(
					"Update",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtAdjusterName.Text,
					txtphoneno.Text,
					txtExtension.Text,
					txtFax.Text,
					txtEmailAdd.Text,
					txtSupervisor.Text,
					txtRemarks.Text,
					empName
					);

				}

			}
			else
			{
				{
					adj.AdjusterDBRequest(
					"Create",
					txtIntID.Text,
					txtInsuranceName.Text,
					txtAdjusterName.Text,
					txtphoneno.Text,
					txtExtension.Text,
					txtFax.Text,
					txtEmailAdd.Text,
					txtSupervisor.Text,
					txtRemarks.Text,
					empName
					);

				}

			}
			ClearData();
			Close();
		}

		private void btnUpdateSave_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void frmModifyAdjusterInfo_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}