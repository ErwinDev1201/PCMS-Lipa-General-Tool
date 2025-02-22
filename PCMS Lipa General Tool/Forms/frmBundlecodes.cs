using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmBundlecodes : Telerik.WinControls.UI.RadForm
	{
		private readonly Bundle bundle = new();
		public string accessLevel;
		public string EmpName;
		private static readonly Notification notif = new();

		public frmBundlecodes()
		{
			InitializeComponent();
			ShowBundleCodes();
		}

		

		private void ShowBundleCodes()
		{
			var dataTable = bundle.ViewBundleCodes(EmpName, out string lblCount);
			dgBundleCode.DataSource = dataTable;
			dgBundleCode.BestFitColumns(BestFitColumnMode.DisplayedCells);
			lblbundeCode.Text = lblCount;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}

		private void dgBundleCode_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (dgBundleCode.SelectedRows.Count == 0)
					return;
				string bundleOptions;
				var selectedRow = dgBundleCode.SelectedRows[0];
				var modBundleCodes = new frmModBundleCodes
				{
					txtIntID = { Text = selectedRow.Cells["Treatment ID"].Value?.ToString() ?? string.Empty },
					txtCPTCode = { Text = selectedRow.Cells["CPT Code"].Value?.ToString() ?? string.Empty },
					txtDescription = { Text = selectedRow.Cells["Description"].Value?.ToString() ?? string.Empty },
					txtBundleCodes = { Text = selectedRow.Cells["Bundle Codes"].Value?.ToString() ?? string.Empty },
					txtRemarks = { Text = selectedRow.Cells["Remarks"].Value?.ToString() ?? string.Empty },
					empName = EmpName
				};
				bundleOptions = selectedRow.Cells["Indicator"].Value?.ToString() ?? string.Empty;
				if (bundleOptions == "Y")
				{
					modBundleCodes.rdoYes.IsChecked = true;
				}
				else
				{
					modBundleCodes.rdoNo.IsChecked = true;
				}

				if (accessLevel != "User")
				{
					modBundleCodes.Text = "View/Update Bundle Codes Information";
					modBundleCodes.btnUpdateSave.Text = "Update";
				}
				else
				{
					modBundleCodes.Text = "View Bundle Codes Information";
					modBundleCodes.btnDelete.Visible = false;
					modBundleCodes.btnUpdateSave.Visible = false;
				}

				modBundleCodes.ShowDialog();
				ShowBundleCodes();
			}
			catch (Exception ex)
			{
				notif.LogError("dgBundleCode_MouseDoubleClick", EmpName, "frmBundleCodes", null, ex);
			}
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
			bundle.GetDBID(out string ID, EmpName);
			dlg.txtIntID.Text = ID;
			dlg.txtCPTCode.Focus();
			dlg.ShowDialog();
			ShowBundleCodes();

		}

		private void frmBundlecodes_Load(object sender, EventArgs e)
		{
			dgBundleCode.BestFitColumns(BestFitColumnMode.DisplayedCells);
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
			try
			{
				DataTable resultTable = bundle.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);
				dgBundleCode.DataSource = resultTable;
				dgBundleCode.BestFitColumns(BestFitColumnMode.DisplayedCells);
				lblbundeCode.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmAdjusterInfo", null, ex);
			}
			//task.SearchTwoColumnOneFieldText(dgBundleCode, "[Bundle Codes]", "[CPT Code]", "[Bundle Codes]", txtSearch, lblbundeCode, EmpName);
		}
	}
}
