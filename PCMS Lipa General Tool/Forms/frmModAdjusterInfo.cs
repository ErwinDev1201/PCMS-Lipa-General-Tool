using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModAdjusterInfo : Telerik.WinControls.UI.RadForm
	{
		private static readonly Error error = new();
		private static readonly FEWinForm fe = new();
		private readonly Database db = new();
		private readonly Adjuster adj = new();
		public string txtID;
		public string empName;

		public frmModAdjusterInfo()
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
			adj.GetDBID(out string intID, empName);
			txtIntID.Text = intID;
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

		

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				{
					bool isSuccess = adj.AdjusterDBRequest(
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
					empName,
					out string message
					);
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
		}  //

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInput();
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record??", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					bool isSuccess = adj.AdjusterDBRequest(
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
					empName,
					out string message
					);
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
				{
					bool isSuccess = adj.AdjusterDBRequest(
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
					empName,
					out string message
					);
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