﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;



namespace PCMS_Lipa_General_Tool.Class
{
	public class HearingRep
	{
		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();

		public DataTable ViewHearinRepList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Hearing Representative]";
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
				task.LogError("ViewHearinRepList", empName, "HearingRep", "N/A", ex);
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
FROM [Hearing Representative]
WHERE [Name] LIKE @searchTerm
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
				task.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

		public void HRRepDBRequest(
			string request,
			string repID,
			string boardNo,
			string hearRepName,
			string email,
			string phoneNo, 
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
								UPDATE [Hearing Representative] SET [Board] = @BOARD,
									[Name] = @NAME,[Email] = @EMAIL,
									[Phone No.] = @PHONENO, REMARKS = @REMARKS 
								WHERE
									[Rep ID] = @REPID",
					"Create" => @"
								INSERT INTO [Hearing Representative] ([Rep ID], [Board], [Name], [Email], [Phone No.], [Remarks])
									VALUES 
								(@REPID, @BOARD, @NAME, @EMAIL, @PHONENO, @REMARKS",
					"Delete" => @"DELETE FROM [Hearing Representative] WHERE [Rep ID] = @REPID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					cmd.Parameters.AddWithValue("@REPID", repID);
					cmd.Parameters.AddWithValue("@BOARD", boardNo);
					cmd.Parameters.AddWithValue("@NAME", hearRepName);
					cmd.Parameters.AddWithValue("@EMAIL", email);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@REPID", repID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Hearing Rep ID: {repID}";
				message = $"Done! {repID} has been successfully {request.ToLower()}d.";
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} HEARING REP INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"HRRepDBRequest - {request}", empName, "Hearing Rep", repID, ex);
				throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
