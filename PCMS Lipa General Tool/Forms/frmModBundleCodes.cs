using DocumentFormat.OpenXml.Wordprocessing;
using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmModBundleCodes : Telerik.WinControls.UI.RadForm
	{
		//private readonly MailSender mailSender = new MailSender();
		private readonly CommonTask task = new();
		private readonly Bundle bundle = new();
		public string empName;
		public string bundleOptions;

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
			GetDBID();
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

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "BundleCodeSeq", txtIntID, null, "TX-");
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			DisableInputs();
			string indicator;
			if (btnUpdateSave.Text == "Update")
			{
				if (DialogResult.Yes == RadMessageBox.Show("Would you like to go ahead and update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
				{
					if (rdoYes.IsChecked == true)
					{
						indicator = "Y";
					}
					else
					{
						indicator = "N";
					}
					bundle.BundleCodesDBRequest("Update", txtIntID.Text, txtCPTCode.Text, txtBundleCodes.Text, txtDescription.Text, indicator, txtRemarks, empName);
				}
			}
			else
			{
				if (rdoYes.IsChecked == true)
				{
					indicator = "Y";
				}
				else
				{
					indicator = "N";
				}
				bundle.BundleCodesDBRequest("Create", txtIntID.Text, txtCPTCode.Text, txtBundleCodes.Text, txtDescription.Text, indicator, txtRemarks, empName);
			}
			DefaultItem();
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			DisableInputs();
			string indicator;
			if (DialogResult.Yes == RadMessageBox.Show("Just checking, do you want to delete this record? You can’t undo this action.", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question))
			{
				if (rdoYes.IsChecked == true)
				{
					indicator = "Yes";
				}
				else
				{
					indicator = "No";
				}
				bundle.BundleCodesDBRequest("Delete", txtIntID.Text, txtCPTCode.Text, txtBundleCodes.Text, txtDescription.Text, indicator, txtRemarks, empName);
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
