using System;
using System.Data;
using System.IO;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Services
{
	public class ExportService
	{
		private readonly OfficeFiles _officeService = new();
		private readonly Notification notif = new();

		public string LoadExportXcel(RadGridView gridView, DateTime fromDate, DateTime toDate, string employeeName)
		{
			try
			{
				if (gridView == null || gridView.Rows.Count == 0)
					throw new ArgumentException("The grid view is empty or null.");

				// Convert RadGridView to DataTable
				DataTable dataTable = GetDataTableFromRadGridView(gridView);

				// Generate a valid sheet name
				string sheetName = GenerateValidSheetName($"Pantry List {fromDate:MM.dd.yy}-{toDate:MM.dd.yy}");

				// Log the generated sheet name (for debugging)
				Console.WriteLine($"Generated Sheet Name: {sheetName}");

				// Export data to Excel
				_officeService.ExportTableToExcel(dataTable, sheetName, employeeName);

				// Define file path
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string filePath = Path.Combine(desktopPath, $"{sheetName}.xlsx");

				return filePath; // Return the path of the exported file
			}
			catch (Exception ex)
			{
				// Log the error instead of showing a message box (since it's in BE)
				notif.LogError("LoadExportXcel", employeeName, "ExportService", null, ex);
				return string.Empty; // Ensure method always returns a string
			}
		}

		// Other BE methods
		private string GenerateValidSheetName(string input)
		{
			if (string.IsNullOrWhiteSpace(input)) return "Sheet1";

			char[] invalidChars = [':', '\\', '/', '?', '*', '[', ']'];
			string validSheetName = input;

			foreach (char invalidChar in invalidChars)
			{
				validSheetName = validSheetName.Replace(invalidChar.ToString(), "");
			}

			return validSheetName.Length > 31 ? validSheetName.Substring(0, 31) : validSheetName;
		}

		private DataTable GetDataTableFromRadGridView(RadGridView gridView)
		{
			DataTable dataTable = new();
			foreach (GridViewDataColumn column in gridView.Columns)
			{
				if (!string.IsNullOrEmpty(column.HeaderText) && column.DataType != null)
				{
					dataTable.Columns.Add(column.HeaderText, column.DataType);
				}
			}

			foreach (GridViewRowInfo row in gridView.Rows)
			{
				if (!row.IsVisible) continue;
				DataRow dataRow = dataTable.NewRow();
				foreach (GridViewDataColumn column in gridView.Columns)
				{
					if (!string.IsNullOrEmpty(column.HeaderText) && dataTable.Columns.Contains(column.HeaderText))
					{
						dataRow[column.HeaderText] = row.Cells[column.Name]?.Value ?? DBNull.Value;
					}
				}
				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}
	}
}
