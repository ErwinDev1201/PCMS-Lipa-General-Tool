using PCMS_Lipa_General_Tool.Class;
using System;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBundlecodes : Telerik.WinControls.UI.RadForm
	{
		private readonly Bundle bundle = new();
		private readonly CommonTask task = new();
		public string accessLevel;
		public string EmpName;

		public frmBundlecodes()
		{
			InitializeComponent();
			ShowBundleCodes();
		}

		private void ShowBundleCodes()
		{
			string lblCount;
			var dataTable = bundle.ViewBundleCodes(EmpName, out lblCount);

			dgBundleCode.DataSource = dataTable;

			lblbundeCode.Text = lblCount;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void dgBundleCode_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (dgBundleCode.SelectedRows.Count > 0)
			{
				var dlgModBundleCodes = new frmModBundleCodes();
				bundle.FillBundleCodes(dgBundleCode, dlgModBundleCodes.txtIntID, dlgModBundleCodes.txtCPTCode, dlgModBundleCodes.txtDescription, dlgModBundleCodes.txtBundleCodes, dlgModBundleCodes.txtRemarks, dlgModBundleCodes.rdoYes, dlgModBundleCodes.rdoNo, EmpName);
				if (accessLevel == "User")
				{
					dlgModBundleCodes.btnDelete.Visible = false;
					dlgModBundleCodes.btnUpdateSave.Visible = false;
					dlgModBundleCodes.Text = "View Diagnosis Info";

				}
				else
				{
					dlgModBundleCodes.btnUpdateSave.Text = "Update";
					dlgModBundleCodes.Text = "View/Modify Diagnosis Info";
				}
				dlgModBundleCodes.ShowDialog();

			}
			ShowBundleCodes();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			var dlg = new frmModBundleCodes
			{
				btnDelete = { Visible = false },
				btnUpdateSave = { Text = "Save" },
				Text = "New Bundle Codes",
				empName = EmpName
			};
			dlg.GetDBID();
			dlg.txtCPTCode.Focus();
			dlg.ShowDialog();
			ShowBundleCodes();

		}

		private void frmBundlecodes_Load(object sender, EventArgs e)
		{
			dgBundleCode.BestFitColumns(BestFitColumnMode.AllCells);
			dgBundleCode.ReadOnly = true;
		}

		private void frmBundlecodes_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{
			task.SearchTwoColumnOneFieldText(dgBundleCode, "[Bundle Codes]", "[CPT Code]", "[Bundle Codes]", txtSearch, lblbundeCode, EmpName);
		}
	}
}
