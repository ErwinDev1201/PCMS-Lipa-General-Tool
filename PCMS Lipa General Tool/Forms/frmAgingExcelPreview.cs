using Microsoft.Win32;
using PCMS_Lipa_General_Tool.Backend;
using PCMS_Lipa_General_Tool.Class;
using PCMS_Lipa_General_Tool.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace PCMS_Lipa_General_Tool.Forms
{
	public partial class frmAgingExcelPreview : RadForm
	{
		private static readonly Provider provider = new();
		private static readonly AgingUpload aging = new();
		private static readonly ActivtiyLogs activity = new();
		public string accessLevel;
		public string empName;
		public string filePath;
		private static readonly Notification notif = new();
		private DataTable currentPreviewTable;

		public frmAgingExcelPreview()
		{
			InitializeComponent();
		}

		public void LoadPreviewSheet()
		{
			try
			{
				currentPreviewTable = aging.ReadExcelToDataTable(filePath);

				dgExcelPreview.DataSource = currentPreviewTable;
				dgExcelPreview.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill; // UI UX improvement
				activity.AddActivityLog("Preview loaded successfully.", empName, $"Aging Preview loaded successfully - {empName}", "AGING PREVIEW");
				//RadMessageBox.Show("Preview loaded successfully.", "Preview Ready", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
			catch (OleDbException ex)
			{
				notif.LogError("LoadPreviewSheet", empName, "AgingUpload", "OleDbException", ex);
				RadMessageBox.Show("Excel read error. Please ensure the file is not open and has the correct format (Excel 2007+).",
					"Excel Import Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			catch (InvalidOperationException ex)
			{
				notif.LogError("LoadPreviewSheet", empName, "AgingUpload", "InvalidOperationException", ex);
				RadMessageBox.Show("There seems to be an issue with the Excel sheet structure. Try opening the file manually and saving it again.",
					"Sheet Validation Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			catch (Exception ex)
			{
				notif.LogError("LoadPreviewSheet", empName, "AgingUpload", "UnhandledException", ex);
				RadMessageBox.Show("Oops! It looks like something went wrong while loading the Excel file. Please check the file format and try again.",
					"Error", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
		}


		//public void LoadPreviewSheet()
		//{
		//	try
		//	{
		//		currentPreviewTable = aging.ReadExcelToDataTable(filePath);
		//		dgExcelPreview.DataSource = currentPreviewTable;
		//		activity.AddActivityLog("Preview loaded successfully.", empName, "Aging Preview", "Aging Preview loaded successfully");
		//		//RadMessageBox.Show("Preview loaded successfully.", "Preview Ready", MessageBoxButtons.OK, RadMessageIcon.Info);
		//	}
		//	catch (Exception ex)
		//	{
		//		notif.LogError("LoadPreviewSheet", empName, "Aging Upload", "N/A", ex);
		//		//RadMessageBox.Show($"Error loading Excel: {ex.Message}", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//		RadMessageBox.Show("Oops! It looks like something went wrong while loading the Excel file. Please check the file format and try again.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//}
		//


		private void cmbProviderList_PopupOpened(object sender, EventArgs e)
		{
			FillProviderperCollectorDropdown();
		}

		private void FillProviderperCollectorDropdown()
		{
			if (accessLevel != "Collector")
			{
				List<string> items = provider.GetProviderList(empName);
				cmbProviderList.Items.Clear(); // Clear existing items, if any
				foreach (var item in items)
				{
					cmbProviderList.Items.Add(item);
				}
			}
			else
			{
				List<string> items = provider.GetProviderListperCollector(empName);
				cmbProviderList.Items.Clear(); // Clear existing items, if any
				foreach (var item in items)
				{
					cmbProviderList.Items.Add(item);
				}
			}
				
		}

		private void btnImportDb_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(cmbProviderList.Text) || (cmbProviderList.Text == "Select Provider"))
			{
				RadMessageBox.Show("Please Select Provider", "Provider", MessageBoxButtons.OK, RadMessageIcon.Error);
				return;
			}
			if (dgExcelPreview == null || dgExcelPreview.Rows.Count == 0)
			{
				RadMessageBox.Show("No preview data available.", "Error", MessageBoxButtons.OK, RadMessageIcon.Info);
				return;
			}

			if (string.IsNullOrWhiteSpace(cmbProviderList.Text))
			{
				RadMessageBox.Show("Please enter a base table name.", "Validation", MessageBoxButtons.OK, RadMessageIcon.Info);
				return;
			}

			//string fullTableName = $"{cmbProviderList.Text.Trim()}_{DateTime.Now:yyyy_MM}";
			string providerName = $"{cmbProviderList.Text.Trim()}";
			int total = currentPreviewTable.Rows.Count;
			int chunkSize = 1000;
			int inserted = 0;

			try
			{
				// Setup progress bar
				ProgBarStatus.Visible = true;
				ProgBarStatus.Value1 = 0;
				ProgBarStatus.Minimum = 0;
				ProgBarStatus.Maximum = total;
				//ProgBarStatus.Text = true;
				ProgBarStatus.Text = "Starting upload...";

				aging.CreateSqlTableFromDataTable(empName, currentPreviewTable);

				for (int i = 0; i < total; i += chunkSize)
				{
					var chunk = currentPreviewTable.AsEnumerable().Skip(i).Take(chunkSize).CopyToDataTable();
					int count = aging.BulkInsertChunk(chunk, empName, providerName, empName);

					inserted += count;
					ProgBarStatus.Value1 = inserted;
					ProgBarStatus.Text = $"Uploading {inserted} of {total} rows...";
					Application.DoEvents();
				}

				ProgBarStatus.Text = $"✅ Upload complete: {inserted} rows.";
				ProgBarStatus.Value1 = total;

				activity.AddActivityLog($"Data imported into SQL table: {empName}", empName, "Aging Upload", $"Data imported into SQL table: {providerName}");
				//RadMessageBox.Show("✅ Data uploaded successfully", "Success", MessageBoxButtons.OK, RadMessageIcon.Info);
			}
			catch (Exception ex)
			{
				ProgBarStatus.Text = "❌ Upload failed.";
				//RadMessageBox.Show("Upload failed.", "Error", MessageBoxButtons.OK, RadMessageIcon.Error);
				notif.LogError("btnImportDb_Click", empName, "Aging Upload - Error", providerName, ex);
			}
			//finally
			//{
			//	await Task.Delay(2000);
			//	ProgBarStatus.Visible = false;
			//	ProgBarStatus.Visible = false;
			//	ProgBarStatus.Text = "";
			//}
		}
	}


}
