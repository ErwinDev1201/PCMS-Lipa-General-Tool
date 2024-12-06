using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class ActivtiyLogs
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		readonly CommonTask task = new();

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
				task.LogError("ViewActivityLogs", empName, "ActivityLogs", "N/A", ex);
			}

			return data;
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
				task.LogError("GetListofAction", empName, "ActivityLogs", "N/A", ex);
			}
			return items;
		}
	}
}
