using PCMS_Lipa_General_Tool.Services;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PCMS_Lipa_General_Tool.Class
{
	public class BillReview
	{

		private readonly string _dbConnection = db.GetDbConnection();
		private static readonly Notification notif = new();
		private static readonly ActivtiyLogs log = new();
		private static readonly Database db = new();

		public void GetDBID(out string ID, string empName)
		{
			ID = string.Empty;

			string nextSequence = db.GetSequenceNo("BillReviewSeq", "BR-0");

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


		public DataTable ViewBillReviewList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Bill Review Directory]";
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
				notif.LogError("ViewBillReviewList", empName, "BillReview", "N/A", ex);
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
FROM [Bill Review Directory]
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
				notif.LogError("SearchData", empName, "BillReviews", null, ex);
				searchCount = "An error occurred while fetching records.";
			}

			return resultTable;
		}


		public bool BillReviewDBRequest(
			string request, string reviewerID, string insuranceName, string phoneNo, string faxNo,
			string urphoneNO, string urfaxNo, string brphoneNo, string brfaxNo,
			string email, string remarks, string empName, out string message)
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
                            UPDATE [Bill Review Directory] 
                            SET [Insurance Name] = @INSURANCENAME, [Phone No.] = @PHONENO, [Fax No.] = @FAXNO, 
                                [UR Phone No.] = @URPHONENO, [UR Fax No.] = @URFAXNO, [BR Phone No.] = @BRPHONENO, 
                                [BR Fax No.] = @BRFAXNO, Email = @EMAIL, Remarks = @REMARKS  
                            WHERE [Reviewer ID] = @REVIEWERID",
					"Create" => @"
                            INSERT INTO [Bill Review Directory] 
                            ([Reviewer ID], [Insurance Name], [Phone No.], [Fax No.], [UR Phone No.], [UR Fax No.], 
                             [BR Phone No.], [BR Fax No.], Email, Remarks) 
                            VALUES (@REVIEWERID, @INSURANCENAME, @PHONENO, @FAXNO, @URPHONENO, @URFAXNO, @BRPHONENO, 
                                    @BRFAXNO, @EMAIL, @REMARKS)",
					"Delete" => "DELETE FROM [Bill Review Directory] WHERE [Reviewer ID] = @REVIEWERID",
					_ => throw new ArgumentException("Invalid request type"),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{

					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@PHONENO", phoneNo);
					cmd.Parameters.AddWithValue("@FAXNO", faxNo);
					cmd.Parameters.AddWithValue("@URPHONENO", urphoneNO);
					cmd.Parameters.AddWithValue("@URFAXNO", urfaxNo);
					cmd.Parameters.AddWithValue("@BRPHONENO", brphoneNo);
					cmd.Parameters.AddWithValue("@BRFAXNO", brfaxNo);
					cmd.Parameters.AddWithValue("@EMAIL", email);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);
				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@REVIEWERID", reviewerID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Bill Reviewer ID: {reviewerID}";
				message = $"Done! {reviewerID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} BILL REVIEW INFORMATION");
				return true;

			}
			catch (Exception ex)
			{
				notif.LogError("BillReviewDBRequest", empName, "BillReview", reviewerID, ex);
				message = $"Failed to {request.ToLower()} {reviewerID}, Please try again later";
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
