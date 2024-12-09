using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;



namespace PCMS_Lipa_General_Tool.Class
{
	public class MPN
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];

		private readonly CommonTask task = new();

		public DataTable ViewMPNList(string empName, out string lblCount)
		{
			const string query = "SELECT * FROM [MPN Information]";
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
				task.LogError("ViewMPNList", empName, "MPN", "N/A", ex);
			}

			return data;
		}


		public void MPNDBRequest(
			string request,
			string mpnID,
			string insuranceName,
			string mpn,
			string userName,
			string passWord,
			string webSite,
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

				cmd.CommandText = request switch
				{
					"Update" => @"
								UPDATE [MPN Information] SET [Insurance Name] = @INSURANCENAME,
									[MPN] = @MPN, [Username] = @USERNAME, [Password] = @PASSWORD,
									[Website] = @WEBSITE, REMARKS = @REMARKS 
								WHERE
									[MPN ID] = @MPNID",
					"Create" => @"
								INSERT INTO [MPN Information] ([MPN ID], [Insurance Name], [MPN], [Username],
									[Password], [Website], [Remarks])
								VALUES
									(@MPNID, @INSURANCENAME, @MPN, @USERNAME, @PASSWORD,  @WEBSITE, @REMARKS)",
					"Delete" => @"DELETE FROM [MPN Information] WHERE [MPN ID] = @MPNID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@MPNID", mpnID);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@MPN", mpn);
					cmd.Parameters.AddWithValue("@USERNAME", userName);
					cmd.Parameters.AddWithValue("@PASSWORD", passWord);
					cmd.Parameters.AddWithValue("@WEBSITE", webSite);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);

				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@MPNID", mpnID);
					
				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d MPN ID: {mpnID}";
				message = $"Done! {mpnID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} MPN INUSURANCE INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError("MPNDBRequest", empName, "MPN", mpnID, ex);
				throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
			}
		}

		public DataTable SearchData(
	string searchTerm,
	out string searchCount,
	string empName)
		{
			DataTable resultTable = new();

			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				string query = $@"
SELECT *
FROM [MPN Information]
WHERE [Insurance Name] LIKE @searchTerm
OR [Remarks] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				task.LogError("SearchData", empName, "MPN", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

	}
}
