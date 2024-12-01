using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModEasyPrintDenial : Telerik.WinControls.UI.RadForm
	{
		private readonly CommonTask task = new();
		private readonly EasyPrint easyPrint = new();
		public string empName;

		public frmModEasyPrintDenial()
		{
			InitializeComponent();
			txtEPCode.Focus();
			txtInsuranceName.Text = "Medicare";
			txtInsuranceName.Enabled = false;
			txtIntID.ReadOnly = true;
		}

		public void GetDBID()
		{
			var query = "SELECT NEXT VALUE FOR EasyPrintSeq";
			task.GetSequenceNo("textbox", query, txtIntID, null, "EP-");
		}

		public void ClearData()
		{
			txtIntID.Clear();
			txtInsuranceName.Clear();
			txtRemarks.Clear();
			txtEPCode.Clear();
			txtDenialDescrption.Clear();
			txtPossibleSolution.Clear();
			//mainProcess.CreateDbId(txtIntID, Sql, @"EP-");
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{

			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					easyPrint.EPDenialDBRequest("Patch", txtIntID, txtEPCode, txtInsuranceName, txtDenialDescrption, txtPossibleSolution, txtRemarks, empName);
				}

			}
			else
			{
				easyPrint.EPDenialDBRequest("Create", txtIntID, txtEPCode, txtInsuranceName, txtDenialDescrption, txtPossibleSolution, txtRemarks, empName);

			}
			ClearData();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				easyPrint.EPDenialDBRequest("Delete", txtIntID, txtEPCode, txtInsuranceName, txtDenialDescrption, txtPossibleSolution, txtRemarks, empName);
				ClearData();
				Close();

			}
		}

		private void frmModEasyPrintDenial_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
