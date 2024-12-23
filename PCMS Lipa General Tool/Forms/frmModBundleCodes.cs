using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModBundleCodes : Telerik.WinControls.UI.RadForm
	{
		//private readonly MailSender mailSender = new MailSender();
		private static readonly FEWinForm fe = new();
		private readonly Bundle bundle = new();
		public string empName;
		public string bundleOptions;
		//RadDesktopAlert alert = new();

		public frmModBundleCodes()
		{
			InitializeComponent();
			txtIntID.ReadOnly = true;

		}

		public void DefaultItem()
		{
			txtBundleCodes.Enabled = true;
			txtCPTCode.Enabled = true;
			txtDescription.Enabled = true;
			txtIntID.Enabled = true;
			txtRemarks.Enabled = true;
			rdoNo.Enabled = true;
			rdoYes.Enabled = true;
			btnDelete.Enabled = true;
			btnUpdateSave.Enabled = true;
			txtBundleCodes.Clear();
			txtCPTCode.Clear();
			txtDescription.Clear();
			txtIntID.ReadOnly = true;
			txtRemarks.Clear();
			bundle.GetDBID(out string ID, empName);
			txtIntID.Text = ID;
		}


		private void DisableInputs()
		{

			txtBundleCodes.Enabled = false;
			txtCPTCode.Enabled = false;
			txtDescription.Enabled = false;
			txtIntID.Enabled = false;
			txtRemarks.Enabled = false;
			rdoNo.Enabled = false;
			rdoYes.Enabled = false;
			btnDelete.Enabled = false;
			btnUpdateSave.Enabled = false;
		}

		

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInputs();

			// Determine the action type ("Update" or "Create") and the indicator ("Y" or "N")
			string actionType = btnUpdateSave.Text == "Update" ? "Update" : "Create";
			string indicator = rdoYes.IsChecked ? "Y" : "N";

			// Confirm update if action is "Update"
			if (actionType == "Update" &&
				DialogResult.Yes != RadMessageBox.Show("Would you like to go ahead and update this record?",
													   "Confirmation",
													   MessageBoxButtons.YesNo,
													   RadMessageIcon.Question))
			{
				return; // Exit if user does not confirm
			}

			// Perform the database request
			bool isSuccess = bundle.BundleCodesDBRequest(actionType,
										txtIntID.Text,
										txtCPTCode.Text,
										txtBundleCodes.Text,
										txtDescription.Text,
										indicator,
										txtRemarks.Text,
										this.empName,
										out string message);
			if (isSuccess)
			{
				fe.SendToastNotifDesktop(message, "Success");
			}
			else
			{
				fe.SendToastNotifDesktop(message, "Failed");
			}
			// Reset the form and close
			DefaultItem();
			Close();
			
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInputs();
			string indicator;
			if (DialogResult.Yes == RadMessageBox.Show(
				"Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation",
				MessageBoxButtons.YesNo,
				RadMessageIcon.Question))
			{
				if (rdoYes.IsChecked == true)
				{
					indicator = "Yes";
				}
				else
				{
					indicator = "No";
				}
				bool isSuccess = bundle.BundleCodesDBRequest("Delete", txtIntID.Text, txtCPTCode.Text, txtBundleCodes.Text, txtDescription.Text, indicator, txtRemarks.Text, empName, out string message);
				if (isSuccess)
				{
					fe.SendToastNotifDesktop(message, "Success");
				}
				else
				{
					fe.SendToastNotifDesktop(message, "Failed");
				}
			}
			DefaultItem();
			Close();
		}

		private void frmModBundleCodes_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
