using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;



namespace PCMS_Lipa_General_Tool.Class
{
	public class Diagnosis
	{

		private readonly string _dbConnection = ConfigurationManager.AppSettings["serverpath"];
		private readonly CommonTask task = new();

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
				task.LogError("ViewDxList", empName, "Diagnosis", "N/A", ex);
			}

			return data;
		}


		public void DiagnosisDBRequest(
			string request,
			string dxID,
			string icd10,
			string icd9,
			string Diagnosis,
			string BodyParts,
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
					cmd.Parameters.AddWithValue("@DXID", dxID);
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
				task.AddActivityLog(message, empName, logs, $"{request.ToUpper()} DIAGNOSIS INFORMATION");
				task.SendToastNotifDesktop(logs);
			}
			catch (Exception ex)
			{
				task.LogError($"DiagnosisDBRequest - {request}", empName, "Diagnosis", dxID, ex);
				RadMessageBox.Show($"Error during {request} operation. Please try again later.", "Operation Failed", MessageBoxButtons.OK, RadMessageIcon.Error);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
