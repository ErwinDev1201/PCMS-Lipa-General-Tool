using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.WinControls.UI;



namespace PCMS_Lipa_General_Tool.Class
{
	public class MPN
	{
		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();


		public void GetDBID(out string intID, string empName)
		{
			// Default assignment for the out parameter
			intID = string.Empty;

			string nextSequence = db.GetSequenceNo("MPNInfoSeq", "MPN-");

			try
			{
				if (!string.IsNullOrEmpty(nextSequence))
				{
					intID = nextSequence;
					return;
				}
			}
			catch (Exception ex)
			{
				error.LogError("GetDBListID", empName, "frmMODMPN", "N/A", ex);
			}
			///db.GetSequenceNo("textbox", "MPNInfoSeq", txtIntID.Text, null, "MPN-");
		}


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
				error.LogError("ViewMPNList", empName, "MPN", "N/A", ex);
			}

			return data;
		}


		public bool MPNDBRequest(
			string request,
			string mpnID,
			string insuranceName,
			string mpn,
			string userName,
			string passWord,
			string webSite,
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
					///cmd.Parameters.AddWithValue("@MPNID", mpnID);
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
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} MPN INUSURANCE INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(message, "success");
			}
			catch (Exception ex)
			{
				error.LogError("MPNDBRequest", empName, "MPN", mpnID, ex);
				message = $"Failed to {request.ToLower()} {mpnID}, Please try again later";
				return false;
				////throw new InvalidOperationException($"Error during {request} operation. Please try again later.");
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
				error.LogError("SearchData", empName, "MPN", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}

	}
}
