﻿using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Data.SqlClient;


namespace PCMS_Lipa_General_Tool.Class
{
	public class Attorney
	{

		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();


		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("AttyInfo", "ATTY-0");

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
				notif.LogError("GetProvID", empName, "Bundle", "N/A", ex);
			}
			//db.GetSequenceNo("textbox", "BundleCodeSeq", txtIntID.Text, null, "TX-");
		}

		public DataTable ViewAttorneyList(string empName, out string lblCount)
		{
			lblCount = "Total records: 0"; // Default value to avoid empty results

			try
			{
				string query = "SELECT [AttorneyID], [Name], [Firm], [Email], [Phone] FROM [Attorney Information] ORDER BY [Name]";

				using (var con = new SqlConnection(_dbConnection))
				using (var adp = new SqlDataAdapter(query, con))
				{
					var data = new DataTable();
					adp.Fill(data);

					// Set record count
					lblCount = $"Total records: {data.Rows.Count}";
					return data;
				}
			}
			catch (Exception ex)
			{
				notif.LogError("ViewAttorneyList", empName, "Attorney", "N/A", ex);
				lblCount = "Error retrieving records.";
				return new DataTable(); // Return empty table on error
			}
		}


		public DataTable SearchData(
	string searchTerm,
	string AttorneyType,
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
WHERE ([Attorney Name] LIKE @searchTerm
OR [Remarks] LIKE @searchTerm)";

				// Add the STATUS filter only if statusColumn is not "All"
				if (AttorneyType != "All")
				{
					query += " AND [Attorney Type] LIKE @attyType";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (AttorneyType != "All")
				{
					cmd.Parameters.AddWithValue("@attyType", $"%{AttorneyType}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				notif.LogError("SearchData", empName, "Adjuster", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}


		public bool AttorneyDBRequest(
			string request,
			string attyID,
			string cmbAttyType,
			string attyName,
			string phoneNo,
			string faxNo,
			string email,
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
								UPDATE [Attorney Information] SET [Attorney Type] = @ATTYTYPE,
									[Attorney Name] = @ATTYNAME, [Phone No.] = @PHONENO,
									[Fax No.] = @FAXNO, [Email] = @EMAIL, REMARKS = @REMARKS
								WHERE
									[Attorney ID] = @ATTYID",
					"Create" => @"
								INSERT INTO [Attorney Informations] ([Attorney ID], [Attorney Type], [Attorney Name],
									[Phone No.], [Fax No.], [Email], [Remarks])
								VALUES
									(@ATTYID, @ATTYTYPE, @ATTYNAME, @PHONENO, @FAXNO, @EMAIL, @REMARKS)",
					"Delete" => @"DELETE FROM [Attorney Information] WHERE [Attorney ID] = @ATTYID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@ATTYID", attyID);
					cmd.Parameters.AddWithValue("@ATTYTYPE", cmbAttyType);
					cmd.Parameters.AddWithValue("@ATTYNAME", attyName);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo);
					cmd.Parameters.AddWithValue("@FAXNO", faxNo);
					cmd.Parameters.AddWithValue("@EMAIL", email);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@ATTYID", attyID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Atty ID: {attyID}";
				message = $"Done! {attyID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} ATTORNEY INFORMATION");
				return true;
				////fe.SendToastNotifDesktop(message, "Success");
			}
			catch (Exception ex)
			{
				notif.LogError($"AttorneyDBRequest {request}", empName, "Attorney", attyID, ex);
				message = $"Failed to {request.ToLower()} {attyID}, Please try again later";
				return false;
				///throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
			}
			//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			finally
			{
				conn.Close();
			}
		}
	}
}
