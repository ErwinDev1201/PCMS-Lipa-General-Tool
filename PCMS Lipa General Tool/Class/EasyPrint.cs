using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace PCMS_Lipa_General_Tool.Class
{
	public class EasyPrint
	{
		
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();


		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("EasyPrintSeq", "EP-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
				}
			}
			catch (Exception ex)
			{
				error.LogError("GetDBID", empName, "EasyPrint", "N/A", ex);
			}
		}

		public DataTable ViewEasyPrintList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Easy Print Denial]";
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
				error.LogError("ViewEasyPrintList", empName, "EasyPrint", "N/A", ex);
			}

			return data;
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
FROM [Easy Print Denial]
WHERE [Easy Print Code] LIKE @searchTerm
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
				error.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

		public bool EPDenialDBRequest(
			string request,
			string epdenialID,
			string easyprintCode,
			string insuranceName,
			string Description,
			string possibleResolution,
			string remarks,
			string empName,
			out string message)
		{
			using SqlConnection conn = new(_dbConnection);
			try
			{
				conn.Open();
				SqlCommand cmd = new()
				{
					Connection = conn
				};

				string logs;

				cmd.CommandText = request switch
				{
					"Update" => @"UPDATE [Easy Print Denial]
								SET [Easy Print Code] = @EPCODE, [Insurance] = @INSURANCENAME,
									[Description] = @DESCRIPTION, [Possible Resolution] = @POSSIBLERESO,
									REMARKS = @REMARKS
								WHERE
									[EP ID] = @EPDENIALCODE",
					"Create" => @"INSERT INTO [Easy Print Denial]
								([EP ID], [Easy Print Code], [Insurance], [Description], [Possible Resolution], [Remarks])
								VALUES (@EPDENIALCODE, @EPCODE, @INSURANCENAME, @DESCRIPTION, @POSSIBLERESO, @REMARKS)",
					"Delete" => @"DELETE FROM [Easy Print Denial] WHERE [EP ID] = @EPDENIALCODE",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID);
					cmd.Parameters.AddWithValue("@EPCODE", easyprintCode);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@DESCRIPTION", Description);
					cmd.Parameters.AddWithValue("@POSSIBLERESO", possibleResolution);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@EPDENIALCODE", epdenialID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Denial ID: {epdenialID}";
				message = $"Done! {epdenialID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} EASYPRINT INFORMATION");
				//fe.SendToastNotifDesktop(message, "Success");
				return true;
			}
			catch (Exception ex)
			{
				error.LogError($"EPDenialDBRequest - {request}", empName, "EasyPrint", epdenialID, ex);
				//throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
				message = $"Failed to {request.ToLower()} {epdenialID}, Please try again later";
				return false;
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
