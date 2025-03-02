using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Data.SqlClient;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Diagnosis
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("DiagSeq", "DX-");

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
				notif.LogError("GetDBID", empName, "Diagnosis", "N/A", ex);
			}
			/////db.GetSequenceNo("textbox", "DiagSeq", txtIntID.Text, null, "DX-");
		}

		public DataTable ViewDxList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Diagnosis]";
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
				notif.LogError("ViewDxList", empName, "Diagnosis", "N/A", ex);
			}

			return data;
		}

		public DataTable SearchData(
			string searchItem,
			string bodyParts,
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
FROM [Diagnosis]
WHERE Diagnosis LIKE @searchItem";

				// Add the STATUS filter only if statusColumn is not "All"
				if (bodyParts != "All")
				{
					query += " AND [Body Parts] LIKE @bodyParts";
				}

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@bodyParts", $"%{bodyParts}%");

				// Add the @statusSearch parameter only if statusColumn is not "All"
				if (bodyParts == "")
				{
					cmd.Parameters.AddWithValue("@searchItem", $"%{searchItem}%");
				}

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				// Log the error and provide feedback
				notif.LogError("SearchEmpTwoColumnOneFieldText", empName, "CommonTask", "N/A", ex);
				searchCount = "Error occurred while fetching records.";
			}

			return resultTable;
		}

		public bool DiagnosisDBRequest(
			string request,
			string dxID,
			string icd10,
			string icd9,
			string Diagnosis,
			string BodyParts,
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
					"Update" => @"UPDATE [Diagnosis]
								SET [ICD-10] = @ICD10, [ICD-9] = @ICD9, [Diagnosis] = @DIAGNOSIS,
								[Body Parts] = @BODYPARTS, REMARKS = @REMARKS 
								WHERE
								[Diagnosis ID] = @DXID",
					"Create" => @"INSERT INTO [Diagnosis]
								([Diagnosis ID], [ICD-10], [ICD-9], [Diagnosis], [Body Parts], [Remarks])
								VALUES ( @DXID, @ICD10, @ICD9, @DIAGNOSIS, @BODYPARTS, @REMARKS)",
					"Delete" => @"DELETE FROM [Diagnosis] WHERE [Diagnosis ID] = @DXID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@DXID", dxID);
					cmd.Parameters.AddWithValue("@ICD10", icd10);
					cmd.Parameters.AddWithValue("@ICD9", icd9);
					cmd.Parameters.AddWithValue("@DIAGNOSIS", Diagnosis);
					cmd.Parameters.AddWithValue("@BODYPARTS", BodyParts);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@DXID", dxID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Diagnosis ID: {dxID}";
				message = $"Done! {dxID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} DIAGNOSIS INFORMATION");
				///fe.SendToastNotifDesktop(message, "Success");
				return true;

			}
			catch (Exception ex)
			{
				notif.LogError($"DiagnosisDBRequest - {request}", empName, "Diagnosis", dxID, ex);
				message = $"Failed to {request.ToLower()} {dxID}, Please try again later";
				return false;
				//RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
