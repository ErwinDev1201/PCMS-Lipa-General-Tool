using ClosedXML.Excel;
using GemBox.Document;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using HorizontalAlignment = GemBox.Document.HorizontalAlignment;



namespace PCMS_Lipa_General_Tool.Class
{
	public class CommonTask
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		readonly RadDesktopAlert alert = new();
		private readonly string alercaption = Assembly.GetExecutingAssembly().GetName().Name.ToString();

		readonly WinDiscordAPI dc = new();
		readonly emailSender mail = new();


		private string email;


		public void CreateRtfFile(string filename)
		{
			ComponentInfo.SetLicense("FREE-LIMITED-KEY");

			// Create a new document
			var document = new DocumentModel();

			// Add a section to the document
			var section = new Section(document);
			document.Sections.Add(section);

			// Add a title "Personal Reminder" in bold format
			section.Blocks.Add(new Paragraph(document, new Run(document, "Personal Reminder")
			{
				CharacterFormat = new CharacterFormat
				{
					Bold = true,
					Size = 16, // Optional: Adjust font size for emphasis
					FontColor = Color.Black // Optional: Ensure black font color for clarity
				}
			})
			{
				ParagraphFormat = new ParagraphFormat
				{
					Alignment = HorizontalAlignment.Center // Center-align the title
				}
			});

			// Save the document as an RTF file
			document.Save(filename);
		}

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
				LogError("SQLToCSV", empName, "CommonTask", null, ex);
			}
		}


		public void AddActivityLog(string TextContent, string empName, string DCLog, string actionLog)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				string logdate = DateTime.Now.ToString("yyyyMMdd-HHmmss");
				using (SqlCommand cmd = new("INSERT INTO [Activity Logs] ([TIME STAMP], NAME, ACTION, MESSAGE, [DISCORD LOGS])" +
					"VALUES (@TIMESTAMP, @EMPNAME, @ACTION, @MESSAGE, @DCLOGS)", conn))
				{
					cmd.Parameters.AddWithValue("@TIMESTAMP", logdate);
					cmd.Parameters.AddWithValue("@EMPNAME", empName);
					cmd.Parameters.AddWithValue("@ACTION", actionLog);
					cmd.Parameters.AddWithValue("@MESSAGE", TextContent);
					cmd.Parameters.AddWithValue("@DCLOGS", DCLog);
					cmd.ExecuteNonQuery();
				}
				dc.PublishtoDiscord(Global.AppLogger, "", DCLog, "", Global.DCActivityLoggerWebhook, Global.DCActivityLoggerInvite);
			}
			catch (Exception ex)
			{

				LogError("AddActivityLog", empName, "CommonTask", "N/A", ex);

			}
			finally
			{
				conn.Close();
			}
		}

		public void SendToastNotifDesktop(string message)
		{

			alert.AutoClose = true;
			alert.AutoCloseDelay = 20;
			alert.CaptionText = alercaption;
			alert.ContentText = message;
			alert.ShowOptionsButton = false;
			alert.AutoSize = true;
			alert.ShowCloseButton = false;
			alert.Opacity = 90;
			alert.Show();
		}

		public void UpdateFirstLoginInfo(string query, string userName, string empName, string message)
		{
			try
			{
				using (var con = new SqlConnection(_dbConnection))
				{
					con.Open();
					using var command = new SqlCommand(query, con);
					// Add the parameter for @UserName
					command.Parameters.AddWithValue("@UserName", userName);

					command.ExecuteNonQuery();
				}
				SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				LogError("UpdateFirstLoginInfo", empName, "CommonTask", "N/A", ex);
			}
		}



		private void GetUsersEmail(string name, string empName)
		{
			using var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				using SqlCommand cmd = new("SELECT [Employee Name], [Email Address] FROM [User Information] WHERE [Employee Name] = '" + name + "'", con);
				using var reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					email = reader.IsDBNull(1) ? null : reader.GetString(1);
				}
			}
			catch (Exception ex)
			{
				LogError("GetUsersEmail", empName, "CommonTask", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void NotifyEmail(string request, string mailContent, string employeeName, string empName, string position)
		{
			string emailAddress;
			string mailSubject;
			string ccEmail1;
			string machineName;
			//string ccEmail2;

			try
			{
				//support pdf attachment in next build. - 03222024
				// uncomment when building release
				//emailAddress = "Edimson@pcmsbilling.net";
				//ccEmail1 = "Angeline@pcmsbilling.net";
				//ccEmail2 = "Shalah@pcmsbilling.net";
				machineName = Environment.MachineName;
				if (machineName == "ERWIN-PC")
				{
					emailAddress = "Edimson@yopmail.com";
					ccEmail1 = "Angeline@yopmail.com";
				}
				else
				{
					emailAddress = "Edimson@pcmsbilling.net";
					ccEmail1 = "Angeline@pcmsbilling.net";
				}
				//yopmail use for testing to avoid sending spam/test email in activate account and comment when building release

				//ccEmail2 = "Shalah@yopmail.com";

				if (request == "file")
				{
					GetUsersEmail(employeeName, empName);
					mailSubject = "Filed Leave from " + employeeName;
					if (position == "Management")
					{

						mail.SendEmail("noAttach", mailContent, null, mailSubject, emailAddress, "Filed Leave Notification (via PCMS Lipa General Tool)", null, null);
					}
					else
					{
						mail.SendEmail("noAttach", mailContent, null, mailSubject, emailAddress, "Filed Leave Notification (via PCMS Lipa General Tool)", ccEmail1, null);
						//emailSender.SendEmail("noAttach", mailContent, null, mailSubject, emailAddress, "Filed Leave Notification (via PCMS Lipa General Tool)", );
					}

				}
				else if (request == "response")
				{
					GetUsersEmail(employeeName, empName);
					emailAddress = email;
					mailSubject = "File Leave Update for " + employeeName;
					mail.SendEmail("noAttach", mailContent, null, mailSubject, emailAddress, "Leave Update Notification (via PCMS Lipa General Tool)", null, null);
				}
			}
			catch (Exception ex)
			{
				LogError("GetUsersEmail", empName, "CommonTask", "N/A", ex);
			}


		}

		public string CheckIfExistinDB(string username, string modLoc, string request)
		{
			using SqlConnection conn = new(_dbConnection);
			conn.Open();
			try
			{
				using SqlCommand command = new(
					@"SELECT COUNT(*) FROM [User Information] WHERE USERNAME = @username", conn);
				command.Parameters.AddWithValue("@username", username);

				int userCount = (int)command.ExecuteScalar();

				if (userCount > 0)
				{
					if (modLoc == "UserMgmt" && request == "Create")
					{
						return "Username already exists.";
					}
				}
				else
				{
					if (request == "Login" && modLoc == "Login")
					{
						return "Username not found.";
					}
				}

				return string.Empty; // No issues found
			}
			catch (SqlException sqlEx)
			{
				LogError("CheckIfExistinDB", "N/A", "CommonTask", "SQL Exception", sqlEx);
				return "A database error occurred. Please try again later.";
			}
			catch (Exception ex)
			{
				LogError("CheckIfExistinDB", "N/A", "CommonTask", "General Exception", ex);
				return "An unexpected error occurred. Please contact support.";
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
		}


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

		public DataTable GetSearch(
			string itemToSearch,
			string statusColumn,
			out string searchCount)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Define the base query
				string query = $@"
SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT],
[USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]
FROM [User Information]
WHERE USERNAME LIKE @itemToSearch";

				// Add the STATUS filter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					query += " AND STATUS LIKE @statusSearch";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (statusColumn != "All")
				{
					cmd.Parameters.AddWithValue("@statusSearch", $"%{statusColumn}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				LogError("SearchEmpTwoColumnOneFieldText", "N/A", "CommonTask", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}



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

		public void AlterDBSequece(RadTextBox sequence, RadDropDownList databaseTable, string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				using SqlCommand cmd = new("ALTER SEQUENCE " + databaseTable.Text + " RESTART WITH " + sequence.Text, conn);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				LogError($"AlterDBSequece", empName, "CommonTask", "N/A", ex);
			}
			finally
			{
				conn.Close();
			}
		}

		public void GetSequenceNoPre(string query, RadLabel nextSequence)
		{
			//int Price;
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					int currSeq = reader.GetInt32(0);
					//nextSequence.Text = preID + (currSeq + 1).ToString();
					nextSequence.Text = currSeq.ToString();// + ToString();
				}
				con.Close();


			}
			catch (Exception ex)
			{
				LogError($"GetSequenceNoPre", "N/A", "CommonTask", "N/A", ex);
			}
		}

		public string GetSequenceNo(string sequenceName, string preID)
		{
			var query = "SELECT NEXT VALUE FOR " + sequenceName;
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();

				if (reader.Read())
				{
					int currSeq = reader.GetInt32(0);
					string result = preID + (currSeq + 1).ToString();

					// Return the formatted sequence string
					return result;
				}

				// If no sequence value is found, return null or an empty string
				return null;
			}
			catch (Exception ex)
			{
				LogError("GetSequenceNo", "N/A", "CommonTask", "N/A", ex);
				return null; // Return null in case of an error
			}
			finally
			{
				con.Close();
			}
		}

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

		public void ExportTableToExcel(DataTable dataTable, string filename, string empName)
		{
			try
			{
				// Path to save the file on the desktop
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string filePath = Path.Combine(desktopPath, $"{filename}.xlsx");

				// Export to Excel using ClosedXML
				using (XLWorkbook workbook = new())
				{
					workbook.Worksheets.Add(dataTable, filename);
					workbook.SaveAs(filePath);
				}

				// Log success
				Console.WriteLine($"Export successful! File saved to: {filePath}");
			}
			catch (Exception ex)
			{
				LogError("ExportTableToExcel", empName, "CommonTask", "N/A", ex);
				throw new Exception("An error occurred during the export process.", ex);
			}
		}


		


		public void LogError(string processName, string empName, string module, string ID, Exception ex)
		{
			int maxlengthforDC = 399;

			// Ensure the substring length does not exceed the actual string length
			string detailedError = ex.ToString().Length > maxlengthforDC
				? ex.ToString().Substring(0, maxlengthforDC)
				: ex.ToString();

			var errorMessage = $@"
				Error: {ex.Message}
				Name: {empName}
				Module: {module}
				Process: {processName}
				ID: {ID}
				Detailed Error: {detailedError}";

			dc.PublishtoDiscord(
				Global.errorNameSender,
				string.Empty,
				errorMessage,
				empName,
				Global.DCErrorWebHook,
				Global.DCErrorInvite);
		}



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