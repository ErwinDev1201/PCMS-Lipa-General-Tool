using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
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

		}

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "AdjusterInfo", txtIntID, null, "ADJ -");
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				adj.AdjusterDBRequest("Delete", txtIntID, txtInsuranceName, txtAdjusterName, txtphoneno, txtExtension, txtFax, txtEmailAdd, txtSupervisor, txtRemarks, empName);
				ClearData();
				Close();
			}
		}  //

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record??", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					adj.AdjusterDBRequest("Update", txtIntID, txtInsuranceName, txtAdjusterName, txtphoneno, txtExtension, txtFax, txtEmailAdd, txtSupervisor, txtRemarks, empName);
				}

			}
			else
			{
				adj.AdjusterDBRequest("Create", txtIntID, txtInsuranceName, txtAdjusterName, txtphoneno, txtExtension, txtFax, txtEmailAdd, txtSupervisor, txtRemarks, empName);

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