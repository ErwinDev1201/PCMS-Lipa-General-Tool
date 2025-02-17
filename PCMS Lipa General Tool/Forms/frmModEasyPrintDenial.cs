using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Web.Caching;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModEasyPrintDenial : Telerik.WinControls.UI.RadForm
	{
		private static readonly Notification notif = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private static readonly EasyPrint easyPrint = new();
		public string empName;

		public frmModEasyPrintDenial()
		{
			InitializeComponent();
			txtEPCode.Focus();
			txtInsuranceName.Text = "Medicare";
			txtInsuranceName.Enabled = false;
			txtIntID.ReadOnly = true;
		}

		

		public void ClearData()
		{
			txtIntID.Clear();
			txtInsuranceName.Clear();
			txtRemarks.Clear();
			txtEPCode.Clear();
			txtDenialDescrption.Clear();
			txtPossibleSolution.Clear();

			txtIntID.Enabled = true;
			txtInsuranceName.Enabled = true;
			txtRemarks.Enabled = true;
			txtEPCode.Enabled = true;
			txtDenialDescrption.Enabled = true;
			txtPossibleSolution.Enabled = true;
			btnDelete.Enabled = true;
			btnUpdateSave.Enabled = true;
			//mainProcess.CreateDbId(txtIntID, Sql, @"EP-");
		}

		private void DisableInput()
		{
			txtIntID.Enabled = false;
			txtInsuranceName.Enabled = false;
			txtRemarks.Enabled = false;
			txtEPCode.Enabled = false;
			txtDenialDescrption.Enabled = false;
			txtPossibleSolution.Enabled = false;
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
					bool isSuccess = easyPrint.EPDenialDBRequest(
						"Update", 
						txtIntID.Text, 
						txtEPCode.Text, 
						txtInsuranceName.Text, 
						txtDenialDescrption.Text, 
						txtPossibleSolution.Text, 
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

				bool isSuccess = easyPrint.EPDenialDBRequest(
					"Create",
					txtIntID.Text,
					txtEPCode.Text,
					txtInsuranceName.Text,
					txtDenialDescrption.Text,
					txtPossibleSolution.Text,
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

				bool isSuccess = easyPrint.EPDenialDBRequest(
					"Delete",
					txtIntID.Text,
					txtEPCode.Text,
					txtInsuranceName.Text,
					txtDenialDescrption.Text,
					txtPossibleSolution.Text,
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

		private void frmModEasyPrintDenial_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
