using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmProvider : RadForm
	{
		private readonly Provider provider = new();
		private readonly Notification notif = new();
		private readonly FEWinForm fe = new();
		public string accessLevel;
		public string EmpName;

		public frmProvider()
		{
			InitializeComponent();
			ShowProviderInfo();
			ProvInDefault();
		}

		private void ProvInDefault()
		{
			//txtNoProv.Enabled = false;
			txtProviderName.Enabled = false;
			txtNoProv.ReadOnly = true;
			txtNoProv.Enabled = false;
			txtNPINo.Enabled = false;
			txtPTANo.Enabled = false;
			txtTaxID.Enabled = false;
			txtPalPTANo.Enabled = false;
			txtPhysicalAdd.Enabled = false;
			txtBillingAdd.Enabled = false;
			txtRemarks.Enabled = false;
			btnNew.Enabled = true;
			//txtSearch.Enabled = true;
			//btnNew.Enabled = true;
			btnCancel.Enabled = false;
			btnUpdateSave.Enabled = false;
			btnDelete.Enabled = false;
			//txtSearch.Clear();
			//txtSearch.Enabled = true;
			dgProviderInfo.Enabled = true;
		}

		private void ShowProviderInfo()
		{
			dgProviderInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
			var dataTable = provider.ViewProviderList(EmpName, out string lblCount);
			dgProviderInfo.DataSource = dataTable;
			lblSearchCount.Text = lblCount;

		}


		private void ClearProvInfo()
		{
			txtProviderName.Clear();
			txtNPINo.Clear();
			txtPTANo.Clear();
			txtTaxID.Clear();
			txtNoProv.Clear();
			txtPalPTANo.Clear();
			txtPhysicalAdd.Clear();
			txtBillingAdd.Clear();
			txtRemarks.Clear();
		}

		private void ProvDoubleClickEnable()
		{
			if (accessLevel == "User")
			{
				txtProviderName.Enabled = true;
				txtNoProv.Enabled = true;
				txtNPINo.Enabled = true;
				txtPTANo.Enabled = true;
				txtTaxID.Enabled = true;
				txtPalPTANo.Enabled = true;
				txtPhysicalAdd.Enabled = true;
				txtBillingAdd.Enabled = true;
				txtRemarks.Enabled = true;
				txtProviderName.ReadOnly = true;
				txtNPINo.ReadOnly = true;
				txtPTANo.ReadOnly = true;
				txtTaxID.ReadOnly = true;
				txtPalPTANo.ReadOnly = true;
				btnCancel.Enabled = true;
				//btnNew.Enabled = false;
				txtPhysicalAdd.ReadOnly = true;
				txtBillingAdd.ReadOnly = true;
				txtRemarks.ReadOnly = true;
				//txtSearch.Enabled = false;
				dgProviderInfo.Enabled = false;
			}
			else
			{
				txtProviderName.Enabled = true;
				txtNoProv.Enabled = true;
				txtNPINo.Enabled = true;
				txtPTANo.Enabled = true;
				txtTaxID.Enabled = true;
				txtPalPTANo.Enabled = true;
				txtPhysicalAdd.Enabled = true;
				txtBillingAdd.Enabled = true;
				txtRemarks.Enabled = true;
				//btnNew.Enabled = false;
				btnUpdateSave.Enabled = true;
				btnDelete.Enabled = true;
				btnCancel.Enabled = true;
				//txtSearch.Enabled = false;
				btnUpdateSave.Text = "Update";
				dgProviderInfo.Enabled = false;
			}
		}



		private void btnCancel_Click(object sender, EventArgs e)
		{
			ClearProvInfo();
			ProvInDefault();

		}

		private bool ProcessProviderRequest(string actionType, out string message)
		{
			bool isSuccess = provider.ProviderInfoDBRequest(
				actionType,
				txtNoProv.Text,
				txtProviderName.Text,
				txtNPINo.Text,
				txtPTANo.Text,
				txtTaxID.Text,
				txtPalPTANo.Text,
				txtPhysicalAdd.Text,
				txtBillingAdd.Text,
				txtRemarks.Text,
				EmpName,
				out message);

			// Execute additional actions only if the request was successful
			if (isSuccess)
			{
				ClearProvInfo();
				ShowProviderInfo();
				ProvInDefault();
				txtProviderName.Focus();
			}

			return isSuccess;
		}


		private void btnUpdateSave_Click(object sender, EventArgs e)
		{
			try
			{
				// Determine action type based on button text
				string actionType = btnUpdateSave.Text == "Save" ? "Create" : "Update";

				// If updating, ask for confirmation
				if (actionType == "Update" &&
					RadMessageBox.Show("Are you sure you want to update this record?", "Confirmation", MessageBoxButtons.YesNo, RadMessageIcon.Question) != DialogResult.Yes)
				{
					return;
				}

				// Process database request
				bool isSuccess = ProcessProviderRequest(actionType, out string message);

				// Show toast notification based on success/failure
				fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");
			}
			catch (Exception ex)
			{
				notif.LogError("btnUpdateSave_Click", EmpName, "frmProvider", txtNoProv.Text, ex);
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				// Confirm deletion before proceeding
				if (RadMessageBox.Show("Are you sure you want to delete this record?", "Confirmation",
					MessageBoxButtons.YesNo, RadMessageIcon.Question) != DialogResult.Yes)
				{
					return;
				}

				// Process delete request
				bool isSuccess = ProcessProviderRequest("Delete", out string message);

				// Show toast notification based on success/failure
				fe.SendToastNotifDesktop(message, isSuccess ? "Success" : "Failed");
			}
			catch (Exception ex)
			{
				notif.LogError("btnDelete_Click", EmpName, "frmProvider", txtNoProv.Text, ex);
			}

		}

		private void frmProvider_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}

		}

		private void frmProvider_Load(object sender, EventArgs e)
		{
			//txtNoProv.ShowEmbeddedLabel = true; txtNoProv.EmbeddedLabelText = "Prov. ID";
			//txtPalPTANo.ShowEmbeddedLabel = true; txtPalPTANo.EmbeddedLabelText = "Palmetto PTA No.";
			//txtNPINo.ShowEmbeddedLabel = true; txtNPINo.EmbeddedLabelText = "NPI No.";
			//txtTaxID.ShowEmbeddedLabel = true; txtTaxID.EmbeddedLabelText = "Tax ID No.";
			//txtPhysicalAdd.ShowEmbeddedLabel = true; txtPhysicalAdd.EmbeddedLabelText = "Physical Address";
			//txtBillingAdd.ShowEmbeddedLabel = true; txtBillingAdd.EmbeddedLabelText = "Billing Address";
			//txtProviderName.ShowEmbeddedLabel = true; txtProviderName.EmbeddedLabelText = "Provider Name";
			//txtPTANo.ShowEmbeddedLabel = true; txtPTANo.EmbeddedLabelText = "PTAN No.";
			//txtRemarks.ShowEmbeddedLabel = true; txtRemarks.EmbeddedLabelText = "Remarks";
			dgProviderInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
			dgProviderInfo.ReadOnly = true;
		}


		private void dgProviderInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{

			ProvDoubleClickEnable();
			try
			{
			
				if (accessLevel == "User")
				{
					btnDelete.Enabled = false;
					//btnNew.Enabled = false;
					btnCancel.Enabled = false;
					btnUpdateSave.Enabled = false;
				}
				dgProviderInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
				if (dgProviderInfo.SelectedRows.Count > 0)
				{
					var selectedRow = dgProviderInfo.SelectedRows[0];

					txtNoProv.Text = selectedRow.Cells["Provider ID"]?.Value?.ToString() ?? string.Empty;
					txtProviderName.Text = selectedRow.Cells["Provider Name"]?.Value?.ToString() ?? string.Empty;
					txtNPINo.Text = selectedRow.Cells["NPI"]?.Value?.ToString() ?? string.Empty;
					txtPTANo.Text = selectedRow.Cells["PTAN"]?.Value?.ToString() ?? string.Empty;
					txtPalPTANo.Text = selectedRow.Cells["RailRoad PTAN"]?.Value?.ToString() ?? string.Empty;
					txtTaxID.Text = selectedRow.Cells["Tax ID"]?.Value?.ToString() ?? string.Empty;
					txtPhysicalAdd.Text = selectedRow.Cells["Physical Address"]?.Value?.ToString() ?? string.Empty;
					txtBillingAdd.Text = selectedRow.Cells["Billing Address"]?.Value?.ToString() ?? string.Empty;
					txtRemarks.Text = selectedRow.Cells["Remarks"]?.Value?.ToString() ?? string.Empty;
				}
				else
				{
					RadMessageBox.Show("No row selected. Please try again.", "Selection Error", MessageBoxButtons.OK, RadMessageIcon.Info);
				}
				
			}
			catch (Exception ex)
			{
				notif.LogError("dgProviderInfo_MouseDoubleClick", EmpName, "frmProvider", txtNoProv.Text, ex);
			}
		}

		private void txtSearch_TextChanged(object sender, EventArgs e)
		{

			try
			{
				DataTable resultTable = provider.SearchData(
				txtSearch.Text,
				out string searchcount, EmpName);
				dgProviderInfo.BestFitColumns(BestFitColumnMode.DisplayedCells);
				dgProviderInfo.DataSource = resultTable;
				lblSearchCount.Text = searchcount;

			}
			catch (Exception ex)
			{
				notif.LogError("txtSearch_TextChanged", EmpName, "frmProvider", null, ex);
			}
			//task.SearchTwoColumnOneFieldText(dgProviderInfo, "[Provider Information]", "[Provider Name]", "REMARKS", txtSearch, lblSearchCount, EmpName);
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			ClearProvInfo();
			ProvDoubleClickEnable();
			txtProviderName.Focus();
			btnUpdateSave.Text = "Save";
			btnDelete.Enabled = false;
			provider.GetProvID(out string ID, EmpName);
			txtNoProv.Text = ID;
			btnNew.Enabled = false;
		}

		//private void txtSearch_TextChanged(object sender, EventArgs e)
		//{
		//	
		//}
	}

}