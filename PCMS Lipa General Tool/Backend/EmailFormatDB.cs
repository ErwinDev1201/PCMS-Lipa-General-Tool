using Newtonsoft.Json.Converters;
using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace PCMS_Lipa_General_Tool.Class
{
	public class EmailFormatDB
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;
			
			string nextSequence = db.GetSequenceNo("EmailFormatSeq", "EF-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					ID = nextSequence;
					return;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("GetDBID", empName, "EmailFormatDB", "N/A", ex);
			}
			//db.GetSequenceNo("textbox", "EmailFormatSeq", txtIntID.Text, null, "EF-");
		}

		public DataTable ViewEmailFormatList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Insurance Email Format]";
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
				notif.LogError("ViewEmailFormatList", empName, "EmailFormatDB", "N/A", ex);
			}

			return data;
		}



		public bool EmailFormatDBRequest(
			string request,
			string formatID,
			string insuranceName, 
			string emailFormat,
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
					"Update" => @"
								UPDATE [Insurance Email Format] SET [Insurance] = @INSURANCE
									[Email Format] = @EMAILFORMAT, REMARKS = @REMARKS 
								WHERE
									[Format ID] = @FORMATID",
					"Create" => @"
								INSERT INTO [Insurance Email Format] ([Format ID],
								[Insurance], [Email Format], [Remarks])
								VALUES
								(@FORMATID, @INSURANCE, @EMAILFORMAT, @REMARKS)",
					"Delete" => @"DELETE FROM [Insurance Email Format] WHERE [Format ID] = @FORMATID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@FORMATID", formatID);
					cmd.Parameters.AddWithValue("@INSURANCE", insuranceName);
					cmd.Parameters.AddWithValue("@EMAILFORMAT", emailFormat);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@FORMATID", formatID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Email Format ID: {formatID}";
				message = $"Done! {formatID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} EMAIL FORMAT INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(logs, "success");
			}
			catch (Exception ex)
			{
				notif.LogError($"EmailFormatDBRequest - {request}", empName, "EmailFormatDB", formatID, ex);
				message = $"Failed to {request.ToLower()} {formatID}, Please try again later";
				return false;
				//throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
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
FROM [Insurance Email Format]
WHERE [Insurance] LIKE @searchTerm
OR [Email Format] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				notif.LogError("SearchData", empName, "BillReviews", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}
	}
}
