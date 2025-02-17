using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class ActivtiyLogs
	{
		private static readonly Database db = new();
		private readonly string _dbConnection = db.GetDbConnection();

		readonly WinDiscordAPI dc = new();
		readonly Notification notif = new();


		public DataTable ViewActivityLogs(string empName, out string lblCount)
		{
			const string query = "SELECT * FROM [Activity Logs] ORDER BY [Activity ID]";
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
				notif.LogError("ViewActivityLogs", empName, "ActivityLogs", "N/A", ex);
			}

			return data;
		}

		public DataTable GetSearch(
			string itemToSearch,
			string actionColumn,
			out string searchCount, string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				// Define the base query
				string query = $@"
SELECT *
FROM [Activity Logs]
WHERE Name LIKE @itemToSearch
OR Message LIKE @itemToSearch";

				// Add the STATUS filter only if statusColumn is not "All"
				if (actionColumn != "All")
				{
					query += " AND ACTION LIKE @actionSearch";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@itemToSearch", $"%{itemToSearch}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (actionColumn != "All")
				{
					cmd.Parameters.AddWithValue("@actionSearch", $"%{actionColumn}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				notif.LogError("GetSearch", empName, "CommonTask", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}

		public List<string> GetListofAction(string empName)
		{
			var query = "SELECT DISTINCT [Action] from [Activity Logs] ORDER BY [Action]";
			var items = new List<string>();
			var con = new SqlConnection(_dbConnection);
			try
			{
				con.Open();
				SqlCommand cmd = new(query, con);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					items.Add(reader.GetString(0));
				}
				con.Close();
			}
			catch (Exception ex)
			{
				notif.LogError("GetListofAction", empName, "ActivityLogs", "N/A", ex);
			}
			return items;
		}

		public void AddActivityLog(string TextContent, string empName, string DCLog, string actionLog)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();

				string logdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
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

				notif.LogError("AddActivityLog", empName, "ActivtiyLogs", "N/A", ex);

			}
			finally
			{
				conn.Close();
			}
		}
	}


}
