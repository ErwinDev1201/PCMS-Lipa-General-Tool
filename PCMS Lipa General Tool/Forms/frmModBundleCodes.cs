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

		public void GetDBID()
		{
			task.GetSequenceNo("textbox", "BundleCodeSeq", txtIntID, null, "TX-");
		}

		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
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
					bundle.BundleCodesDBRequest("Update", txtIntID, txtCPTCode, txtBundleCodes, txtDescription, indicator, txtRemarks, empName);
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
				bundle.BundleCodesDBRequest("Create", txtIntID, txtCPTCode, txtBundleCodes, txtDescription, indicator, txtRemarks, empName);
			}
			Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
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
				bundle.BundleCodesDBRequest("Delete", txtIntID, txtCPTCode, txtBundleCodes, txtDescription, indicator, txtRemarks, empName);
				Close();
			}
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
