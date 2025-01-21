using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Bundle
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("BundleCodeSeq", "PR-");

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
				error.LogError("GetProvID", empName, "Bundle", "N/A", ex);
			}
			//db.GetSequenceNo("textbox", "BundleCodeSeq", txtIntID.Text, null, "TX-");
		}


		public DataTable ViewBundleCodes(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Bundle Codes]";
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
				error.LogError("ViewBundleCodes", empName, "Bundle", "N/A", ex);
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
FROM [Bundle Codes]
WHERE [CPT Code] LIKE @searchTerm
OR [Bundle Codes] LIKE @searchTerm
OR [Adjuster Name] LIKE @searchTerm";

				using SqlCommand cmd = new(query, conn);
				cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

				using SqlDataAdapter adapter = new(cmd);
				adapter.Fill(resultTable);

				// Calculate the search count
				searchCount = $"Total records: {resultTable.Rows.Count}";
			}
			catch (Exception ex)
			{
				error.LogError("SearchData", empName, "Bundle", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}	


		public bool BundleCodesDBRequest(
			string request,
			string treatmentID,
			string cptCode,
			string bundleCodes,
			string Description, 
			string indicator,
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
								UPDATE [Bundle Codes]
									SET [CPT Code] = @CPTCODE, [Bundle Codes] = @BUNDLECODE,
									[Description] = @DESCRIPTION, [Indicator] = @INDICATOR, 
									REMARKS = @REMARKS
								WHERE
									[Treatment ID] = @TREAMENTID",
					"Create" => @"
									INSERT INTO [Bundle Codes]
									([Treatment ID], [CPT Code], [Bundle Codes], [Description], [Indicator], [Remarks])
									VALUES (@TREAMENTID, @CPTCODE, @BUNDLECODE, @DESCRIPTION, @INDICATOR, @REMARKS)",
					"Delete" => @"DELETE FROM [Bundle Codes] WHERE [Treatment ID] = @TREAMENTID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@TREAMENTID", treatmentID);
					cmd.Parameters.AddWithValue("@CPTCODE", cptCode);
					cmd.Parameters.AddWithValue("@BUNDLECODE", bundleCodes);
					cmd.Parameters.AddWithValue("@DESCRIPTION", Description);
					cmd.Parameters.AddWithValue("@INDICATOR", indicator);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@TREAMENTID", treatmentID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Treatment ID: {treatmentID}";
				message = $"Done! {treatmentID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} BUNDLE CODES INFORMATION");
				//fe.SendToastNotifDesktop(message, "Success");
				return true;
			}
			catch (Exception ex)
			{
				error.LogError("BundleCodesDBRequest", empName, "Bundle", treatmentID, ex);
				message = $"Failed to {request.ToLower()} {treatmentID}, Please try again later";
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
