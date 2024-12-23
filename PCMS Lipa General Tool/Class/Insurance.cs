using PCMS_Lipa_General_Tool.HelperClass;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Insurance
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private static readonly Error error = new();
		private static readonly ActivtiyLogs log = new();

		public DataTable ViewInsuraceList(string empName, out string lblCount)
		{
			var query = "SELECT * FROM [Insurance Info]";
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
				error.LogError("ViewInsuraceList", empName, "Insurance", "N/A", ex);
			}

			return data;
		}


		public bool InsuranceInfoDBRequest(
			string request, 
			string insID, 
			string insuranceName,
			string insCode, 
			string address,
			string payerID,
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
								UPDATE [Insurance Info] SET [Insurance Name] = @INSURANCENAME
									[Insurance Code] = @INSCODE, [Address] = @ADDRESS,
									[Payer ID] = @PAYERID, REMARKS = @REMARKS
								WHERE
									[Insurance ID] = @INSID",
					"Create" => @"
								INSERT INTO [Insurance Info] ([Insurance ID], [Insurance Name], [MPN],
									[Username], [Password], [Website], [Remarks])
								VALUES
									(@INSID, @INSURANCENAME, @MPN, @USERNAME, @PASSWORD,  @WEBSITE, REMARKS",
					"Delete" => @"DELETE FROM [Insurance Info] WHERE [MPN ID] = @INSID",
					_ => throw new ArgumentException("Invalid request type."),
				};

				// Add parameters common to Patch and Create
				if (request != "Delete")
				{
					//cmd.Parameters.AddWithValue("@INSID", insID);
					cmd.Parameters.AddWithValue("@INSURANCENAME", insuranceName);
					cmd.Parameters.AddWithValue("@INSCODE", insCode);
					cmd.Parameters.AddWithValue("@ADDRESS", address);
					cmd.Parameters.AddWithValue("@PAYERID", payerID);
					cmd.Parameters.AddWithValue("@REMARKS", remarks);

				}

				// Common parameter for all requests
				cmd.Parameters.AddWithValue("@INSID", insID);

				// Execute query
				cmd.ExecuteNonQuery();

				// Log activity
				logs = $"{empName} {request.ToLower()}d Insurance ID: {insID}";
				message = $"Done! {insID} has been successfully {request.ToLower()}d.";
				log.AddActivityLog(message, empName, logs, $"{request.ToUpper()} INSURANCE INFORMATION");
				return true;
				//fe.SendToastNotifDesktop(message, "Success");
			}
			catch (Exception ex)
			{
				error.LogError($"InsuranceInfoDBRequest - {request}", empName, "Insurance", insID, ex);
				message = $"Failed to {request.ToLower()} {insID}, Please try again later";
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
FROM [Insurance Info]
WHERE [Insurance Name] LIKE @searchTerm
OR [Address] LIKE @searchTerm";

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
	}
}
