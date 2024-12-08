using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace PCMS_Lipa_General_Tool.Class
{
	public class CollectorNotes
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();



		public void FillNotesInfo(RadGridView dgCurrentNotes, RadTextBox txtIntID, RadDropDownList cmbProviderList, RadTextBox txtChartNo, RadTextBox txtPatientName, RadTextBoxControl txtNotes, RadTextBoxControl txtRemarks, string empName)
		{
			using SqlConnection con = new(_dbConnection);
			try
			{
				con.Open();
				{
					using SqlCommand cmd = new("SELECT * FROM [COLLECTOR NOTES]", con);
					cmd.ExecuteNonQuery();
					var dgRow = dgCurrentNotes.SelectedRows[0];
					{
						txtIntID.Text = dgRow.Cells[0].Value + string.Empty;
						cmbProviderList.Text = dgRow.Cells[3].Value + string.Empty;
						txtChartNo.Text = dgRow.Cells[4].Value + string.Empty;
						txtPatientName.Text = dgRow.Cells[5].Value + string.Empty;
						txtNotes.Text = dgRow.Cells[6].Value + string.Empty;
						txtRemarks.Text = dgRow.Cells[7].Value + string.Empty;
					}
				}
			}
			catch (Exception ex)
			{
				task.LogError("FillNotesInfo", empName, "ModifyNotes", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}

		public void NoteDBRequest(
			string request,
			string noteID,
			string providerName,
			string chartNo,
			string patientName,
			string notes,
			string remarks,
			string empName)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs, message;

				// Define SQL command based on the request type
				cmd.CommandText = request switch
				{
					"Update" => @"
                    UPDATE [Collector Notes]
                    SET 
                        [Provider Name] = @providerName,
                        [Chart No] = @chartNo, [Patient Name] = @patientName,
                        [Notes] = @Notes, [Collector Name] = @collectorName,
                        REMARKS = @REMARKS
                    WHERE
                        [Notes ID] = @noteID",
					"Create" => @"
                    INSERT INTO [Collector Notes] ([Notes ID], Date, [Time Stamp], [Provider Name], [Chart No],
                        [Patient Name], Notes, [Collector Name], Remarks)
                    VALUES
                        (@noteID, @Date, @TimeStamp, @providerName,
                        @chartNo, @patientName, @Notes, @collectorName, @Remarks)",
					"Delete" => @"
                    DELETE FROM
                        [Collector Notes]
                    WHERE
                        [Notes ID] = @noteID",
					_ => throw new ArgumentException("Invalid request type"),
				};

				// Add parameters for Update and Create requests
				if (request != "Delete")
				{
					var currentDate = DateTime.Now.ToString("yyyy-MM-dd"); // Date only
					DateTime currentTimeStamp = DateTime.Now; // Full timestamp

					cmd.Parameters.AddWithValue("@Date", currentDate);
					cmd.Parameters.AddWithValue("@TimeStamp", currentTimeStamp);
					cmd.Parameters.AddWithValue("@providerName", string.IsNullOrEmpty(providerName) ? (object)DBNull.Value : providerName);
					cmd.Parameters.AddWithValue("@chartNo", chartNo ?? string.Empty);
					cmd.Parameters.AddWithValue("@patientName", patientName ?? string.Empty);
					cmd.Parameters.AddWithValue("@Notes", notes ?? string.Empty);
					cmd.Parameters.AddWithValue("@collectorName", empName ?? string.Empty);
					cmd.Parameters.AddWithValue("@Remarks", remarks ?? string.Empty);
				}

				// Add common parameter for all requests
				cmd.Parameters.AddWithValue("@noteID", noteID ?? string.Empty);

				// Execute the SQL command
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Collector Notes ID: {noteID}";
				message = $"Done! {noteID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} COLLECTOR NOTES INFORMATION");
				task.SendToastNotifDesktop(message);
			}
			catch (Exception ex)
			{
				task.LogError("CollectorNotes", empName, "NoteDBRequest", noteID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}

		//public void ViewNotesToday(RadGridView dataGrid, RadLabel lblcount, string empName)
		//{
		//	var query = $"SELECT * FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now:yyyy-MM-dd}'";
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		using (var adp = new SqlDataAdapter(query, con))
		//		{
		//			var data = new DataTable();
		//			adp.Fill(data);
		//			adp.Update(data);
		//			dataGrid.DataSource = data.DefaultView;
		//			lblcount.Text = $"Total Notes (Today): {dataGrid.RowCount}";
		//		}
		//		dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError($"ViewNotesToday", empName, "CollectorNotes", "N/A", ex);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//}

		public DataTable ViewNotesToday(string empName, out string lblCount)
		{
			var query = $"SELECT * FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now:yyyy-MM-dd}'";
			var data = new DataTable();
			lblCount = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var adp = new SqlDataAdapter(query, con);

				// Fill the DataTable with data from the query
				adp.Fill(data);

				// Calculate the record count
				lblCount = $"Total records: {data.Rows.Count}";
			}
			catch (Exception ex)
			{
				task.LogError("ViewAdjusterList", empName, "Adjuster", "N/A", ex);
			}

			return data;
		}

		

		public void ViewNotesMonth(string lblcount, string lblAverage, string empName)
		{
			var noofNotesMonthQuery = $"SELECT COUNT(*) FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now:yyyy-MM}%'";
			var noofdaysQuery = $"SELECT COUNT(DISTINCT CAST(DATE AS DATE)) FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now:yyyy-MM}%'";

			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();

				int noofNotesMonth = 0;
				int noofdays = 0;

				// Execute query to get total notes for the month
				using (var cmd = new SqlCommand(noofNotesMonthQuery, con))
				{
					noofNotesMonth = (int)cmd.ExecuteScalar();
					lblcount = $"Total Notes (This Month): {noofNotesMonth}";
				}

				// Execute query to get the count of distinct days with notes
				using (var cmd = new SqlCommand(noofdaysQuery, con))
				{
					noofdays = (int)cmd.ExecuteScalar();
				}

				// Calculate average notes per day
				double averageNotesPerDay = noofdays > 0 ? (double)noofNotesMonth / noofdays : 0;
				lblAverage = $"Average Notes per Day: {averageNotesPerDay:F2}";
			}
			catch (Exception ex)
			{
				task.LogError($"ViewNotesMonth", empName, "CollectorNotes", "N/A", ex);
			}
			finally
			{
				con.Close();
			}
		}


		public DataTable ViewNotes(string empName, out string lblCount, string position)
		{
			string query;
			if (position == "Collector")
			{
				query = $"SELECT * FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%'";
			}
			else
			{
				query = $"SELECT * FROM [Collector Notes]";
			}
			var data = new DataTable();
			lblCount = string.Empty;

			try
			{
				using var con = new SqlConnection(_dbConnection);
				using var adp = new SqlDataAdapter(query, con);

				// Fill the DataTable with data from the query
				adp.Fill(data);

				// Calculate the record count
				lblCount = $"Total records: {data.Rows.Count}";
			}
			catch (Exception ex)
			{
				task.LogError("ViewAdjusterList", empName, "Adjuster", "N/A", ex);
			}

			return data;
		}

		//public void ViewNotes(DataTable dataGrid, string empName, string position)
		//{
		//	string query;
		//	if (position == "Collector")
		//	{
		//		query = $"SELECT * FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%'";
		//	}
		//	else
		//	{
		//		query = $"SELECT * FROM [Collector Notes]";
		//	}
		//
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		using (var adp = new SqlDataAdapter(query, con))
		//		{
		//			var data = new DataTable();
		//			adp.Fill(data);
		//			adp.Update(data);
		//			dataGrid.DataSource = data.DefaultView;
		//			//lcount.Text = $"Total Notes (Today): {dataGrid.RowCount}";
		//		}
		//		dataGrid.BestFitColumns(BestFitColumnMode.AllCells);
		//	}
		//	catch (Exception ex)
		//	{
		//		task.LogError($"ViewNotes", empName, "CollectorNotes", "N/A", ex);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//}

		public void SearchTextAcrossColumns(RadGridView uiTableName, string dbTableName, string searchValue, RadLabel searchCount, string empName)
		{
			List<string> columnsToSearch = ["[PROVIDER NAME]", "[CHART NO]", "[PATIENT NAME]", "NOTES", "REMARKS"];

			if (columnsToSearch == null || columnsToSearch.Count == 0)
			{
				throw new ArgumentException("Columns to search must be non-empty.");
			}

			using SqlConnection conn = new(_dbConnection);
			try
			{
				using DataTable dt = new(dbTableName);
				// Build the WHERE clause dynamically to search the value across multiple columns
				var whereClauses = columnsToSearch.Select((col, index) => $"{col} LIKE @searchValue").ToList();
				string whereClause = string.Join(" OR ", whereClauses);

				string query = $"SELECT * FROM {dbTableName} WHERE {whereClause}";

				using SqlCommand cmd = new(query, conn);
				// Use a single parameter for the search value
				cmd.Parameters.AddWithValue("@searchValue", $"%{searchValue}%");

				SqlDataAdapter adapter = new(cmd);
				adapter.Fill(dt);
				uiTableName.DataSource = dt;
				searchCount.Text = $"Total records: {uiTableName.RowCount}";
			}
			catch (Exception ex)
			{
				task.LogError($"SearchTextAcrossColumns", empName, "CollectorNotes", "N/A", ex);
			}
		}


		public void FilterCollectorNotes(RadGridView uiTableName, string dbTableName, RadLabel searchCount, string providerName, string dateFilterStart, string dateFilterEnd, string patientName, string empName)
		{
			if (string.IsNullOrEmpty(dbTableName))
				throw new ArgumentException("Table name cannot be null or empty.");

			// Ensure columns and search values are aligned
			var filters = new Dictionary<string, string>
			{
				{
					"[PROVIDER NAME]", providerName
				},
				{
					"[PATIENT NAME]", patientName
				}
			};

			// Remove empty filter criteria
			filters = filters.Where(f => !string.IsNullOrEmpty(f.Value)).ToDictionary(f => f.Key, f => f.Value);

			// Build query dynamically
			var whereClauses = new List<string>();
			int paramIndex = 0;

			foreach (var filter in filters)
			{
				whereClauses.Add($"{filter.Key} LIKE @param{paramIndex}");
				paramIndex++;
			}

			if (!string.IsNullOrEmpty(dateFilterStart) && !string.IsNullOrEmpty(dateFilterEnd))
			{
				whereClauses.Add("[DATE] BETWEEN @dateStart AND @dateEnd");
			}

			string whereClause = whereClauses.Any() ? $"WHERE {string.Join(" AND ", whereClauses)}" : string.Empty;
			string query = $"SELECT * FROM {dbTableName} {whereClause}";

			try
			{
				using var conn = new SqlConnection(_dbConnection);
				using var cmd = new SqlCommand(query, conn);
				conn.Open();

				// Add parameters
				paramIndex = 0;
				foreach (var filter in filters)
				{
					cmd.Parameters.Add($"@param{paramIndex}", SqlDbType.NVarChar).Value = $"%{filter.Value}%";
					paramIndex++;
				}

				if (!string.IsNullOrEmpty(dateFilterStart) && !string.IsNullOrEmpty(dateFilterEnd))
				{
					cmd.Parameters.Add("@dateStart", SqlDbType.Date).Value = dateFilterStart;
					cmd.Parameters.Add("@dateEnd", SqlDbType.Date).Value = dateFilterEnd;
				}

				using var adapter = new SqlDataAdapter(cmd);
				using var dt = new DataTable(dbTableName);
				adapter.Fill(dt);
				uiTableName.DataSource = dt;
				searchCount.Text = $"Total records: {dt.Rows.Count}";
			}
			catch (Exception ex)
			{
				task.LogError($"filterCollectorNotes", empName, "CollectorNotes", "N/A", ex);
			}
		}


		//public void filterCollectorNotes(RadGridView uiTableName, string dbTableName, RadLabel searchCount, string providerName, string dateFilterStart, string dateFilterEnd, string patientName)
		//{
		//	List<string> columnsToSearch = new List<string> { "[PROVIDER NAME]", "[PATIENT NAME]" };
		//	List<object> searchValues = new List<object> { providerName, patientName };
		//
		//	if (columnsToSearch == null || columnsToSearch.Count != searchValues.Count || columnsToSearch.Count == 0)
		//	{
		//		throw new ArgumentException("Columns to search and search values must be non-empty and have the same count.");
		//	}
		//
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			using (DataTable dt = new DataTable(dbTableName))
		//			{
		//				// Dynamically build the WHERE clause
		//				var whereClauses = new List<string>();
		//				for (int i = 0; i < columnsToSearch.Count; i++)
		//				{
		//					whereClauses.Add($"{columnsToSearch[i]} LIKE %@param{i}%");
		//				}
		//
		//				// Add date range filter
		//				if (!string.IsNullOrEmpty(dateFilterStart) && !string.IsNullOrEmpty(dateFilterEnd))
		//				{
		//					whereClauses.Add("[DATE] BETWEEN @dateStart AND @dateEnd");
		//				}
		//
		//				string whereClause = string.Join(" AND ", whereClauses);
		//
		//				string query = $"SELECT * FROM {dbTableName} WHERE {whereClause}";
		//
		//				using (SqlCommand cmd = new SqlCommand(query, conn))
		//				{
		//					// Add parameters for provider and patient
		//					for (int i = 0; i < searchValues.Count; i++)
		//					{
		//						cmd.Parameters.AddWithValue($"@param{i}", $"%{searchValues[i]}%");
		//					}
		//
		//					// Add parameters for date range
		//					if (!string.IsNullOrEmpty(dateFilterStart) && !string.IsNullOrEmpty(dateFilterEnd))
		//					{
		//						cmd.Parameters.AddWithValue("@dateStart", dateFilterStart);
		//						cmd.Parameters.AddWithValue("@dateEnd", dateFilterEnd);
		//					}
		//
		//					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
		//					adapter.Fill(dt);
		//					uiTableName.DataSource = dt;
		//					searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			var errorMessage = ex.Message + "\n\n Name: " + providerName +
		//							   "\nModule: SearchEmpDynamicColumnsWithDateRange \n Process: Search \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", errorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//	}
		//}
		//



		//public void SearchEmpTwoColumnTwoFieldText(RadGridView uiTableName, string dbTableName, string columntosearch1, string columntosearch2, RadTextBox itemToSearch, RadDropDownList itemToSearch2, RadLabel searchCount, string empName)
		//{
		//	using (SqlConnection conn = new SqlConnection(_dbConnection))
		//	{
		//		try
		//		{
		//			using (DataTable dt = new DataTable(dbTableName))
		//			{
		//				using (SqlCommand cmd = new SqlCommand("SELECT [EMPLOYEE ID], [EMPLOYEE NAME], USERNAME, [DEPARTMENT], [USER ACCESS], POSITION, STATUS, OFFICE, [EMAIL ADDRESS]" +
		//					"FROM [User Information] WHERE " + columntosearch1 + " LIKE @columntoSearch2 AND " + columntosearch2 + " LIKE @columntoSearch3", conn))
		//				{
		//					cmd.Parameters.AddWithValue("columntoSearch2", string.Format("%{0}%", itemToSearch.Text));
		//					cmd.Parameters.AddWithValue("columntoSearch3", string.Format(itemToSearch2.Text));
		//					SqlDataAdapter adapter = new SqlDataAdapter(cmd);
		//					adapter.Fill(dt);
		//					uiTableName.DataSource = dt;
		//					searchCount.Text = $"Total records: {uiTableName.RowCount}";
		//				}
		//			}
		//
		//		}
		//		catch (Exception ex)
		//		{
		//			var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: SearchTwoColumnOneField \n Process: Search \n\n Detailed Error: " + ex.ToString();
		//			winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//		}
		//	}
		//}

		//public void ViewNotesMonth(RadLabel lblcount, RadLabel lblAverage, string empName)
		//{
		//	var noofNotesMonth = $"SELECT COUNT(*) FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now.ToString("yyyy-MM")}%'";
		//	var noofdays = $"SELECT DISTINCT COUNT(*) FROM [Collector Notes] WHERE [Collector Name] LIKE '%{empName}%' AND DATE LIKE '{DateTime.Now.ToString("yyyy-MM")}%'";
		//	var con = new SqlConnection(_dbConnection);
		//	try
		//	{
		//		con.Open();
		//		using (var cmd = new SqlCommand(noofNotesMonth, con))
		//		{
		//			int count = (int)cmd.ExecuteScalar();
		//			lblcount.Text = $"Total Notes (This Month): {count}";
		//		}
		//		using (var cmd = new SqlCommand(noofNotesMonth, con))
		//		{
		//			int count2 = (int)cmd.ExecuteScalar();
		//		}
		//		lblAverage.Text = c
		//	}
		//	catch (Exception ex)
		//	{
		//		var ErrorMessage = ex.Message + "\n\n Name: " + empName + "\nModule: DBQuery \n Process: ViewDataTable \n\n Detailed Error: " + ex.ToString();
		//		winDiscordAPI.PublishtoDiscord(Global.errorNameSender, "", ErrorMessage, "", Global.DCErrorWebHook, Global.DCErrorInvite);
		//	}
		//	finally
		//	{
		//		con.Close();
		//	}
		//}


	}
}