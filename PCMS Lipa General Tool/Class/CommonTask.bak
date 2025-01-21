using ClosedXML.Excel;
using GemBox.Document;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Telerik.WinControls.UI;
using Color = GemBox.Document.Color;
using DataTable = System.Data.DataTable;
using HorizontalAlignment = GemBox.Document.HorizontalAlignment;



namespace PCMS_Lipa_General_Tool.Class
{
	public class CommonTask
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		readonly RadDesktopAlert alert = new();
		FEWinForm fehelper = new();
		private readonly string alercaption = Assembly.GetExecutingAssembly().GetName().Name.ToString();

		readonly WinDiscordAPI dc = new();
		readonly emailSender mail = new();
		readonly Error error = new();


		


		

		//public void ExecutedbCollBackupCsv(string empName)
		//{
		//	var localbackup = @"D:\PCMS Maintenance\PCMS Lipa General Tool";
		//	var query = "SELECT [Login ID], [Insurance Name], [URL Link], USERNAME, PASSWORD, [ACCOUNT OWNER], [Browser] REMARKS FROM [ONLINE LOGINS]";
		//	var path = dbBackupcoll + @"\Online Logins.csv";
		//	if (!Directory.Exists(dbBackupcoll))
		//	{
		//		Directory.CreateDirectory(dbBackupcoll);
		//	}
		//	if (File.Exists(path))
		//	{
		//		File.Delete(path);
		//	}
		//	try
		//	{
		//		SQLToCSV(query, path, empName);
		//	}
		//	catch (Exception)
		//	{
		//		path = localbackup + @"\Online Logins.csv";
		//		if (!Directory.Exists(localbackup))
		//		{
		//			Directory.CreateDirectory(localbackup);
		//		}
		//		if (File.Exists(path))
		//		{
		//			File.Delete(path);
		//		}
		//		SQLToCSV(query, path, empName);
		//	}
		//}
		//


		public void SQLToCSV(string query, string Filename, string empName)
		{
			try
			{
				using SqlConnection conn = new(_dbConnection);
				conn.Open();
				using SqlCommand cmd = new(query, conn);
				using SqlDataReader dr = cmd.ExecuteReader();
				using StreamWriter fs = new(Filename);
				// Loop through the fields and add headers
				for (int i = 0; i < dr.FieldCount; i++)
				{
					string name = dr.GetName(i);
					if (name.Contains(","))
						name = "\"" + name + "\"";

					fs.Write(name + ",");
				}
				fs.WriteLine();

				// Loop through the rows and output the data
				while (dr.Read())
				{
					for (int i = 0; i < dr.FieldCount; i++)
					{
						string value = dr[i].ToString();
						if (value.Contains(","))
							value = "\"" + value + "\"";

						fs.Write(value + ",");
					}
					fs.WriteLine();
				}
			}
			catch (Exception ex)
			{
				error.LogError("SQLToCSV", empName, "CommonTask", null, ex);
			}
		}


		

		


			//alert.AutoClose = true;
			//alert.AutoCloseDelay = 5000;
			//alert.CaptionText = alercaption;
			//alert.ContentText = message;
			//alert.ShowOptionsButton = false;
			//alert.AutoSize = true;
			//alert.ShowCloseButton = false;
			//alert.Opacity = 0.9f;
			//alert.ScreenPosition = AlertScreenPosition.BottomRight;
			//
			//Bitmap icon = null;
			//
			//icon = alertType switch
			//{
			//	"Success" => SystemIcons.Information.ToBitmap(),
			//	"Warning" => SystemIcons.Warning.ToBitmap(),
			//	"Error" => SystemIcons.Error.ToBitmap(),
			//	_ => SystemIcons.Application.ToBitmap(),
			//};
			//if (icon != null)
			//{
			//	RadLabelElement imageElement = new()
			//	{
			//		Image = icon,
			//		ImageAlignment = ContentAlignment.MiddleLeft,
			//		TextAlignment = ContentAlignment.MiddleRight,
			//		Text = message,
			//		StretchHorizontally = true
			//	};
			//
			//	// Replace the content of the popup with a custom layout
			//	alert.Popup.AlertElement.ContentElement.Children.Clear();
			//	alert.Popup.AlertElement.ContentElement.Children.Add(imageElement);
			//}
			//alert.Show();
		

		



		

		

		


		//public void CheckIfExistinDB(string username, string modLoc, string request, RadLabel lblalert)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	conn.Open();
		//	try
		//	{
		//		SqlCommand command = new(
		//			$@"SELECT COUNT(*)
		//			FROM
		//				[User Informmation]
		//			WHERE
		//				USERNAME = @username", conn);
		//		command.Parameters.AddWithValue("@username", username);
		//		int count = (int)command.ExecuteScalar();
		//		if (count > 0)
		//		{
		//			if (modLoc == "UserMgmt" && request == "Create")
		//			{
		//				//if (request == "Create")
		//				//{
		//				RadMessageBox.Show("Username already exist", "Information", MessageBoxButtons.OK, RadMessageIcon.Info);
		//				//}
		//			}
		//
		//		}
		//		else
		//		{
		//			if (request == "Login" && modLoc == "Login")
		//			{
		//				lblalert.Visible = true;
		//				lblalert.Text = "Username not found";
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("CheckIfExistinDB", "n/A", "CommonTask", "N/A", ex);
		//	}
		//	finally
		//	{
		//		conn.Close();
		//	}
		//}
		//

		



		//		public void SearchEmpTwoColumnOneFieldText(
		//			DataTable uiTableName,
		//			string dbTableName,
		//			string employeeName,
		//			string Status,
		//			string itemToSearch,
		//			string searchCount,
		//			string empName)
		//		{
		//			using SqlConnection conn = new(_dbConnection);
		//			try
		//			{
		//				using DataTable dt = new(dbTableName);
		//				using SqlCommand cmd = new($@"
		//SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT],
		//[USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]
		//FROM
		//[User Information]
		//WHERE {employeeName} LIKE @employeeName OR @empStatus LIKE @itemSearch", conn);
		//				cmd.Parameters.AddWithValue("@employeeName", string.Format("%{0}%", itemToSearch));
		//				cmd.Parameters.AddWithValue("@empStatus", string.Format("%{0}%", itemToSearch));
		//				SqlDataAdapter adapter = new(cmd);
		//				adapter.Fill(dt);
		//				uiTableName.DataSource = dt;
		//				searchCount = $"Total records: {uiTableName.RowCount}";
		//
		//			}
		//			catch (Exception ex)
		//			{
		//				LogError("GetUsersEmail", empName, "CommonTask", "N/A", ex);
		//
		//			}
		//		}

		//public void SearchEmpTwoColumnTwoFieldText(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch, RadDropDownList itemToSearch2, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]" +
		//			"FROM [User Information] WHERE " + columntosearch1 + " LIKE @columntoSearch2 AND " + columntosearch2 + " LIKE @columntoSearch3", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch3", string.Format(itemToSearch2.Text));
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchEmpTwoColumnTwoFieldText", empName, "CommonTask", "N/A", ex);
		//	}
		//}

		//public void SearchTwoColumnOneFieldText(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT * FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch2 OR " + columntosearch2 + " LIKE @columntoSearch3", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch3", string.Format("%{0}%", itemToSearch.Text));
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchTwoColumnOneFieldText", empName, "CommonTask", null, ex);
		//	}
		//}

		//public void SearchTwoColumnOneFieldCombo(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadDropDownList itemToSearch, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT * FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch2 OR " + columntosearch2 + " LIKE @columntoSearch3", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch3", string.Format("%{0}%", itemToSearch.Text));
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchTwoColumnOneFieldCombo", empName, "CommonTask", null, ex);
		//	}
		//}
		//
		//public void SearchThreeColumnOneFieldText(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, string columntosearch3, RadTextBox itemToSearch, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT * FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch2 OR " + columntosearch2 + " LIKE @columntoSearch3 OR " + columntosearch3 + " LIKE @columntoSearch4", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch3", string.Format("%{0}%", itemToSearch.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch4", string.Format("%{0}%", itemToSearch.Text));
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchThreeColumnOneFieldText", empName, "CommonTask", null, ex);
		//	}
		//}
		//
		//public void SearchTwoColumnTwoFieldCombo(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch1, RadDropDownList itemToSearch2, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT * FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch1 AND " + columntosearch2 + "LIKE @columntoSearch2", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch1", string.Format("%{0}%", itemToSearch1.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch2", itemToSearch2.Text);
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchTwoColumnTwoFieldCombo", empName, "CommonTask", null, ex);
		//	}
		//
		//}

		//public void SearchTwoColumnTwoFieldText(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch1, RadTextBox itemToSearch2, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT * FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch1 AND " + columntosearch2 + " = @columntoSearch2", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch1", string.Format("%{0}%", itemToSearch1.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch2.Text));
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("SearchTwoColumnTwoFieldText", empName, "CommonTask", null, ex);
		//	}
		//}
		//
		//public void EmpSearchTwoColumnTwoFieldCombo(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch1, RadDropDownList itemToSearch2, RadLabel searchCount, string empName)
		//{
		//	using SqlConnection conn = new(_dbConnection);
		//	try
		//	{
		//		using DataTable dt = new(dbTableName);
		//		using SqlCommand cmd = new("SELECT [Employee ID], [Employee Name], [Username], [Department], [User Access], [Position], [Status], [Office], [Email Address] FROM " + dbTableName +
		//			" WHERE " + columntosearch1 + " LIKE @columntoSearch1 AND " + columntosearch2 + "LIKE @columntoSearch2", conn);
		//		cmd.Parameters.AddWithValue("columntoSearch1", string.Format("%{0}%", itemToSearch1.Text));
		//		cmd.Parameters.AddWithValue("columntoSearch2", itemToSearch2.Text);
		//		SqlDataAdapter adapter = new(cmd);
		//		adapter.Fill(dt);
		//		uiTableName.DataSource = dt;
		//		searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError("EmpSearchTwoColumnTwoFieldCombo", empName, "CommonTask", null, ex);
		//	}
		//}

		

		//public void GetSequenceNo(string inputType, string sequenceName, string txtnextSequence, string lblnextSequnce, string preID)
		//{
		//	//int Price;
		//	var query = "SELECT NEXT VALUE FOR " + sequenceName;
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		SqlCommand cmd = new(query, con);
		//		SqlDataReader reader = cmd.ExecuteReader();
		//		while (reader.Read())
		//		{
		//			int currSeq = reader.GetInt32(0);
		//			if (inputType == "label")
		//			{
		//				lblnextSequnce = preID + (currSeq + 1).ToString();
		//			}
		//			else
		//			{
		//				txtnextSequence = preID + (currSeq + 1).ToString();
		//			}
		//			//nextSequence.Text = preID + currSeq;// + ToString();
		//		}
		//		con.Close();
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError($"GetSequenceNo", "N/A", "CommonTask", "N/A", ex);
		//	}
		//}
		//


		//public void ExportTabletoExcel(RadGridView dgGridView, string filename, string empName)
		//{
		//	try
		//	{
		//		// Path to save the file on the desktop
		//		string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		string filePath = Path.Combine(desktopPath, filename + ".xlsx");
		//
		//		// Convert RadGridView data to a DataTable
		//		DataTable dataTable = GetDataTableFromRadGridView(dgGridView);
		//
		//		// Export to Excel using ClosedXML
		//		using (XLWorkbook workbook = new())
		//		{
		//			workbook.Worksheets.Add(dataTable, filename);
		//			workbook.SaveAs(filePath);
		//		}
		//
		//		// Notify the user
		//		RadMessageBox.Show($"Export successful! File saved to:\n{filePath}", "Export Complete", MessageBoxButtons.OK, RadMessageIcon.Info);
		//
		//		// Open the file after export (optional)
		//		if (RadMessageBox.Show("Would you like to open the file?", "Open File", MessageBoxButtons.YesNo, RadMessageIcon.Question) == DialogResult.Yes)
		//		{
		//			System.Diagnostics.Process.Start(filePath);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		LogError($"ExportTabletoExcel", empName, "CommonTask", "N/A", ex);
		//		RadMessageBox.Show($"An error occurred during export:\n{ex.Message}", "Export Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//}

		


		


		



		//old excel
		//public void ExportTabletoExcel(RadGridView dgGridView, string fileName)
		//{
		//	try
		//	{
		//		// Path to save the file on the desktop
		//		string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		string filePath = Path.Combine(desktopPath, fileName + ".xls");
		//
		//		// Initialize export
		//		ExportToExcelML exporter = new ExportToExcelML(dgGridView);
		//		exporter.ExportVisualSettings = false; // Include visual formatting
		//		exporter.SheetName = "RadGridView Data";
		//
		//		// Export to the file
		//		exporter.RunExport(filePath);
		//
		//		// Notify the user
		//		RadMessageBox.Show($"Export successful! File saved to:\n{filePath}", "Export Complete", MessageBoxButtons.OK, RadMessageIcon.Info);
		//
		//		// Open the file after export (optional)
		//		if (RadMessageBox.Show("Would you like to open the file?", "Open File", MessageBoxButtons.YesNo, RadMessageIcon.Question) == DialogResult.Yes)
		//		{
		//			System.Diagnostics.Process.Start(filePath);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		RadMessageBox.Show($"An error occurred during export:\n{ex.Message}", "Export Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
		//	}
		//}
		//
	}


}